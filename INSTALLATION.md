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
