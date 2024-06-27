using System;
using System.IO;
using static System.Console;
using static System.Math;

static class main
{
	static void Main()
	{
		Func<double,double> g = x => Cos(5*x-1)*Exp(-x*x);
		partAB( g, n: 6, points: 15, interval: new double[] {-1,1}, resolution: 50, name: "6nn15p");
		partAB( g, n: 6, points: 7, interval: new double[] {-1,1}, resolution: 50, name: "6nn7p");
		partAB( g, n: 3, points: 15, interval: new double[] {-1,1}, resolution: 50, name: "3nn15p");
	}
	
	static void partAB(Func<double,double> g, int n, int points, double[] interval, int resolution, string name)
	{
		vector xs = new vector(points); 
		vector ys = new vector(xs.size);

	/*training data*/
		Directory.CreateDirectory("data");
		using(var output = new StreamWriter($"data/{name}.trainingData.txt"))
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
		for(int i=0; i<n; i++)WriteLine($"{nn.b(i)}");
		
		vector interpolationxs = new vector(resolution);

		WriteLine($"{nn.response(xs[0])}");
		using(var output = new StreamWriter($"data/{name}.interpolation.txt"))
		{
			for(int i=0; i<resolution; i++)
			{
				interpolationxs[i]= interval[0]+(interval[1]-interval[0])/(interpolationxs.size-1)*i;
				output.WriteLine($"{interpolationxs[i]} {nn.response(interpolationxs[i])} {nn.derivative(interpolationxs[i])} {nn.secondDerivative(interpolationxs[i])} {nn.antiDerivative(interpolationxs[i])}");
			}
		}	
	}
}
