using System;
using static System.Console;
using static System.Math;
public class main{
	public static void Main(string[] args){ 
		char[] split_delimiters = {' ','\t','\n'}; // defines delimiters used to split string into substrings
		var split_options = StringSplitOptions.RemoveEmptyEntries; //remove empty element from list
		for( string line = ReadLine(); line != null; line = ReadLine() ){
			var numbers = line.Split(split_delimiters,split_options); //reads off line numbers based on delimiters set above
			foreach(var number in numbers){
				double x = double.Parse(number);
				Error.WriteLine($"{x} {Sin(x)} {Cos(x)}");//write numbers in string to std error stream
                }
        }
	}//Main
}//main
