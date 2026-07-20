import math
import random
from dataclasses import dataclass
from enum import Enum
from typing import List, Dict, Tuple, Optional


# -----------------------------
# Basic vector and utilities
# -----------------------------

@dataclass
class Vector:
    x: float
    y: float

    @staticmethod
    def distance(a: "Vector", b: "Vector") -> float:
        return math.sqrt((a.x - b.x) ** 2 + (a.y - b.y) ** 2)

    def __add__(self, other: "Vector") -> "Vector":
        return Vector(self.x + other.x, self.y + other.y)

    def __sub__(self, other: "Vector") -> "Vector":
        return Vector(self.x - other.x, self.y - other.y)

    def __mul__(self, scalar: float) -> "Vector":
        return Vector(self.x * scalar, self.y * scalar)

    def __truediv__(self, scalar: float) -> "Vector":
        return Vector(self.x / scalar, self.y / scalar)

    @property
    def length(self) -> float:
        return math.sqrt(self.length_squared)

    @property
    def length_squared(self) -> float:
        return self.x * self.x + self.y * self.y

    def inside(self, x_min: float, x_max: float, y_min: float, y_max: float) -> bool:
        return x_min <= self.x <= x_max and y_min <= self.y <= y_max

    @staticmethod
    def misalignment(a: "Vector", b: "Vector", wavelength: float) -> "Vector":
        phase = 2 * math.pi * Vector.distance(a, b) / wavelength
        return Vector(math.cos(phase), math.sin(phase))


def stochastic_choice(source, weight_selector, rng: random.Random):
    total = sum(weight_selector(item) for item in source)
    pick = rng.random() * total
    for item in source:
        pick -= weight_selector(item)
        if pick <= 0:
            return item
    return source[-1]


def next_double(rng: random.Random, min_value: float, max_value: float) -> float:
    return min_value + rng.random() * (max_value - min_value)


# -----------------------------
# Geometry and spatial grid
# -----------------------------

@dataclass
class Geometry:
    canvas_size: float
    emitter_x: float
    barrier_x: float
    receptor_x: float
    slit_half_height: float
    slit_gap: float

    @property
    def center_y(self) -> float:
        return self.canvas_size / 2

    @property
    def slit1_center_y(self) -> float:
        return self.center_y - self.slit_gap / 2 - self.slit_half_height

    @property
    def slit2_center_y(self) -> float:
        return self.center_y + self.slit_gap / 2 + self.slit_half_height

    @property
    def slit1_top(self) -> float:
        return self.slit1_center_y - self.slit_half_height

    @property
    def slit1_bottom(self) -> float:
        return self.slit1_center_y + self.slit_half_height

    @property
    def slit2_top(self) -> float:
        return self.slit2_center_y - self.slit_half_height

    @property
    def slit2_bottom(self) -> float:
        return self.slit2_center_y + self.slit_half_height


