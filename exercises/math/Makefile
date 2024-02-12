Out.txt : math.exe
		mono math.exe > Out.txt

math.exe : math.cs sfuns.dll
		mcs -reference:sfuns.dll -Out:math.exe math.cs

sfuns.dll : sfuns.cs
	mcs -target:library -out:sfuns.dll sfuns.cs

clean:
	rm --force Out.txt math.exe
