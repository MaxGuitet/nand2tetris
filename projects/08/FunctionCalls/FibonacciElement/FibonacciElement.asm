@256
D=A
@SP
M=D
@LCL
M=D
// call Sys.init 0
// push return address for @Sys$ret0
@Sys$ret0
D=A
@SP
A=M
M=D
@SP
M=M+1
// push LCL
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// push ARG
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// push THIS
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// push THAT
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// Set ARG position
@SP
D=M
@5
D=D-A
@ARG
M=D
// Set new LCL value
@SP
D=M
@LCL
M=D
@Sys.init
0;JMP
(Sys$ret0)
// function Main.fibonacci 0
(Main.fibonacci)

// push argument 0
@ARG
D=M
@0
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// push constant 2
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
// lt
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
D=M-D
@TRUE0
D;JLT
@FALSE0
0;JMP
(TRUE0)
  D=-1
  @END0
  0;JMP
(FALSE0)
  D=0
(END0)
  @SP
  A=M
  M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@Main$IF_TRUE
D;JNE
@Main$IF_FALSE
0;JMP
(Main$IF_TRUE)
// push argument 0
@ARG
D=M
@0
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// return
// set endFrame to LCL
@LCL
D=M
@R13
M=D
// restore return
@R13
D=M
@5
A=D-A
D=M
@R14
M=D
// restore ARG
@SP
A=M-1
D=M
@ARG
A=M
M=D
// reposition SP
@ARG
D=M+1
@SP
M=D
// restore that
@R13
D=M
@1
A=D-A
D=M
@THAT
M=D
// restore this
@R13
D=M
@2
A=D-A
D=M
@THIS
M=D
// restore arg
@R13
D=M
@3
A=D-A
D=M
@ARG
M=D
// restore local
@R13
D=M
@4
A=D-A
D=M
@LCL
M=D
// go to return address
@R14
A=M
0;JMP
(Main$IF_FALSE)
// push argument 0
@ARG
D=M
@0
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// push constant 2
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
D=M-D
@SP
A=M
M=D
@SP
M=M+1
// call Main.fibonacci 1
// push return address for @Main$ret1
@Main$ret1
D=A
@SP
A=M
M=D
@SP
M=M+1
// push LCL
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// push ARG
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// push THIS
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// push THAT
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// Set ARG position
@SP
D=M
@5
D=D-A
D=D-1
@ARG
M=D
// Set new LCL value
@SP
D=M
@LCL
M=D
@Main.fibonacci
0;JMP
(Main$ret1)
// push argument 0
@ARG
D=M
@0
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// push constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
D=M-D
@SP
A=M
M=D
@SP
M=M+1
// call Main.fibonacci 1
// push return address for @Main$ret2
@Main$ret2
D=A
@SP
A=M
M=D
@SP
M=M+1
// push LCL
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// push ARG
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// push THIS
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// push THAT
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// Set ARG position
@SP
D=M
@5
D=D-A
D=D-1
@ARG
M=D
// Set new LCL value
@SP
D=M
@LCL
M=D
@Main.fibonacci
0;JMP
(Main$ret2)
// add
@SP
M=M-1
A=M
D=M
@SP
M=M-1
A=M
D=M+D
@SP
A=M
M=D
@SP
M=M+1
// return
// set endFrame to LCL
@LCL
D=M
@R13
M=D
// restore return
@R13
D=M
@5
A=D-A
D=M
@R14
M=D
// restore ARG
@SP
A=M-1
D=M
@ARG
A=M
M=D
// reposition SP
@ARG
D=M+1
@SP
M=D
// restore that
@R13
D=M
@1
A=D-A
D=M
@THAT
M=D
// restore this
@R13
D=M
@2
A=D-A
D=M
@THIS
M=D
// restore arg
@R13
D=M
@3
A=D-A
D=M
@ARG
M=D
// restore local
@R13
D=M
@4
A=D-A
D=M
@LCL
M=D
// go to return address
@R14
A=M
0;JMP
// function Sys.init 0
(Sys.init)

// push constant 4
@4
D=A
@SP
A=M
M=D
@SP
M=M+1
// call Main.fibonacci 1
// push return address for @Sys$ret3
@Sys$ret3
D=A
@SP
A=M
M=D
@SP
M=M+1
// push LCL
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// push ARG
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// push THIS
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// push THAT
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// Set ARG position
@SP
D=M
@5
D=D-A
D=D-1
@ARG
M=D
// Set new LCL value
@SP
D=M
@LCL
M=D
@Main.fibonacci
0;JMP
(Sys$ret3)
(Sys$WHILE)
@Sys$WHILE
0;JMP
