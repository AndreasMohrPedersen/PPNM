Out.txt : main.exe
		mono main.exe > Out.txt

main.exe : main.cs vec.dll
	mcs main.cs -target:exe -out:main.exe -reference:vec.dll

vec.dll : vec.cs
	mcs -target:library -out:vec.dll vec.cs

clean:
	rm --force Out.txt main.exe
