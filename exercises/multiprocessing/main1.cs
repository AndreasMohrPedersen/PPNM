public static class main {

	public static void Main(string[] args){
		double sum=0;
		int nterms = (int)1e8;
		System.Threading.Tasks.Parallel.For( 1, nterms+1, (int i) => sum+=1.0/i );
		System.Console.WriteLine($"result with Parallel.For: {sum}");
	}//Main
}//main
