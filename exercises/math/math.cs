//using system.math
static class main{ 
	static double sqrt2 = System.Math.Sqrt(2);
	static double pow215 = System.Math.Pow(2, 1.0/5);
	static double pi = System.Math.PI;
	static double expPi = System.Math.Exp(pi);
	static double piExp = System.Math.Pow(pi, System.Math.E);
	static void Main(){
		System.Console.WriteLine($"Square root of 2: {sqrt2}");
		System.Console.WriteLine($"Test of sqrt2: {sqrt2*sqrt2}");
		System.Console.WriteLine($"2^(1/5): {pow215}");
		System.Console.WriteLine($"Test of 2^(1/5): {pow215*pow215*pow215*pow215*pow215}");
		System.Console.WriteLine($"e^pi: {expPi}");
		System.Console.WriteLine($"Test of e^pi: {System.Math.Pow(System.Math.E, pi)}");
		System.Console.WriteLine($"pi^e: {piExp}");
		System.Console.WriteLine($"Test of e^pi: {System.Math.Pow(pi, System.Math.E)}");

		int[] exactGammas = {1, 1, 2, 6, 4, 120, 720, 5040, 40320, 362880};
		for(int i=0;i<10;++i){
			System.Console.WriteLine($"Γ({i+1}): {sfuns.fgamma(i+1)}");
			System.Console.WriteLine($"ln(Γ({i+1})): {sfuns.lngamma(i+1)}");
			System.Console.WriteLine($"Exact value of Γ({i+1}): {exactGammas[i]}");
		}
	}
}
