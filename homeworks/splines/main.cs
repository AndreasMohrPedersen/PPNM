using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class main
{
	public static void Main()
	{
		partA();
		partB1();
		partB2();
		partC();
	}//Main
	public static void partA()
	{

		int length = 20;
		//double lInterpInt = 0;
		double[] xs = new double[length];
		double[] ys = new double[length];
		for(int i=0;i<length;i++) {xs[i]=(double)i/2; ys[i]=Cos(xs[i]);}// WriteLine($"{xs[i]} {ys[i]}");}

		Directory.CreateDirectory("data"); //create directory data if it does not exist
		using (StreamWriter output = new StreamWriter($"data/linear.txt"))
		{
			for(int i=0;i<length-1;i++) 
			{
				double z= xs[i]+0.05;
				double lInterp = splines.linterp(xs, ys, z);
				double lInterpInt = splines.linterpInt(xs, ys, z);		
				output.WriteLine($"{xs[i]} {ys[i]} {z} {lInterp} {lInterpInt}");
				//WriteLine($"{z}");
			}
		}
		
	}//partA
	
	public static void partB1()
	{	
		int length = 20;
		vector xs = new vector(length);
		vector ys = new vector(length);
		using(StreamWriter output = new StreamWriter($"data/quadratic_xydata.txt"))
		{
			for(int i=0;i<length;i++)
			{
				xs[i]=(double)i/2; ys[i]=Sin(xs[i]);
				output.WriteLine($"{xs[i]} {ys[i]}");
			}
		}
		qsplines qInterp = new qsplines(xs, ys);
		
		using(StreamWriter output = new StreamWriter($"data/quadratic.txt"))
		{
			for(int i=0;i<length-1;i++) 
			{
				for(int j=0;j<length;j++)
				{
					double z = xs[i]+(xs[i+1]-xs[i])/(length-1)*j;
					double qSpline = qInterp.evaluate(z);
					double qSplineInt = qInterp.integral(z);
					double qSplineDerivative = qInterp.derivative(z);	

					output.WriteLine($"{z} {qSpline} {qSplineInt-1} {qSplineDerivative}");//-1 since we are missing a integration constant
				}
			}
		}
	}//partB1
	
	public static void partB2()
	{
		vector xs = new vector(1,2,3,4,5);
		vector y1 = new vector(xs.size);
		vector y2 = new vector(xs.size);
		vector y3 = new vector(xs.size);
		
		for(int i=0;i<xs.size;i++) {y1[i]=1; y2[i]=xs[i]; y3[i]=xs[i]*xs[i];}
		
		qsplines qInterp_y1 = new qsplines(xs, y1);	
		qsplines qInterp_y2 = new qsplines(xs, y2);
		qsplines qInterp_y3 = new qsplines(xs, y3);
		for(int i=0; i<xs.size-1;i++){ qInterp_y1.evaluate(1+i+0.5);}

		WriteLine("{xi=i, yi=1} , i=1…5");
		WriteLine($"b coefficients:");
		WriteLine($"	qspline:	{qInterp_y1.b[0]}, {qInterp_y1.b[1]}, {qInterp_y1.b[2]}, {qInterp_y1.b[3]}");
		WriteLine($"	Manually:	{0.0}, {0.0}, {0.0}, {0.0}");
		
		WriteLine($"c coefficients:");
		WriteLine($"	qspline:	{qInterp_y1.c[0]}, {qInterp_y1.c[1]}, {qInterp_y1.c[2]}, {qInterp_y1.c[3]}");
		WriteLine($"	Manually:	{0}, {0}, {0}, {0}");

		WriteLine("\n{xi=i, yi=xi} , i=1…5");
		WriteLine($"b coefficients:");
		WriteLine($"	qspline:	{qInterp_y2.b[0]}, {qInterp_y2.b[1]}, {qInterp_y2.b[2]}, {qInterp_y2.b[3]}");
		WriteLine($"	Manually:	{1}, {1}, {1}, {1}");
		
		WriteLine($"c coefficients:");
		WriteLine($"	qspline:	{qInterp_y2.c[0]}, {qInterp_y2.c[1]}, {qInterp_y2.c[2]}, {qInterp_y2.c[3]}");
		WriteLine($"	Manually:	{0.0}, {0.0}, {0.0}, {0.0}");

		WriteLine("\n{xi=i, yi=xi^2} , i=1…5");
		WriteLine($"b coefficients:");
		WriteLine($"	qspline:	{qInterp_y3.b[0]}, {qInterp_y3.b[1]}, {qInterp_y3.b[2]}, {qInterp_y3.b[3]}");
		WriteLine($"	Manually:	{2}, {4}, {6}, {8}");
		
		WriteLine($"c coefficients:");
		WriteLine($"	qspline:	{qInterp_y3.c[0]}, {qInterp_y3.c[1]}, {qInterp_y3.c[2]}, {qInterp_y3.c[3]}");
		WriteLine($"	Manually:	{1}, {1}, {1}, {1}");
	}//partB2

	public static void partC()
	{
		int length = 20; 
		vector xs = new vector(length); 
		vector ys = new vector(length);
		
		using(StreamWriter output = new StreamWriter($"data/cubic_xydata.txt"))
		{
			for(int i=0;i<length;i++)
			{
				xs[i]=(double)i/2; ys[i]=Sin(xs[i]);
				output.WriteLine($"{xs[i]} {ys[i]}");
			}
		}
		csplines cInterp = new csplines(xs,ys);

		using(StreamWriter output = new StreamWriter($"data/cubic.txt"))
		{
			for(int i=0;i<length-1;i++) 
			{
				for(int j=0; j<length; j++)
				{
					double z = xs[i]+(xs[i+1]-xs[i])/(length-1)*j;
					double cSpline = cInterp.evaluate(z);
					double cSplineInt = cInterp.integral(z);
					double cSplineDerivative = cInterp.derivative(z);
					output.WriteLine($"{z} {cSpline} {cSplineInt-1} {cSplineDerivative}");//-1 due to missing integration constant
				}
			}
		}
	}//partC 
}//main