class SpatialGrid:
    def __init__(
        self,
        rays: List["Ray"],
        min_coord_x: float,
        max_coord_x: float,
        min_coord_y: float,
        max_coord_y: float,
        cell_size: float,
        resolution_multiplier: float,
        neighbor_radius: int,
    ):
        self.scale = resolution_multiplier / cell_size
        self.neighbor_radius = neighbor_radius

        self.min_cell_x = int(math.floor(min_coord_x * self.scale))
        self.max_cell_x = int(math.floor(max_coord_x * self.scale))
        self.min_cell_y = int(math.floor(min_coord_y * self.scale))
        self.max_cell_y = int(math.floor(max_coord_y * self.scale))

        self.grid_size_x = self.max_cell_x - self.min_cell_x + 1
        self.grid_size_y = self.max_cell_y - self.min_cell_y + 1

        grid: List[List[Ray]] = [[[] for _ in range(self.grid_size_y)] for _ in range(self.grid_size_x)]
        self.centers: List[List[Optional[Tuple[Vector, float]]]] = [
            [None for _ in range(self.grid_size_y)] for _ in range(self.grid_size_x)
        ]

        for ray in rays:
            p = ray.waypoints[-1]
            ix, iy = self._to_index(p)
            grid[ix][iy].append(ray)

        for x in range(self.grid_size_x):
            for y in range(self.grid_size_y):
                lst = grid[x][y]
                if not lst:
                    continue
                sum_vec = Vector(0.0, 0.0)
                for r in lst:
                    sum_vec = sum_vec + r.waypoints[-1]
                center = sum_vec / len(lst)
                self.centers[x][y] = (center, float(len(lst)))

    def _to_index(self, p: Vector) -> Tuple[int, int]:
        cx = int(math.floor(p.x * self.scale))
        cy = int(math.floor(p.y * self.scale))
        ix = cx - self.min_cell_x
        iy = cy - self.min_cell_y
        return ix, iy

    def query_neighbors(self, ray: "Ray"):
        p = ray.waypoints[-1]
        cx, cy = self._to_index(p)

        for dx in range(-self.neighbor_radius, self.neighbor_radius + 1):
            for dy in range(-self.neighbor_radius, self.neighbor_radius + 1):
                x = cx + dx
                y = cy + dy
                if x < 0 or x >= self.grid_size_x or y < 0 or y >= self.grid_size_y:
                    continue
                c = self.centers[x][y]
                if c is not None:
                    center, strength = c
                    yield center, strength, (dx == 0 and dy == 0)


# -----------------------------
# Rays, bundles, emitter
# -----------------------------

class Ray:
    def __init__(self, index: int, position: Vector, velocity: Vector):
        self.index = index
        self.velocity = velocity
        self.waypoints: List[Vector] = [position]


class Intersection(Enum):
    OUTSIDE_CANVAS = 0
    ABSORBED_AT_BARRIER = 1
    DETECTED_AT_RECEPTOR = 2
    DETECTED_AT_WHICHPATH = 3


class RayBundle:
    def __init__(self):
        self.rays: Dict[int, Ray] = {}
        self.candidate_resolutions: List[Tuple[float, Intersection, Ray]] = []
        self.passed_wpd: bool = False

    def add(self, index: int, ray: Ray):
        self.rays[index] = ray

    def remove(self, index: int):
        if index in self.rays:
            del self.rays[index]

    @property
    def count(self) -> int:
        return len(self.rays)

    def evolve_possibilities(
        self,
        pressure_strength: float,
        wavelength: float,
        geometry: Geometry,
        hitbox: float,
        resolution_multiplier: float,
        neighbor_radius: int,
    ):
        grid = SpatialGrid(
            list(self.rays.values()),
            -geometry.canvas_size,
            geometry.canvas_size,
            0.0,
            geometry.canvas_size,
            wavelength,
            resolution_multiplier,
            neighbor_radius,
        )

        for ray in list(self.rays.values()):
            steer = self.compute_geodesic(ray, grid, pressure_strength, wavelength, hitbox)
            new_velocity = Vector(ray.velocity.x + steer.x, ray.velocity.y + steer.y)
            scale = wavelength / new_velocity.length
            new_velocity = new_velocity * scale
            ray.velocity = new_velocity
            ray.waypoints.append(ray.waypoints[-1] + ray.velocity)

    def compute_geodesic(
        self,
        ray: Ray,
        grid: SpatialGrid,
        pressure_strength: float,
        wavelength: float,
        hitbox: float,
    ) -> Vector:
        steer = Vector(0.0, 0.0)
        p = ray.waypoints[-1]
        for center, strength, is_center in grid.query_neighbors(ray):
            distance = Vector.distance(center, p)
            strength_adj = strength - (1.0 if is_center else 0.0)
            if hitbox > distance > 1e-9:
                g = -hitbox + distance
                d = center - p
                steer = steer + (d / distance) * (g * pressure_strength * strength_adj)

        if self.count == 0:
            return Vector(0.0, 0.0)

        steer = steer / float(self.count)
        return steer


