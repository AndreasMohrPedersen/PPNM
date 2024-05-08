using System;
using System.IO;
using static System.Console;
using static System.Math;

public class montecarlo
{
	public static (double, double) plainMC(Func<vector, double> f, vector a, vector b, int N, genlist<double> xList=null, genlist<double> yList=null)
	{
		int dim = a.size; double V=1; 
		for(int i=0; i<dim; i++)
		{
			V*=b[i]-a[i];
		}
		double sum=0, sum2=0;
		var x = new vector(dim);
		var rnd = new Random();
		for(int i=0; i<N;i++)
		{
			for(int k=0;k<dim;k++)
			{
				x[k]=a[k]+rnd.NextDouble()*(b[k]-a[k]);
			}
			double fx=f(x); sum+=fx; sum2+=fx*fx;
			if(xList!=null)
			{
				xList.add(x[0]);
				yList.add(x[1]); 
			}
		}
		double mean = sum/N, sigma=Sqrt(sum2/N-mean*mean);
		var result = (mean*V, sigma*V/Sqrt(N));
		return result;
	}//plainMontecarlo

/*quasi sampling:*/
	public static (double, double) quasiMC(Func<vector, double> f, vector a, vector b, int N, genlist<double> xList=null, genlist<double> yList=null)
	{
		int dim = a.size; double V=1; 
		for(int i=0; i<dim; i++)
		{
			V*=b[i]-a[i];
		}
		double sum1=0, sum2=0;//sum2 not the err
		var x1 = new vector(dim);
		var x2 = new vector(dim);
		for(int i=0; i<N;i++)
		{
			for(int k=0;k<dim;k++)
			{
				vector hal1 = halton(i, dim);
				vector hal2 = halton(i, dim, offset: 7);
				x1[k]=a[k]+hal1[k]*(b[k]-a[k]);//use primes for base
				x2[k]=a[k]+hal2[k]*(b[k]-a[k]);
			}
			sum1+=f(x1); sum2+=f(x2);	
			if(xList!=null)
			{
				xList.add(x1[0]);
				yList.add(x1[1]); 
			}
		}
		double mean1 = sum1/N, mean2 = sum2/N;
		double err=Abs(mean1-mean2);
		
		var result = (mean1*V, V*err);
		return result;
	}//quasiMontecarlo


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
	}
/*Stratified sampling*/ 
public static (double, double) stratifiedMC(Func<vector,double> f, vector a, vector b, int N, int nmin=100, genlist<double> xList=null, genlist<double> yList=null)
{
		if(N<nmin)
		{
			return plainMC(f,a,b,Max(N,1), xList, yList);
		}
		else
		{
			int n = Max(N/10, 2);//number of sampling point cannot go below 2
						
			double[] subErrs = new double[a.size]; //subErr in each dimensions.
			double[] leftErrRatio = new double[a.size];
			for(int i=0; i<a.size; i++)
			{
				double mid = (b[i]+a[i])/2; //calc midpoint of volume in each dimension
				vector midLeft = b.copy();//to reset every iteraration
				vector midRight= a.copy();

				midLeft[i] = mid;//set midpoint in each direction 
				midRight[i] = mid;

				
				//calc Err
				double errLeft = plainMC(f, a, midLeft, n).Item2;
				double errRight= plainMC(f, midRight, b, n).Item2;
				leftErrRatio[i]= errLeft/(errLeft+errRight);//for estimating nLeft/nRight
				subErrs[i] = Abs(errLeft*errLeft-errRight*errRight);
			}

			int dimensionOfLargestSubErr = 0;
			for(int i=0; i<a.size; i++)
			{
				if(subErrs[i]>subErrs[dimensionOfLargestSubErr]) dimensionOfLargestSubErr = i;
			}//finds dimension of largest sub error
			
			vector midLeft2 = b.copy();
			vector midRight2 = a.copy();
			midLeft2[dimensionOfLargestSubErr] = (b[dimensionOfLargestSubErr]+a[dimensionOfLargestSubErr])/2;
			midRight2[dimensionOfLargestSubErr] = (b[dimensionOfLargestSubErr]+a[dimensionOfLargestSubErr])/2;

			int nLeft = (int) (N*leftErrRatio[dimensionOfLargestSubErr]);
			int nRight = N-nLeft;

			//recursive call of stratifiedMC i largest sub err dimension
			(double leftInt, double leftErr) = stratifiedMC(f, a, midLeft2, nLeft, nmin, xList, yList);
			(double rightInt, double rightErr)= stratifiedMC(f, midRight2, b, nRight, nmin, xList, yList);		

			/*Grand avg+err*/
			double grandInt = leftInt+rightInt;
			double grandErr = Sqrt(leftErr*leftErr+rightErr*rightErr);
			return (grandInt, grandErr);
		}
	}//stratifiedMC
}//montecarlo
