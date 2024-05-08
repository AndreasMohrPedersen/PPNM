using System;
using static System.Math;

public class splines
{
	public static double linterpInt(double[] x, double[] y, double z)
	{
		double integral = 0; //value of integral
		int i = 0;
		while(x[i+1]<z)
		{
			double dx = x[i+1]-x[i];
			integral += (y[i+1]+y[i])*dx/2;
			i++;
		}
		integral += (y[i] + linterp(x,y,z))*(z-x[i])/2;//add the rest of the integral 
		return integral;
	}

	public static double linterp(double[] x, double[] y, double z)
	{
		int i = binsearch(x,z);
		double dx = x[i+1]-x[i]; if(!(dx>0)) throw new Exception("uups...");
		double dy = y[i+1]-y[i];

		return y[i]+dy/dx*(z-x[i]);
	}//linterp

	public static int binsearch(double[] x, double z)
	{
		if (z<x[0] || z>x[x.Length-1]) throw new Exception("binsearch: bad z: ");
		int i=0, j=x.Length-1;
		while(j-i>1)
		{
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
		}
		return i;
	}
}//splines
