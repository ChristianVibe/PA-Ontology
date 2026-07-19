public record Vector(double X, double Y) {
    public static double Distance(Vector a, Vector b) => Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector operator *(Vector a, double scalar) => new(a.X * scalar, a.Y * scalar);
    public static Vector operator /(Vector a, double scalar) => new(a.X / scalar, a.Y / scalar);
    public double Length => Math.Sqrt(LengthSquared);
    public double LengthSquared => X * X + Y * Y;
    public bool Inside(double xMin, double xMax, double yMin, double yMax) => X >= xMin && X <= xMax && Y >= yMin && Y <= yMax;

    // Local misalignment operator.
    // Computes the phase difference between two points using only the final segment.
    // Because waypoints are equidistant and the model is strictly local,
    // global path length does not contribute to interference.
    public static Vector Misalignment(Vector a, Vector b, double wavelength) {
        double phase = 2 * Math.PI * Distance(a, b) / wavelength;
        return new(Math.Cos(phase), Math.Sin(phase));
    }
}

static class Extensions {
    // Weighted stochastic choice (used by actualization operator 𝒜)
    public static T StochasticChoice<T>(this IEnumerable<T> source, Func<T, double> weightSelector, Random rng) {
        double total = source.Sum(weightSelector);
        double pick = rng.NextDouble() * total;
        foreach (var item in source) {
            pick -= weightSelector(item);
            if (pick <= 0) {
                return item;
            }
        }
        return source.Last();
    }

    public static double NextDouble(this Random rng, double minValue, double maxValue) => minValue + rng.NextDouble() * (maxValue - minValue);
}

// Simple rectangular geometry: emitter → barrier → receptor. 
// Chosen for visual clarity, not physical realism.
class Geometry {
    public double CanvasSize;
    public double EmitterX;
    public double BarrierX;
    public double ReceptorX;
    public double SlitHalfHeight;
    public double SlitGap;
    public double CenterY => CanvasSize / 2;
    public double Slit1CenterY => CenterY - SlitGap / 2 - SlitHalfHeight;
    public double Slit2CenterY => CenterY + SlitGap / 2 + SlitHalfHeight;
    public double Slit1Top => Slit1CenterY - SlitHalfHeight;
    public double Slit1Bottom => Slit1CenterY + SlitHalfHeight;
    public double Slit2Top => Slit2CenterY - SlitHalfHeight;
    public double Slit2Bottom => Slit2CenterY + SlitHalfHeight;
}

// Spatial grid for efficient neighbor queries.
class SpatialGrid {
    private readonly double Scale;
    private readonly int NeighborRadius;
    private readonly int MinCellX;
    private readonly int MaxCellX;
    private readonly int MinCellY;
    private readonly int MaxCellY;
    private readonly int GridSizeX;
    private readonly int GridSizeY;
    private readonly (Vector center, double strength)?[,] centers;

    public SpatialGrid(IEnumerable<Ray> rays, double minCoordX, double maxCoordX, double minCoordY, double maxCoordY, double cellSize, double resolutionMultiplier, int neighborRadius) {
        Scale = resolutionMultiplier / cellSize;
        NeighborRadius = neighborRadius;
        MinCellX = (int)Math.Floor(minCoordX * Scale);
        MaxCellX = (int)Math.Floor(maxCoordX * Scale);
        MinCellY = (int)Math.Floor(minCoordY * Scale);
        MaxCellY = (int)Math.Floor(maxCoordY * Scale);
        GridSizeX = MaxCellX - MinCellX + 1;
        GridSizeY = MaxCellY - MinCellY + 1;
        var grid = new List<Ray>[GridSizeX, GridSizeY];
        centers = new (Vector center, double strength)?[GridSizeX, GridSizeY];
        foreach (var ray in rays) {
            var p = ray.Waypoints.Last();
            var (ix, iy) = ToIndex(p);
            grid[ix, iy] ??= [];
            grid[ix, iy].Add(ray);
        }

        for (int x = 0; x < GridSizeX; x++) {
            for (int y = 0; y < GridSizeY; y++) {
                var list = grid[x, y];
                if (list == null || list.Count == 0) continue;
                var sum = list.Aggregate(new Vector(0, 0), (acc, ray) => acc + ray.Waypoints.Last());
                centers[x, y] = (sum / list.Count, list.Count);
            }
        }
    }

