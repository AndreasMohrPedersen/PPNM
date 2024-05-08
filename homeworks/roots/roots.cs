using System;
using static System.Math;
public static class roots
{
	/*newton-raphson*/
	public static vector newton(
		Func<vector,vector> f, //function to find the root of
		vector x, //initial guess
		double acc=1e-3, //accuracy goal, ||f(x)|| should be less than acc upon exit
		vector dx = null) //optional deltaxvector for calcuation of jacobian
	{
		vector fx=f(x), z, fz;
		do
		{
			if(fx.norm()<acc)break; //job done
			matrix J = jacobian(f,x,fx,dx);
			//var QRofJ = givensQR(J); //perform givens rotation of J/alternatively QR-decomposition??
			(matrix Q, matrix R) = QRGS.decomp(J); //decomposition of J
			vector Dx = QRGS.solve(Q, R,-fx); //newtons step(eq 5 in the notes)
			double lambda =1;
			do
			{//linesearch
				double lambdaMin = 1/64;
				z = x+lambda*Dx;
				fz = f(z);
				if(fz.norm()<(1-lambda/2)*fx.norm() || lambda<lambdaMin) break;
				lambda /=2;
			}while(true);
			x=z; fx=fz;
		}while(true);
		return x;
	}//newton

	static matrix jacobian(Func<vector, vector> f, vector x, vector fx=null, vector dx=null)
	{
		if(dx==null)dx=x.map(xi=>Abs(xi)*Pow(2,-26));
		if(fx==null)fx=f(x);
		matrix J= new matrix(x.size);
		for(int j=0;j < x.size;j++)
		{
			x[j]+=dx[j]; //adds delta xj to j'th element
			vector df=f(x)-fx;
			for(int i=0;i < x.size;i++) J[i,j]=df[i]/dx[j];
			x[j]-=dx[j];//resets for next iteration
		}
		return J;
	}//jacobian
}//roots

