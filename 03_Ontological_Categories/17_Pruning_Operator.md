# Pruning Operator

Given an actualized event $e_n$, the pruning operator produces the updated
possibility space

$$
P_{n+1} = P(P_n, e_n)
$$

by removing all rays incompatible with $e_n$.

A ray

$$
r = (w_0, w_1, \ldots)
$$

is incompatible with $e_n$ if none of its waypoints can satisfy the
relevant compatibility condition with that event:

$$
r \text{ is incompatible with } e_n
\;\Longleftrightarrow\;
\forall n,\; (w_n, e_n) \notin C.
$$

Pruning applies to:

- the carriers in $P_{e_n}$, and  
- all carriers entangled with them.

Pruning acts only within the realm of possibility. It does not directly
modify actuality.

---

## Structural Role

The pruning operator enforces physical constraints on the modal futures
of the universe. It:

- removes rays that cannot participate in the newly actualized event,
- ensures that future modal evolution remains consistent with actuality,
- and restricts the possibility space to futures compatible with the
  updated causal structure.

Pruning is the mechanism by which locality, conservation laws, and
causal consistency enter the modal domain.

---

## Relations

- Pruning updates the possibility space after each actualization step.
- Rays are removed when all their waypoints fail compatibility with the
  new event.
- Pruning affects carriers that participated in the event and those
  entangled with them.
- Actuality remains unchanged; only $P$ is modified.
- Pruning and actualization jointly drive the evolution of $(A_n, P_n)$.
