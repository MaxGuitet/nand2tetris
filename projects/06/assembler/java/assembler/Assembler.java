package assembler;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;

import assembler.parser.Parser;
import assembler.code.Code;
import assembler.parser.CommandType;

public class Assembler {
    private Parser parser;
    private BufferedWriter writer;

    public Assembler(String inputFilePath, String destinationFilePath) {
        try {
            FileReader fileReader = new FileReader(inputFilePath);
            BufferedReader reader = new BufferedReader(fileReader);

            FileWriter fileWriter = new FileWriter(destinationFilePath, false);
            this.writer = new BufferedWriter(fileWriter);

            this.parser = new Parser(reader);
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }

    public void parseFile() {
        while (parser.hasMoreCommands()) {
            parser.advance();

            CommandType commandType = parser.commandType();
            String commandHackCode = "";

            try {
                switch (commandType) {
                    case A_COMMAND:
                        commandHackCode = this.parseACommand();
                        break;

                    case C_COMMAND:
                        commandHackCode = this.parseCCommand();
                        break;

                    case L_COMMAND:
                        commandHackCode = this.parseLCommand();
                        break;

                    default:
                        System.out.println("Invalid command type.");
                }
            } catch (Exception ex) {
                System.out.println("Failed to parse " + commandType + " command.");
                ex.printStackTrace();
            }

            try {
                this.writer.write(commandHackCode);
                this.writer.newLine();
            } catch (IOException ex) {
                System.out.println("Failed to parse code to destination file.");
                ex.printStackTrace();
            }
        }

        try {
            this.writer.close();
        } catch (IOException ex) {
            ex.printStackTrace();
        }

    }

    String parseACommand() throws Exception {
        String symbol = parser.symbol();
        int value = Integer.parseInt(symbol);
        String rawCode = Integer.toBinaryString(value);

        return String.format("%16s", rawCode).replace(" ", "0");
    }

    String parseCCommand() throws Exception {
        String comp = parser.comp();
        String compHack = Code.comp(comp);

        String dest = parser.dest();
        String destHack = Code.dest(dest);

        String jump = parser.jump();
        String jumpHack = Code.jump(jump);
        return "111" + compHack + destHack + jumpHack;
    }

    String parseLCommand() {
        return "";
    }
}
