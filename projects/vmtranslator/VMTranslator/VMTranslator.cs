using System;

public class VMTranslator
{
    private Parser parser;
    public VMTranslator()
    {
        parser = new Parser("/home/max/Projects/nand2tetris/projects/07/StackArithmetic/SimpleAdd/SimpleAdd.vm");
    }
}