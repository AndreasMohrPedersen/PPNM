using static System.Math;

public static class Hydrogen
{
	public static (double, double) readCMD(string[] args)
	{
		//default values 
		double rmax = 20;
		double dr = 0.2;
		//read cmdline
		foreach(string arg in args)
		{
			var words = arg.Split(':');
			if (words[0]=="-rmax") rmax = double.Parse(words[1]);
			if (words[0]=="-dr") dr = double.Parse(words[1]);
		}
		System.Console.WriteLine($"rmax: {rmax}, dr: {dr}");
	return (rmax, dr);	
	}//readCMD


	public static vector swave(int k, double rmax, double dr)//int k for k'th swave
	{
		int npoints = (int)(rmax/dr)-1;

		double a = 0.529*1e-10; //bohr radius i meters
		vector f = new vector(npoints); //initalises vector for s-wave reduced radial eigenfuncs f^k(r)
		
		if(k==1)
		{
			for(int i=0; i<npoints; i++)
			{
				f[i] = a*dr*(i+1)*2*Pow(a,-3/2)*Exp(-dr*(i+1));
			}
			return f;
		}

		if(k == 2)
			{
			for(int i=0;i<npoints;i++)
			{
			f[i]= a*dr*(i+1)*Pow(a,-3/2)*(1 - dr*(i+1)/2)*Exp(-dr*(i+1)/2)/Sqrt(2);
			}
		return f;
		}

		if(k == 3){
			for(int i=0;i<npoints;i++)
			{
				f[i]= a*dr*(i+1)*2*Pow(a,-3/2)*(1 - 2*dr*(i+1)/3 + 2*Pow(dr*(i+1),2)/27)*Exp(-dr*(i+1)/3)/Sqrt(3)/3;
			}
		return f;
		}
		else //if k!= 1,2,3 is passed
		{
			for(int i=0;i<npoints;i++)
				{
					f[i]= 0;
				}
		return f;
		}		
	}//swave

	public static (vector, matrix) SE(double rmax, double dr)//shrodinger eq.
	{
		//System.Console.Error.WriteLine($"SE: rmax={rmax} dr={dr}");
		int npoints = (int) (rmax/dr)-1;// values read from commandline
		//System.Console.Error.WriteLine($"SE: npoints={npoints}");
		vector r = new vector(npoints);
		for(int i=0; i<npoints; i++)//calcs r_i 
		{
			r[i] = dr*(i+1);//defines r[i] as the i+1'th step
		}
		matrix K = new matrix(npoints, npoints); //defines kinetic part of hamiltonian
		matrix W = new matrix(npoints, npoints); //defines potential energi
		for(int i=0; i<npoints-1; i++) //leaves out the last element in such that we kan fill in off-diagonals
		{
			K[i,i] = -1/(2*dr*dr)*(-2);//diagonal elements
			K[i,i+1] = -1/(2*dr*dr);//upper off diagonal
			K[i+1,i] = -1/(2*dr*dr);
		}
		K[npoints-1, npoints-1] = -2*(-1/(2*dr*dr));

		for(int i=0; i<npoints; i++)
		{
			W[i, i] = -1/r[i];
		}
		matrix H = K+W; //hamiltonian
		return jacobi.cyclic(H);	
	}//SE
}//He 
