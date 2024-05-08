using System;
using static System.Console;
public class main{
	public static void Main(){
		var list = new genlist<double[]>(); // make variable list as a generic list containing double 
		char[] delimiters = {' ', '\t'}; //specifies delimeter
		var options = StringSplitOptions.RemoveEmptyEntries; //to removes empty entries in list
		for(string line = ReadLine(); line!=null; line = ReadLine()){ //read the next line until line = null
			var words = line.Split(delimiters,options);
			int n = words.Length;
			var numbers = new double[n];//new list of length n
			for(int i=0; i<n; i++) numbers[i] = double.Parse(words[i]);
			list.add(numbers);// loop sets i'th element in empty array numbers = i'th element in words which is read of through readLine 
			}
		for(int i=0; i<list.size;i++){
			var numbers = list[i];
			foreach(var number in numbers) Write($"{number : 0.00e+00;-0.00e+00}");
			WriteLine();//print
			}
	}//Main
}//main
