using System.Linq;
public static class main{

	public static void Main(string[] args){
		int N =(int)1e8;
		var sum = new System.Threading.ThreadLocal<double>( ()=>0, trackAllValues:true);
		System.Threading.Tasks.Parallel.For( 1, N+1, (int i)=>sum.Value+=1.0/i );
		double totalsum=sum.Values.Sum();
		System.Console.WriteLine($"Using {totalsum}");

	}//Main
}//main