class Emitter:
    def __init__(self, wavelength: float):
        self.wavelength = wavelength

    def emit(self, number_of_rays: int, geometry: Geometry, rng: random.Random) -> RayBundle:
        bundle = RayBundle()
        for i in range(number_of_rays):
            y = next_double(
                rng,
                geometry.slit1_bottom - geometry.slit_gap,
                geometry.slit2_top + geometry.slit_gap,
            )
            position = Vector(geometry.emitter_x, y)
            velocity = Vector(self.wavelength, 0.0)
            bundle.add(i, Ray(i, position, velocity))
        return bundle


# -----------------------------
# Universe functions
# -----------------------------

def find_resolutions_outside_canvas(bundle: RayBundle, geometry: Geometry, max_count: int):
    for ray in list(bundle.rays.values()):
        if (
            not ray.waypoints[-1].inside(-geometry.canvas_size, geometry.canvas_size, 0.0, geometry.canvas_size)
            or len(ray.waypoints) > max_count
        ):
            bundle.remove(ray.index)
            bundle.candidate_resolutions.append((1.0, Intersection.OUTSIDE_CANVAS, ray))


def find_resolutions_at_barrier(bundle: RayBundle, geometry: Geometry, has_which_path_detector: bool):
    for ray in list(bundle.rays.values()):
        if ray.waypoints[-2].x <= geometry.barrier_x <= ray.waypoints[-1].x:
            y = ray.waypoints[-1].y
            in_top_slit = geometry.slit1_top <= y <= geometry.slit1_bottom
            in_bottom_slit = geometry.slit2_top <= y <= geometry.slit2_bottom
            if not in_top_slit and not in_bottom_slit:
                bundle.remove(ray.index)
                bundle.candidate_resolutions.append((1.0, Intersection.ABSORBED_AT_BARRIER, ray))
            elif has_which_path_detector and in_top_slit:
                bundle.remove(ray.index)
                bundle.candidate_resolutions.append((1.0, Intersection.DETECTED_AT_WHICHPATH, ray))


def find_resolutions_at_receptor(bundle: RayBundle, geometry: Geometry):
    for ray in list(bundle.rays.values()):
        if ray.waypoints[-1].x >= geometry.receptor_x:
            bundle.remove(ray.index)
            bundle.candidate_resolutions.append((1.0, Intersection.DETECTED_AT_RECEPTOR, ray))


def interaction_compatibility_kernel(
    bundle: RayBundle,
    geometry: Geometry,
    wavelength: float,
    bucket_size: int,
) -> List[float]:
    receptor_resolutions = [
        r for r in bundle.candidate_resolutions if r[1] == Intersection.DETECTED_AT_RECEPTOR
    ]
    receptor_buckets = [0.0 for _ in range(bucket_size)]
    bucket_height = geometry.canvas_size / float(bucket_size)

    for i in range(bucket_size):
        y_min = i * bucket_height
        y_max = (i + 1) * bucket_height

        bucket_rays = []
        for _, _, ray in receptor_resolutions:
            p1 = ray.waypoints[-2]
            p2 = ray.waypoints[-1]
            t = (geometry.receptor_x - p1.x) / (p2.x - p1.x)
            y_hit = p1.y + t * (p2.y - p1.y)
            hit = Vector(geometry.receptor_x, y_hit)
            if y_min <= hit.y < y_max:
                bucket_rays.append((ray, hit))

        if bucket_rays:
            misalignment_sum = Vector(0.0, 0.0)
            for ray, hit in bucket_rays:
                m = Vector.misalignment(ray.waypoints[-2], hit, wavelength)
                misalignment_sum = misalignment_sum + m
            receptor_buckets[i] = misalignment_sum.length_squared / float(len(bucket_rays))

    total = sum(receptor_buckets)
    if total == 0.0:
        return receptor_buckets

    count_receptor = len(receptor_resolutions)
    return [p / total * count_receptor for p in receptor_buckets]


