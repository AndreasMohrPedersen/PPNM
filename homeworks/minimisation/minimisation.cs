using System;
using static System.Console;
using static System.Math;
public static class minimisation
{
	public static (vector, int) Newton(
		Func<vector,double> phi,/* objective function */
		vector x,				/* starting point */
		double acc=1e-3,		/* accuracy goal, on exit |∇φ| should be < acc */
		int maxSteps = 1000,		/*max number of iteer*/
		string method = "forward") /*default method is forward*/
	{
		int steps = 0;
		do{ /* Newton's iterations */
			vector DeltaPhi = new vector(x.size);
			matrix H = new matrix(x.size);

			if(method=="forward")
			{
				DeltaPhi = gradient(phi,x);
				if(DeltaPhi.norm()<acc || steps==maxSteps) break; // job done
				H = hessian(phi,x);
			}
			else if(method=="central")
			{
				DeltaPhi = centralGradient(phi,x);
				if(DeltaPhi.norm()<acc || steps==maxSteps) break;
				H = centralHessian(phi,x);
			}
			
		/* QR decomposition */
			(matrix Q, matrix R)=QRGS.decomp(H);
			var dx = QRGS.solve(Q,R,-DeltaPhi);
			double lambda=1,phix=phi(x);
			
			do{ /* linesearch */
				double lambdaMin = Pow(2,-10);
				//WriteLine($"lambda:{lambda} lambdaMin:{lambdaMin} phi(x+lambda*dx):{phi(x+lambda*dx)} phix:{phix}");
				if(phi(x+lambda*dx)<phix) break; /* good step: accept */
				if( lambda < lambdaMin ) break; /* accept anyway */
				lambda/=2;
			}while(true);
			x+=lambda*dx;
			steps++;
		}while(true);//steps <maxSteps);
		return (x, steps);
	}//newton

	public static vector gradient(Func<vector,double> phi,vector x)
	{
		vector DeltaPhi = new vector(x.size);
		double phix = phi(x); /* no need to recalculate at each step */
		for(int i=0;i<x.size;i++)
		{
			double dx=Abs(x[i])*Pow(2,-29);
			x[i]+=dx;
			DeltaPhi[i]=(phi(x)-phix)/dx;
			x[i]-=dx;
		}
		return DeltaPhi;
	}//gradient

	public static matrix hessian(Func<vector,double> phi,vector x){
		matrix H=new matrix(x.size);
		vector DeltaPhix=gradient(phi,x);
		for(int j=0;j<x.size;j++){
			double dx=Abs(x[j])*Pow(2,-13); /* for numerical gradient */
			x[j]+=dx;
			vector dDeltaPhi=gradient(phi,x)-DeltaPhix;
			for(int i=0;i<x.size;i++) H[i,j]=dDeltaPhi[i]/dx;
			x[j]-=dx;
		}
		//return H;
		return (H+H.T)/2;
	}

/*gradient and hessian matrix for central finite difference formula*/	
	public static vector centralGradient(Func<vector,double> phi,vector x)
	{
		vector xp = x.copy(); vector xm = x.copy();
		vector DeltaPhi = new vector(x.size);
		for(int i=0;i<x.size;i++)
		{
			double dx=Abs(x[i])*Pow(2,-30);
			xp[i]+=dx;
			xm[i]-=dx;

			DeltaPhi[i]=(phi(xp)-phi(xm))/(2*dx);

			xp[i]-=dx;
			xm[i]+=dx;
		}
		return DeltaPhi;
	}//centralGradient 

	public static matrix centralHessian(Func<vector,double> phi,vector x)
	{
		matrix H=new matrix(x.size); 
		vector xpp=x.copy(); vector xpm=x.copy(); 
		vector xmp=x.copy(); vector xmm=x.copy(); 

		for(int j=0;j<x.size;j++){
			for(int i=0;i<x.size;i++) 
			{
				double dxj=Abs(x[j])*Pow(2,-13); /* for numerical gradient */
				double dxi=Abs(x[i])*Pow(2,-13);
				
				xpp[j]+=dxj; xpm[j]+=dxj; xmp[j]-=dxj; xmm[j]-=dxj;
				xpp[i]+=dxi; xpm[i]-=dxi; xmp[i]+=dxi; xmm[i]-=dxi;
				
				H[i,j] = (phi(xpp)-phi(xpm)-phi(xmp)+phi(xmm))/(4*dxj*dxi);

				xpp[i]-=dxi; xpm[i]+=dxi; xmp[i]-=dxi; xmm[i]+=dxi;
				xpp[j]-=dxj; xpm[j]-=dxj; xmp[j]+=dxj; xmm[j]+=dxj;
			}
		}
		//return H;
		return (H+H.T)/2; 
	}//centralHessian

/*downhill simplex algorithm*/
static int iterations;
public static (vector,int) NelderMead(Func<vector,double> f, vector x, double acc=1e-3, double simplexSize=0.1, int maxIterations=10000)
	{
		iterations = 0;
		int n = x.size;
		matrix points = new matrix(n, n+1);
		points[0] = x;
		for(int i=0;i<n;i++)
		{
			vector newPoint = x.copy();
			newPoint[i] += simplexSize;
			points[i+1] = newPoint;
		}
		do
		{
			iterations++;
			if(iterations > maxIterations) throw new ArgumentException($"Max amount of iterations reached, {maxIterations}");
			vector high = points[0];
			vector low = points[0];
			vector centroid = points[0];
			int highIndex = 0, lowIndex = 0;
			for(int i=1;i<n+1;i++) 
			{
				if(f(high) < f(points[i])) {high = points[i];highIndex=i;} 
				if(f(low) > f(points[0])) {low = points[i];lowIndex=i;}
				centroid+=points[i];
			}
			centroid = (centroid-high)/n;
			double A = 0;
			for(int i=0;i<n+1;i++)
				for(int j=0;j<n+1;j++)
					if(j>i){vector diff=points[i]-points[j];A = Max(A,diff.norm());}
			if(A < acc) return (points[lowIndex], iterations);
			
			vector reflection = 2*centroid - high;
			if(f(reflection) < f(low))
			{
				vector expansion = 3*centroid - high;
				if(f(expansion) < f(reflection)) points[highIndex] = expansion;
				else points[highIndex] = reflection;
			}
			else if(f(reflection) < f(high)) points[highIndex] = reflection;
			else
			{
				vector contraction = 0.5*(centroid+high);
				if(f(contraction) < f(high)) points[highIndex] = contraction;
				else for(int i=0;i<n+1;i++) if(i != lowIndex) points[i] = 0.5*(points[i]+points[lowIndex]);
			}		
		}while(true);
	}//NelderMead
}//minimisation
