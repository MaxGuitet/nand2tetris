// Extract each line (delimited by the \n => 128)
// and convert it to the corresponding hack instruction

// R0 will store the address of the character we are reading
// R10 will store the destination address for the next instruction, starting at 8184 (=(16384-16)/2)
// Initialize to the first address of the program to parse
@16
D=A
@R0
M=D
@8184
D=A
@R10
M=D


(READ)
  // @ char code is 64. Store it and compare to determine
  // if we have a A-instruction
  @64
  D=A
  @R0
  A=M
  // If the character at position M[R0] is an @, D=0, jump to A-instruction
  D=D-M
  @INIT_A_INS
  D;JEQ
  // If character is \n (128), continue reading
  @128
  D=A
  @R0
  A=M
  D=D-M
  @READ_NEXT_CHAR
  D;JEQ
  // If we have a character , it's a D-ins
  @R0
  A=M
  D=M
  @D_INS
  D;JGT
  // else, end script
  @END
  0;JMP

(READ_NEXT_CHAR)
  @R0
  M=M+1
  @READ
  0;JMP

// A-instruction
// R1 will be used as pointer to the current char (starting with the left-most one)
// R2 will store the number of times we need to multiply by 10
// R3 will store the value of the number multiplied by 10^n to add to A-ins output
// R4 will be used as the loop index for multiplication, initialized at R2
// R5 will store the final value of the C-ins
(INIT_A_INS)
// Skip @ char
  @R0
  M=M+1
  D=M
  // Set pointer to the starting char 
  @R1
  M=D
  @R2
  M=0
  @R5
  M=0
  @A_INS
  0;JMP

(A_INS)
  // Check if \n
  @128
  D=A
  @R0
  A=M
  D=D-M
  @PARSE_A_INS
  D;JEQ
  // Store address of current number in R1
  @R0
  D=M
  @R1
  M=D
  // Move to next char
  @R0
  M=M+1
  @A_INS
  0;JMP

(PARSE_A_INS)
  // Based on the length of the instruction, multiply each
  // character by 10 the corresponding number of times
  // When we finish A_INS, the pointer is at the unit value.
  // then we go back to tenth, hunderdth, etc.
  // The number of times to multiply by 10 is in R3, initialized at 0

  // Read back until we reach @ char
  @64
  D=A
  @R1
  A=M
  // If the character at position M[R1] is an @, finish A-INS
  D=D-M
  @END_A_INS
  D;JEQ

  // Store value of current number to R3
  @R1
  A=M
  D=M
  // There is a 48 offset in ASCII between number and it's charCode
  // ie 0 => 48, 1 => 49, etc.
  // thus we remove 48 to the char value
  @48
  D=D-A
  @R3
  M=D

  // If R2 > 0, we need to mutiply by 10, otherwise go to next char
  @R2
  D=M
  // Store number of times we need to multiply by 10
  @R4
  M=D
  @MULT_10
  D;JGT

  @ADD_TO_A_RESULT
  0;JMP

(MULT_10)
  // Get the value of the current number and multiply by 10 000
  @R3
  D=M
  D=D+M
  D=D+M
  D=D+M
  D=D+M
  D=D+M
  D=D+M
  D=D+M
  D=D+M
  D=D+M // D=M[R1] * 10
  @R3
  M=D
  @R4
  M=M-1
  D=M
  // If R4 still > 0, keep multiplying
  @MULT_10
  D;JGT
  // Otherwise, Add to A-result
  @ADD_TO_A_RESULT
  0;JMP

(ADD_TO_A_RESULT)
  // Get final value of current number * 10^n and add to R5
  @R3
  D=M
  @R5
  M=M+D
  // Move back to next number (eg 12[3] => 1[2]3)
  @R1
  M=M-1
  // Since we move up, we need to multiply by 10 one more time
  @R2
  M=M+1
  @PARSE_A_INS
  0;JMP

(END_A_INS)
  // R5 holds the final value for the A-INS
  @R5
  D=M
  // Store value to M[R10]
  @R10
  A=M
  M=D
  // Update pointer to next instruction
  @R10
  M=M+1
  @READ
  0;JMP

