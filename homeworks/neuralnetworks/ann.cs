using System;
using static System.Console;
using static System.Math;
public class ann{
	int n; // number of hidden neurons
	Func<double,double> f = x => x*Exp(-x*x); // activation function gaussian wavelet
	Func<double,double> fPrime = x => Exp(-x*x)-2*x*x*Exp(-x*x);
	Func<double,double> fDoublePrime = x => Exp(-x*x)*(4*x*x*x - 6*x);
	Func<double,double> F = x =>  -Exp(-x*x)/2;
	
	vector p; // network parameters
	public double a(int i) {return p[i];}
	public double b(int i) {return p[n+i];}
	public double w(int i) {return p[2*n+i];}
//set method for a,b,w
	public void seta(int i, double x) {p[i]=x;}
	public void setb(int i, double x) {p[n+i]=x;}
	public void setw(int i, double x) {p[2*n+i]=x;}
		
/* constructors*/
	public ann(int n)
	{
		this.n = n;	
		p = new vector(3*n);
	}
	public ann(vector p)
	{
		this.n=p.size/3; 
		this.p=p;
	}//overload

/*response function and derivatives*/
	public double response(double x)
	{  
		p.print("response: p=");
		double Fp=0; 
		for(int i=0; i<n; i++) Fp+=f(x-a(i))/b(i)*w(i);
		return Fp;
	}
	public double derivative(double x)
	{  
		double Fp=0; 
		for(int i=0; i<n; i++) Fp+=fPrime(x-a(i))/b(i)/b(i)*w(i);
		return Fp;
	}
	public double secondDerivative(double x)
	{  
		double Fp=0; 
		for(int i=0; i<n; i++) Fp+=fDoublePrime(x-a(i))/b(i)/b(i)/b(i)*w(i);
		return Fp;
	}
	public double antiDerivative(double x)
	{  
		double Fp=0; 
		for(int i=0; i<n; i++) Fp+=F(x-a(i))/b(i)*w(i)*b(i);
		return Fp;
	}
	
	
/* training function*/ 
	public void train(vector x,vector y) 
	{
/*set starting values for p-vector*/
		for(int i=0; i<n; i++)
		{
			seta(i,x[i]+(x[x.size-1]-x[0])*i/(n-1));
//			seta(i,(double)(i+1)/n);
			setb(i,1);
			setw(i,1);	
		}
		
		vector dp=new vector(p.size);
		for(int i=0;i<n;i++)dp[i]=x[x.size-1]-x[0];
		for(int i=0;i<n;i++)dp[n+i]=0.9;
		for(int i=0;i<n;i++)dp[2*n+i]=2;

		Func<vector,double> C = u =>
		{
			ann annu = new ann(u);//create object ann
			double Cp=0;
			for(int k=0; k<x.size;k++)Cp+=Pow(annu.response(x[k])-y[k],2);//calls Func Fp
			return Cp;
		};
		
//		p = minimisation.Newton(C,p,acc: 1e-3,maxSteps: 1000, method: "forward").Item1;
//		p = minimisation.Newton(C,p,acc:1e-3,maxSteps: 1000,method: "central").Item1;
		p = randmin.go(C,p,dp,nsamples:10000);
	}//train
}//ann
