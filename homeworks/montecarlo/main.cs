using System;
using System.IO;
using static System.Console;
using static System.Math;

public class main
{
	public static void Main()
	{
		partAB();
		partC();
	}//Main
	static void partAB()
	{
/*Part AB*/
		vector a = new vector(-1,-1);
		vector b = new vector(1,1);
		Func<vector,double> unitcircle = x => {if(x.norm()>1)return 0; return 1;};
		double min=1, max=1e5;
		int iterations = 100;

		int[] Ns = new int[iterations];
		double[] actualErr = new double[iterations];
		Func<vector, double> deviation = u =>
		{
			double sum=0;
			for(int i=0;i<iterations;i++) sum+=Pow(fit(Ns[i], u)-actualErr[i],2);
			return sum;
		};

		
		Directory.CreateDirectory("data");
		using(var output = new StreamWriter("data/unitcircle.txt"))	
		{
			for(int i=0;i<iterations;i++)
			{				
				double n = Log10(min)+(Log10(max)-Log10(min))/(iterations-1)*i;
				int N = (int)Pow(10, n);
				Ns[i]=N;

				(double plainInt, double plainErr) = montecarlo.plainMC(unitcircle, a,b,N);
				(double quasiInt, double quasiErr) = montecarlo.quasiMC(unitcircle, a,b,N);
				if(N<0) break;
				actualErr[i] = Abs(PI-plainInt);
				//	WriteLine($"{N}");
				output.WriteLine($"{N} {plainInt} {plainErr} {actualErr[i]} {1/Sqrt(N)} {quasiInt} {quasiErr}");
				
			}
		}
		vector init = new vector(1.0);
		vector bestparams = minimisation.Newton(deviation, init, method: "central").Item1;
		 
		using(var output2 = new StreamWriter("data/fit.txt"))	
		{
			for(int i=0;i<iterations;i++)
			{				
				output2.WriteLine($"{Ns[i]} {fit(Ns[i],bestparams)}");
				
			}
		}
		
		int N2 = (int)1e6;
		vector a2 = new vector(0,0,0);
		vector b2 = new vector(PI,PI,PI);
		Func<vector, double> singular = u => {double x=u[0], y=u[1], z=u[2]; double res = 1/(1-Cos(x)*Cos(y)*Cos(z))/PI/PI/PI;return res;};
		(double plainInt2, double plainErr2) = montecarlo.plainMC(singular, a2, b2, N2);
		
		WriteLine($"Part A3:");
		WriteLine($"N: {N2}");
		WriteLine($"Value of ∫0π dx/π ∫0π dy/π ∫0π dz/π[1-cos(x)cos(y)cos(z)]^(-1): {plainInt2}+-{plainErr2}");
		//WriteLine($"{PlainInt2}");
		
/*Part C*/
		(double stratifiedInt, double stratifiedErr) = montecarlo.stratifiedMC(unitcircle, a,b,10000, nmin:10);
		
		WriteLine($"\nstratInt:	{stratifiedInt} stratErr: {stratifiedErr}");	
	} //partAB

	static void partC()
	{
		vector a = new vector(-1,-1);
		vector b = new vector(1,1);
		Func<vector,double> unitcircle = x => {if(x.norm()>1)return 0; return 1;};

		/*stratified sampling*/
		genlist<double> xs = new genlist<double>();
		genlist<double> ys = new genlist<double>();
		
		using(var stratifiedOutput = new StreamWriter("data/stratifiedSampling.txt"))	
		{				
			(double stratifiedInt, double stratifiedErr) = montecarlo.stratifiedMC(unitcircle, a,b,10000,nmin: 10, xList: xs, yList: ys);
			stratifiedOutput.WriteLine($"\"StratifiedMC: {Round(stratifiedInt,4)}+-{Round(stratifiedErr,6)}\"");
			for(int i=0; i<xs.size; i++)stratifiedOutput.WriteLine($"{xs[i]} {ys[i]}");

		/*pseudo random sampling*/
		xs = new genlist<double>();
		ys = new genlist<double>();
		
		using(var pseudoOutput = new StreamWriter("data/pseudoSampling.txt"))	
		{	
			(double plainInt, double plainErr) = montecarlo.plainMC(unitcircle, a,b,10000, xList: xs, yList: ys);
			pseudoOutput.WriteLine($"\"PlainMC: {Round(plainInt,4)}+-{Round(plainErr,4)}\"");
			for(int i=0; i<xs.size; i++)pseudoOutput.WriteLine($"{xs[i]} {ys[i]}");

		}
		/*Quasi random sampling*/
		xs = new genlist<double>();
		ys = new genlist<double>();
		
		using(var quasiOutput = new StreamWriter("data/quasiSampling.txt"))	
		{
			(double quasiInt, double quasiErr) = montecarlo.quasiMC(unitcircle, a,b,10000, xList: xs, yList: ys);
			quasiOutput.WriteLine($"\"QuasiMC: {Round(quasiInt, 4)}+-{Round(quasiErr,4)}\"");
			for(int i=0; i<xs.size; i++)quasiOutput.WriteLine($"{xs[i]} {ys[i]}");
		}		
		
		}
	}//partC
	static double fit(int N, vector fitparams) 
	{
		return fitparams[0]/Sqrt(N);
	}//fit
}//main