    private (int ix, int iy) ToIndex(Vector p) {
        int cx = (int)Math.Floor(p.X * Scale);
        int cy = (int)Math.Floor(p.Y * Scale);
        int ix = cx - MinCellX;
        int iy = cy - MinCellY;
        return (ix, iy);
    }

    public IEnumerable<(Vector center, double strength, bool isCenter)> QueryNeighbors(Ray ray) {
        var p = ray.Waypoints.Last();
        var (cx, cy) = ToIndex(p);

        for (int dx = -NeighborRadius; dx <= NeighborRadius; dx++) {
            for (int dy = -NeighborRadius; dy <= NeighborRadius; dy++) {
                int x = cx + dx;
                int y = cy + dy;

                if (x < 0 || x >= GridSizeX || y < 0 || y >= GridSizeY)
                    continue;

                var c = centers[x, y];
                if (c.HasValue)
                    yield return (c.Value.center, c.Value.strength, dx == 0 && dy == 0);
            }
        }
    }
}

// A single possible trajectory in P.
class Ray(int index, Vector position, Vector velocity) {
    public int Index => index;
    public Vector Velocity = velocity;
    public readonly List<Vector> Waypoints = [position];
}

enum Intersection {
    OutsideCanvas,
    AbsorbedAtBarrier,
    DetectedAtReceptor,
    DetectedAtWhichPath
}

// A bundle of rays representing all possible trajectories of a single particle in P.
class RayBundle : Dictionary<int, Ray> {
    public List<(double weight, Intersection intersection, Ray ray)> CandidateResolutions = [];
    public bool PassedWpd = false;

    // Local P‑evolution: rays steer each other through a purely local compatibility kernel.
    internal void EvolvePossibilities(double pressureStrength, double wavelength, Geometry geometry, double hitbox, double resolutionMultiplier, int neighborRadius) {
        var grid = new SpatialGrid(Values, -geometry.CanvasSize, geometry.CanvasSize, 0, geometry.CanvasSize, wavelength, resolutionMultiplier, neighborRadius);
        Parallel.ForEach(this.Values, ray => {
            ray.Velocity += ComputeGeodesic(ray, this, grid, pressureStrength, wavelength, hitbox);
            ray.Waypoints.Add(ray.Waypoints.Last() + ray.Velocity);
        });
    }

    // Local geometric steering kernel.
    // Rays within the hitbox exert symmetric pressure on each other.
    // This is the sole mechanism producing diffraction in the model.
    // No global phase information is used here.
    internal static Vector ComputeGeodesic(Ray ray, RayBundle particle, SpatialGrid grid, double pressureStrength, double wavelength, double hitbox) {
        Vector steer = new(0, 0);
        var p = ray.Waypoints.Last();
        foreach (var other in grid.QueryNeighbors(ray)) {
            double distance = Vector.Distance(other.center, p);
            var strength = other.strength - (other.isCenter ? 1 : 0);
            if (distance < hitbox && distance > 1e-9) {
                double g = -hitbox + distance;
                Vector d = other.center - p;
                steer += d / distance * g * pressureStrength * strength;
            }
        }

        // Normalize velocity magnitude to the wavelength.
        // Ensures all rays advance in equal spatial steps,
        // which is required for the local-phase model.
        var nv = ray.Velocity + steer / particle.Count;
        double scale = wavelength / nv.Length;
        return nv * scale - ray.Velocity;
    }
}

class Emitter(double wavelength) {
    // Emit a bundle of possible trajectories with small vertical spread.
    public RayBundle Emit(int numberOfRays, Geometry geometry, Random rng) {
        var particle = new RayBundle();
        for (int i = 0; i < numberOfRays; i++) {
            var position = new Vector(geometry.EmitterX, rng.NextDouble(geometry.Slit1Bottom - geometry.SlitGap, geometry.Slit2Top + geometry.SlitGap));
            var velocity = new Vector(wavelength, 0);
            particle.Add(i, new Ray(i, position, velocity));
        }
        return particle;
    }
}

static class Universe {
    // Identify rays that leave the canvas (absorbed by environment)
    // this prevents the simulation from going into an endless loop, if a ray "goes dark"
    internal static void FindResolutionsOutsideCanvas(RayBundle particle, Geometry geometry, int maxCount) {
        foreach (var ray in particle.Values.ToList()) {
            if (!ray.Waypoints.Last().Inside(-geometry.CanvasSize, geometry.CanvasSize, 0, geometry.CanvasSize) || ray.Waypoints.Count > maxCount) {
                particle.Remove(ray.Index);
                particle.CandidateResolutions.Add((1, Intersection.OutsideCanvas, ray));
            }
        }
    }

