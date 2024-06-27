MATLIBDIR = ../../matlib
GENLIBDIR = ../../exercises/genericList/generics.cs
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

plots: plots.gpi out.txt main.cs
	gnuplot $<

out.txt: main.exe python.txt Makefile #to update makefile every time is edited
	python3 integration.py > python.txt
	mono $< > $@

python.txt: integration.py

main.exe: main.cs integration.dll; $(MKEXE)

integration.dll: integration.cs; $(MKLIB)

clean:
	$(RM) integration.dll main.exe [Oo]ut.txt python.txt pythonCounts.txt\
	rm -rfv data Plots # to delete directories
