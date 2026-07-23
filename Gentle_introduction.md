# Intuitive Illustration of the P/A Ontology

In the P/A ontology, particles behave somewhat like tiny billiard balls.  
They move, collide, and exchange conserved quantities such as momentum.  
But unlike classical billiard balls, their physical properties are not fully determined between interactions.

This difference is central to the ontology.

---

## A Collision: Establishing Shared Constraints

Consider two particles, $p_a$ and $p_b$, colliding at an event

$$
e_{\text{source}}.
$$

This collision is an **actualization event**: a moment where something becomes physically real.

At the collision, each particle contributes its own momentum. The total momentum must be conserved. But after the event, there is **no fact of the matter** about how the shared momentum is distributed between the two particles.

Particles do not carry definite physical quantities between events. Instead, each particle progresses in [**modal time**](03_Ontological_Categories/04_Modal_Time.md) as a *set of admissible futures*, each representing a different possible momentum consistent with conservation at $e_{\text{source}}$.

Nothing forces the universe to choose one yet.

---

## Modal Futures: What Could Happen

Between interactions, each particle carries a [**ray bundle**](03_Ontological_Categories/10_Ray_Bundle.md): a finite set of possible modal trajectories. Along each ray, the particle has waypoints representing possible states at successive modal-time steps.

For $p_a$ and $p_b$, these ray bundles include futures with different momenta. All of them are allowed, as long as they respect the shared constraints from $e_{\text{source}}$.

The ontology does not assume hidden variables or pre-existing facts.  
It simply keeps track of **what could happen**.

---

## A Measurement: Establishing What *Does* Happen

Later, Alice measures the momentum of $p_a$ by letting it collide with her particle $p_{\text{alice}}$ at a new event

$$
e_{\text{alice}}.
$$

This measurement is a **new actualization event**.

At $e_{\text{alice}}$, the momentum of $p_a$ from $e_{\text{source}}$ to $e_{\text{alice}}$ becomes **established**.  
The universe selects one admissible future for $p_a$, and that choice becomes part of actuality.

Because momentum was shared at $e_{\text{source}}$, the momentum of $p_b$ is now also established. Any previously admissible future of $p_b$ with a different momentum is **immediately pruned** from the possibility space.

This pruning is not a physical signal.  
It is simply the requirement that **possibility must remain consistent with actuality**.

---

## Bob’s Measurement: Consistency Without Communication

When Bob later measures the momentum of $p_b$ at

$$
e_{\text{bob}},
$$

he can only obtain a value consistent with what Alice established at $e_{\text{alice}}$.

This is entanglement in the P/A ontology:

- shared events create shared constraints,
- modal futures evolve independently until they must agree,
- and actualization enforces consistency without communication.

No hidden variables.  
No nonlocal influence.  
Just the coordination of possibility and actuality.

---

## Why This Matters

This intuitive picture shows how the ontology:

- preserves conservation laws exactly,
- avoids nonlocal influences,
- explains entanglement geometrically,
- and treats measurement as a *creative* act that establishes facts rather than revealing them.

The P/A ontology describes a universe where:

- **possibility evolves locally**,  
- **actuality evolves globally**,  
- and **consistency is enforced by pruning**.

It is a clean, geometric, event-driven framework for understanding physical reality.

# Interference

In standard quantum mechanics, interference is explained by modeling a
particle’s progression as a wave. In the P/A ontology, particles are
point-like and there are **no waves**. Nevertheless, interference
patterns such as those in the double-slit experiment arise naturally
from the geometry of admissible futures.

---

## Admissible Futures at the Slit Barrier

Consider a particle approaching a double-slit barrier. As it progresses
in modal time, its ray bundle contains admissible futures corresponding
to:

- passing through slit 1,
- passing through slit 2,
- or being absorbed by the barrier.

If the particle survives the barrier, its admissible futures now split
into two **disjoint ray bundles**:

- one consisting of rays that passed through slit 1,
- one consisting of rays that passed through slit 2.

Each ray is a sequence of waypoints with fixed modal-time spacing.

---

## Rays Arriving at the Receptor

At the receptor screen, **exactly two rays** (one from each bundle)
reach each spacetime point where detection is possible.

Let these rays be

$$
r_1 = (w_0^{(1)}, w_1^{(1)}, \ldots), \qquad
r_2 = (w_0^{(2)}, w_1^{(2)}, \ldots).
$$

Because waypoints are spaced uniformly in modal time, the two rays may
arrive at the receptor with:

- **aligned waypoints**, or  
- **misaligned waypoints**.

This alignment determines the likelihood of actualization.

---

## Alignment and Misalignment

If the waypoints of $r_1$ and $r_2$ **align** at the receptor, then the
[misalignment](03_Ontological_Categories/14_Misalignment.md) between their spacetime projections is small:

$$
\mathrm{misalign}(r_1, x) \approx \mathrm{misalign}(r_2, x).
$$

Aligned rays satisfy the compatibility relation more strongly, making
actualization at that point **more likely**.

If the waypoints are **misaligned**, then the spatial deviation is
larger:

$$
\mathrm{misalign}(r_1, x) \not\approx \mathrm{misalign}(r_2, x),
$$

and the compatibility relation is weaker, reducing the chance of
actualization.

---

## Emergence of the Interference Pattern

The receptor screen records actualized events. Because the probability
of actualization depends on waypoint alignment:

- points where the two rays arrive **in phase** (aligned waypoints)  
  have **high actualization probability** → bright fringes,

- points where the rays arrive **out of phase** (misaligned waypoints)  
  have **low actualization probability** → dark fringes.

Thus the familiar interference pattern emerges **without waves**.  
It arises from:

1. the geometry of admissible futures,  
2. the fixed modal-time spacing of waypoints,  
3. and the compatibility relation’s dependence on misalignment.

The P/A ontology reproduces interference entirely through **ray geometry
and modal progression**, not through wave amplitudes.

---

## Relation to wave mechanics 
### Hamilton–Jacobi perspective)  
According to Hamilton–Jacobi theory, ray-based descriptions and wave-based descriptions are mathematically interchangeable: ray bundles can encode the same predictive structure as wavefunctions. In this sense, the P/A ontology should not be seen as a competitor to the wave equation, but as an alternative ontological reading of the same underlying mathematical model. It replaces continuous wavefields with discrete admissible futures and actualization events, while preserving the empirical content.

### Waypoints vs phase: fewer ontological commitments  
In standard quantum mechanics, interference is typically described in terms of phase oscillations of a wavefunction. The P/A ontology instead uses equidistant waypoint progression in modal time and a compatibility relation between rays. These two views are mathematically interchangeable at the level of predictions, but waypoint progression carries fewer ontological commitments: it does not require physically real waves or fields, yet still reproduces interference via ray geometry and modal alignment.