    // Identify rays that cross the barrier plane
    // If they don't go through a slit → absorbed at barrier.
    internal static void FindResolutionsAtBarrier(RayBundle particle, Geometry geometry, bool hasWhichPathDetector) {
        foreach (var ray in particle.Values.ToList()) {
            if (ray.Waypoints[^2].X <= geometry.BarrierX && ray.Waypoints.Last().X >= geometry.BarrierX) {
                bool inTopSlit = ray.Waypoints.Last().Y >= geometry.Slit1Top && ray.Waypoints.Last().Y <= geometry.Slit1Bottom;
                bool inBottomSlit = ray.Waypoints.Last().Y >= geometry.Slit2Top && ray.Waypoints.Last().Y <= geometry.Slit2Bottom;
                if (!inTopSlit && !inBottomSlit) {
                    particle.Remove(ray.Index);
                    particle.CandidateResolutions.Add((1, Intersection.AbsorbedAtBarrier, ray));
                } else if (hasWhichPathDetector && inTopSlit) {
                    particle.Remove(ray.Index);
                    particle.CandidateResolutions.Add((1, Intersection.DetectedAtWhichPath, ray));
                }
            }
        }
    }

    // Identify rays that cross the receptor plane within the hitbox → detected at receptor.
    internal static void FindResolutionsAtReceptor(RayBundle particle, Geometry geometry) {
        foreach (var ray in particle.Values.ToList()) {
            if (ray.Waypoints.Last().X >= geometry.ReceptorX) {
                particle.Remove(ray.Index);
                particle.CandidateResolutions.Add((1, Intersection.DetectedAtReceptor, ray));
            }
        }
    }

    // Compute the compatibility of all rays detected at the receptor
    // This is the only place where interference happens:
    // rays that arrive at the same Y‑position on the receptor, but come from different paths,
    // can interfere constructively or destructively.
    public static double[] InteractionCompatibilityKernel(RayBundle particle, Geometry geometry, double wavelength, int bucketSize) {
        // Rays that actually hit the receptor
        var receptorResolutions = particle.CandidateResolutions.Where(r => r.intersection == Intersection.DetectedAtReceptor).ToList();
        var receptorBuckets = new double[bucketSize];
        double bucketHeight = (double)geometry.CanvasSize / bucketSize;

        for (int i = 0; i < bucketSize; i++) {
            double yMin = i * bucketHeight;
            double yMax = (i + 1) * bucketHeight;
            var bucketRays = receptorResolutions.Select(r => r.ray).Select(ray => {
                var p1 = ray.Waypoints[^2];
                var p2 = ray.Waypoints[^1];

                // Linear interpolation to find the exact Y-coordinate where the ray
                // crosses the receptor plane. Required because waypoints do not
                // necessarily land exactly on the receptor X position.
                double t = (geometry.ReceptorX - p1.X) / (p2.X - p1.X);
                double yHit = p1.Y + t * (p2.Y - p1.Y);
                return (ray, hit: new Vector(geometry.ReceptorX, yHit));
            }).Where(br => br.hit.Y >= yMin && br.hit.Y < yMax).ToList();


            if (bucketRays.Count != 0) {
                var misalignment =
                    bucketRays.Select(br => Vector.Misalignment(br.ray.Waypoints[^2], br.hit, wavelength))
                    .Aggregate(new Vector(0.0, 0.0), (total, m) => total + m);
                receptorBuckets[i] = misalignment.LengthSquared / bucketRays.Count;
            }
        }
        // Normalize bucket strengths so that the total probability equals
        // the number of rays detected at the receptor.
        // This converts coherence magnitudes into ac½tual detection weights.
        return receptorBuckets.Select(p => p / receptorBuckets.Sum() * receptorResolutions.Count).ToArray();
    }

    // Actualization operator 𝒜:
    // Stochastically selects one resolution weighted by compatibility.
    // If a which-path detection occurs, the particle's RayBundle is pruned to the
    // subset of rays consistent with that detection.
    public static (Intersection interaction, double y) Actualize(RayBundle particle, Random rng) {
        var resolutions = particle.CandidateResolutions.ToList();
        var (_, intersection, ray) = resolutions.StochasticChoice(r => r.weight, rng);
        if (intersection == Intersection.DetectedAtWhichPath) {
            // If a which-path detection occurs, the particle starts progressing from that point onward,
            // with all possibilities compatible with that detection.
            particle.Clear();
            particle.CandidateResolutions.Clear();
            particle.PassedWpd = true;
            foreach (var r in resolutions.Where(d => d.intersection == Intersection.DetectedAtWhichPath)) {
                particle.Add(r.ray.Index, new(r.ray.Index, r.ray.Waypoints.Last(), r.ray.Velocity));
            }
        }
        return (intersection, ray.Waypoints.Last().Y);
    }
}

