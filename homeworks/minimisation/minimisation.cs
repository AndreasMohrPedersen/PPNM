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
				x.print("newton: x=");
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
				if(DeltaPhi.norm()<acc || steps==maxSteps)break;
				H = centralHessian(phi,x);
			}
			
		/* QR decomposition */
			(matrix Q, matrix R)=QRGS.decomp(H);
			var dx = QRGS.solve(Q,R,-DeltaPhi);
			double lambda=1,phix=phi(x);
			
			do{ /* linesearch */
				double lambdaMin = Pow(2,-13);
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
			double dx=Abs(x[i])*Pow(2,-26);
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
			double dx=Abs(x[i])*Pow(2,-26);
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
}//minimisation
