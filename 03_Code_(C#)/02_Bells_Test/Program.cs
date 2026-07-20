using System.Numerics;

public static class Extensions {
    // Uniform random float in [minValue, maxValue]
    public static float NextFloat(this Random rng, float minValue = 0, float maxValue = 1) => (float)rng.NextDouble() * (maxValue - minValue) + minValue;

    // using the Marsaglia method to generate a random vector
    public static Vector3 RandomUnitVector(this Random rng) {
        while (true) {
            var u = 2f * rng.NextFloat() - 1f;
            var v = 2f * rng.NextFloat() - 1f;
            var s = u * u + v * v;
            if (s < 1f && s != 0f) {
                var k = MathF.Sqrt(1f - s);
                return new Vector3(2f * u * k, 2f * v * k, 1f - 2f * s);
            }
        }
    }
}

public class Ray(Vector3 orientation) {
    public readonly Vector3 Orientation = Vector3.Normalize(orientation);
}

public class RayBundle : List<Ray> {
    public RayBundle(int numberOfRays, Random rng) {
        for (int i = 0; i < numberOfRays; i++) {
            Add(new Ray(rng.RandomUnitVector()));
        }
    }
}

public class MeasurementRay(Vector3 orientation, int value) : Ray(orientation) {
    public int Value => value;
}

public class Apparatus : List<MeasurementRay> {
    public readonly Vector3 Setting;

    public Apparatus(Vector3 setting, int numberOfRays, Random rng) {
        Setting = setting;
        foreach (int spin in new[] { 1, -1 }) {
            var rays = new List<MeasurementRay>();
            for (int i = 0; i < numberOfRays; i++) {
                rays.Add(new(rng.RandomUnitVector(), spin));
            }
            // Intentional stochastic construction of apparatus: sample measurement‑ray densities per local weighting
            rays.RemoveAll(r => rng.NextDouble() > 0.5 * (1.0 + spin * Vector3.Dot(r.Orientation, setting)));
            AddRange(rays);
        }
    }
}

public class Universe(Random rng) {
    public Dictionary<RayBundle, RayBundle> EntangledPairs = [];

    private static bool InteractionCompatibilityKernel(in Ray ray1, in Ray ray2, in float hitbox) {
        return Vector3.Dot(ray1.Orientation, ray2.Orientation) > MathF.Cos(hitbox);
    }

    public int? ActualizeAndAlign(Apparatus apparatus, RayBundle rayBundle, float hitbox) {
        var candidates = (
            from mr in apparatus
            from r in rayBundle
            where InteractionCompatibilityKernel(mr, r, hitbox)
            select mr.Value
            ).ToList();
        if (candidates.Count != 0) {
            var candidate = candidates[rng.Next(candidates.Count)];
            // this code simulates the assumption that the Stein Gerlach aligns
            // the spin of the particle to the setting of the apparatus
            rayBundle.Clear();
            rayBundle.Add(new(candidate * apparatus.Setting));
            return candidate;
        } else {
            return null;
        }
    }

    public void PruneEntangledBundle(RayBundle cause) {
        // this code forces the entangled particle to have the opposite spin of
        // the actualized particle. this represents conservation of spin
        var effect = EntangledPairs[cause];
        effect.Clear();
        effect.Add(new(-1 * cause.Single().Orientation));
        // Break entanglement, once pruning has occurred
        EntangledPairs.Remove(cause);
        EntangledPairs.Remove(effect);
    }
}

public class Emitter(Universe universe, Random rng) {
    // Emits an entangled pair
    public (RayBundle alicesRayBundle, RayBundle bobsRayBundle) Emit(int numberOfRays) {
        var entangledPair = (a: new RayBundle(numberOfRays, rng), b: new RayBundle(numberOfRays, rng));
        universe.EntangledPairs[entangledPair.a] = entangledPair.b;
        universe.EntangledPairs[entangledPair.b] = entangledPair.a;
        return entangledPair;
    }
}

public static class Program {
    // Parameters
    const int Trials = 10000;
    const int NumberOfRays = 10000;
    const float Hitbox = 0.1F;
    const int seed = 42;

    public static void Main() {
        var rng = new Random(seed);

        var x = new Vector3(1, 0, 0);
        var z = new Vector3(0, 0, 1);

        var a0 = z;
        var a1 = x;
        var b0 = (1.0F / MathF.Sqrt(2.0F)) * (x + z);
        var b1 = (1.0F / MathF.Sqrt(2.0F)) * ((-1.0F) * x + z);

        float E00 = 0, E01 = 0, E10 = 0, E11 = 0;
        int N00 = 0, N01 = 0, N10 = 0, N11 = 0;

        var universe = new Universe(rng);
        var emitter = new Emitter(universe, rng);
        var nullTrials = 0;

        for (int k = 0; k < Trials; k++) {
            Console.Write($"\rTrial {k + 1}/{Trials}...");
            var choice = rng.Next(4);
            (Vector3 a, Vector3 b) = choice switch {
                0 => (a0, b0),
                1 => (a0, b1),
                2 => (a1, b0),
                3 => (a1, b1),
                _ => throw new()
            };
            var alicesApparatus = new Apparatus(a, NumberOfRays, rng);
            var bobsApparatus = new Apparatus(b, NumberOfRays, rng);

            int? alicesResult;
            int? bobsResult;

            // begin experiment
            var (alicesRayBundle, bobsRayBundle) = emitter.Emit(NumberOfRays);
            if (rng.Next(2) == 0) {
                alicesResult = universe.ActualizeAndAlign(alicesApparatus, alicesRayBundle, Hitbox);
                universe.PruneEntangledBundle(alicesRayBundle);
                bobsResult = universe.ActualizeAndAlign(bobsApparatus, bobsRayBundle, Hitbox);
            } else {
                bobsResult = universe.ActualizeAndAlign(bobsApparatus, bobsRayBundle, Hitbox);
                universe.PruneEntangledBundle(bobsRayBundle);
                alicesResult = universe.ActualizeAndAlign(alicesApparatus, alicesRayBundle, Hitbox);
            }
            // end experiment

            if (alicesResult != null && bobsResult != null) {
                var ab = alicesResult.Value * bobsResult.Value;
                switch (choice) {
                    case 0: E00 += ab; N00++; break;
                    case 1: E01 += ab; N01++; break;
                    case 2: E10 += ab; N10++; break;
                    case 3: E11 += ab; N11++; break;
                    default: throw new();
                }
            } else {
                nullTrials++; N00++; N01++; N10++; N11++;
            }
        }
        var e00 = E00 / Math.Max(1, N00);
        var e01 = E01 / Math.Max(1, N01);
        var e10 = E10 / Math.Max(1, N10);
        var e11 = E11 / Math.Max(1, N11);
        var S = Math.Abs(e00 + e01 + e10 - e11);

        Console.WriteLine($"Trials with null result: {nullTrials}");
        Console.WriteLine($"  E(a0,b0) = {e00:F4}");
        Console.WriteLine($"  E(a0,b1) = {e01:F4}");
        Console.WriteLine($"  E(a1,b0) = {e10:F4}");
        Console.WriteLine($"  E(a1,b1) = {e11:F4}");
        Console.WriteLine($"S = {S:F4}  (target ≈ 2.828)");
    }
}