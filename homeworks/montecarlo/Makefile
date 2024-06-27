MATLIBDIR = ../../matlib
GENLIBDIR = ../../exercises/genericList
MINLIBDIR = ../minimisation
LINEQLIBDIR = ../linearEq
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^)) # macros
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

plots: plots.gpi out.txt main.cs 
	gnuplot $<

out.txt: main.exe Makefile #to update makefile every time is edited
	mono $< > $@

main.exe: main.cs genlib.dll linalg.dll montecarlo.dll min.dll; $(MKEXE)

montecarlo.dll: montecarlo.cs linalg.dll genlib.dll; $(MKLIB)

linalg.dll: $(MATLIBDIR)/matrix.cs $(MATLIBDIR)/vector.cs $(LINEQLIBDIR)/QRGS.cs; $(MKLIB)

genlib.dll: $(GENLIBDIR)/generic.cs; $(MKLIB)

min.dll: $(MINLIBDIR)/minimisation.cs linalg.dll; $(MKLIB)

clean:
	$(RM) *.dll main.exe [Oo]ut.txt\
	rm -rfv data Plots # to delete directories
