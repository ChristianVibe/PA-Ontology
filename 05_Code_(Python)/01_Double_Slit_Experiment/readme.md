# Python Version of the Double‑Slit Experiment (DSE)

This folder contains a lightweight Python port of the C# double‑slit experiment used in the P/A ontology.  
It is intended for **accessibility, readability, and pedagogical exploration**, not for high‑performance simulation.

---

## Why a Python Version?

Many physicists and researchers work primarily in Python.  
This version allows readers to:

- inspect the core ideas of the simulation,
- experiment with ray bundles and local steering,
- visualize detection distributions,
- and understand how the ontology’s local‑compatibility dynamics produce interference.

The Python code mirrors the structure of the C# implementation as closely as possible, but without the performance optimizations available in C#.

---

## Important Performance Note

The original C# code uses:

- **aggressive parallelization** (`Parallel.ForEach`)
- **compiled execution**
- **optimized memory layout**

The Python version uses:

- pure Python loops,
- pure Python objects,
- no parallelization,
- no NumPy acceleration.

As a result, **full‑scale simulations are not feasible in pure Python**.

### ✔ Default parameters are intentionally tiny

```python
number_of_particles = 10
number_of_rays_per_particle = 10