def actualize(bundle: RayBundle, rng: random.Random) -> Tuple[Intersection, float]:
    resolutions = list(bundle.candidate_resolutions)
    weight, intersection, ray = stochastic_choice(resolutions, lambda r: r[0], rng)

    if intersection == Intersection.DETECTED_AT_WHICHPATH:
        bundle.rays.clear()
        bundle.candidate_resolutions.clear()
        bundle.passed_wpd = True
        for w, inter, r in resolutions:
            if inter == Intersection.DETECTED_AT_WHICHPATH:
                bundle.add(r.index, Ray(r.index, r.waypoints[-1], r.velocity))

    return intersection, ray.waypoints[-1].y


# -----------------------------
# Main simulation
# -----------------------------

def main():
    number_of_particles =  10
    number_of_rays_per_particle = 10
    wavelength = 0.2
    hitbox = 0.2
    bucket_size = 240
    pressure_strength = 0.5
    max_count = 1000
    seed = 42

    resolution_multiplier = 5
    neighbor_radius = 5

    geometry = Geometry(
        canvas_size=24.0,
        barrier_x=0.0,
        receptor_x=20.0,
        emitter_x=-10.0,
        slit_half_height=0.25,
        slit_gap=2.0,
    )

    emitter = Emitter(wavelength)
    rng = random.Random(seed)

    has_which_path_detector = input("Does the simulation include a which-path-detector? (y/n) ").strip().lower() == "y"

    buckets = [0 for _ in range(bucket_size)]
    which_path_buckets = [0 for _ in range(bucket_size)]
    interactions: Dict[Intersection, int] = {i: 0 for i in Intersection}

    for particle_counter in range(number_of_particles):
        bundle = emitter.emit(number_of_rays_per_particle, geometry, rng)
        while bundle.count > 0:
            bundle.evolve_possibilities(
                pressure_strength,
                wavelength,
                geometry,
                hitbox,
                resolution_multiplier,
                neighbor_radius,
            )
            find_resolutions_outside_canvas(bundle, geometry, max_count)
            find_resolutions_at_barrier(bundle, geometry, has_which_path_detector)
            find_resolutions_at_receptor(bundle, geometry)

            if bundle.count == 0:
                probabilities = interaction_compatibility_kernel(bundle, geometry, wavelength, bucket_size)
                bundle.candidate_resolutions = [
                    r for r in bundle.candidate_resolutions if r[1] != Intersection.DETECTED_AT_RECEPTOR
                ]
                for i, p in enumerate(probabilities):
                    y = (geometry.canvas_size * i + 0.5) / bucket_size
                    fake_ray = Ray(-1, Vector(geometry.receptor_x, y), Vector(0.0, 0.0))
                    bundle.candidate_resolutions.append((p, Intersection.DETECTED_AT_RECEPTOR, fake_ray))

                interaction, y = actualize(bundle, rng)
                interactions[interaction] += 1

                if interaction == Intersection.DETECTED_AT_RECEPTOR:
                    bucket_index = int(y / geometry.canvas_size * bucket_size)
                    bucket_index = max(0, min(bucket_size - 1, bucket_index))
                    buckets[bucket_index] += 1
                    if bundle.passed_wpd:
                        which_path_buckets[bucket_index] += 1

        print(f"Simulated {particle_counter} particles...", file=sys.stderr)

    for inter, count in interactions.items():
        print(f"{inter.name}: {count}")

    print("Receptor detection distribution")
    print("BucketIndex Count WhichPathCount")
    for i in range(bucket_size):
        print(f"{i} {buckets[i]} {which_path_buckets[i]}")


if __name__ == "__main__":
    import sys
    main()
