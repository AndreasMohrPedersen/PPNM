class main{
	public static int Main(){
		using(var output = new System.IO.StreamWriter("erf.txt")){
			for(double x=-3;x<=3;x+=1.0/64){
				output.WriteLine($"{x} {sfuns.erf(x)}");
				}
			}
		using(var output = new System.IO.StreamWriter("gamma.txt")){
			for(double x=-6;x<=6;x+=1.0/64){
				output.WriteLine($"{x} {sfuns.gamma(x)}");
				}
			}
		using(var output = new System.IO.StreamWriter("lngamma.txt")){
			for(double x=1;x<=10;x+=1.0/64){
				output.WriteLine($"{x} {sfuns.lngamma(x)}");
				}
			}
		return 0;
	}//main
}//Main
