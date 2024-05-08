using System;
using System.IO;
using static System.Console;
using static System.Math;
using static ODEsolver;

public static class main
{	
	static double eps;
	
	public static void Main()
	{
		partA();
		partB();
		partC();
	}//Main
	public static void partA()
	{
		/*generic list for storing data*/
		genlist<double> xs;
		genlist<vector> ys;

/*A2*/			
		(xs,ys) = driver(harmonicOscillator, (0,10), ystart: new vector("1 0"), h: 1e-2, acc: 1e-7, eps: 1e-7);
		write.WriteData(xs, ys, "harmonic oscillator", outfile: "harmonicOscillator.txt", analytical: Cos);
/*A3*/
		(xs,ys) = driver(pend, (0,10), ystart: new vector($"{PI-0.1} 0"), h: 5e-1, acc: 1e-7, eps: 1e-7);
		write.WriteData(xs, ys, "pendulum",outfile: "pendulum.txt");
	}//partA

	public static void partB()
	{
/*B3*/
		genlist<double> xs; 
		genlist<vector> ys;
		
		/*netwonian circular motion*/
		eps = 0;
		(xs,ys) = driver(equatorialMotion, (0,10*PI), ystart: new vector("1 0"),  h: 1e-2,hmax: 0.125, acc: 1e-7, eps: 1e-7);
		write.WriteData(xs, ys, "circular", outfile: "circularMotion.txt");

		/*newtonian elliptical motion*/
		(xs,ys) = driver(equatorialMotion, (0,10*PI), ystart: new vector("1 -0.5"),  h: 1e-2, acc: 1e-7, eps: 1e-7);
		write.WriteData(xs, ys, "elliptical", outfile: "ellipticalMotion.txt");

		/*relativistic precession*/
		eps = 0.01;
		(xs,ys) = driver(equatorialMotion, (0,10*PI), ystart: new vector("1 -0.5"),  h: 1e-2, acc: 1e-7, eps: 1e-7);
		write.WriteData(xs, ys, "precession", outfile: "relativisticMotion.txt");
	}//partB

	public static void partC()
	{
/*C*/
		genlist<double> xs;
		genlist<vector> ys;

		/*initial conditions from fig 1 https://arxiv.org/pdf/math/0011268.pdf*/
		double x1=0.97000436, y1=-0.24308753,x2=-x1, y2=-y1, x3=0, y3=0;
		double vx3=-0.93240737, vy3=-0.86473146, vx1=-vx3/2, vy1=-vy3/2, vx2=vx1, vy2=vy1;
		vector y0 = new vector($"{x1} {y1} {vx1} {vy1} {x2} {y2} {vx2} {vy2} {x3} {y3} {vx3} {vy3}");

		(xs,ys) = driver(threeBody, (0,2*6.325913982), ystart: y0,  h: 1e-2, acc: 1e-6, eps: 1e-6);
		write.WriteData(xs, ys, "t x1 m_1 vx1 vy1 x2 m_2 vx2 vy2 x3 m_3 vx3 vy3",outfile: "threeBodyProblem.txt");
	}//partC

		
	/*Differential equations*/

	static vector threeBody(double t, vector y)//masses and G set to 1
	{
		vector dydt = new vector(y.size);
		
		for(int i=0; i<y.size/4; i++)// y is a multiple of 4
		{
			vector ri = new vector(y[4*i], y[4*i+1]);
			vector dvidt = new vector(2);
			for(int j=0; j<y.size/4; j++)
			{
				if(j!=i)
				{	
					vector rj = new vector(y[4*j], y[4*j+1]);
					dvidt += 1/(Pow((rj-ri).norm(), 3))*(rj-ri);
				}
			}
			dydt[4*i] = y[4*i+2];
			dydt[4*i+1] = y[4*i+3];
			dydt[4*i+2] = dvidt[0];
			dydt[4*i+3] = dvidt[1];
		}
		return dydt;
	}

	/*u''(φ) + u(φ) = 1 + εu(φ)2*/
	static vector equatorialMotion(double phi, vector y)
	{
		double u = y[0]; 
		double uPrime = y[1];
		double uDoublePrime = 1 - u + eps*u*u;
		return new vector(uPrime,uDoublePrime);
	}

	/*u=cos*/
	public static vector harmonicOscillator(double x, vector u)
	{
		double y = u[0];
		double yPrime = u[1];
		double yDoublePrime = -y;
		return new vector(yPrime, yDoublePrime); 
	}
	/*oscillator with friction: theta''(t) + b*theta'(t) + c*sin(theta(t)) = 0*/
	public static vector pend(double x, vector u)
	{
		double theta = u[0];
		double thetaPrime = u[1];
		double b=0.25, c=5;
		double thetaDoublePrime = -b*thetaPrime - c*Sin(theta);
		return new vector(thetaPrime, thetaDoublePrime);
	}	
}//main
