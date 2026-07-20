# Compatibility Relation

Compatibility is specified by a decidable predicate

$$
C(w_c, w_{c'}),
$$

defined on pairs of waypoints. It determines whether a pair of waypoint
states is jointly admissible under the relevant interaction constraints.
The predicate may depend on:

- intrinsic modal state variables,
- misalignment relative to the relevant interaction geometry,
- conservation‑relevant quantities encoded in the waypoint structure.

More generally, the same compatibility criterion extends to finite sets
of waypoints whenever an interaction involves more than two carriers.

---

## Structural Role

The compatibility relation determines which modal futures can jointly
participate in an admissible interaction. It governs:

- whether two carriers can form a joint event,
- how geometric intersection conditions are evaluated,
- and which combinations of waypoints remain viable under pruning.

Compatibility is the central constraint linking modal structure to
actualizable interactions.

---

## Relations

- Compatibility is evaluated on waypoints drawn from rays in $P$.
- Misalignment contributes directly to the compatibility predicate.
- Conservation‑relevant quantities encoded in waypoint structure restrict
  admissible combinations.
- Actualization selects interactions only from compatible waypoint sets.
- Pruning removes rays whose waypoints fail compatibility with newly
  actualized events.
