MATLIBDIR = ../../matlib
GENLIBDIR = ../../exercises/genericList
LINEQDIR = ../linearEq
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^)) # macros
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

plots: plots.gpi out.txt main.cs
	gnuplot $<

out.txt: main.exe Makefile #to update makefile every time is edited
	mono $< > $@
main.exe: main.cs min.dll QRGS.dll genlib.dll linalg.dll; $(MKEXE)

linalg.dll: $(MATLIBDIR)/matrix.cs $(MATLIBDIR)/vector.cs; $(MKLIB)

genlib.dll: $(GENLIBDIR)/generic.cs; $(MKLIB)

QRGS.dll: $(LINEQDIR)/QRGS.cs linalg.dll; $(MKLIB)

min.dll: minimisation.cs linalg.dll QRGS.dll; $(MKLIB)

clean:
	$(RM) *.dll *.exe [Oo]ut* [Dd]ata* [Ll]og*\
	rm -rfv data Plots # to delete directories
