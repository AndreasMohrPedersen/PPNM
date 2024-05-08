import numpy as np
from scipy.integrate import quad
import math

def func1(x):
	return 1/math.sqrt(x)

def func2(x):
	return math.log(x)/math.sqrt(x)

test1py = quad(func1, 0, 1,full_output=1, epsabs=1e-4, epsrel=1e-4)
evals1 = test1py[2]["neval"]
print(f"{test1py[0]},	Evaluations; {evals1}")

test2py = quad(func2, 0, 1,full_output=1, epsabs=1e-4, epsrel=1e-4)
evals2 = test2py[2]["neval"]
print(f"{test2py[0]},	Evaluations: {evals2}")

#part c:
def func3(x):
	return math.exp(-x**2)

def func4(x):
	return 1/x**2 

test3py = quad(func3, -math.inf, math.inf,full_output=1, epsabs=1e-4, epsrel=1e-4)
evals3 = test3py[2]["neval"]
print(f"{test3py[0]}+-{test3py[1]},	Evaluations: {evals3}")

test4py = quad(func4, 1, math.inf ,full_output=1, epsabs=1e-4, epsrel=1e-4)
evals4 = test4py[2]["neval"]
print(f"{test4py[0]}+-{test4py[1]},	Evaluations: {evals4}")
