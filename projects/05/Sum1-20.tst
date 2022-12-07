load Computer.hdl,
output-file Sum1-20.out,
compare-to Sum1-20.cmp,
output-list time%S1.4.1 reset%B2.1.2 ARegister[]%D1.7.1 DRegister[]%D1.7.1 PC[]%D0.4.0 RAM16K[15]%D1.7.1 RAM16K[16]%D1.7.1 RAM16K[17]%D1.7.1;

// Load a program written in the Hack machine language.
// The program computes the maximum of RAM[0] and RAM[1] 
// and writes the result in RAM[2].

ROM32K load Sum1-20.hack,

output;

repeat 300 {
    tick, tock, output;
}
