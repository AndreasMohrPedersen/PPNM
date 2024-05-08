using System;
using System.IO;
using static System.Math;
using static System.Console;
using static integration;

public static class main
{
	static void Main()
	{
/*Part A*/

	/*test of openQuad*/
		/*integrals*/
		double acc = 0.001;
		double test1 = openQuad(func1, 0,1).Item1;
		(double test2, int ncalls2) = (openQuad(func2, 0,1).Item1, openQuad(func2, 0,1).Item3);
		double test3 = openQuad(func3, 0,1).Item1;
		(double test4, int ncalls4) = (openQuad(func4, 0,1).Item1, openQuad(func4, 0,1).Item3);

		
		WriteLine("Part A: Test of integrator");
		WriteLine($"\nintegral of sqrt(x) from 0 to 1:\n	Numerical: {test1}\n	Exact: {2f/3}");
		if(Abs(test1-2f/3)<acc){WriteLine("	Numerical result within accuracy");}
		else{WriteLine("	Numerical result not within accuracy");}
		
		WriteLine($"\nintegral of 1/sqrt(x) from 0 to 1:\n	Numerical: {test2}\n	Exact: {2}");
		if(Abs(test2-2)<acc){WriteLine("	Numerical result within accuracy");}
		else{WriteLine("	Numerical result not within accuracy ");}
		
		WriteLine($"\nintegral of 4*sqrt(1-x^2) from 0 to 1:\n	Numerical: {test3}\n	Exact: {PI}");
		if(Abs(test3-PI)<acc){WriteLine("	Numerical result within accuracy");}
		else{WriteLine("	Numerical result not within accuracy");}
		
		WriteLine($"\nintegral of ln(x)/sqrt(x) from 0 to 1:\n	Numerical: {test4}\n	Exact: {-4}");
		if(Abs(test4-(-4))<acc){WriteLine("	Numerical result within accuracy");}
		else{WriteLine("	Numerical result not within accuracy");}
	/*errorfunction*/
		Directory.CreateDirectory("data");
		using(var output = new StreamWriter("data/erf.txt"))
		{
			for(double x=-3;x<=3; x+=1.0/64)
			{
				output.WriteLine($"{x} {intErf(x, del: 1e-10, eps:1e-10)}");
			}
		}
		
		using(var input = new StreamReader("erf.data.txt"))//from plots exercise
		{
			using(var output2 = new StreamWriter("data/comparisonData.txt"))
			{
				
				for(string dataLine=input.ReadLine(); dataLine!=null;dataLine=input.ReadLine())
				{				
					string[] dataStr = dataLine.Split('\t');
					
					double[] dataColumns = new double[dataStr.Length];
					for(int i=0; i<dataStr.Length; i++)
					{
						 dataColumns[i] = double.Parse(dataStr[i]);
						}
					output2.WriteLine($"{dataColumns[0]} {Abs(intErf(dataColumns[0], del: 1e-10, eps: 1e-10)-dataColumns[1])} {Abs(erf(dataColumns[0])-dataColumns[1])}");
				}
			}
		}
/*Part B*/
	
		string[] python = File.ReadAllText("python.txt").Split("\n");
		(double test5, double err5, int ncalls5) = openQuadCC(func2, 0,1, del: 1e-4, eps: 1e-4);
		(double test6, double err6, int ncalls6) = openQuadCC(func4, 0,1, del: 1e-4, eps: 1e-4);

		
		WriteLine("\nPart B:");
		WriteLine("\nIntegral of 1/Sqrt(x):");
		WriteLine($"open quad:		Numerical: {test2},	Evaluations: {ncalls2}");
		WriteLine($"Clenshaw-Curtis:	Numerical: {test5},	Evaluations: {ncalls5}");
		WriteLine($"scipy.quad:		Numerical: {python[0]}");
		WriteLine("\nIntegral of Log(x)/Sqrt(x):");
		WriteLine($"open quad:		Numerical: {test4},	Evaluations: {ncalls4}");
		WriteLine($"Clenshaw-Curtis:	Numerical: {test6},	Evaluations: {ncalls6}");
		WriteLine($"scipy.quad:		Numerical: {python[1]}");
		
/*Part C*/
		(double test7, double err7, int ncalls7) = infLimInt(func5, double.NegativeInfinity, double.PositiveInfinity, del: 1e-4, eps: 1e-4);
		(double test8, double err8, int ncalls8) = infLimInt(func6, 1,double.PositiveInfinity, del: 1e-4, eps: 1e-4);

		
		WriteLine("\nPart C:");
		WriteLine("Integral of Exp(-x^2) from -inf to inf:");
		WriteLine($"open quad:		Numerical: {test7}+-{err7},	Evaluations: {ncalls7}");
		WriteLine($"scipy.quad:		Numerical: {python[2]}");

		WriteLine("\nIntegral of 1/X^2 from 1 to inf:");
		WriteLine($"open quad:		Numerical: {test8}+-{err8}, Evaluations: {ncalls8}");
		WriteLine($"scipy.quad:		Numerical: {python[3]}");

	}
	
	static double func1(double x){return Sqrt(x);}
	static double func2(double x){return 1/Sqrt(x);}
	static double func3(double x){return 4*Sqrt(1-Pow(x,2));}
	static double func4(double x){return Log(x)/Sqrt(x);}
	static double func5(double x){return Exp(-Pow(x,2));}
	static double func6(double x){return 1/Pow(x, 2);}

}//main
