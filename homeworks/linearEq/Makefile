GENLIBDIR = ../../exercises/genericList
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

plots: plots.gpi Out.txt main.cs 
	gnuplot $<

Out.txt: main.exe Makefile
	mono $< > $@

main.exe: main.cs linalg.dll QRGS.dll min.dll genlib.dll;	$(MKEXE)

linalg.dll: ../../matlib/matrix.cs ../../matlib/vector.cs; $(MKLIB)

QRGS.dll: QRGS.cs linalg.dll; $(MKLIB)

min.dll: ../minimisation/minimisation.cs linalg.dll QRGS.dll; $(MKLIB)

genlib.dll: $(GENLIBDIR)/generic.cs; $(MKLIB)

clean:
	$(RM) main.exe [Oo]ut.txt linalg.dll QRGS.dll min.dll genlib.dll
	rm -rfv data Plots # to delete directories