// When reading @ char, simply ignore it and go to next
// (AT_CHAR)
//   @R0
//   M=M+1
//   @A_INS
//   0;JMP

// C-instruction
// We will first read the full instruction to see if we have a "=" or a ";"
// R1 will store the assertion for "="
// R2 will store the assertion for ";"
// R3 will store the starting address of the instruction
// R4 will store the sum for COMP
// R5 will store the output value of the instruction
(D_INS)
  @R0
  D=M
  @R3
  M=D
  @R1
  M=0
  @R2
  M=0
  @CHECK_D_INS
  0;JMP

(CHECK_D_INS)
  // Check if = char
  @61
  D=A
  @R0
  A=M
  D=D-M
  @EQ_CHAR
  D;JEQ
  // Check if ; char
  @59
  D=A
  @R0
  A=M
  D=D-M
  @SEMI_CHAR
  D;JEQ
  // if \n, it's the end of the instruction
  @128
  D=A
  @R0
  A=M
  D=D-M
  @INIT_PARSE_D_INS
  D;JEQ
  // else keep reading it
  @R0
  M=M+1
  @CHECK_D_INS
  0;JMP

(EQ_CHAR)
  @R1
  M=1
  @R0
  M=M+1
  @CHECK_D_INS
  0;JMP

(SEMI_CHAR)
  @R2
  M=1
  @R0
  M=M+1
  @CHECK_D_INS
  0;JMP

(INIT_PARSE_D_INS)
  // Set output to 1110000000000000 [2] = 57344 [10]
  // however 57344 is off-limit so instead we use 28672 = 57344/2 and add it twice
  @28672
  D=A
  // Add it twice to R5
  @R5
  M=D
  M=M+D
  // Reinitialize R0 to the begining of the instruction
  @R3
  D=M
  M=0
  @R0
  M=D
  @PARSE_D_INS
  0;JMP

(PARSE_D_INS)
  // If we have a = sign, parse instruction before it
  @R1
  D=M-1
  @PARSE_DEST
  D;JEQ
  // Whether or not we had DEST, we **will** have a COMP
  @PARSE_COMP
  0;JMP

// We can have any combination of A,D and M.
// Each is defined by a single bit in the DEST part
// Loop and add all those which are present
(PARSE_DEST)
  // Check if "=" char (61)
  @61
  D=A
  @R0
  A=M
  D=D-M
  @END_PARSE_DEST
  D;JEQ
  // else get current char and add it to the sum in R5
  // check if A (65)
  @65
  D=A
  @R0
  A=M
  D=D-M
  @ADD_DEST_A
  D;JEQ
  // check if D (68)
  @68
  D=A
  @R0
  A=M
  D=D-M
  @ADD_DEST_D
  D;JEQ
  // check if M (77)
  @77
  D=A
  @R0
  A=M
  D=D-M
  @ADD_DEST_M
  D;JEQ

(ADD_DEST_A)
  // Dest A => 100000 [2] = 32 [10]
  @32
  D=A
  // Compute the bit or with A Dest
  @R5
  M=M|D
  // Read next char
  @R0
  M=M+1
  @PARSE_DEST
  0;JMP

(ADD_DEST_D)
  // Dest D => 010000 [2] = 16 [10]
  @16
  D=A
  // Compute the bit or with D Dest
  @R5
  M=M|D
  // Read next char
  @R0
  M=M+1
  @PARSE_DEST
  0;JMP

(ADD_DEST_M)
  // Dest M => 001000 [2] = 8 [10]
  @8
  D=A
  // Compute the bit or with M Dest
  @R5
  M=M|D
  // Read next char
  @R0
  M=M+1
  @PARSE_DEST
  0;JMP
 

// We have finished parsing before "="
// continue to see what is after
(END_PARSE_DEST)
  @R0
  M=M+1
  // Set R1 to 0 to indicate we have finished with the =
  @R1
  M=0
  @PARSE_D_INS
  0;JMP

// Parse comp until we see either a ; or a \n
// To know which computation to do, we get all characters
// and sum their char codes with the following formula:
// sum = 1 * char1 + 2 * char2 + 3 * char3
// We do this to avoid redundancy in the sum
(PARSE_COMP)
  // Reset R4
  @R4
  M=0
  @PARSE_COMP_1
  0;JMP

