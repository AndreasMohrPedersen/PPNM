using static System.Math;

public static class jacobi
{	
	public static void timesJ(matrix A, int p, int q, double theta)
	{
		double c = Cos(theta);
		double s = Sin(theta);
		for(int i=0; i<A.size1; i++)
		{
			double aip = A[i,p];
			double aiq = A[i,q];
			A[i,p] = c*aip-s*aiq;
			A[i,q] = s*aip+c*aiq;
		}
	}//timesJ

	public static void Jtimes(matrix A, int p, int q, double theta)
	{
		double c = Cos(theta);
		double s = Sin(theta);
		for(int j=0; j<A.size1; j++)
		{
			double apj = A[p,j];
			double aqj = A[q,j];
			A[p,j] =  c*apj+s*aqj;
			A[q,j] = -s*apj+c*aqj;
		}
	}//Jtimes
	
	public static (vector, matrix) cyclic(matrix A, double acc = 1e-9)
	{
		matrix D = A.copy();// copy of A, such that we do not change it
		matrix V = matrix.id(D.size1); //matrix with 1 in the diagonal elements and non-initialized off-diagonal elements
		vector w = new vector(D.size1);
		
		bool changed;
		do
		{
			changed = false;
			for(int p=0;p<D.size1-1; p++)
			{
				for(int q=p+1; q<D.size1; q++)
				{
					double apq = D[p,q];
					double app = D[p,p];
					double aqq = D[q,q];
					double theta = 0.5*Atan2(2*apq, aqq-app);
					double c = Cos(theta), s = Sin(theta);
					double new_app = c*c*app-2*s*c*apq+s*s*aqq;
					double new_aqq = s*s*app+2*s*c*apq+c*c*aqq;
					if(Abs(new_app - app) > acc || Abs(new_aqq - aqq) > acc)
					{
						changed = true;
						timesJ(D,p,q,theta);
						Jtimes(D,p,q,-theta);
						timesJ(V,p,q,theta);
					}
				}
			}
		}while(changed); //runs while changed == true
		for(int i=0; i<D.size1; i++)
		{
			w[i] = D[i,i]; 
		}

	return (w,V);
	}//cyclic
		
}//EVD
