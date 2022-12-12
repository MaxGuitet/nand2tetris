// The aim of this version of the assembler is 
// to follow the steps of the first computer engineers
// and to write an assembler written itself in asm.
// The assembler code can then be manually translated to .hack
// and loaded in the CPU to actually convert any .asm file to a .hack file.

// First, let's write a program that allows the user to enter
// the .asm code using the keyboard

// Initialize position for first char

// We will use the predefined positions as follows:

// R0 will track when a key is pressed. It will be set to 1 until the key is released.
// The purpose is to prevent registering multiple times the same key press

// R1 will store the RAM address for the current char to write

@16
D=A
@R1
M=D

(KBDLOOP)
  @KBD
  D=M
  @KEY_DOWN
  D;JGT
  @KEY_UP
  0;JMP

(KEY_DOWN)
  @R0
  D=M
  @KBDLOOP
  D;JGT
  // Register character typed iat the current RAM address
  @KBD
  D=M
  @R1
  A=M
  M=D
  // Increment RAM address for next char
  @R1
  M=M+1
  // Toggle R0 to 1 to prevent multi typing
  @R0
  M=1
  // Go back to KBDLOOP and wait for KEY_UP event
  @KBDLOOP
  0;JMP

(KEY_UP)
  @R0
  M=0
  @KBDLOOP
  0;JMP