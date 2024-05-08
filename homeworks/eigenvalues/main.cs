using static System.Console;
using static System.Math;
using System.IO;
static class main
{
	static void Main(string[] args)
	{
		partA();
		rValues(args);
		wavefuncs();
	}
	static void partA()
	{
		int n = 5;
		string divider = new string('-', n*11);
				
		matrix A = new matrix(n,n); //creates n,n dimensional matrix for randomizer
		A.randomSymmetric(); //generates randomsymmetric matrix(from matrix.cs)
		WriteLine(divider);
		A.print("random symmetric matrix, A"); 
		WriteLine(divider);
		
		(vector w, matrix V) = jacobi.cyclic(A);
		WriteLine(divider);
		V.print("Orthogonal matrix V:");
		matrix VTV = V.T*V;
		matrix VVT = V*V.T;


		WriteLine(divider);
		VTV.print("V^T*V=1:");
		WriteLine(divider);
		
		VVT.print("V*V.T=1:");
		WriteLine(divider);
		w.print("Eigenvalues:\n");
		WriteLine(divider);
		
		//decomposed matrix
		matrix VTAV = V.T*A*V;
		matrix VDVT = V*VTAV*V.T;
	
		VTAV.print("D=V^T*A*V:");
		WriteLine(divider);
		VDVT.print("A=V*D*V^T:");
		WriteLine(divider);
		
		matrix eigenvector  = new matrix(w.size);
		for(int i=0; i<w.size; i++) eigenvector[i] = w[i]*V[i];
		matrix eigenvector2 = A*V;

		eigenvector.print("eigenvalues * eigenvectors V:");
		WriteLine(divider);
		eigenvector2.print("matrix A * eigenvectors V:");
		WriteLine(divider);
	}//EVDtest
/*partB*/
	static void rValues(string[] args)
	{
		(double rmax, double drValue) = Hydrogen.readCMD(args);
		
		
		using(var rMax = new StreamWriter("drVaried.txt"))
		{
			for(double dr=0.1; dr<=rmax/2; dr+=drValue)
			{
				(vector e1, matrix f1) = Hydrogen.SE(rmax, dr);
				rMax.WriteLine($"{dr} {e1[0]}");
			}
		}
		using(var dr = new StreamWriter("rMaxVaried.txt"))
		{
			for(int r=1; r<rmax; r++)
			{
				(vector e2, matrix f2) = Hydrogen.SE(r, drValue);
				dr.WriteLine($"{r} {e2[0]}");
			}
		}
	}//rvalues
	static void wavefuncs()
	{
	double rmax = 30;
	double dr = 0.1;
	/*analytical solutions*/	
	vector s1 = Hydrogen.swave(1, rmax, dr);
	vector s2 = Hydrogen.swave(2, rmax, dr);
	vector s3 = Hydrogen.swave(3, rmax, dr);

	/*numerical solution*/
	(vector v, matrix f) = Hydrogen.SE(rmax, dr);
	

	vector f1 = f[0]/Sqrt(dr);
	vector f2 = f[1]/Sqrt(dr);
	vector f3 = f[2]/Sqrt(dr);

	using(var waveSols = new StreamWriter("wavefuncs.txt"))
		{
			for(int i=0; i<f1.size; i++)
			{
				waveSols.WriteLine($"{(i+1)*dr} {s1[i]} {s2[i]} {s3[i]} {f1[i]} {f2[i]} {f3[i]}");//r_i=i*dr
			}
		}	

	}
	
}//main
