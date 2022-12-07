// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input.
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel;
// the screen should remain fully black as long as the key is pressed. 
// When no key is pressed, the program clears the screen, i.e. writes
// "white" in every pixel;
// the screen should remain fully clear as long as no key is pressed.

(KBDLOOP)
  @KBD
  D=M
  @KEY_DOWN
  D;JGT
  @KEY_UP
  0;JMP

(KEY_DOWN)
  @value
  D=M-1 // If previous value was 1 => D = 0, else D = -1
  M=1 // Register new value
  @FILLSCREENBLACK
  D;JLT // If D=-1, then we now have a key pressed. Fill screen black
  @KBDLOOP
  0;JMP

(FILLSCREENBLACK)
@8191
D=A
@i
M=D
(LOOPBLACK)
  @SCREEN
  D=A
  @i
  D=D+M // Get address for next pixel to update
  A=D   // Set A to point to next pixel
  M=-1
  @i
  M=M-1
  D=M
  @KBDLOOP
  D;JLT
  @LOOPBLACK
  0;JMP

(KEY_UP)
  @value
  D=M-1 // If previous value was 1 => D = 0, else D = -1
  M=0 // Register new value
  @FILLSCREENWHITE
  D;JEQ // If D=0, then we had a key pressed before, but not anymore. Reset screen
  @KBDLOOP
  0;JMP

(FILLSCREENWHITE)
@8191
D=A
@i
M=D
(LOOPWHITE)
  @SCREEN
  D=A
  @i
  D=D+M // Get address for next pixel to update
  A=D   // Set A to point to next pixel
  M=0
  @i
  M=M-1
  D=M
  @KBDLOOP
  D;JLT
  @LOOPWHITE
  0;JMPUntitled-1