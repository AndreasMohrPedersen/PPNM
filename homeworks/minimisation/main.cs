using System;
using System.IO;
using static System.Console;
using static System.Math;
public static class main
{
	public static void Main()
	{
		partAC();
		partB();
	}
	public static void partAC()
	{
		vector x0 = new vector(2,2);
		Func<vector,double> rosenbrock = x => Pow((1-x[0]), 2)+100*Pow((x[1]-x[0]*x[0]),2);
		(vector forwardRosenbrock, double stepsForwardRosenbrock) = minimisation.Newton(rosenbrock, x0);
		(vector centralRosenbrock, double stepsCentralRosenbrock) = minimisation.Newton(rosenbrock, x0, method: "central");
	
		WriteLine($"Rosenbrock's valley function:");
		WriteLine($"	Initial guess: ({x0[0]}, {x0[1]})");
		WriteLine("Forward method:");
		WriteLine($"	Minima:	({forwardRosenbrock[0]},{forwardRosenbrock[1]}),	steps: {stepsForwardRosenbrock}");
		WriteLine("Central method:");
		WriteLine($"	Minima:	({centralRosenbrock[0]},{centralRosenbrock[1]}),	steps: {stepsCentralRosenbrock}");

		x0 = new vector(-3,-3);
		Func<vector,double> himmelblau = x => Pow((x[0]*x[0]+x[1]-11),2)+Pow((x[0]+x[1]*x[1]-7),2);
		(vector forwardHimmelblau, double stepsForwardHimmelblau) = minimisation.Newton(himmelblau, x0);
		(vector centralHimmelblau, double stepsCentralHimmelblau) = minimisation.Newton(himmelblau, x0, method: "central");
		
		WriteLine($"\nHimmelblau's function:");
		WriteLine($"	Initial guess: ({x0[0]}, {x0[1]})");
		WriteLine("Forward method:");
		WriteLine($"	Minima:	({forwardHimmelblau[0]},{forwardHimmelblau[1]}),	steps: {stepsForwardHimmelblau} ");
		WriteLine("Central method:");
		WriteLine($"	Minima:	({centralHimmelblau[0]},{centralHimmelblau[1]}),	steps: {stepsCentralHimmelblau}\n");


	}//partA

	public static void partB()
	{
		var energy = new genlist<double>();
		var signal = new genlist<double>();
		var error  = new genlist<double>();
		var separators = new char[] {' ','\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;
	/*read from higgs.data.txt*/		
		using(var input = new StreamReader("higgs.data.txt"))
		{
			for(string line=input.ReadLine(); line!=null; line=input.ReadLine())
			{
				string[] data = line.Split(separators, options);
				if(data[0] == "energy") continue;
				for(int i=0;i<data.Length; i++)
				{
					energy.add(double.Parse(data[0]));
		        	signal.add(double.Parse(data[1]));
		        	error .add(double.Parse(data[2]));
				}
			}
		}

		Func<vector, double> deviation = u =>
		{
			double sum=0;
			for(int i=0; i<energy.size; i++)
			{
				sum+= Pow((breitWigner(energy[i], u)-signal[i])/error[i],2);
			}
			return sum;
		};

		vector startguess = new vector(125, 5,30);//initial guess for minimisation func
		vector fitParams = minimisation.Newton(deviation, startguess).Item1;//fitting parameters
		vector fitParamsCN = minimisation.Newton(deviation, startguess).Item1;//fitting parameters central newton

		WriteLine($"Estimated mass of the Higgs:");
		WriteLine($"	From Wikipedia: 	125.25(17) 	 GeV/c^2");
		WriteLine($"	Forward Newton method:	{fitParams[0]} GeV/c^2");
		WriteLine($"	Central Newton method: 	{fitParams[0]} GeV/c^2");
		
		Directory.CreateDirectory("data");
		using(var output = new StreamWriter("data/fitting.txt"))
		{
			int iterations = 1000;
			double Es = energy[0];
			double step = (energy[energy.size-1]-energy[0])/iterations;
			for(int i=0; i<iterations; i++)
			{
			
				output.WriteLine($"{Es} {breitWigner(Es, fitParams)} {breitWigner(Es, fitParamsCN)}");
				Es += step;
			}	
		}
	}//partB

	static double breitWigner(double E, vector u)
	{
		double m = u[0], gamma=u[1], A=u[2]; //fitting parameters
		return A/(Pow(E-m,2) +gamma*gamma/4);
	}
}//main
