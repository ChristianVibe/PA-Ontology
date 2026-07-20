# Python Version of the Bell / CHSH Test

This folder contains a Python port of the C# Bell‑test simulation used in the P/A ontology.  
It is designed for **clarity, accessibility, and experimentation**, not for maximum performance.

The Python version mirrors the structure of the C# implementation but uses pure Python + NumPy for vector math.  
This makes it easy to read and modify, but slower than the compiled and parallelized C# version.

---

## Why the Python Version Uses Small Parameters

The original C# code uses:

- 10,000 trials  
- 10,000 rays per apparatus  
- full parallelization (`Parallel.ForEach`)  
- optimized memory layout  

This allows the C# version to reach **Tsirelson‑bounded correlations** (≈ 2.828) reliably.

The Python version does **not** use parallelization and does **not** optimize memory access.  
As a result, full‑scale simulations would run too slowly for practical use.

### ✔ Default Python parameters

```python
TRIALS = 100
NUMBER_OF_RAYS = 1000
