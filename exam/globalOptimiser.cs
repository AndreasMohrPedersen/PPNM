using System;
using System.Collections.Generic;
using static System.Console; 

public class globalOptimiser
{
	public static (vector, vector) SGO(	//stochastic global optimizer
		Func<vector, double> f, 	//cost function to be minimised
		vector init, 				//initial guess
		double boxDimensions, 		//dimension of box around initial guess 
		double nsamples, 			//numbers of samplepoints pr. dimension
		double acc = 1e-3,			//accuracy for minimisations 
		int maxIterations = 10000,	//max nr. of iterations for minimising algorithm
		string method = "forward", //specify local minimiser
		Dictionary<string, double> options = null,
		genlist<vector> xs = null)
	{
/*low discrepancy sequence + find best guess for local minimiser*/
		int dimension = init.size;
		double bestFunctionValue=f(init);
		vector bestGuess = init;
		vector x = new vector(dimension);	
		
		for(int i=1;i<nsamples;i++)
		{
			for(int j=0; j<dimension; j++)
			{
				vector hal = halton(i, dimension);
				x[j]=init[j]+(2*hal[j]-1)*boxDimensions/2;
			}
			double functionValue = f(x);
			if(xs!=null) xs.add(x.copy());
			
			if(functionValue < bestFunctionValue)
			{
				bestFunctionValue = functionValue; 
				bestGuess = x.copy();
			}
		}

/*call local minimiser on best guess*/
		vector min = new vector(dimension);
		switch(method)
		{
			case "central":
				min = minimisation.Newton(f, bestGuess, acc: acc, maxSteps: maxIterations, method: "central").Item1;
				break;

			case "NelderMead":
				double simplexSize = 0.1;		
				if(options != null) simplexSize = options["simplexSize"];
				min = minimisation.NelderMead(f, bestGuess, simplexSize: simplexSize, acc: acc, maxIterations: maxIterations).Item1;		
				break;

			default:
				min = minimisation.Newton(f, bestGuess, acc: acc, maxSteps: maxIterations, method: "forward").Item1;
				break;
		}	
		return (min, bestGuess);
	}//SGO



/*Halton sequence used to create low discrepancy sequence*/
	static double corput(int n, int b)//van der Corput
	{
		double q=0, bk=(double)1/b;
		while(n>0)
		{
			q+= (n%b)*bk; n/=b; bk/=b;
		}
		return q;
	}//corput
	
	static vector halton(int n, int d, int offset=0)//halton
	{
		vector x = new vector(d);
		int[] base_={2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61};
		int maxd=base_.Length/sizeof(int);
		if(d<=maxd);
		for(int i=0;i<d;i++)
		{
			x[i]=corput(n, base_[i+offset]);
		}
		return x;
	}//halton

}
