using System;
using static System.Math;
using static System.Console;

public class csplines
{
	public vector x,y,b,c,d;

	public csplines(vector xs, vector ys)
	{
		int n = xs.size;
		x=xs.copy(); y=ys.copy();
		vector h = new vector(n-1);
		vector p = new vector(n-1);
		b = new vector(n); c = new vector(n-1); d = new vector(n-1);
		vector D = new vector(n); vector Q = new vector(n-1); vector B=new vector(n);

	/*calc b, c and d*/
		for(int i=0; i<n-1;i++)
		{
			h[i]=x[i+1]-x[i];
			p[i]=(y[i+1]-y[i])/h[i];
			//break;
		}

		/*tridiagonal system*/
		D[0]=2; D[n-1]=2; Q[0]=1; B[0]=3*p[0]; B[n-1]=3*p[n-2];
		for(int i=0; i<n-2;i++)
		{	
			D[i+1]=2*h[i]/h[i+1]+2;
			Q[i+1]=h[i]/h[i+1];
			B[i+1]=3*(p[i]+p[i+1]*h[i]/h[i+1]);
		//	break;	
		}

		for(int i=1;i<n;i++)
		{
			D[i]-=Q[i-1]/D[i-1]; 
			B[i]-=B[i-1]/D[i-1];
		//	break;
		}//gauss elimination

		b[n-1]=B[n-1]/D[n-1];
		for(int i=n-2; i>=0;i--)
		{
			b[i]=(B[i]-Q[i]*b[i+1])/D[i];
		//	break;
		}
		
		for(int i=0;i<n-1;i++)
		{
			c[i]=(-2*b[i]-b[i+1]+3*p[i])/h[i];
			d[i]=(b[i]+b[i+1]-2*p[i])/h[i]/h[i];
		//	break;
		}	
	}//cspline
	
	public double evaluate(double z)
	{
		int i = binsearch(x, z);
		double h = z-x[i];
		return y[i] + b[i]*h + c[i]*h*h + d[i]*h*h*h;
	}//evaluate

	public double derivative(double z)
	{
		int i = binsearch(x, z);
		double h = z-x[i];
		return b[i]+2*c[i]*h+3*d[i]*h*h;
	}//derivative

	public double integral(double z)
	{
		double Int=0;
		int i=0;
		while(x[i+1]<z)
		{
			double dx = x[i+1]-x[i];
			Int+=y[i]*dx+b[i]*dx*dx/2+c[i]*Pow(dx,3)/3+d[i]*Pow(dx,4)/4;
			i++;
		}
		double h = z-x[i];
		Int+=y[i]*h+b[i]*h*h/2+c[i]*Pow(h,3)/3+d[i]*Pow(h,4)/4;
		return Int;
	}//integral
		
	static int binsearch(double[] x, double z)
	{	/* locates the interval for z by bisection */ 
		if(z<x[0] || z>x[x.Length-1]) throw new Exception("binsearch: bad z");
		int i=0, j=x.Length-1;
		while(j-i>1)
		{
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
		}
		return i;
	}//binsearch
}//csplines 
