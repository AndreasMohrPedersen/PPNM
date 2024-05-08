MATLIBDIR = ../../matlib
GENLIBDIR = ../../exercises/genericList
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

plots: plots.gpi Out.txt main.cs 
	gnuplot $<

Out.txt : main.exe Makefile
		mono $< > $@

main.exe : main.cs linalg.dll genlib.dll splines.dll; $(MKEXE)

linalg.dll : $(MATLIBDIR)/matrix.cs $(MATLIBDIR)/vector.cs; $(MKLIB)

genlib.dll : $(GENLIBDIR)/generic.cs; $(MKLIB)

splines.dll : splines.cs qsplines.cs csplines.cs linalg.dll genlib.dll;$(MKLIB)

clean :
	rm main.exe Out.txt linalg.dll splines.dll genlist.dll
