using System;
using System.IO;
using static System.Console;
using static System.Math;
using static leastsquares;

public static class main
{
	public static void Main()
	{
		/*law of radioactive decay*/
		vector time = new vector(new double[] {1,  2,  3, 4, 6, 9,   10,  13,  15}); // time t(days)
		vector activity = new vector(new double[] {117,100,88,72,53,29.5,25.2,15.2,11.1});// activity y of thX (realative units)
		vector dy = new vector(new double[] {6,5,4,4,4,3,3,2,2}); //uncertainty

		Func<double,double>[] fs = new Func<double,double>[] {z => 1.0, z => -z};//linear fittig function

		/*data for logarithmic fit*/
		vector logActivity = new vector(activity.size);
		vector logDy = new vector(activity.size);

		for(int i=0;i<activity.size; i++)
		{
			logActivity[i] = Log(activity[i]);
			logDy[i] = dy[i]/activity[i];
		}
/*leastsquare fit*/
		(vector c, matrix covariance) = lsfit(fs,time,logActivity,logDy);
		string divider = new string('-',covariance.size2*11);
/*Covariance Matrix*/
		WriteLine(divider);
		covariance.print("Covariance matrix:");
		WriteLine(divider);

		double a = c[0];
		double lambda = c[1];
		double aError = Sqrt(covariance[0,0]); 
		double lambdaError = Sqrt(covariance[1,1]);
/*coefficients from fit*/
		WriteLine("Fit parameters:");
		WriteLine($"	Activity: {Round(a, 4)}+-{Round(aError, 4)}");
		WriteLine($"	lambda: {Round(lambda, 4)}+-{Round(lambdaError, 4)} days^-1\n");

/*calculation of halflife with errors*/
		WriteLine("Comparison with modern value for half-life:");
		WriteLine($"	Half-life: {Round(Log(2)/lambda, 4)}+-{Round(Log(2)*lambdaError/Pow(lambda,2),4)} days");	
		WriteLine($"	Modern value for half-life: {3.6319} days(Wikipedia)\n");
		WriteLine($"	Modern half-life is found to be lower than \neven the lower limit of out calculated value");

/*Writing datafiles for plot*/
		using(var decayData = new StreamWriter("rutherford.txt"))
		{
			for(int i=0; i<time.size; i++)
			{
				decayData.WriteLine($"{time[i]} {activity[i]} {dy[i]}");
			}
		}
		/*fit*/
		using( var fit = new StreamWriter($"lsfit.txt"))
		{	
			double t = 0;
			double steps = 100;
			double stepsize = time[time.size-1]/steps;	
			
			for(int i=0; i<steps-1; i++)
			{
				t += stepsize;
				double lny = 0;
				for(int j=0; j<c.size; j++)
				{
					lny += c[j]*fs[j](t);
  
				}
				fit.WriteLine($"{t} {Exp(lny)}");
			}		
		}
	}//Main
}//main
