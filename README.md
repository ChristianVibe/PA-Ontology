# Possibility/Actuality Ontology (P/A Ontology)
*A discrete, ray‑based ontology for closed physical systems.*

<p align="center">
  <img src="https://img.shields.io/badge/License-MIT-blue.svg" />
  <img src="https://img.shields.io/badge/Build-.NET_10-blueviolet.svg" />
  <img src="https://img.shields.io/badge/Platform-Windows-lightgrey.svg" />
  <img src="https://img.shields.io/badge/IDE-Visual_Studio_2026-5C2D91.svg" />
  <img src="https://img.shields.io/badge/IDE-VS_Code-007ACC.svg" />
</p>

## About
A discrete, ray‑based ontology for closed physical systems built on a distinction between actualized events and admissible futures. The model uses finite causal propagation, local records, and event‑defined entanglement to reproduce interference and Tsirelson‑bounded correlations without assuming Hilbert space or wavefunctions.

---

## Theoretical Overview
## 🔍 Theoretical Overview

The P/A ontology is a discrete, ray‑based framework built around two primitive carrier types—**particles** and **quanta**—each with its own causal and modal progression rules. Particles possess proper‑time evolution and extended causal chains, while quanta propagate only in modal time and participate in at most two events (emission and absorption). Together, these carriers define the ontology’s causal and modal structure.

The framework adopts three operational commitments:

- **Stable local records** — actualized events store definite, intersubjectively accessible information.  
- **Finite‑speed causal propagation** — influence spreads locally, respecting relativistic constraints.  
- **Modal futures encoded as ray bundles** — each carrier holds a finite set of admissible futures, ordered by modal time.

Actuality consists of discrete events, each involving a finite set of carriers at a spacetime location. Possibility consists of ray bundles and waypoints that encode admissible modal futures. The universe evolves through two operators:

- the **actualization operator**, which selects an admissible interaction and produces an event, and  
- the **pruning operator**, which removes incompatible modal futures and enforces causal consistency.

A structural tension arises within this space: ontologies that combine **local triggers** with **unbounded global updates** tend to make **stable interference statistically atypical**. The P/A ontology serves as a constructive witness showing that this tension can be resolved. By using finite ray bundles, local geometric rules, and event‑defined entanglement constraints, the ontology maintains **bounded update domains** while preserving interference‑like behavior.

Simulations demonstrate that the framework can reproduce:

- qualitative **double‑slit interference**, and  
- **Tsirelson‑bounded correlations** in CHSH‑type tests,

using only discrete carriers, modal progression, and local pruning—without wavefunctions, amplitudes, or Hilbert‑space evolution. The ontology is structural rather than foundational: it specifies how actualization and modal futures can be organized in a discrete causal system, without making claims about underlying physics or empirical completeness.

---

## Repository Structure

```
PA-Ontology/
│
├── 📄 README.md # This file
├── ⚙️ Contributing.txt # Guidelines for contributing
│
├── 📂 Formal assumptions/ # Formal assumptions for realist ontologies
│ └── 00_Index.md # Index of formal assumptions
│ ├── 01_Axiom_1_Lawlike_Regularity.md
│ └── 02_Axiom_2_Universal_Applicability.md
│ └── 03_Axiom_3_Observer_Independence.md
│ └── 04_Postulate_4_Emperical_Conservation.md'
│ └── 05_Postulate_5_Finite_Speed_Local_Dynamics.md
│ └── 06_Postulate_6_Local_Stable_Records.md
│ └── 07_Postulate_7_Tsirelson_Bounded_Nonlocality.md
│ └── 08_Postulate_8_Quantum_Interference.md
│ └── 09_Postulate_9_Local_Availaility_Of_Triggers.md
│ └── 10_Postulate_9b_Weak_Mixing_No_Fine_Tuning.md
```

---

## Installation & Usage
This repository includes a Visual Studio solution (`PA-Ontology.slnx`) at the root.

### 1. Clone the repository
```bash
git clone https://github.com/ChristianVibe/PA-Ontology.git
```
### 2. Open the solution
Open PA-Ontology.slnx in:

- Visual Studio 2026, or

- VS Code with the C# Dev Kit extension.

### 3. Build & run
Build the solution and run the simulator project to reproduce:

- event‑evolution dynamics,

- interference experiments,

- correlation tests.

No external frameworks or environment setup required.

### Reproducibility
All simulations and figures can be reproduced by building and running the solution. The model uses only finite combinatorial structures and local update rules—no quantum libraries or Hilbert‑space machinery.