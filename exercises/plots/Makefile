CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

Erf.svg : Out.txt Gamma.svg lngamma.svg Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key left ;\
		set xlabel "x" ;\
		set ylabel "erf(x)" ;\
		set tics out ;\
		set grid ;\
		set title "Error function" ;\
		plot [-3:3]\
			 "erf.txt" with lines title "Err func plot"\
			,"data/erf.data.txt" using 1:2 with points pointtype 6 title "Tabulated values"\
			,"data/erf.data.txt" using (-($$1)):(-($$2)) with points pointtype 6 notitle ;\
	' | tee log.erf.gpi  | gnuplot

Gamma.svg : Out.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key left ;\
		set xlabel "x" ;\
		set ylabel "gamma(x)" ;\
		set tics out ;\
		set grid ;\
		set title "Gamma function" ;\
		plot [-6:6][-6:6]\
			 "gamma.txt" with lines title "Gamma func plot"\
			,"data/gamma.data.txt" using ($$1+1):($$2) with points pointtype 6 title "Tabulated values";\
	' | tee log.gamma.gpi  | gnuplot



lngamma.svg : Out.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key left ;\
		set xlabel "x" ;\
		set ylabel "lngamma(x)" ;\
		set tics out ;\
		set grid ;\
		set title "Ln(gamma) function" ;\
		plot [0:10][-2:10]\
			 "lngamma.txt" with lines title "lngamma func plot"\
			,"data/gamma.data.txt" using ($$1+1):($$3) with points pointtype 6 title "Tabulated values";\
	' | tee log.lngamma.gpi  | gnuplot


Out.txt: main.exe #Out.txt empty
	mono $< > $@

sfuns.dll : sfuns.cs ; $(MKLIB)
main.exe : main.cs sfuns.dll; $(MKEXE)

clean:	
	$(RM) main.exe sfuns.dll [Oo]ut.txt [Ll]og.erf.gpi [Ll]og.gamma.gpi [Ll]og.lngamma.gpi Erf.svg erf.txt Gamma.svg gamma.txt lngamma.svg lngamma.txt
