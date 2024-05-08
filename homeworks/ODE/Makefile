MATLIBDIR = ../../matlib
GENLISTDIR = ../../exercises/genericList
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

plots: plots.gpi out.txt main.cs ode.cs
	gnuplot $< #2>log

out.txt: main.exe Makefile
	mono $< > $@

main.exe: main.cs ode.dll write.dll linalg.dll genlist.dll; $(MKEXE)

ode.dll: ode.cs butchertables.dll linalg.dll genlist.dll; $(MKLIB)

butchertables.dll: butchertables.cs linalg.dll; $(MKLIB)

write.dll: write.cs linalg.dll genlist.dll; $(MKLIB)

linalg.dll: $(MATLIBDIR)/matrix.cs $(MATLIBDIR)/vector.cs; $(MKLIB)

genlist.dll: $(GENLISTDIR)/generic.cs; $(MKLIB)

clean:
	$(RM) main.exe out.txt *.dll
	rm -rfv data Plots

