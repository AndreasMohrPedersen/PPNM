MATLIBDIR = ../../matlib
CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

all :  out.txt dr.svg rMax.svg wavefuncs.svg

dr.svg : Makefile
	echo ' \
		set term svg;\
		set out "$@";\
		set title "E_0 convergence with dr";\
		set key bottom right;\
		set xlabel "dr";\
		set ylabel "E_{0}";\
		plot "drVaried.txt" with points pt 7 title "fixed r_{max} = 25 ";\
	' | tee drVaried.gpi | gnuplot

rMax.svg : Makefile
	echo ' \
		set term svg;\
		set out "$@";\
		set title "E_0 convergence with rmax";\
		set xlabel "rmax";\
		set ylabel "E_{0}";\
		plot "rMaxVaried.txt" with points pt 7 title "fixed dr = 0.2 ";\
	' | tee rMaxVaried.gpi | gnuplot

wavefuncs.svg : wavefuncs.txt Makefile
	echo ' \
		set term svg;\
		set out "$@";\
		set title "";\
		set xlabel "r";\
		set ylabel "f(r)";\
		plot [0:30][-1:1]\
			 "$<" using 1:5 with points pointtype 7 pointsize 0.5 title "Nummerical n = 1"\
			,"$<" using 1:6 with points pointtype 7 pointsize 0.5 title "Nummerical n = 2"\
			,"$<" using 1:7 with points pointtype 7 pointsize 0.5 title "Nummerical n = 3"\
			,"$<" using 1:2 with lines title "Analytical n = 1"\
			,"$<" using 1:3 with lines title "Analytical n = 2"\
			,"$<" using 1:4 with lines title "Analytical n = 3"\
	' | tee wavefuncs.gpi | gnuplot


out.txt: main.exe Makefile
	#mono $< > $@
	mono main.exe -rmax:25 -dr:0.2 >> $@

main.exe: main.cs matrix.dll jacobi.dll Hydrogen.dll; $(MKEXE)

matrix.dll: $(MATLIBDIR)/matrix.cs $(MATLIBDIR)/vector.cs; $(MKLIB)

jacobi.dll: jacobi.cs matrix.dll; $(MKLIB)

Hydrogen.dll: Hydrogen.cs matrix.dll jacobi.dll; $(MKLIB)

clean:
	$(RM) main.exe out.txt jacobi.dll matrix.dll Hydrogen.dll drVaried.txt rMaxVaried.txt wavefuncs.txt drVaried.gpi rMaxVaried.gpi wavefuncs.gpi dr.svg rMax.svg wavefuncs.svg
