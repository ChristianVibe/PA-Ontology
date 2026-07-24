# The Hydrogen Atom — Conceptual Walkthrough (P/A Ontology)

In 1913, Niels Bohr introduced quantized orbits to explain the hydrogen spectrum, but offered no mechanism for why only certain energies were allowed. In 1926, Schrödinger replaced Bohr’s postulates with standing waves of a continuous wavefunction, making quantization a condition on sinusoidal phase coherence around closed loops. The discrete spectrum of hydrogen emerged from the mathematics of differential equations and boundary conditions.

The P/A ontology revisits the same structural question — why does hydrogen exhibit a discrete energy spectrum at all? — but answers it without waves, fields, or continuous potentials. Instead of sinusoidal phase, quantization arises from the geometric self‑consistency of ray bundles under intrinsic tick, curvature, and the intersection‑based actualization rule. In P/A, the familiar \(1/n^2\) spectrum is not imposed; it emerges from the discrete waypoint density of allowed cycles, together with the curvature‑dependent tick‑to‑displacement mapping and the quadratic nature of progression strain.

The purpose of this walkthrough is conceptual clarity rather than numerical prediction. It shows how the hydrogen spectrum — and the associated orbital radii — arise naturally from the internal mechanics of P/A, without invoking wavefunctions, continuous fields, or differential equations.

Notably, the hydrogen spectrum is derived without assuming Coulomb’s law or any continuous electromagnetic potential. The familiar $1/𝑛^2$
 structure arises entirely from modal‑geodesic geometry and misalignment conservation.

---

## Ontological Ingredients

### Electron as a Ray‑Bundle Entity

The electron is represented as a bundle of rays. Each ray carries:

- a sampled orbital radius $r$,
- an orbit circumference $C = 2\pi r$,
- a current waypoint along the orbit,
- a history of past waypoints,
- a modal alignment value determined by its circumference.

Rays progress independently, and their geometric behavior determines which orbital cycles are stable.

### Waypoint Progression

Each ray advances one unit along its orbit:



$$
w \rightarrow (w + 1) \bmod C.
$$



As the ray progresses, it checks whether the new waypoint nearly coincides with any previous waypoint. This recurrence test determines whether the ray has completed a cycle.

### Cycle Closure

A ray becomes **resolved** when its new waypoint is sufficiently close to a past waypoint.  
A ray becomes **found** when the closure is extremely tight, indicating a geometrically stable orbit.

Only **found** rays are eligible for actualization.

---

## Modal Alignment

Modal alignment is the Lorentz‑invariant analogue of **action** in the ontology. It is not an ad‑hoc quantity; its form is structurally forced.

A ray’s circumference determines:

1. the **action per progression step**, and  
2. the **number of steps required to complete an orbit**.

Because action accumulates over the full cycle, the circumference appears **twice**, giving:



$$
M = -\frac{\alpha^2}{2C^2}.
$$



This is the only expression consistent with:

- discrete progression,
- Lorentz invariance,
- circumference‑based geometry,
- and cycle‑accumulated action.

### Modal Geodesics

Unactualized rays always follow **modal geodesics** — the discrete paths that extremize modal alignment. This is the P/A version of the principle of least action:

> **Stable orbits are modal‑geodesic cycles.**

Cycle closure is simply the discrete signature of a geodesic loop.

---

## Noether’s Theorem in P/A Terms

Because modal alignment is the discrete analogue of action, its conservation is the P/A expression of **Noether’s theorem**:

> **Symmetry of modal alignment → conservation of modal alignment.**

This means:

- modal alignment cannot be created or destroyed,
- any change must be accounted for,
- transitions must either **supply** misalignment or **carry it away**.

This is the structural origin of photons.

---

## Actualization and Misalignment Conservation

### Candidate Selection

At each iteration, the electron progresses. With small probability, the universe attempts actualization. All rays marked **found** are considered candidates.

A single ray is selected stochastically:



$$
\text{pick ray } r \propto 1.
$$



This selection determines the next orbital circumference of the electron.

### Misalignment Conservation

Let:

- $M_i$ be the modal alignment of the previously actualized orbit,
- $M_f$ be the modal alignment of the newly selected orbit.

The difference:



$$
\Delta M = |M_f - M_i|
$$



must be preserved.

### Photons as Carriers of Modal Misalignment

Let $M_i$ and $M_f$ denote the modal alignment of two consecutively actualized
electron orbits, with



$$
M = -\frac{\alpha^2}{2C^2},
$$



where $C$ is the orbit circumference and $\alpha$ is the fine‑structure constant.
Because modal alignment is the Lorentz‑invariant analogue of action, and because
unactualized rays follow modal geodesics, modal alignment is conserved in the
sense of Noether’s theorem: any change in alignment must be accounted for.

The **modal misalignment** associated with a transition is



$$
\Delta M = |M_f - M_i|.
$$



This quantity cannot be destroyed. It must either be supplied (absorption) or
carried away (emission). A photon is the ontic object that transports this
misalignment across the universe.

To express $\Delta M$ as a photon wavelength, the ontology uses the intrinsic
displacement scale associated with the electron’s Compton wavelength,
corrected for reduced mass:



$$
\lambda_C^{\mathrm{eff}}
  = \lambda_C \left(1 + \frac{m_e}{m_p}\right),
$$



where $m_e$ and $m_p$ are the electron and proton masses.

A photon produced by the transition carries exactly the conserved misalignment,
encoded as its wavelength:



$$
\boxed{
\lambda
  = \frac{\lambda_C^{\mathrm{eff}}}{\Delta M}
}
$$



This relation is the discrete analogue of the connection between action,
frequency, and energy in continuous physics. It ensures that modal alignment is
conserved across separated events and establishes the structural role of
photons as misalignment carriers in the modal‑alignment ontology.

---

## Status of this Construction

This walkthrough is intended as a conceptual illustration of how quantization arises within the P/A ontology. The associated simulation reproduces the hydrogen spectrum with high numerical agreement (approximately 0.999 under the current implementation) using modal-geodesic progression and misalignment conservation alone.

The construction is not part of the formal paper and is not presented as a new physical prediction. It is included to document a potentially interesting consequence of the ontology and to establish conceptual priority. A full technical treatment remains future work.

Readers interested in the implementation details or numerical results are encouraged to contact the author.
