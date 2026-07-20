import numpy as np
import random

# ----------------------------------------
# Utility functions
# ----------------------------------------

def next_float(rng, min_value=0.0, max_value=1.0):
    return rng.random() * (max_value - min_value) + min_value

def random_unit_vector(rng):
    # Marsaglia method
    while True:
        u = 2 * next_float(rng) - 1
        v = 2 * next_float(rng) - 1
        s = u*u + v*v
        if 0 < s < 1:
            k = np.sqrt(1 - s)
            return np.array([2*u*k, 2*v*k, 1 - 2*s], dtype=float)

# ----------------------------------------
# Rays and bundles
# ----------------------------------------

class Ray:
    def __init__(self, orientation):
        self.orientation = orientation / np.linalg.norm(orientation)

class RayBundle:
    def __init__(self, number_of_rays, rng):
        self.rays = [Ray(random_unit_vector(rng)) for _ in range(number_of_rays)]

    def clear(self):
        self.rays.clear()

    def add(self, ray):
        self.rays.append(ray)

    def single(self):
        return self.rays[0]

# ----------------------------------------
# Measurement rays and apparatus
# ----------------------------------------

class MeasurementRay(Ray):
    def __init__(self, orientation, value):
        super().__init__(orientation)
        self.value = value

class Apparatus:
    def __init__(self, setting, number_of_rays, rng):
        self.setting = setting
        self.rays = []

        for spin in [1, -1]:
            temp = []
            for _ in range(number_of_rays):
                temp.append(MeasurementRay(random_unit_vector(rng), spin))

            # stochastic filtering
            temp = [
                r for r in temp
                if rng.random() <= 0.5 * (1.0 + spin * np.dot(r.orientation, setting))
            ]

            self.rays.extend(temp)

# ----------------------------------------
# Universe
# ----------------------------------------

class Universe:
    def __init__(self, rng):
        self.rng = rng
        self.entangled_pairs = {}  # maps RayBundle -> RayBundle

    @staticmethod
    def interaction_compatibility(ray1, ray2, hitbox):
        return np.dot(ray1.orientation, ray2.orientation) > np.cos(hitbox)

    def actualize_and_align(self, apparatus, ray_bundle, hitbox):
        candidates = [
            mr.value
            for mr in apparatus.rays
            for r in ray_bundle.rays
            if Universe.interaction_compatibility(mr, r, hitbox)
        ]

        if not candidates:
            return None

        result = random.choice(candidates)

        # align spin to apparatus setting
        ray_bundle.clear()
        ray_bundle.add(Ray(result * apparatus.setting))

        return result

    def prune_entangled_bundle(self, cause):
        effect = self.entangled_pairs[cause]
        effect.clear()
        effect.add(Ray(-1 * cause.single().orientation))

        # break entanglement
        del self.entangled_pairs[cause]
        del self.entangled_pairs[effect]

# ----------------------------------------
# Emitter
# ----------------------------------------

class Emitter:
    def __init__(self, universe, rng):
        self.universe = universe
        self.rng = rng

    def emit(self, number_of_rays):
        a = RayBundle(number_of_rays, self.rng)
        b = RayBundle(number_of_rays, self.rng)
        self.universe.entangled_pairs[a] = b
        self.universe.entangled_pairs[b] = a
        return a, b

# ----------------------------------------
# Main experiment
# ----------------------------------------

def main():
    TRIALS = 100
    NUMBER_OF_RAYS = 1000
    HITBOX = 0.1
    seed = 42

    rng = random.Random(seed)

    x = np.array([1, 0, 0], dtype=float)
    z = np.array([0, 0, 1], dtype=float)

    a0 = z
    a1 = x
    b0 = (x + z) / np.sqrt(2)
    b1 = (-x + z) / np.sqrt(2)

    E00 = E01 = E10 = E11 = 0.0
    N00 = N01 = N10 = N11 = 0
    null_trials = 0

    universe = Universe(rng)
    emitter = Emitter(universe, rng)

    for k in range(TRIALS):
        print(f"\rTrial {k+1}/{TRIALS}...", end="")

        choice = rng.randint(0, 3)
        if choice == 0:
            a, b = a0, b0
        elif choice == 1:
            a, b = a0, b1
        elif choice == 2:
            a, b = a1, b0
        else:
            a, b = a1, b1

        alice_app = Apparatus(a, NUMBER_OF_RAYS, rng)
        bob_app = Apparatus(b, NUMBER_OF_RAYS, rng)

        alice_bundle, bob_bundle = emitter.emit(NUMBER_OF_RAYS)

        # random ordering
        if rng.randint(0, 1) == 0:
            alice_result = universe.actualize_and_align(alice_app, alice_bundle, HITBOX)
            universe.prune_entangled_bundle(alice_bundle)
            bob_result = universe.actualize_and_align(bob_app, bob_bundle, HITBOX)
        else:
            bob_result = universe.actualize_and_align(bob_app, bob_bundle, HITBOX)
            universe.prune_entangled_bundle(bob_bundle)
            alice_result = universe.actualize_and_align(alice_app, alice_bundle, HITBOX)

        if alice_result is not None and bob_result is not None:
            ab = alice_result * bob_result
            if choice == 0:
                E00 += ab; N00 += 1
            elif choice == 1:
                E01 += ab; N01 += 1
            elif choice == 2:
                E10 += ab; N10 += 1
            else:
                E11 += ab; N11 += 1
        else:
            null_trials += 1
            N00 += 1; N01 += 1; N10 += 1; N11 += 1

    e00 = E00 / max(1, N00)
    e01 = E01 / max(1, N01)
    e10 = E10 / max(1, N10)
    e11 = E11 / max(1, N11)

    S = abs(e00 + e01 + e10 - e11)

    print("\n")
    print(f"Trials with null result: {null_trials}")
    print(f"E(a0,b0) = {e00:.4f}")
    print(f"E(a0,b1) = {e01:.4f}")
    print(f"E(a1,b0) = {e10:.4f}")
    print(f"E(a1,b1) = {e11:.4f}")
    print(f"S = {S:.4f}  (target ≈ 2.828)")


if __name__ == "__main__":
    main()
