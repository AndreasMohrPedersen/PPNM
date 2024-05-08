using System;
using System.IO;
using static System.Console;
using static System.Math;

static class main
{
	static void Main()
	{
		partAB(n: 6, points: 10, interval: new double[] {-1,1}, name: "6neurons");
	}
	
	static void partAB(int n, int points, double[] interval, string name)
	{
		Func<double,double> g = x => Cos(5*x-1)*Exp(-x*x);
		g = x=>1;
		vector xs = new vector(points); vector ys = new vector(xs.size);

		Directory.CreateDirectory("data");
		using(var output = new StreamWriter($"data/{name}.xyData.txt"))
		{
			for(int i=0; i<xs.size; i++)
			{
				xs[i]= interval[0]+(interval[1]-interval[0])/(xs.size-1)*i;
				ys[i]=g(xs[i]);
				output.WriteLine($"{xs[i]} {ys[i]}");
			}
		}

		ann nn = new ann(n);//calls constructor
		nn.train(xs,ys);

		WriteLine($"{nn.response(xs[0])}");
		using(var output = new StreamWriter($"data/{name}.interpolation.txt"))
		{
			for(int i=0; i<xs.size; i++) output.WriteLine($"{xs[i]} {nn.response(xs[i])} {nn.derivative(xs[i])} {nn.secondDerivative(xs[i])} {nn.antiDerivative(xs[i])}");
		}	
	}
}
