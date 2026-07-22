# Possibility/Actuality Ontology (P/A Ontology)

<p align="center">
  <img src="https://img.shields.io/badge/License-MIT-blue.svg" />
  <img src="https://img.shields.io/badge/Build-.NET_10-blueviolet.svg" />
  <img src="https://img.shields.io/badge/IDE-Visual_Studio_2026-5C2D91.svg" />
  <img src="https://img.shields.io/badge/IDE-VS_Code-007ACC.svg" />
  <img src="https://img.shields.io/badge/Python-3.12-yellow.svg" />
</p>

## Overview

This repository presents a four‑layer research program:

- **Minimal realist axioms and postulates** — modest structural commitments any ontology that claims to be realist is structurally required to satisfy.

- **A structural no‑go theorem** — showing that locally triggered, unbounded actualization rules are statistically incompatible with stable interference.

- **The P/A ontology** — a constructive witness demonstrating that the axiomatic space is non‑empty and capable of reproducing interference and Tsirelson‑bounded correlations.

- **Simulation results** — empirical demonstrations that the ontology’s structural commitments are computationally realizable and reproduce interference, which‑path suppression, and Tsirelson‑bounded nonlocal correlations.

Together, these components define a coherent framework for discrete, event‑driven physical ontology.

---

## Executive Summary

The P/A ontology is a discrete, ray‑based model for closed physical systems built on a distinction between actualized events and admissible futures. It uses finite causal propagation, stable local records, and event‑defined entanglement to reproduce interference and Tsirelson‑bounded correlations without assuming Hilbert space, wavefunctions, or amplitudes. The ontology offers a geometric, event‑driven alternative to standard quantum ontology while preserving empirical predictions and avoiding nonlocal influences.

For a gentle, non‑technical introduction, see the [Gentle_Introduction](Gentle_Introduction.md).

---

## Minimal Realist Axioms and Postulates
The repository begins with a set of minimal realist axioms and postulates. These define the structural space of ontologies capable of supporting locality, finite‑speed causal influence, stable records, interference, and Tsirelson‑bounded correlations. They do not presuppose quantum mechanics, wavefunctions, collapse, classicality, or any specific interpretation.

The full list is available in [01_Formal_Assumptions](01_Formal_Assumptions).

**Axioms**
* Lawlike Regularity

* Universal Applicability

* Observer Independence

**Postulates**
* Empirical Conservation

* Finite‑Speed Local Dynamics

* Local Stable Records

* Tsirelson‑Bounded Nonlocality

* Quantum Interference

* Local Availability of Triggers  
* Weak Mixing / No Fine‑Tuning

These assumptions define the minimal realist backdrop.
They are not tied to the P/A ontology; they define the structural landscape within which any ontology can be evaluated.

---

## Structural No‑Go Theorem
A central structural result shows that certain actualization rules are statistically incompatible with stable interference. The full derivation is available in [02_Structural_Results](02_Structural_Results).

> **Main Theorem (Statistical No‑Go for Undivided Actualization)**
Let the ontology satisfy the Axioms and Postulates.
Assume the Undivided Actualization Hypothesis (UAH): that there exists at least one actualization rule whose trigger is local but whose update domain is unbounded
> **Theorem.**  
Under these assumptions, there exists 
$p_0 > 0$
 such that the probability that interference persists across 
$N$
 independent runs of an interference experiment is at most
$$(1 - p_{0})^{N}$$.
Consequently, UAH‑type rules are statistically incompatible with indefinite repeatability of interference, except on a set of measure‑zero histories in the induced probability measure.

This establishes a structural constraint:
locally triggered, unbounded global actualization rules cannot coexist with stable interference.

---

## The P/A Ontology (Constructive Witness)
The P/A ontology is a constructive witness showing that the axiomatic space is not empty. It satisfies all axioms and postulates while avoiding UAH entirely.

### Theoretical Overview
The ontology is built from two primitive **carrier** types:

- **Particles** — proper‑time evolution, extended causal chains.

- **Quanta** — modal‑time propagation, at most two events (emission and absorption).

**Actuality** consists of discrete events involving finite sets of carriers.
**Possibility** consists of ray bundles encoding admissible modal futures.

The universe evolves through:

- an **actualization operator** (selects an admissible interaction), and

- a **pruning operator** (removes incompatible futures).

### Structural Resolution
UAH‑type rules destabilize interference.
The P/A ontology resolves this tension by:

- using finite ray bundles,

- enforcing local geometric constraints,

- defining entanglement through shared events, and

- maintaining bounded update domains.

### Empirical Behavior
Simulations show that the framework reproduces:

- qualitative double‑slit interference,

- Tsirelson‑bounded correlations in CHSH‑type tests,

using only discrete carriers, modal progression, and local pruning — without wavefunctions or Hilbert‑space evolution.

---

## Simulation Results
The P/A ontology reproduces the qualitative structure of key quantum phenomena using only ray bundles, local compatibility, pruning, and actualization. These simulations demonstrate that the ontology’s structural commitments are computationally realizable and capable of producing nontrivial empirical signatures without wavefunctions or Hilbert‑space evolution.

- **Double Slit — No Which‑Path Detection**
Interference appears when no subsystem carries which‑path information.
The detection statistics cannot be represented as a convex mixture of exclusive path alternatives.
![Double slit without which‑path detection](DSE_42.png)


- **Double Slit — With Which‑Path Detection**
When a subsystem records which‑path information, interference is removed.
The detection statistics become a convex mixture over definite path alternatives.
![Double slit with which‑path detection](DSE_WPD_42.png)

- **Bell Test — Tsirelson‑Bounded Correlations**
A CHSH‑type experiment implemented with finite ray bundles, local actualization, and sparse conservation‑driven pruning yields correlations approaching the Tsirelson bound:
```code
E(a_0,b_0) = -0.6803
E(a_0,b_1) = -0.7130
E(a_1,b_0) = -0.7060
E(a_1,b_1) =  0.7099
```

giving a CHSH value:
$$
S = 2.8091,
$$
which is within the Tsirelson bound of $2\sqrt{2} \approx 2.8284$
.

### Summary

These results demonstrate that the P/A ontology:

- produces interference when no which‑path information is present,

- suppresses interference when which‑path detection occurs, and

- yields Tsirelson‑bounded nonlocal correlations in Bell‑type tests,

all using discrete carriers, modal progression, and local pruning — without wavefunctions, amplitudes, or Hilbert‑space evolution.

---

## Gentle Introduction
For a conceptual, jargon‑free walkthrough of the ontology’s core ideas, see the
[Gentle_Introduction](Gentle_Introduction.md).

---

## Folder Structure
The full technical development is organized into three folders:

```
📂 [01_Formal_Assumptions](01_Formal_Assumptions/) — axioms and postulates

📂 [02_Structural_Results](02_Structural_Results/) — no‑go theorem and derived constraints

📂 [03_Ontological_Categories](03_Ontological_Categories/) — formal definition of carriers, events, ray bundles, and operators
```

---

## Contact
If you have questions about the P/A ontology, wish to discuss the structural results, or are interested in collaboration, you’re welcome to reach out.

**Christian Vibe Scheller**  
Kongens Lyngby, Denmark

- Email: [paontology@gmail.com](mailto:paontology@gmail.com)
- GitHub Issues: https://github.com/ChristianVibe/PA-Ontology/issues
- GitHub Discussions: https://github.com/ChristianVibe/PA-Ontology/discussions
