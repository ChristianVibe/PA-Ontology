**ID:** structural_formalproof_nogo  
**Type:** Structural Result / Proof Summary  
**Category:** Derived Constraint / Typicality  

# Formal Proof Summary: Statistical No‑Go Theorem

This summary outlines the probabilistic structure behind the statistical no‑go theorem for Undivided Actualization. It makes explicit the weak typicality condition used to show that **trigger‑free histories are exponentially suppressed**, without introducing new physical assumptions beyond those in the main framework.

---

## Local State Space and Trigger States

For a fixed interference experiment, let $V_{\mathrm{exp}}$ be the finite set of sites associated with the experimental region, and let $S(V_{\mathrm{exp}})$ be its local state space.

Define:

- $S_{\mathrm{trig}} \subseteq S(V_{\mathrm{exp}})$ — the subset of local states in which the trigger condition for the UAH rule $\ell_{\mathrm{UAH}}$ is satisfied in the coherence‑relevant region (where path coherence is established).

The **Local Availability** postulate requires:

$$
\frac{|S_{\mathrm{trig}}|}{|S(V_{\mathrm{exp}})|} > 0,
$$

meaning trigger‑compatible states occupy a nonzero fraction of the local state space. This is a combinatorial condition, not yet probabilistic.

---

## Run‑Level Variables

For each run $n = 1, 2, 3, \ldots$, define a binary random variable:

$$
X_n =
\begin{cases}
1 & \text{if the local state in run $n$ lies in } S_{\mathrm{trig}} \text{ before detection}, \\
0 & \text{otherwise}.
\end{cases}
$$

The sequence $(X_n)$ records whether the trigger condition for $\ell_{\mathrm{UAH}}$ is satisfied in each run. Its statistical behavior is constrained only by a weak typicality assumption.

---

## Weak Mixing / No Fine‑Tuned Suppression

The **Weak Mixing** postulate states:

$$
\mathbb{P}(X_n = 1 \mid X_1 = 0, \ldots, X_{n-1} = 0) \ge p_0
$$

for some constant $p_0 > 0$ and all $n$.

Interpretation:

- Histories in which the system *systematically avoids* all trigger‑compatible states across runs require fine‑tuned global coordination and are excluded as non‑typical.
- No independence, stationarity, or identical distribution is assumed—only that trigger states are not suppressed to zero by pathological correlations.

This is a **weak typicality condition**, strictly weaker than standard probabilistic assumptions.

---

## Exponential Suppression of Trigger‑Free Histories

Consider the probability that **no trigger occurs** in the first $N$ runs:

$$
\mathbb{P}(X_1 = 0, \ldots, X_N = 0)
= \prod_{n=1}^{N} \mathbb{P}(X_n = 0 \mid X_1 = 0, \ldots, X_{n-1} = 0).
$$

For each factor:

$$
\mathbb{P}(X_n = 0 \mid X_1 = 0, \ldots, X_{n-1} = 0)
= 1 - \mathbb{P}(X_n = 1 \mid X_1 = 0, \ldots, X_{n-1} = 0)
\le 1 - p_0.
$$

Hence:

$$
\mathbb{P}(X_1 = 0, \ldots, X_N = 0) \le (1 - p_0)^N,
$$

and therefore:

$$
\lim_{N \to \infty} \mathbb{P}(X_1 = 0, \ldots, X_N = 0) = 0.
$$

So **histories with no trigger events across arbitrarily many runs are exponentially suppressed** and non‑typical.

---

## Consequence for Interference Stability

By the structural lemma in the main text:

- If $X_n = 1$ (trigger fires in the coherence region), interference is destroyed in run $n$, because the UAH rule assigns definite path‑relevant values.

Thus:

$$
\{\text{interference preserved in run } n\} \subseteq \{X_n = 0\},
$$

and the probability that interference is preserved in **all** $N$ runs satisfies:

$$
\mathbb{P}(\text{interference preserved in all } N \text{ runs})
\le (1 - p_0)^N \to 0 \quad \text{as } N \to \infty.
$$

---

## Structural Conclusion

Combining:

- local availability of trigger states,  
- weak mixing (no fine‑tuned suppression),  
- the effect of UAH firing on interference, and  
- exponential suppression of trigger‑free histories,

we obtain:

> **Actualization rules with unbounded update domains are statistically incompatible with the indefinite stability of interference phenomena.**  
> Only fine‑tuned, non‑typical histories of measure zero preserve interference across arbitrarily many repetitions.

This completes the conceptual structure of the statistical no‑go theorem without reproducing the full formal appendix.
