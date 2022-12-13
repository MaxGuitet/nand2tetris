// Those are the char codes for the Add.asm script
// To simulate the fact that the user typed it, load it manually in RAM
// from address 16
// 64,
// 50,
// 128,
// 68,
// 61,
// 65,
// 128,
// 64,
// 51,
// 128,
// 68,
// 61,
// 68,
// 43,
// 65,
// 128,
// 64,
// 48,
// 128,
// 77,
// 61,
// 68

// Loading @
@64
D=A
@16
M=D
// Loading 2
@50
D=A
@17
M=D
// Loading \n
@128
D=A
@18
M=D
// Loading D
@68
D=A
@19
M=D
// Loading =
@61
D=A
@20
M=D
// Loading A
@65
D=A
@21
M=D
// Loading \n
@128
D=A
@22
M=D
// Loading @
@64
D=A
@23
M=D
// Loading 3
@51
D=A
@24
M=D
// Loading \n
@128
D=A
@25
M=D
// Loading D
@68
D=A
@26
M=D
// Loading =
@61
D=A
@27
M=D
// Loading D
@68
D=A
@28
M=D
// Loading +
@43
D=A
@29
M=D
// Loading A
@65
D=A
@30
M=D
// Loading \n
@128
D=A
@31
M=D
// Loading @
@64
D=A
@32
M=D
// Loading 0
@48
D=A
@33
M=D
// Loading \n
@128
D=A
@34
M=D
// Loading M
@77
D=A
@35
M=D
// Loading =
@61
D=A
@36
M=D
// Loading D
@68
D=A
@37
M=D
// Setting R1 to the correct position for next char
@38
D=M
@R1
M=D

(END)
  @END
  0;JMP