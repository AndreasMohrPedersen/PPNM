class main{

//dataclass
public class data { public int a,b; public double sum;}

//function for calculating harmonic series
public static void harm(object obj){
	data arg = (data)obj;
	arg.sum=0;
	for(int i=arg.a;i<arg.b;i++)arg.sum+=1.0/i;//calcs sum from a and b 
	}

	public static int Main(string[] args){
	//default values
	int nterms = (int)1e8; 
	int nthreads = 1;//runs one treads if nothing else specified i Makefile

	//reads from command line
	foreach(string arg in args) {
		var words = arg.Split(':');
		if(words[0]=="-nthreads") nthreads =(int)double.Parse(words[1]);
		if(words[0]=="-nterms"  ) nterms =(int)double.Parse(words[1]);
		}


	//Prepare data in intervals to be used locally in separate threads
	data[] intervals = new data[nthreads];//creates and array containing elements of type data?
	for(int i=0;i<nthreads;i++) {
		intervals[i] = new data();
		intervals[i].a = 1 + nterms/nthreads*i;
		intervals[i].b = 1 + nterms/nthreads*(i+1);//sets values in data()
	   	}

	//prepare the seperate threads to run 
	intervals[nthreads-1].b=1+nterms;
	var threads = new System.Threading.Thread[nthreads];//creates variable threads of length nthreads
	for(int i=0;i<nthreads;i++) {
		threads[i] = new System.Threading.Thread(harm); //calls function harm for the i'th threads 
		threads[i].Start(intervals[i]); // starts the i'th threads by specifying the paramters for function harm
		}
	//join the threads back together to the total sum
	foreach(var thread in threads) thread.Join();

	double total=0; 
	foreach(var interval in intervals) {total+=interval.sum;
	}

	System.Console.WriteLine($"Harmonic sum with {nthreads} threads: {total}");
	return 0;
	}//Main
}//main
