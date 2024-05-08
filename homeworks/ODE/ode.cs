using System;
using static System.Double;
using static System.Math;
using static butchertables;
public class ODEsolver
{
/*part A*/
	public static (vector, vector) rkstep45(
		matrix a,
		vector b5, 
		vector b4, 
		vector stepsizes, //values from butcher tableau
		Func<double, vector, vector> f,
		double x, 
		vector y, 
		double h //stepsize
		)
		{
	/*defining coefficients*/
			matrix k = new matrix(y.size, b5.size);
			vector k5 = new vector(y.size);
			vector k4 = new vector(y.size);

	/*generating coefficients*/
			for(int i=0; i<b5.size; i++)
			{
				double xstep = stepsizes[i]*h;
				vector ystep = new vector(y.size);
				for(int j=0; j<i; j++)
				{
					ystep +=h*a[i,j]*((vector)k[j]);
				}
				k[i]=f(x+xstep,y+ystep);
		
				k5+=b5[i]*k[i];
				k4+=b4[i]*k[i];
			}
			vector yh = y + h*k5;
			vector dy = (k5-k4)*h;// err estimate
			return (yh, dy);
		}//rkstep12
		
		public static (genlist<double>, genlist<vector>) driver(
			Func<double, vector, vector> F,
			(double,double) interval,
			vector ystart,
			double h = 0.125,
			double hmax=NaN, //max stepsize for newtonian circular motion
			double acc=0.01,
			double eps=0.01,
			int nmax = 999)
			{
				var (init,final) = interval; 
				double x = init;
				int stepcount = 0;
				matrix a;
				vector b5, b4, stepsizes;
				(a, b5, b4, stepsizes) = butchertables.rk45table(); 
				vector y = ystart.copy();

				var xlist = new genlist<double>(); xlist.add(x);
				var ylist = new genlist<vector>(); ylist.add(y);
				do
				{
					if(x>=final) return (xlist, ylist); //integration complete
					if(x+h>final) h=final-x;
					var (yh,dy) = rkstep45(a,b5,b4,stepsizes, F,x,y,h);
					double tol = (acc+eps*yh.norm())*Sqrt(h/(final-init));
					double err = dy.norm();
					if(err<=tol)
					{
						x+=h; 
						y=yh;
						xlist.add(x);
						ylist.add(y);
					}
					stepcount++;
					h*= Min(Pow(tol/err, 0.25)*0.95, 2); //adjusted stepsize
					if(hmax!=Double.NaN && h>hmax) h=hmax; 	
				}while(stepcount<=nmax);
				throw new ArgumentException("Driver: Did not reach final step");
		}//driver

/*part B*/
/*linear interpolater*/
	public static Func<double,vector> make_linear_interpolant(genlist<double> x, genlist<vector> y)
	{
		Func<double,vector> interpolant = delegate(double z){
			int i=binsearch(x,z);
			double dx=x[i+1]-x[i];
			vector dy=y[i+1]-y[i];
			return y[i]+dy/dx*(z-x[i]);
		};
		return interpolant;
	}
/*binary search*/	
	public static int binsearch(genlist<double> x, double z)
	{/* locates the interval for z by bisection */ 
		if( z<x[0] || z>x[x.size-1] ) throw new Exception("binsearch: bad z");
		int i=0, j=x.size-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		return i;
	}
/*ODE_ivp interpolant*/
	public static Func<double,vector> make_ode_ivp_interpolant
	(matrix a, 
	vector b5, 
	vector b4, 
	vector stepsizes, 
	Func<double,vector,vector> f,
	(double,double)interval,
	vector y, 
	double hstart=0.125, 
	double acc=0.01,
	double eps=0.01, 
	int nmax=999)
	{
		(var xlist,var ylist) = driver(f,interval,y,hstart,acc,eps,nmax);
		return make_linear_interpolant(xlist,ylist);
	}
}//solver
