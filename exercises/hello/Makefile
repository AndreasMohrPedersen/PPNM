Out.txt : hello.exe
		mono hello.exe > Out.txt

hello.exe : hello.cs
		mcs -Out:hello.exe hello.cs

.PHONEY:clean
clean:
	rm --force Out.txt hello.exe
