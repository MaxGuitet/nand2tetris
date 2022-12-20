package assembler;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.Reader;
import java.io.StringReader;
import java.nio.Buffer;
import assembler.parser.Parser;

public class Assembler {
    public static void main(String[] args) {
        String code = "D=M";
        StringReader stringReader = new StringReader(code);
        BufferedReader bReader = new BufferedReader(stringReader);

        Parser parser = new Parser(bReader);

        while (parser.hasMoreCommands()) {
            System.out.println(parser.commandType());
            parser.advance();
        }

    }
}
