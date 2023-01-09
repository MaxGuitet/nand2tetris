@256
D=A
@SP
M=D
@LCL
M=D
// call Sys.init 0
// push return address for @Sys.init$ret0
@Sys.init$ret0
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
(Sys.init$ret0)
// function Class2.set 0
(Class2.set)

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
// pop static 0
@SP
M=M-1
@SP
A=M
D=M
@Class2.0
M=D
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
// pop static 1
@SP
M=M-1
@SP
A=M
D=M
@Class2.1
M=D
// push constant 0
@0
D=A
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
// function Class2.get 0
(Class2.get)

// push static 0
@Class2.0
D=M
@SP
A=M
M=D
@SP
M=M+1
// push static 1
@Class2.1
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
0;JMP
// function Class1.set 0
(Class1.set)

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
// pop static 0
@SP
M=M-1
@SP
A=M
D=M
@Class1.0
M=D
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
// pop static 1
@SP
M=M-1
@SP
A=M
D=M
@Class1.1
M=D
// push constant 0
@0
D=A
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
// function Class1.get 0
(Class1.get)

// push static 0
@Class1.0
D=M
@SP
A=M
M=D
@SP
M=M+1
// push static 1
@Class1.1
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
0;JMP
// function Sys.init 0
(Sys.init)

// push constant 6
@6
D=A
@SP
A=M
M=D
@SP
M=M+1
// push constant 8
@8
D=A
@SP
A=M
M=D
@SP
M=M+1
// call Class1.set 2
// push return address for @Class1.set$ret1
@Class1.set$ret1
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
D=D-1
@ARG
M=D
// Set new LCL value
@SP
D=M
@LCL
M=D
@Class1.set
0;JMP
(Class1.set$ret1)
// pop temp 0
@5
D=A
@0
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
A=M
M=D
// push constant 23
@23
D=A
@SP
A=M
M=D
@SP
M=M+1
// push constant 15
@15
D=A
@SP
A=M
M=D
@SP
M=M+1
// call Class2.set 2
// push return address for @Class2.set$ret2
@Class2.set$ret2
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
D=D-1
@ARG
M=D
// Set new LCL value
@SP
D=M
@LCL
M=D
@Class2.set
0;JMP
(Class2.set$ret2)
// pop temp 0
@5
D=A
@0
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
A=M
M=D
// call Class1.get 0
// push return address for @Class1.get$ret3
@Class1.get$ret3
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
@Class1.get
0;JMP
(Class1.get$ret3)
// call Class2.get 0
// push return address for @Class2.get$ret4
@Class2.get$ret4
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
@Class2.get
0;JMP
(Class2.get$ret4)
(Sys.init$WHILE)
@Sys.init$WHILE
0;JMP