// The 1st char is necessarily not ; or \n
(PARSE_COMP_1)
  @R0
  A=M
  D=M
  // Add it to R4
  @R4
  M=M+D
  // Go to next char
  @R0
  M=M+1
  // Not needed since it's below, but helps clarifying
  @PARSE_COMP_2
  0;JMP

(PARSE_COMP_2)
  // Check if ; char
  @59
  D=A
  @R0
  A=M
  D=D-M
  @EXTRACT_COMP
  D;JEQ
  // check if \n => if so go to next instruction
  @128
  D=A
  @R0
  A=M
  D=D-M
  @EXTRACT_COMP
  D;JEQ
  // Else take the value and add it twice
  @R0
  A=M
  D=M
  // Add it to R4
  @R4
  M=M+D
  M=M+D
  // Go to next char
  @R0
  M=M+1
  // Not needed since it's below, but helps clarifying
  @PARSE_COMP_3
  0;JMP

(PARSE_COMP_3)
  // Check if ; char
  @59
  D=A
  @R0
  A=M
  D=D-M
  @EXTRACT_COMP
  D;JEQ
  // check if \n => if so go to next instruction
  @128
  D=A
  @R0
  A=M
  D=D-M
  @EXTRACT_COMP
  D;JEQ
  // Else take the value and add it thrice
  @R0
  A=M
  D=M
  // Add it to R4
  @R4
  M=M+D
  M=M+D
  M=M+D
  // We have now finished, extract value and go to next char
  @R0
  M=M+1
  // Not needed since it's below, but helps clarifying
  @EXTRACT_COMP
  0;JMP

// Probably not the most efficient thing.
// We're reading brute-force the sum and mapping it to the correct output
(EXTRACT_COMP)
  // 0 => 48
  @R4
  D=M
  @48
  D=D-A
  @COMP_0
  D;JEQ
  // 1 => 49
  @R4
  D=M
  @49
  D=D-A
  @COMP_1
  D;JEQ
  // -1 => 143
  @R4
  D=M
  @143
  D=D-A
  @COMP_MIN_1
  D;JEQ
  // D => 68
  @R4
  D=M
  @68
  D=D-A
  @COMP_D
  D;JEQ
  // A => 65
  @R4
  D=M
  @65
  D=D-A
  @COMP_A
  D;JEQ
  // M => 77
  @R4
  D=M
  @77
  D=D-A
  @COMP_M
  D;JEQ
  // !D => 169
  @R4
  D=M
  @169
  D=D-A
  @COMP_NOT_D
  D;JEQ
  // !A => 163
  @R4
  D=M
  @163
  D=D-A
  @COMP_NOT_A
  D;JEQ
  // !M => 187
  @R4
  D=M
  @187
  D=D-A
  @COMP_NOT_M
  D;JEQ
  // -D => 181
  @R4
  D=M
  @181
  D=D-A
  @COMP_NEG_D
  D;JEQ
  // -A => 175
  @R4
  D=M
  @175
  D=D-A
  @COMP_NEG_A
  D;JEQ
  // -M => 199
  @R4
  D=M
  @199
  D=D-A
  @COMP_NEG_M
  D;JEQ
  // D+1 => 301
  @R4
  D=M
  @301
  D=D-A
  @COMP_D_PLUS_1
  D;JEQ
  // A+1 => 298
  @R4
  D=M
  @298
  D=D-A
  @COMP_A_PLUS_1
  D;JEQ
  // M+1 => 310
  @R4
  D=M
  @310
  D=D-A
  @COMP_M_PLUS_1
  D;JEQ
  // D-1 => 305
  @R4
  D=M
  @305
  D=D-A
  @COMP_D_MINUS_1
  D;JEQ
  // A-1 => 302
  @R4
  D=M
  @302
  D=D-A
  @COMP_A_MINUS_1
  D;JEQ
  // M-1 => 314
  @R4
  D=M
  @314
  D=D-A
  @COMP_M_MINUS_1
  D;JEQ
  // D+A => 349
  @R4
  D=M
  @349
  D=D-A
  @COMP_D_PLUS_A
  D;JEQ
  // D+M => 385
  @R4
  D=M
  @385
  D=D-A
  @COMP_D_PLUS_M
  D;JEQ
  // D-A => 353
  @R4
  D=M
  @353
  D=D-A
  @COMP_D_MINUS_A
  D;JEQ
  // D-M => 389
  @R4
  D=M
  @389
  D=D-A
  @COMP_D_MINUS_M
  D;JEQ
  // A-D => 359
  @R4
  D=M
  @359
  D=D-A
  @COMP_A_MINUS_D
  D;JEQ
  // M-D => 371
  @R4
  D=M
  @371
  D=D-A
  @COMP_M_MINUS_D
  D;JEQ
  // D&A => 339
  @R4
  D=M
  @339
  D=D-A
  @COMP_D_AND_A
  D;JEQ
  // D&M => 375
  @R4
  D=M
  @375
  D=D-A
  @COMP_D_AND_M
  D;JEQ
  // D|A => 511
  @R4
  D=M
  @511
  D=D-A
  @COMP_D_OR_A
  D;JEQ
  // D|M => 547
  @R4
  D=M
  @547
  D=D-A
  @COMP_D_OR_M
  D;JEQ
  // If nothing matched, we have an invalid code, go to end
  @END
  0;JMP


