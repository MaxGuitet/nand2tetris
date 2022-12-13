// Extract each line (delimited by the \n => 128)
// and convert it to the corresponding hack instruction

// Initialize to the first address of the program to parse
@16
D=A
@R0
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
  @A_INS
  D;JEQ
  // If we have a character other than @, it's a D-ins
  @R0
  A=M
  D=M
  @D_INS
  D;JGE
  // else, end script
  @END
  0;JMP

(END_A_INS)
  @R0
  M=M+1
  @READ
  0;JMP

// A-instruction
(A_INS)
  // Check if @ char
  @64
  D=A
  @R0
  A=M
  D=D-M
  @AT_CHAR
  D;JEQ
  // Check if \n
  @128
  D=A
  @R0
  A=M
  D=D-M
  @END_A_INS
  D;JEQ
  // else, increment and keep looping for now
  @R0
  M=M+1
  @A_INS
  0;JMP

  
// When reading @ char, simply ignore it and go to next
(AT_CHAR)
  @R0
  M=M+1
  @A_INS
  0;JMP

// C-instruction
// We will first read the full instruction to see if we have a "=" or a ";"
// R1 will store the assertion for "="
// R2 will store the assertion for ";"
// R3 will store the starting address of the instruction
// R4 will store the output value of the instruction
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
  // Add it twice to R4
  @R4
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

// We have a somehow limited number of character combination. 
// Compute the sum and figure out which it is
// A  => 65
// D  => 68
// M  => 77
// AD => 133
// AM => 142
// DM => 145
// ADM=> 210
(PARSE_DEST)
  // Check if = char (61)
  @61
  D=A
  @R0
  A=M
  D=D-M
  @END_PARSE_DEST
  D;JEQ
  // else get current char and add it to the sum in R4
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
  @R4
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
  @R4
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
  @R4
  M=M|D
  // Read next char
  @R0
  M=M+1
  @PARSE_DEST
  0;JMP
 

// We have finished parsing before =
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
// Similarly to DEST, compute the sum
(PARSE_COMP)
  // Check if ; char
  @59
  D=A
  @R0
  A=M
  D=D-M
  @END_PARSE_COMP
  D;JEQ
  // check if \n
  @128
  D=A
  @R0
  A=M
  D=D-M
  @END_PARSE_COMP
  D;JEQ

// We have finished parsing before =
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
  @READ
  0;JMP


(PARSE_JUMP)

(END)
  @END
  0;JMP