using System;
using System.IO;
using static System.Console;
using System.Collections.Generic;

public static class write
{
	public static void WriteData(genlist<double> xdata, genlist<vector> ydata, string name, string outfile, Func<double,double> analytical=null)
	{
		Directory.CreateDirectory("data");
		using (StreamWriter output = new StreamWriter($"data/{outfile}", append: false))
		{
			output.WriteLine(name); // numerical-data header
			for(int i=0;i<xdata.size;i++) 
			{
				string outfile_content = $"{xdata[i]}";
				for(int j=0;j<ydata[0].size;j++) outfile_content += $" {ydata[i][j]}";
				output.WriteLine(outfile_content);
			}
			if(analytical != null)
			{
				output.Write("\n\n\"Analytical solution\"\n"); // analytical-data header
				for(int i=0;i<xdata.size;i++) output.WriteLine($"{xdata[i]} {analytical(xdata[i])}");	
			}
		}
	}//WriteData
}//write
