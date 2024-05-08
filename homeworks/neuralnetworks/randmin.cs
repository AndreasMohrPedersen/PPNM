public static class randmin{
	public static vector go(System.Func<vector,double> φ, vector x, vector dx, int nsamples=10000){
		var rnd=new System.Random();
		vector s=x.copy();
		vector b=x.copy();
		double φb=φ(x);
		for(int n=0;n<nsamples;n++){
			for(int i=0;i<x.size;i++)s[i]=x[i]+2*(rnd.NextDouble()-0.5)*dx[i];
			double φs=φ(s);
			if(φs<φb){
				b=s.copy();
				φb=φs;
			}
		}
		return b;
	}
}
