static class main{
	static int maxInteger = int.MaxValue;
	static int minInteger = int.MinValue;
	static double machineEpsilonDouble = System.Math.Pow(2,-52);
	static double machineEpsilonFloat = System.Math.Pow(2,-23);
	static void Main(){

		//max/min integer values:
		System.Console.Write($"part 1:\n");
		System.Console.WriteLine($"	max int value: {maxInteger}");
		int i=1;
		while(i+1>i){i++;}
		System.Console.Write($"	maximum int = {i}\n"); //regular while loop

		System.Console.WriteLine($"	max int value: {minInteger}");
		/*do{ System.Console.Write($"minimum int = {i}\n"); //do while loop
		i--;}
		while(i-1<i);*/
		
		while(i-1<i){i--;}
		System.Console.Write($"	minimum int = {i}\n");

		//calc. machine epsilon
		System.Console.Write($"part 2:\n");
		System.Console.WriteLine($"	2^(-52): {machineEpsilonDouble}");
		double x=1; while(1+x!=1){x/=2;} x*=2; 
		System.Console.Write($"	machineEpsilon for double: {x}\n");
		
		System.Console.WriteLine($"	2^(-23): {machineEpsilonFloat}");
		float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F; 
		System.Console.Write($"	machineEpsilon for float: {y}\n");

		//tiny=epsilon/2
		double epsilon = machineEpsilonDouble;
		double tinyValue = epsilon/2;
		double a = 1 + tinyValue + tinyValue;
		double b = tinyValue + tinyValue + 1;
		System.Console.Write($"part 3:\n");
		System.Console.Write($"	a==b:	{a==b}\n");//false the two numbers are not equal 
		System.Console.Write($"	a>1:	{a>1}\n");//false a is not > 1
		System.Console.Write($"	b>1:	{b>1}\n");// true

		//comparing doubles:
		double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
		double d2 = 8*0.1;
		System.Console.Write($"part 4:\n");
		System.Console.WriteLine($"	d1={d1:e15}");
		System.Console.WriteLine($"	d2={d2:e15}");
		System.Console.Write($"	d1==d2: {d1==d2}\n");//not the same since 0.1 cant be represented precisely by 52 digit binary code				
		System.Console.Write($"	approx func: {approx(a, b)}\n");

	}//Main
	static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
		if(System.Math.Abs(b-a) <= acc) return true;
		if(System.Math.Abs(b-a) <= System.Math.Max(System.Math.Abs(a),System.Math.Abs(b))*eps) return true;
		return false;}
}//main

