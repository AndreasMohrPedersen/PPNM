Out.txt : main.exe
		mono $< > $@

main.exe : main.cs cmath.dll
		mcs -target:exe -out:main.exe main.cs -reference:cmath.dll

cmath.dll : cmath.cs complex.cs
		mcs -target:library -out:cmath.dll cmath.cs complex.cs

clean:
	rm --force Out.txt main.exe
