Out.txt : main.exe
		mono main.exe < input.txt > Out.txt

main.exe : main.cs generic.dll
		mcs -out:main.exe main.cs -reference:generic.dll

generic.dll : generic.cs
		mcs -target:library -out:generic.dll generic.cs 

clean:
	rm --force Out.txt generic.exe main.exe
