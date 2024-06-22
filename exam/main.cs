using System;
using System.IO;
using System.Collections.Generic;
using static System.Math;
using static System.Console;

public static class main
{
	static void Main()
	{
		bealeTest();
		ackleyTest();
	}
	static void bealeTest()
	{
	/*Beale function*/
		Func<vector, double> beale = x => Pow((1.5-x[0]+x[0]*x[1]),2)+Pow((2.25-x[0]+x[0]*x[1]*x[1]),2)+Pow((2.625-x[0]+x[0]*x[1]*x[1]*x[1]),2);

		vector x0 = new vector(0,0);//center off square 
		
	/*generic list for low descrepancy sequence data*/
		genlist<vector> xsForward = new genlist<vector>();
		genlist<vector> xsCentral = new genlist<vector>();
		genlist<vector> xsNM = new genlist<vector>();
		
		(vector forward, vector forwardBestGuess) = globalOptimiser.SGO(beale, init: x0, boxDimensions: 8 , nsamples: 100, xs: xsForward);
		(vector central, vector centralBestGuess) = globalOptimiser.SGO(beale, init: x0, boxDimensions: 8 , nsamples: 100, method: "central", xs: xsCentral);
		(vector nelderMead, vector nmBestGuess) = globalOptimiser.SGO(beale, init: x0, boxDimensions: 8 , nsamples: 100, method: "NelderMead", options: new Dictionary<string, double>(){{"simplexSize", 0.5}}, xs: xsNM);

	/*out.txt*/
		WriteLine($"\nBeales's function, global minimum  at (3,0.5):\n");
//		WriteLine($"	Initial guess: ({x0[0]}, {x0[1]})");
		WriteLine("Forward Newton method:");
		WriteLine($"	Best guess:	({forwardBestGuess[0]},{forwardBestGuess[1]})");//startguess given to local minimiser
		WriteLine($"	Minima:	({forward[0]},{forward[1]})");
		WriteLine("Central Newton method:");
//		WriteLine($"	Best guess:	({centralBestGuess[0]},{centralBestGuess[1]})");
		WriteLine($"	Minima:	({central[0]},{central[1]})");
		WriteLine("NelderMead method:");
//		WriteLine($"	Best guess:	({nmBestGuess[0]},{nmBestGuess[1]})");
		WriteLine($"	Minima:	({nelderMead[0]},{nelderMead[1]})");
		
/*Data for plots*/		
		Directory.CreateDirectory("data"); //create directory data if it does not exist
		using (StreamWriter output = new StreamWriter($"data/quasiRandomDataBeale.txt"))
		{
			for(int i=0;i<xsNM.size;i++) 
			{		
				output.WriteLine($"{xsForward[i][0]} {xsForward[i][1]} {xsCentral[i][0]} {xsCentral[i][1]} {xsNM[i][0]} {xsNM[i][1]}");
			}
		} 
		using(StreamWriter output = new StreamWriter($"data/min&guessBeale.txt"))
		{
			output.WriteLine($"{forward[0]} {forward[1]} {forwardBestGuess[0]} {forwardBestGuess[1]} {central[0]} {central[1]} {centralBestGuess[0]} {centralBestGuess[1]} {nelderMead[0]} {nelderMead[1]} {nmBestGuess[0]} {nmBestGuess[1]}");
		}
	}
	static void ackleyTest()
	{
	/*Ackley function*/
		Func<vector, double> ackley = x => -20*Exp(-0.2*Sqrt(0.5*(x[0]*x[0]+x[1]*x[1])))-Exp(0.5*(Cos(2*PI*x[0]) + Cos(2*PI*x[1])))+E+20;
		vector x0 = new vector(0.2,0.2);//initial guess
		genlist<vector> xsForward = new genlist<vector>();
		genlist<vector> xsCentral = new genlist<vector>();
		genlist<vector> xsNM = new genlist<vector>();
		
		(vector forward, vector forwardBestGuess) = globalOptimiser.SGO(ackley, init: x0, boxDimensions: 8 , nsamples: 100, xs: xsForward);
		(vector central, vector centralBestGuess) = globalOptimiser.SGO(ackley, init: x0, boxDimensions: 8 , nsamples: 100, method: "central", xs: xsCentral);
		(vector nelderMead, vector nmBestGuess) = globalOptimiser.SGO(ackley, init: x0, boxDimensions: 8 , nsamples: 100, method: "NelderMead", options: new Dictionary<string, double>(){{"simplexSize", 0.5}}, xs: xsNM);
	/*out txt*/	
		WriteLine($"\nAckley's function, global minimum  at (3,0.5):\n");
//		WriteLine($"	Initial guess: ({x0[0]}, {x0[1]})");
		WriteLine("Forward Newton method:");
		WriteLine($"	Best guess:	({forwardBestGuess[0]},{forwardBestGuess[1]})");
		WriteLine($"	Minima:	({forward[0]},{forward[1]})");
		WriteLine("Central Newton method:");
//		WriteLine($"	Best guess:	({centralBestGuess[0]},{centralBestGuess[1]})");
		WriteLine($"	Minima:	({central[0]},{central[1]})");
		WriteLine("NelderMead method:");
//		WriteLine($"	Best guess:	({nmBestGuess[0]},{nmBestGuess[1]})");
		WriteLine($"	Minima:	({nelderMead[0]},{nelderMead[1]})");
		
	/*Data for plots*/		
		Directory.CreateDirectory("data");
		using (StreamWriter output = new StreamWriter($"data/quasiRandomDataAckley.txt"))
		{
			for(int i=0;i<xsNM.size;i++) 
			{		
				output.WriteLine($"{xsForward[i][0]} {xsForward[i][1]} {xsCentral[i][0]} {xsCentral[i][1]} {xsNM[i][0]} {xsNM[i][1]}");
			}
		} 
		using(StreamWriter output = new StreamWriter($"data/min&guessAckley.txt"))
		{
			output.WriteLine($"{forward[0]} {forward[1]} {forwardBestGuess[0]} {forwardBestGuess[1]} {central[0]} {central[1]} {centralBestGuess[0]} {centralBestGuess[1]} {nelderMead[0]} {nelderMead[1]} {nmBestGuess[0]} {nmBestGuess[1]}");
		}
	}
}
