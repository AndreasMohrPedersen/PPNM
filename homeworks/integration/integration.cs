using System;
using static System.Math;
using static System.Double;

public static class integration
{	
	public static (double, double, int) openQuad(Func<double,double> f, double a, double b, double del=0.001, double eps=0.001, double f2=NaN, double f3=NaN)
	{
		double h = b-a;
		int ncalls=0;
		if(IsNaN(f2)){f2=f(a+2*h/6); f3=f(a+4*h/6); ncalls+=2;}//first call
		double f1 = f(a+h/6), f4 = f(a+5*h/6);ncalls += 2; //add evaluations
		double Q = (2*f1+f2+f3+2*f4)/6*(b-a); //higher order
		double q = (f1+f2+f3+f4)/4*(b-a);// lower order
		double err = Abs(Q-q);
		//Func<double,double> f = z => {ncalls++;return z*z;};
		if(err <= del+eps*Abs(Q)) 
		{
			return (Q, err, ncalls);
		}
		else 
		{
			(double leftBranch, double errLeft, int ncallsLeft) = openQuad(f,a,(a+b)/2, del/Sqrt(2), eps, f1, f2);//left
			(double rightBranch, double errRight, int ncallsRight) = openQuad(f,(a+b)/2, b, del/Sqrt(2), eps, f3, f4);//right
			double totErr =  Sqrt(errLeft*errLeft + errRight*errRight);
			return (leftBranch + rightBranch, totErr, ncallsLeft+ncallsRight+ncalls);
		}
	}//openQuad

/*implementation of the error function via its integral representation*/
	public static double intErf(double z, double del=0.001, double eps=0.001)
	{
		if(z<=1)
		{
			if(z<0)
			{
				return erf(z);
			}
			if(0<=z)
			{
				Func<double,double> f = x => Exp(-Pow(x,2));
				double integral1 = openQuad(f, 0, z, del, eps).Item1;	
				return 2/Sqrt(PI)*integral1;
			}
		}
		
		Func<double, double> F = x => Exp(-Pow((z+(1-x)/x),2))/x/x;
		double integral2 = openQuad(F, 0, 1, del, eps).Item1;
		return 1-2/Sqrt(PI)*integral2;
	}

	public static double erf(double x)
	{
		/// single precision error function (Abramowitz and Stegun, from Wikipedia)
		if(x<0) return -erf(-x);
		double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
		double t=1/(1+0.3275911*x);
		double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
		return 1-sum*Exp(-x*x);
	}//erf

	public static (double, double, int) openQuadCC(Func<double,double> f, double a, double b, double del=0.001, double eps=0.001)
	{
		Func<double,double> clenshawCurtis = theta => f( (a+b)/2+(b-a)/2*Cos(theta) )*Sin(theta)*(b-a)/2;//if a=-1m b=1 we get the special case
		return openQuad(clenshawCurtis, 0, PI, del, eps);
	}//openQuadCC

	public static (double, double, int) infLimInt(Func<double,double> f, double a, double b, double del=0.001, double eps=0.001)
	{
		if(double.IsNegativeInfinity(a) && double.IsPositiveInfinity(b))
		{
			Func<double,double> bothInf = x => f(x/(1-x*x))*(1+x*x)/((1-x*x)*(1-x*x));
			return openQuad(bothInf, -1,1, del, eps);
		}
		if(double.IsPositiveInfinity(b))
		{
			Func<double,double> bInf = x => f(a+x/(1-x))*1/((1-x)*(1-x));
			return openQuad(bInf, 0,1, del, eps);
		}
		if(double.IsNegativeInfinity(a))
		{
			Func<double,double> aInf = x => f(b+x/(1+x))*1/((1+x)*(1+x));
			return openQuad(aInf, -1,0, del, eps);
		}
		else
		{
			throw new ArgumentException("Integral hs no infinite limit");
			//return (0,0,0);
		}			
	}//infLimInts

}//integration
