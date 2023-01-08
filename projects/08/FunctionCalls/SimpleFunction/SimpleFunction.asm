@317
D=A
@SP
M=D
@LCL
M=D
@Sys.init
0;JMP
// function SimpleFunction.test 2
(SimpleFunction.test)
// push local 0
@LCL
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
// push local 0
@LCL
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
// push local 0
@LCL
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
// push local 1
@LCL
D=M
@1
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
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
// not
@SP
M=M-1
A=M
D=!M
@SP
A=M
M=D
@SP
M=M+1
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
// push argument 1
@ARG
D=M
@1
D=D+A
A=D
D=M
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
