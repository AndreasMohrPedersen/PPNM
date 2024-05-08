using System;
using static System.Math;
using static System.Console;
using System.Diagnostics;
using System.IO;


public static class main{
	public static void Main(){
		partAB();
		partC();
		}
	public static void partAB(){
		int n1 = 8;
		int m1 = 5;
		string divider = new string('-',m1*11);
		WriteLine(divider);
		matrix A1 = randomMatrix(n1,m1);
		A1.print("A1 matrix");
		WriteLine(divider);

/*Test of decomp*/
		//decomposing A
		(matrix Q, matrix R) = QRGS.decomp(A1); 
		Q.print("Q matrix");
		WriteLine(divider);
		R.print("R matrix");
		WriteLine(divider);
		//Q.T*Q=I??
		matrix I = Q.T*Q;
		I.print("Q.T*Q:");
		WriteLine(divider);
		//Q*R=A??
		matrix QR = Q*R;
		QR.print("Q*R:");
		WriteLine(divider);
		matrix QRminusA1 = QR-A1;
		QRminusA1.print("QR-A1:");
		WriteLine(divider);

/*Test of solve*/
		int n2 = 5;
		int m2 = 5;
		matrix A2 = randomMatrix(n2,m2);
		vector b2 = randomVector(n2);
		A2.print("A2 matrix:");
		WriteLine(divider);
		b2.print("b2 vector:");
		WriteLine(divider);
		(matrix Q2, matrix R2) = QRGS.decomp(A2);
		vector x2 = QRGS.solve(Q2, R2, b2);
		x2.print("QRx=b:" );
		WriteLine(divider);
		vector Ax = A2*x2;
		Ax.print("Ax=b");
		WriteLine(divider);
/*Test of inverse*/
		matrix B = QRGS.inverse(Q2, R2);
		B.print("inverse of A:");
		WriteLine(divider);
		matrix A2B = A2*B;
		A2B.print("AB");
		WriteLine(divider);

		}//tests

	public static matrix randomMatrix(int n, int m){
		var rnd = new Random();
		matrix rndMatrix = new matrix(n,m);//new random matrix of dimensions n,m  
		for(int i=0; i<n; i++){ 
			for(int j=0; j<m; j++){ 
				rndMatrix[i,j] = rnd.Next(100);
				}}
		return rndMatrix;
	}//randomMatrix	

	public static vector randomVector(int n){ //random vector of dimension n
		var rnd = new Random();
		vector rndVector = new vector(n);
		for(int i =0; i<n; i++){
			rndVector[i] = rnd.Next(100);
			
			}
		return rndVector;		
	}//randomVector
/*partC*/
	public static void partC()
	{
		Directory.CreateDirectory("data");
		using(var timestamps = new StreamWriter("data/times.txt")) // writes directly to times.txt with data
		{
			for(int N=100; N<601; N+=50)
			{
				
				var starttime = Stopwatch.GetTimestamp();//takes a timestamp before generating matrix
				Random rnd = new Random();
				matrix rndMatrix = new matrix(N,N);
				for(int i=0; i<N; i++)
				{
					for(int j=0; j<N; j++)
					{
						rndMatrix[i,j] = rnd.Next(100);
					}
				}
				(matrix Q, matrix R) = QRGS.decomp(rndMatrix); 
				var elapsedtime = Stopwatch.GetTimestamp();// timestamp after matrix decomposition
				TimeSpan duration = new TimeSpan(elapsedtime-starttime);
				double seconds = duration.TotalSeconds;// converts from ticks to seconds
				timestamps.WriteLine($"{N} {seconds}");
			}
		}

		var data = new StreamReader("data/times.txt");
		genlist<int> Ns = new genlist<int>();
		genlist<double> ts = new genlist<double>();
		for(string line=data.ReadLine(); line!=null; line=data.ReadLine()) 
		{
			var words = line.Split(' ');
			Ns.add(int.Parse(words[0]));
			ts.add(double.Parse(words[1]));
		}
		
		Func<vector,double> deviation = u =>
		{
			double sum=0;
			for(int i=0; i<Ns.size;i++) sum+=Pow(fit(Ns[i],u)-ts[i],2);
			return sum;
		};
		
/*find optimal parameters*/
		vector init = new vector(1e-9);
		vector bestparams = minimisation.Newton(deviation,init, method: "central").Item1;
		
		int iterations=100;
		using(var output = new StreamWriter("data/fit.txt"))
		{
			for(int i=0;i<iterations;i++)
			{
				double N = Ns[0] + ((double) Ns[Ns.size-1]-Ns[0])/(iterations - 1) * i;
				double t = fit(N, bestparams);
				output.WriteLine($"{N} {t}");
			}
		}
	}
	static double fit(double x, vector fitparams)
	{
		return fitparams[0]*x*x*x;
	}//partC
}//main