static class Program {
    // Note: Simulation parameters chosen for clarity, not performance.
    // Total rays simulated = NumberOfParticles × NumberOfRaysPerParticle.
    public static void Main() {
        const int numberOfParticles = 20000;
        const int numberOfRaysPerParticle = 20000;
        const double wavelength = 0.2;
        const double hitbox = 0.2;
        const int bucketSize = 240;
        const double pressureStrength = 0.5;
        const int maxCount = 1000;
        const int seed = 42;

        // resolutionMultiplier and neighborRadius chosen empirically.
        // Higher values increase smoothness of the local compatibility kernel
        // but also increase computational cost.
        const int ResolutionMultiplier = 5;
        const int NeighborRadius = 5;

        var Geometry = new Geometry() {
            CanvasSize = 24,
            BarrierX = 0,
            ReceptorX = 20,
            EmitterX = -10,
            SlitHalfHeight = 0.25,
            SlitGap = 2
        };

        var Emitter = new Emitter(wavelength);
        var Rng = new Random(seed);

        Console.Write("Does the simulation include a which-path-detector? (y/n) ");
        bool hasWhichPathDetector = Console.ReadLine() == "y";
        var buckets = new int[bucketSize];
        var whichPathBuckets = new int[bucketSize];
        var interactions = Enum.GetValues<Intersection>().ToDictionary(i => i, i => 0);
        // start experiment
        // Each particle evolves until all possible trajectories resolve.
        // When the bundle becomes empty, the interaction kernel determines
        // the final detection probabilities, and 𝒜 selects the actual outcome.
        for (int particleCounter = 0; particleCounter < numberOfParticles; ++particleCounter) {
            var Particle = Emitter.Emit(numberOfRaysPerParticle, Geometry, Rng);
            while (Particle.Count > 0) {
                Particle.EvolvePossibilities(pressureStrength, wavelength, Geometry, hitbox, ResolutionMultiplier, NeighborRadius);
                Universe.FindResolutionsOutsideCanvas(Particle, Geometry, maxCount);
                Universe.FindResolutionsAtBarrier(Particle, Geometry, hasWhichPathDetector);
                Universe.FindResolutionsAtReceptor(Particle, Geometry);
                if (Particle.Count == 0) {
                    var probabilities = Universe.InteractionCompatibilityKernel(Particle, Geometry, wavelength, bucketSize);
                    if (probabilities != null) {
                        // Replace the uniform probabilities of all rays detected at the receptor
                        // with the ones computed by the interaction kernel.
                        Particle.CandidateResolutions.RemoveAll(r => r.intersection == Intersection.DetectedAtReceptor);
                        for (int i = 0; i < probabilities.Length; i++) {
                            Particle.CandidateResolutions.Add((probabilities[i], Intersection.DetectedAtReceptor, new(-1, new(Geometry.ReceptorX, (Geometry.CanvasSize * i + 0.5) / bucketSize), new(0, 0))));
                        }
                    }
                    (Intersection interaction, double y) = Universe.Actualize(Particle, Rng);
                    interactions[interaction]++;
                    if (interaction == Intersection.DetectedAtReceptor) {
                        int bucketIndex = (int)(y / Geometry.CanvasSize * bucketSize);
                        bucketIndex = Math.Clamp(bucketIndex, 0, bucketSize - 1);
                        buckets[bucketIndex]++;
                        if (Particle.PassedWpd) {
                            whichPathBuckets[bucketIndex]++;
                        }
                    }
                }
            }
            Console.Error.WriteLine($"Simulated {particleCounter} particles...");
        }
        // end experiment
        foreach (var interaction in interactions) {
            Console.WriteLine($"{interaction.Key}: {interaction.Value}");
        }
        Console.WriteLine("Receptor detection distribution");
        Console.WriteLine("BucketIndex Count WhichPathCount");
        for (int i = 0; i < bucketSize; i++) {
            Console.WriteLine($"{i} {buckets[i]} {whichPathBuckets[i]}");
        }
    }
}