(COMP_0)
  // Code needs to be (0)101010(000000) [2] => 2688 [10]
  @2688
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_1)
  // Code needs to be (0)111111(000000) [2] => 4032 [10]
  @4032
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_MIN_1)
  // Code needs to be (0)111010(000000) [2] => 3712 [10]
  @3712
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D)
  // Code needs to be (0)001100(000000) [2] => 768 [10]
  @768
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_A)
  // Code needs to be (0)110000(000000) [2] => 3072 [10]
  @3072
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_M)
  // Code needs to be (1)110000(000000) [2] => 7168 [10]
  @7168
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_NOT_D)
  // Code needs to be (0)001101(000000) [2] => 832 [10]
  @832
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_NOT_A)
  // Code needs to be (0)110001(000000) [2] => 3136 [10]
  @3136
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_NOT_M)
  // Code needs to be (1)110001(000000) [2] => 7232 [10]
  @7232
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_NEG_D)
  // Code needs to be (0)001111(000000) [2] => 960 [10]
  @960
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_NEG_A)
  // Code needs to be (0)110011(000000) [2] => 3264 [10]
  @3264
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_NEG_M)
  // Code needs to be (1)110011(000000) [2] => 7360 [10]
  @7360
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_PLUS_1)
  // Code needs to be (0)011111(000000) [2] => 1984 [10]
  @1984
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_A_PLUS_1)
  // Code needs to be (0)110111(000000) [2] => 3520 [10]
  @3520
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_M_PLUS_1)
  // Code needs to be (1)110111(000000) [2] => 7616 [10]
  @7616
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_MINUS_1)
  // Code needs to be (0)001110(000000) [2] => 896 [10]
  @896
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_A_MINUS_1)
  // Code needs to be (0)110010(000000) [2] => 3200 [10]
  @3200
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_M_MINUS_1)
  // Code needs to be (1)110010(000000) [2] => 7296 [10]
  @7296
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_PLUS_A)
  // Code needs to be (0)000010(000000) [2] => 128 [10]
  @128
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_PLUS_M)
  // Code needs to be (1)000010(000000) [2] => 4224 [10]
  @4224
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_MINUS_A)
  // Code needs to be (0)010011(000000) [2] => 1216 [10]
  @1216
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_MINUS_M)
  // Code needs to be (1)010011(000000) [2] => 5312 [10]
  @5312
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_A_MINUS_D)
  // Code needs to be (0)000111(000000) [2] => 448 [10]
  @448
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_M_MINUS_D)
  // Code needs to be (1)000111(000000) [2] => 4544 [10]
  @4544
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_AND_A)
  // Code needs to be (0)000000(000000) [2] => 0 [10]
  D=0
  @ADD_COMP_BITS
  0;JMP

