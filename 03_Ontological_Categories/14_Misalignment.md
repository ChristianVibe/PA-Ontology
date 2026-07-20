# Misalignment

Let $x$ be a geometric interaction point in spacetime, such as a slit
plane or barrier surface. For a ray

$$
r = (w_0, w_1, \ldots),
$$

the misalignment of $r$ relative to $x$ is the minimal deviation between
the spacetime projections of its waypoints and $x$:

$$
\mathrm{misalign}(r, x)
  = \min_n d\!\bigl(\pi(w_n), x\bigr),
$$

where $\pi(w_n)$ denotes the spacetime projection of the waypoint $w_n$
and $d$ is the spatial distance defined in spacetime.

Misalignment quantifies how closely the modal trajectory represented by
$r$ approaches the geometric interaction point $x$. The compatibility
kernel uses this quantity in determining whether the ray can participate
in an interaction at that location.

---

## Structural Role

Misalignment provides a geometric measure of how well a ray’s modal
trajectory aligns with a potential interaction point. It determines:

- whether a ray is close enough to participate in an interaction,
- how compatibility is evaluated for modal futures,
- and which rays remain viable under pruning.

It is a purely geometric quantity applied to the spacetime projections of
waypoints.

---

## Relations

- Misalignment is evaluated on particle waypoints, which have spacetime
  projections.
- Quantum waypoints contribute only through the event structure in which
  they participate.
- The compatibility kernel uses misalignment to determine admissible
  interactions.
- Rays with large misalignment may be pruned if they cannot participate
  in any admissible event.
