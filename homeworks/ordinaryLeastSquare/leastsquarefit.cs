using System;

public static class leastsquares
{
	public static (vector,matrix) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy)
	{
		int n = x.size, m = fs.Length;
		
		matrix A = new matrix(n,m);
		vector b = new vector(n);

		for(int i=0;i<n;i++)
		{
			b[i] = y[i]/dy[i];
			
			for(int k=0;k<m;k++)
			{
				A[i,k] = fs[k](x[i])/dy[i];	
			}
		}
		/*least squares solution via QR-decomposition using the QRGS class*/
		(matrix Q, matrix R) = QRGS.decomp(A);
		vector c = QRGS.solve(Q, R, b);// solves linear system of equations via backsubstitution
		/*covariance in QR-scheme*/
		matrix Ainverse = QRGS.inverse(Q, R); //calcs inverse of A, from QRGS class
		matrix covariance = Ainverse*Ainverse.T;//eq (29) in the leastsquares note
		
		return (c,covariance);
	}//lsfit
}//leastsquaresfit
