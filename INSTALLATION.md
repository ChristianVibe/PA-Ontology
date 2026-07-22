## Installation & Usage

This repository includes a Visual Studio solution (PA-Ontology.slnx) at the root. This document explains how to set up the environment, open the solution, and run both the C# and Python versions of the simulations.

### 1. Clone the Repository

```bash
git clone https://github.com/ChristianVibe/PA-Ontology.git
```

### 2. Open the Solution

The root directory contains:

```
PA-Ontology.slnx
```

You can open this solution using:

- **Visual Studio 2022 or later**

-- File → Open → Project/Solution

-- Select PA-Ontology.slnx

- **Visual Studio Code**

-- Install the C# Dev Kit extension

-- Open the repository folder

-- VS Code will automatically detect and load the .slnx solution

The .slnx format is recognized by both environments.

### 3. Build & Run (C# Implementation)

Once the solution is open:

- In **Visual Studio**:

-- Select a simulation project (e.g., DoubleSlit, BellTest)

-- Press Run or F5

- In **VS Code**:

-- Open the Run and Debug panel

-- Choose a simulation project

-- Start debugging

All simulation outputs will appear in the console window.

### Requirements

- .NET 10 SDK

- Visual Studio 2022+ or VS Code with C# Dev Kit

### Reproducibility

All simulations can be reproduced by building and running the solution. The model uses only finite combinatorial structures and local update rules—no quantum libraries or Hilbert‑space machinery.

## Python Usage (Lightweight Version)

The repository also includes Python ports of the core simulations (double‑slit and Bell/CHSH). These versions are designed for accessibility and readability, not high‑performance execution.

### 1. Requirements

- Python 3.12+

- NumPy

- (Optional) Matplotlib for plotting

Install dependencies:

```
pip install numpy matplotlib
```

### 2. Run the Python Simulations

Python experiments are located under:

```
05_Code_(Python)/
`´´

Example:

`´`
python bells_test.py
`´`

### 3. Performance Note

The Python versions intentionally avoid:

- parallelization

- Numba/JIT acceleration

- vectorized NumPy kernels

- GPU execution

As a result, full‑scale simulations (e.g., 10,000 trials × 10,000 rays) are not feasible in pure Python.

Default parameters are intentionally small:

```
TRIALS = 100
NUMBER_OF_RAYS = 1000
``´

These demonstrate the algorithmic structure but will not reach Tsirelson’s bound without increasing ray density and adding performance optimizations.

### 4. Purpose of the Python Version

The Python code is meant for:

- understanding the ontology’s local update rules

- experimenting with ray bundles

- visualizing small‑scale behavior

- exploring Bell/CHSH logic interactively

For full‑scale, reproducible figures and high‑resolution interference/correlation curves, use the C# implementation, which is parallelized and optimized.
