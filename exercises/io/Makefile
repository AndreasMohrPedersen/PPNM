Out.txt : main.exe main2.exe main3.exe Makefile
		mono main.exe -numbers:1,2,3,4,5 > Out.txt
		#echo 1 2 3 4 5 | mono main2.exe 2> Out2.txt
		echo "1\n2\n3\n4\n5" > input.txt
		mono main2.exe < input.txt 2> Out2.txt
		mono main3.exe -input:input.txt -output:Out3.txt

main.exe : main.cs
		mcs main.cs -target:exe -out:main.exe

main2.exe : main2.cs
		mcs main2.cs -target:exe -out:main2.exe

main3.exe : main3.cs
		mcs main3.cs -target:exe -out:main3.exe

clean:
	rm --force Out.txt main.exe main2.exe main3.exe *.txt