(COMP_D_AND_M)
  // Code needs to be (1)000000(000000) [2] => 4096 [10]
  @4096
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_OR_A)
  // Code needs to be (0)010101(000000) [2] => 1344 [10]
  @1344
  D=A
  @ADD_COMP_BITS
  0;JMP

(COMP_D_OR_M)
  // Code needs to be (1)010101(000000) [2] => 5440 [10]
  @5440
  D=A
  @ADD_COMP_BITS
  0;JMP

// The value we want to add is in D
// Compute the logical OR
(ADD_COMP_BITS)
  @R5
  M=M|D
  @END_PARSE_COMP
  0;JMP

// We have finished parsing COMP
// continue to see what is after
(END_PARSE_COMP)
  @R0
  M=M+1
  //Check if we have a ";". If so, parse JUMP 
  @R2
  D=M-1
  @PARSE_JUMP
  D;JEQ
  // If we do not have a ";", we are done, move to next instruction
  @END_D_INS
  0;JMP


(PARSE_JUMP)
  // Ignore the first char as it is "J"
  @R0
  M=M+1
  // Read next char
  // If second is "N" (78) => JNE
  @R0
  A=M
  D=M
  @78
  D=D-A
  @PARSE_JNE
  D;JEQ
  // If second is "M" (77) => JMP
  @R0
  A=M
  D=M
  @77
  D=D-A
  @PARSE_JMP
  D;JEQ
  // If second is "E" (69) => JEQ
  @R0
  A=M
  D=M
  @69
  D=D-A
  @PARSE_JEQ
  D;JEQ
  // If second is "G" (71)
  @R0
  A=M
  D=M
  @71
  D=D-A
  @PARSE_JG
  D;JEQ
  // If second is "L" (76)
  @R0
  A=M
  D=M
  @76
  D=D-A
  @PARSE_JL
  D;JEQ

(PARSE_JNE)
  // JNE => 101 [2] = 5 [10]
  @5
  D=A
  @R5
  M=D|M
  @R0
  M=M+1
  M=M+1
  @END_JUMP
  0;JMP

(PARSE_JMP)
  // JMP => 111 [2] = 7 [10]
  @7
  D=A
  @R5
  M=D|M
  @R0
  M=M+1
  M=M+1
  @END_JUMP
  0;JMP

(PARSE_JEQ)
  // JMP => 010 [2] = 2 [10]
  @2
  D=A
  @R5
  M=D|M
  @R0
  M=M+1
  M=M+1
  @END_JUMP
  0;JMP

(PARSE_JG)
  // Read next char
  @R0
  M=M+1
  A=M
  D=M
  // If third is "E" (69) => JGE
  @69
  D=D-A
  @PARSE_JGE
  D;JEQ
  // Else it's JGT
  @PARSE_JGT
  0;JMP

(PARSE_JGE)
  // JGE => 011 [2] = 3 [10]
  @3
  D=A
  @R5
  M=D|M
  @R0
  M=M+1
  @END_JUMP
  0;JMP

(PARSE_JGT)
  // JGT => 001 [2] = 1 [10]
  @1
  D=A
  @R5
  M=D|M
  @R0
  M=M+1
  @END_JUMP
  0;JMP

(PARSE_JL)
  // Read next char
  @R0
  M=M+1
  A=M
  D=M
  // If third is "E" (69) => JGE
  @69
  D=D-A
  @PARSE_JLE
  D;JEQ
  // Else it's JLT
  @PARSE_JLT
  0;JMP

(PARSE_JLE)
  // JLE => 110 [2] = 6 [10]
  @6
  D=A
  @R5
  M=D|M
  @R0
  M=M+1
  @END_JUMP
  0;JMP

(PARSE_JLT)
  // JLT => 100 [2] = 4 [10]
  @4
  D=A
  @R5
  M=D|M
  @R0
  M=M+1
  @END_JUMP
  0;JMP

(END_JUMP)
  @R0
  M=M+1
  @END_D_INS
  0;JMP

(END_D_INS)
  // Get the full computed C-instruction and add it to output
  @R5
  D=M
  @R10
  A=M
  M=D
  // Update pointer to next instruction
  @R10
  M=M+1
  @READ
  0;JMP

(END)
  @END
  0;JMP