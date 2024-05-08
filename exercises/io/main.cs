using System;
using static System.Console;
using static System.Math;
public class main{
	public static void Main(string[] args){ //list of strings, with element split by blankspace
		foreach(var arg in args){ 
			var words = arg.Split(':');//split strings into substrings based on delimiter :
			if(words[0]=="-numbers"){
				var numbers=words[1].Split(','); //defines delimiter for substring numbers
				foreach(var number in numbers){ 
					double x = double.Parse(number); //for each number in substring numbers string representation of number is converted into 32 bit integer signed equivalent
					WriteLine($"{x} {Sin(x)} {Cos(x)}"); //prints result
				}
			}
		}
	}//Main
}//main
