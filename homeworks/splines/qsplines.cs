using System;
using static System.Math;

public class qsplines
{
	public vector x,y,b,c;
	
	public qsplines(vector xs,vector ys)
	{
		x=xs.copy(); y=ys.copy();
		vector h = new vector(xs.size-1);
		vector p = new vector(xs.size-1);
		b= new vector(xs.size-1); c=new vector(xs.size-1); 
	/*calculate b and c */
		for(int i=0;i<xs.size-1;i++)
		{
			h[i]=x[i+1]-x[i];
			p[i]=(y[i+1]-y[i])/h[i];
		}
		c[0]=0;
		for(int i=0; i<xs.size-2; i++) c[i+1] = (p[i+1]-p[i]-c[i]*h[i])/h[i+1];
		c[xs.size-2]/=2;
		for(int i=xs.size-3; i>=0; i--) c[i] = (p[i+1]-p[i]-c[i+1]*h[i+1])/h[i];
		for(int i=0; i<xs.size-1; i++) b[i] = p[i]-c[i]*h[i];
	}//constructor
	
	public double evaluate(double z)
	{/* evaluate the spline */
		int i = binsearch(x, z);
		double h = z-x[i];
		return y[i]+b[i]*h+c[i]*h*h;
	}

	public double derivative(double z)
	{/* evaluate the derivative */
		int i = binsearch(x,z);
		double h = z-x[i];
		return b[i]+2*c[i]*h;		
	}
	public double integral(double z)
	{/* evaluate the integral */
		double Int = 0;
		int i =0;
		while(x[i+1]<z)
		{
			double dx = x[i+1]-x[i];
			Int += y[i]*dx+b[i]*dx*dx/2+c[i]*dx*dx*dx/3;
			i++;
		}
		double h = z-x[i];
		Int += y[i]*h+b[i]*h*h/2+c[i]*h*h*h/3;
		return Int;
	}

	public static int binsearch(double[] x, double z)
	{/* locates the interval for z by bisection */ 
	if( z<x[0] || z>x[x.Length-1] ) throw new Exception("binsearch: bad z");
	int i=0, j=x.Length-1;
	while(j-i>1){
		int mid=(i+j)/2;
		if(z>x[mid]) i=mid; else j=mid;
		}
	return i;
	}

}
