using System;
using static System.Math;
using static System.Console;
public class main{
	public static int Main(string[] args){
	string infile=null,outfile=null; 
		foreach(var arg in args){
			var words=arg.Split(':'); //sets variable words equal to arg :
			if(words[0]=="-input")infile=words[1];// if the previous arg is equal "-indput" infile is set equal to the subsequent word
			if(words[0]=="-output")outfile=words[1]; //similar
			}
		if( infile==null || outfile==null) { // || is conditional statement OR
			Error.WriteLine("wrong filename argument"); // if consition fulfilled print error message
			return 1;
			}
	var instream =new System.IO.StreamReader(infile); //reads infile
	var outstream=new System.IO.StreamWriter(outfile,append:true);//creates a stream connected directly with outfile
	for(string line=instream.ReadLine();line!=null;line=instream.ReadLine()){
		double x=double.Parse(line);
		outstream.WriteLine($"{x} {Sin(x)} {Cos(x)}");
        }
	instream.Close();
	outstream.Close();
	return 0;
	}//Main
}//main
