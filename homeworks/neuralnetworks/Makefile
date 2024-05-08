MATLIBDIR = ../../matlib
GENLIBDIR = ../../exercises/genericList
MINLIBDIR = ../minimisation
LINEQDIR = ../linearEq
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^)) # macros
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

#plots: plots.gpi out.txt main.cs 
#	gnuplot $<

out.txt: main.exe Makefile #to update makefile every time is edited
	mono $< > $@
main.exe: main.cs ann.dll PPNM.dll linalg.dll; $(MKEXE)

ann.dll: ann.cs linalg.dll PPNM.dll; $(MKLIB)

linalg.dll: $(MATLIBDIR)/matrix.cs $(MATLIBDIR)/vector.cs; $(MKLIB)

PPNM.dll: randmin.cs $(LINEQDIR)/QRGS.cs $(GENLIBDIR)/generic.cs $(MINLIBDIR)/minimisation.cs linalg.dll; $(MKLIB)

clean:
	$(RM) *.dll *.exe [Oo]ut* [Dd]ata* [Ll]og*
	#rm -rfv data Plots # to delete directories
