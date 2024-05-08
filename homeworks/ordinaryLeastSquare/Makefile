MATLIBDIR = ../../matlib
QRDIR = ../linearEq

CODE = $(filter %.cs, $^)
LIBS = $(addprefix -reference:,$(filter %.dll, $^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

all: out.txt plots

plots : plots.gpi main.cs
	gnuplot $<

out.txt : main.exe Makefile
	mono $< > $@

main.exe : main.cs linalg.dll QRGS.dll leastsquarefit.dll; $(MKEXE)

linalg.dll : $(MATLIBDIR)/matrix.cs $(MATLIBDIR)/vector.cs; $(MKLIB)

QRGS.dll : $(QRDIR)/QRGS.cs linalg.dll; $(MKLIB)

leastsquarefit.dll : leastsquarefit.cs QRGS.dll linalg.dll; $(MKLIB)

clean:
	$(RM) main.exe [Oo]ut.txt linalg.dll QRGS.dll leastsquarefit.dll lsfit.gpi lsfit.svg lsfit.txt rutherford.txt
