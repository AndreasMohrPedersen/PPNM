MATLIBDIR = ../matlib
GENLIBDIR = ../exercises/genericList
MINLIBDIR = ../homeworks/minimisation
LINEQDIR = ../homeworks/linearEq
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^)) # macros
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

plots: plots.gpi out.txt main.cs 
	gnuplot $<

out.txt: main.exe Makefile #to update makefile every time is edited
	mono $< > $@
main.exe: main.cs sgo.dll linalg.dll genlib.dll; $(MKEXE)

sgo.dll: globalOptimiser.cs min.dll linalg.dll genlib.dll; $(MKLIB)

linalg.dll: $(MATLIBDIR)/matrix.cs $(MATLIBDIR)/vector.cs; $(MKLIB)

min.dll: $(MINLIBDIR)/minimisation.cs $(LINEQDIR)/QRGS.cs linalg.dll; $(MKLIB)

genlib.dll: $(GENLIBDIR)/generic.cs; $(MKLIB)

clean:
	$(RM) *.dll *.exe [Oo]ut* [Dd]ata* [Ll]og* \
	rm -rfv data Plots # to delete directories
