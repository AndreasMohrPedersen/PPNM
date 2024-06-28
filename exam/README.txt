
(exam project 30) Yet another stochastic global optimizer:
Implement a stochastic global optimizer using the following algorithm,

Use the given number of seconds (or the number of samples) to search for the global minimum of the given cost-function 
in the given volume by sampling the function using a low-discrepancy sequence.
From the best point found at the previous step run your favourite local minimizer.

- I implemented stochastic global optimization (SGO) using a given number of samples.
- I implemented SGO using a given number of milliseconds.
- My SGO implementation can use forward and central Newton routines or the Nelder-Mead downhill simplex routine.
- My implementation is tested on two functions. The Beale function sampling over a time interval (ms) and the Ackley
  function sampling a given number of points.
- In each case the sampled point along with the starting guess and minimum found by SGO is plotted on top of the test
  function searched
- Exact results can be found in the out.txt    

I give myself 10 points:)
