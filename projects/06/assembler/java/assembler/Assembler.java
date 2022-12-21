package assembler;

import java.util.ArrayList;
import java.util.List;

import assembler.parser.Parser;
import assembler.code.Code;
import assembler.parser.CommandType;

public class Assembler {
    private Parser parser;
    private List<String> outputContent = new ArrayList<String>();

    public Assembler(String filepath) {
        this.parser = new Parser(filepath);
    }

    public List<String> run() {
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

            outputContent.add(commandHackCode);
        }

        return outputContent;

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
