
using System;
using System.IO;
using static System.Console;
using static System.Math;
public static class main
{
	public static void Main()
	{
		//partA();
		partB();
	}
	static void partA()
	{
		WriteLine($"##### Part A #####");
	
		WriteLine($"Parabola test:");
		vector x0 = new vector("2");
		vector parabolaRoot = roots.newton(parabola(1,0,-5), x0, acc: 1e-3);
		WriteLine($"Parabola:");
		WriteLine($"Initial guess: {x0[0]}, accuracy: {1e-3}");
		WriteLine($"Root:  ({parabolaRoot[0]})");
		
		Func<vector, vector> rosenbrock = x => 
		{
			vector df = new vector(x.size);
			double dfdx =-2*(1-x[0])-200*(x[1]-Pow(x[0],2))*2*x[0];
			double dfdy = 200*(x[1]-x[0]*x[0]);
			df[0]= dfdx; df[1]=dfdy;
			return df;
		};

		x0 = new vector("2,2");
		vector rosenbrockRoots = roots.newton(rosenbrock, x0, acc: 1e-3);
		WriteLine($"Rosenbrock's valley function:");
		WriteLine($"derivatives:\n	∂f/∂x = -2(1-x)-200x(y-x²)\n	∂f/∂y = 200(y-x²)");
		WriteLine($"Initial guess: ({x0[0]}, {x0[1]}), accuracy: {1e-3}");
 		WriteLine($"Root:  ({rosenbrockRoots[0]},{rosenbrockRoots[1]})");

		Func<vector, vector> himmelblau = x => 
		{
			vector df = new vector(x.size);
			double dfdx =4*(x[0]*x[0]+x[1]-11)*x[0]+2*(x[0]+x[1]*x[1]-7);
			double dfdy =2*(x[0]*x[0]+x[1]-11)+4*(x[0]+x[1]*x[1]-7)*x[1];
			df[0]=dfdx; df[1]=dfdy;
			return df;
		};

		x0 = new vector("-3,-3");
		vector himmelblauRoots = roots.newton(himmelblau, x0, acc: 1e-3);
		WriteLine($"Himmelblau's function:");
		WriteLine($"derivatives:\n	∂f/∂x = 4x(x²+y-11)-2(x+y²-7)\n	∂f/∂y = 2(x²+y-11)+4y(x+y²-7)");
		WriteLine($"Initial guess: ({x0[0]}, {x0[1]}), accuracy: {1e-3}");
		WriteLine($"Root:  ({himmelblauRoots[0]},{himmelblauRoots[1]})");
	}//part A

	public static void partB()
	{
	/*generic lists for data*/
		genlist<double> rs = new genlist<double>();
		genlist<vector> fs = new genlist<vector>();
		
	/*conditions for odesolver*/
		double rmin = 0.05, rmax=8;
		vector e0 = new vector(-0.7);
		vector fstart = new vector(rmin-rmin*rmin, 1-2*rmin);		

		

		//M(rmin, rmax);
		
		vector Eroots = roots.newton(M(rmin, rmax), e0, acc: 1e-3);
		
		(rs,fs)=ODEsolver.driver(radialSE(Eroots[0]), (rmin, rmax), ystart: fstart, h:1e-2, acc: 1e-3, eps: 1e-3);//need to run to fill out genlists
/*
		WriteLine($"E-Root: {Eroots[0]}");
		WriteLine($"last value of fs {fs[fs.size-1][0]}");
		WriteLine($"fs {fs.size}");
*/
	
		Directory.CreateDirectory("data");
		using (StreamWriter output = new StreamWriter("data/numericalswave.txt"))
		{
		//	output.WriteLine($"r \"E_0  found numerically to {Round(Eroots[0],5)}\"");
			for(int i=0;i<rs.size;i++) output.WriteLine($"{rs[i]} {fs[i][0]}");
		}

	/*convergence*/
		/*generate variables*/

		int n=100;
		genlist<double> rmins = new genlist<double>();
		genlist<double> rmaxs = new genlist<double>();
		genlist<double> accs = new genlist<double>();
		genlist<double> epss = new genlist<double>();

		for(int i=0; i<n;i++)
		{
			//rmins.add(0.1/Pow(2,i));
			rmins.add(0.001+i*(0.4-0.001)/(n-1));
			rmaxs.add(1+i*(rmax-1)/(n-1));
			accs.add(Log10(1e-6)+(Log10(1e-1)-Log10(1e-6))/(n-1)*i);//Log10(1e-1), Log10(1e-6)
			epss.add(Log10(1e-9)+(Log10(1e-1)-Log10(1e-9))/(n-1)*i);
		}

	/*generating convergence data*/

		using (StreamWriter output = new StreamWriter("data/convergenceData.txt"))
		{
			for(int i=0;i<n;i++) 
			{
				double rmin_data = roots.newton(M(rmins[i], rmax, acc: 1e-4, eps: 1e-4), e0)[0];
				double rmax_data = roots.newton(M(rmin, rmaxs[i], acc: 1e-4, eps: 1e-4), e0)[0];
				double acc_data = roots.newton(M(rmin, rmax, acc: Pow(10,accs[i]), eps: 1e-4), e0)[0];
				double eps_data = roots.newton(M(rmin, rmax, acc: 1e-4, eps: Pow(10,epss[i])), e0)[0];
			
				output.WriteLine($"{rmins[i]} {rmin_data} {rmaxs[i]} {rmax_data} {accs[i]} {acc_data} {epss[i]} {eps_data}");
			}
		}

	}//partB
	static Func<vector,vector> M(double rmin, double rmax, double acc = 1e-4, double eps=1e-4)
	{
		vector fstart = new vector(rmin-rmin*rmin, 1-2*rmin);
		Func<vector,vector> FE = e =>
		{
			genlist<vector> ys = ODEsolver.driver(radialSE(e[0]), (rmin, rmax), ystart: fstart, h:1e-2, acc: acc, eps: eps).Item2;
			return new vector(ys[ys.size-1][0]);
		};
		return FE;
	}	

	/* s-wave radial Schrödinger equation for the Hydrogen atom*/
	static Func<double, vector, vector> radialSE(double E)
	{
		Func<double, vector, vector> f = (r,u) =>
		{
			double fr=u[0];
			double frPrime=u[1];
			double frDoublePrime=-2*(E+1/r)*u[0];
			return new vector(frPrime, frDoublePrime);
		};
		return f;
	}//radialSE

/*parabola*/
	static Func<vector,vector> parabola(double a,double b,double c)
	{
		Func<vector,vector> f = x => 
		{
			vector fx = new vector(x.size);
			for(int i=0;i<x.size;i++) fx[i] = a*x[i]*x[i] + b*x[i] + c;
			return fx;
		};
		return f;
	}//parabola	
}//main
