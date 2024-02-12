Out.txt : machineEpsilon.exe
		mono machineEpsilon.exe > Out.txt

machineEpsilon.exe : machineEpsilon.cs
		mcs -Out:machineEpsilon.exe machineEpsilon.cs

clean:
	rm --force Out.txt machineEpsilon.exe
