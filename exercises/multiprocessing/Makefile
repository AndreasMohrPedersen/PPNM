CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)
TIME = time --portability --append --output $@

N = 2e9

Out.txt : main.exe main1.exe main2.exe Makefile
		>$@
		$(TIME) mono $< -nterms:$N -nthreads:1 >>$@
		$(TIME) mono $< -nterms:$N -nthreads:2 >>$@
		$(TIME) mono $< -nterms:$N -nthreads:3 >>$@
		$(TIME) mono $< -nterms:$N -nthreads:4 >>$@

		$(TIME) mono main1.exe >> $@ #part 2
		$(TIME) mono main2.exe >> $@ #part 3


main.exe : main.cs; $(MKEXE)
main1.exe : main1.cs; $(MKEXE)
main2.exe : main2.cs; $(MKEXE)


clean:	
	$(RM) main.exe main1.exe main2.exe [Oo]ut.txt
