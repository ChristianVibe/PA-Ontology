# Ray Bundle

Each carrier $c$ has a finite ray bundle

$$
R_c = \{r_1, \ldots, r_{n_c}\},
$$

representing its admissible modal futures. The elements of $R_c$ are the
rays available to that carrier.

---

## Structural Role

The ray bundle encodes all admissible modal futures for a carrier in the
realm of possibility $P$. It determines:

- which modal trajectories the carrier may follow,
- how waypoint progression unfolds along each ray,
- and which futures remain viable after pruning.

The finiteness of $R_c$ ensures that each carrier has a discrete,
computable set of modal futures available at any iteration.

---

## Relations

- Each ray $r \in R_c$ is a sequence of waypoints ordered by modal time.
- Actualization selects events based on geometric intersections reached
  by rays in $R_c$.
- Pruning removes rays from $R_c$ that are incompatible with newly
  actualized events.
- Entanglement couples the ray bundles of carriers that share an event,
  requiring coherent pruning across their bundles.
