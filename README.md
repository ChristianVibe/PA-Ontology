# Possibility/Actuality Ontology (P/A Ontology)

<p align="center">
  <img src="https://img.shields.io/badge/License-MIT-blue.svg" />
  <img src="https://img.shields.io/badge/Build-.NET_10-blueviolet.svg" />
  <img src="https://img.shields.io/badge/IDE-Visual_Studio_2026-5C2D91.svg" />
  <img src="https://img.shields.io/badge/IDE-VS_Code-007ACC.svg" />
  <img src="https://img.shields.io/badge/Python-3.12-yellow.svg" />
</p>

## About
A discrete, ray‑based ontology for closed physical systems built on a distinction between actualized events and admissible futures. The model uses finite causal propagation, local records, and event‑defined entanglement to reproduce interference and Tsirelson‑bounded correlations without assuming Hilbert space or wavefunctions.

---

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
├── 📂 01_Formal_Assumptions/ # Formal assumptions for realist ontologies
│ └── 00_Index.md # Index of formal assumptions
│ ├── 01_Axiom_1_Lawlike_Regularity.md
│ ├── 02_Axiom_2_Universal_Applicability.md
│ ├── 03_Axiom_3_Observer_Independence.md
│ ├── 04_Postulate_4_Emperical_Conservation.md'
│ ├── 05_Postulate_5_Finite_Speed_Local_Dynamics.md
│ ├── 06_Postulate_6_Local_Stable_Records.md
│ ├── 07_Postulate_7_Tsirelson_Bounded_Nonlocality.md
│ ├── 08_Postulate_8_Quantum_Interference.md
│ ├── 09_Postulate_9_Local_Availaility_Of_Triggers.md
│ └── 10_Postulate_9b_Weak_Mixing_No_Fine_Tuning.md
├── 📂 02_Structural_Results/ # Nogo theorems for realist ontologies
│ └── 01_Nogo_theorem.md
│ ├── 02_Toy_Model_Abbott_Costello.md
│ └── 03_Formal_Proof_Summary.md
├── 📂 03_Code_(C#)/ # Code for structural results
│ └── 📂 01_Double_Slit_Experiment/
|   └── DoubleSlitExperiment.csproj
|   └── program.cs
│ └── 📂 02_Bells_Test/
|   └── BellsTest.csproj
|   └── program.cs
├── 📂 04_Code_(Python)/ 
│ └── double_slit_experiment.py
│ └── bells_test.py

```
---

## Purpose of the Formal Assumptions

The repository includes a set of formal assumptions and empirical postulates
that define the structural setting for the statistical no‑go theorem.  
These assumptions are intentionally modest: they are not tied to any
specific ontology, and they do not presuppose quantum mechanics,
wavefunctions, collapse, or classicality.

Their purpose is to articulate the **minimal structural commitments**
that any realist ontology ought to adopt if it aims to:

- describe physical systems as having ontic states,
- respect locality in the operational sense,
- account for finite-speed causal influence,
- support stable, localized records,
- reproduce interference and Bell-type correlations,
- and avoid fine‑tuned conspiratorial suppression of admissible states.

The assumptions do not impose a particular dynamical model.  
They simply define the weakest background conditions under which
questions about locality, signaling, records, and interference can be
posed sharply. The statistical no‑go theorem is then derived within this
minimal framework.

These assumptions are not part of the P/A ontology itself.  
They form a neutral structural backdrop against which the theorem is
stated, and against which any realist ontology — including P/A —
can be evaluated.

---

## Conceptual Walkthroughs

To make the ontology accessible and intuitive, the repository includes
a set of conceptual walkthroughs of canonical quantum experiments:

- Double slit experiment  
- Bell/CHSH test
- Mach–Zehnder interferometer  
- Page-Geilker experiment
- Stern–Gerlach experiment  
- Delayed-choice quantum eraser  
- Schrödinger’s cat  
- Wigner’s friend  

These walkthroughs are not part of the formal paper. They are provided
to illustrate how the P/A ontology accounts for the qualitative structure
of quantum phenomena using only ray bundles, local compatibility,
pruning, and actualization.

### Extended Walkthrough: The Hydrogen Atom

In addition to the experiment-level walkthroughs, the repository also
includes a full-length conceptual analysis of the hydrogen atom.  
This walkthrough is structurally deeper than the others and demonstrates
how discrete progression, curvature, and intersection-based actualization
give rise to the familiar $1/n^2$	 spectrum without wavefunctions or
continuous potentials.

It is included here in full to establish conceptual priority and to
provide a reference for future work. Like the other walkthroughs, it is
not part of the formal paper.

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

## Python Usage (Lightweight Version)

This repository also includes Python ports of the core simulations (double‑slit experiment and Bell/CHSH test).  
These versions are designed for **accessibility and readability**, not for high‑performance execution.

### 1. Requirements
- Python 3.12+
- NumPy  
- (Optional) Matplotlib for plotting

Install dependencies:
```bash
pip install numpy matplotlib
```

### 2. Run the Python simulations
Each Python experiment is located under:

```
04_Code_(Python)/
```
Run, for example, the Bell test:

```bash
python bells_test.py
```
### 3. Important performance note
The Python versions are not optimized and intentionally avoid:

- parallelization,
- Numba/JIT acceleration,
- vectorized NumPy kernels,
- GPU execution.

As a result, full‑scale simulations (e.g., 10,000 trials × 10,000 rays) are not feasible in pure Python.

Default parameters are intentionally small:

```
TRIALS = 100
NUMBER_OF_RAYS = 1000
```
These values demonstrate the algorithmic structure but will not reach Tsirelson’s bound without increasing ray density and adding performance optimizations.

### 4. Purpose of the Python version
The Python code is meant for:

- understanding the ontology’s local update rules,

- experimenting with ray bundles,

- visualizing small‑scale behavior,

- exploring the Bell/CHSH logic interactively.

For full‑scale, reproducible figures and high‑resolution interference/correlation curves,
use the C# implementation, which is parallelized and optimized.