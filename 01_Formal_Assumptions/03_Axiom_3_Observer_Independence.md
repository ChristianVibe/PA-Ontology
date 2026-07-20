# Axiom 3 — Observer Independence

**ID:** ax_observer_independence  
**Type:** Axiom  
**Category:** Structural / Locality

---

## Formal Statement

No rule or trigger may reference an external observer-index or label.  
All triggers must be predicates on local ontic variables in a finite neighborhood

$$
\bigcup_{y:\, d(x,y) \le r} S_y.
$$

---

## Definition (Trigger)

A trigger for a rule $\ell$ is a Boolean predicate

$$
T_\ell : S_{U_\ell} \to \{0,1\},
$$

defined on the local state space of the subsystem $U_\ell$ on which $\ell$ acts.  
Triggers may depend only on the configuration in $S_{U_\ell}$ and never on observer-relative or epistemic quantities.  
They cannot reference global constraints or boundary conditions; they are strictly local predicates.

---

## Interpretation

Axiom 3 asserts that the ontology’s dynamics are entirely observer-independent.  
No observer, label, epistemic state, or external indexing may influence the activation of rules.  
All triggering conditions arise solely from the ontic configuration within a finite neighborhood.  
This ensures that:

- the dynamics are local,  
- the rules depend only on the actual ontic state,  
- no epistemic or observer-relative information enters the evolution,  
- the ontology behaves as a self-contained physical system rather than a simulation dependent on an external vantage point.

The axiom enforces that only ontic variables participate in the dynamics.

---

## Ontological Role

### 1. Enforces Locality of Rule Activation

By restricting triggers to finite neighborhoods, the axiom guarantees that rule activations propagate information at finite speed.  
This supports **Postulate 5 (Finite-Speed Local Dynamics)** and prevents nonlocal or instantaneous influences.

### 2. Prevents Epistemic Contamination

Rules cannot depend on what an observer knows, believes, or labels.  
This preserves the ontic nature of the ontology and prevents observer-dependent or operational interpretations from entering the dynamics.

### 3. Maintains Structural Autonomy

The ontology evolves according to its own internal state, not external bookkeeping or global constraints.  
This autonomy is essential for **Postulate 6 (Local Stable Records)** and for the coherence of the rule-based framework.

Axiom 3 thus secures the locality, neutrality, and autonomy of the ontology’s dynamical structure.
