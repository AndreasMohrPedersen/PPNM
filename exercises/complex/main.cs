class main{ 
	//approx method
	static bool approx(complex a, complex b,double acc=1e-9,double eps=1e-9){
		if(cmath.abs(a-b)<acc)return true;
		if(cmath.abs(a-b)<(cmath.abs(a)+cmath.abs(b))*eps)return true;
		return false;
		}

	static void Main(){
		//variable 
		complex i = complex.I;
		complex cOne = complex.One;

		//sqrt(-1)
		complex a = cmath.sqrt(-cOne);
		complex aResult = new complex(0,+- 1);
		System.Console.Write($"sqrt(-1) by program:	{a}\n");
		System.Console.Write($"sqrt(-1) manual:	{aResult}\n");
		System.Console.WriteLine($"comparison:		{a.approx(aResult)}\n");
		
		//sqrt(-i)
		complex b = cmath.sqrt(-i);
		complex bResult = new complex(1/cmath.sqrt(2), +-1/cmath.sqrt(2));
		System.Console.Write($"sqrt(-i) by program:	{b}\n");
		System.Console.Write($"sqrt(-i) manual:	{bResult}\n");
		System.Console.WriteLine($"comparison:		{b.approx(bResult)}\n");
		
		//exp(i)
		complex c = cmath.exp(i);
		complex cResult = new complex(cmath.cos(1),cmath.sin(1));
		System.Console.Write($"exp(i) by program:	{c}\n");
		System.Console.Write($"exp(i) manual:		{cResult}\n");
		System.Console.WriteLine($"comparison:		{c.approx(cResult)}\n");
		
		//exp(i*pi)
		complex d = cmath.exp(i*System.Math.PI);
		complex dResult = new complex(-1,0);
		System.Console.Write($"exp(i*pi) by program:	{d}\n");
		System.Console.Write($"exp(i*pi) manual:	{dResult}\n");
		System.Console.WriteLine($"comparison:		{d.approx(dResult)}\n");
		
		//i^i
		complex e = cmath.pow(i,i);
		complex eResult = new complex(0.2078795764, 0);
		System.Console.Write($"i^i by program:		{e}\n");
		System.Console.Write($"i^i manual:		{eResult}\n");
		System.Console.WriteLine($"comparison:		{e.approx(eResult)}\n");
		
		//ln(i)
		complex f = cmath.log(i);
		complex fResult = new complex(0,System.Math.PI/2);
		System.Console.Write($"ln(i) by program:	{f}\n");
		System.Console.Write($"ln(i) manual:		{fResult}\n");
		System.Console.WriteLine($"comparison:		{f.approx(fResult)}\n");
		
		//sin(i*pi)
		complex g = cmath.sin(i*System.Math.PI); 
		complex gResult = i/2*cmath.exp(System.Math.PI)-i/2*cmath.exp(-System.Math.PI);
		System.Console.Write($"sqrt(i*pi) by program:	{g}\n");
		System.Console.Write($"sqrt(i*pi) manual:	{gResult}\n");
		System.Console.WriteLine($"comparison:		{g.approx(gResult)}\n");
			
	}//Main
}//main
