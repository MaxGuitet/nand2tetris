package assembler;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.regex.Pattern;

import assembler.parser.Parser;
import assembler.symboltable.SymbolTable;
import assembler.code.Code;
import assembler.parser.CommandType;

public class Assembler {
    private String inputFilePath;
    private Parser parser;
    private SymbolTable symbolTable;
    private BufferedReader reader;
    private BufferedWriter writer;
    // addresses are 0-indexed, initialize at -1 to have the correct value when
    // reading currentLine+1
    private int currentLine = -1;

    public Assembler(String inputFilePath, String destinationFilePath) {
        this.inputFilePath = inputFilePath;
        this.symbolTable = new SymbolTable();

        try {
            this.reader = createBufferedReader();
            Path inputPath = Paths.get(inputFilePath);
            int size = (int) Files.size(inputPath);
            this.reader.mark(size + 1);

            FileWriter fileWriter = new FileWriter(destinationFilePath, false);
            this.writer = new BufferedWriter(fileWriter);

            this.parser = new Parser(reader);
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }

    BufferedReader createBufferedReader() throws FileNotFoundException {
        FileReader fileReader = new FileReader(this.inputFilePath);
        return new BufferedReader(fileReader);
    }

    public void parseFile() {
        this.extractSymbols();
        this.parseCommands();
        try {
            this.parser.close();
        } catch (IOException ex) {
            System.out.println("Failed to close buffer.");
        }
    }

    // Labels are used to point to a specific location in the code.
    // However they can be used by A-instruction before they are defined.
    // Thus we first extract them before parsing the rest of the code
    void extractSymbols() {
        while (parser.hasMoreCommands()) {
            parser.advance();

            CommandType commandType = parser.commandType();

            try {
                switch (commandType) {
                    case L_COMMAND:
                        this.extractLSymbols();
                        break;

                    default:
                        currentLine += 1;
                        continue;
                }
            } catch (Exception ex) {
                System.out.println("Failed to extract Label symbol.");
                ex.printStackTrace();
            }
        }

        try {
            // We feed the Parser with a new buffer to reinitialize reading at the begining
            // of the file.
            BufferedReader newReader = this.createBufferedReader();
            parser.setInputBuffer(newReader);
        } catch (IOException ex) {
            System.out.println("Failed to reset buffered reader");
            ex.printStackTrace();
        }
    }

    String extractLSymbols() throws Exception {
        String symbol = parser.symbol();
        // For labels, add symbol pointing to next line
        symbolTable.addEntry(symbol, currentLine + 1);

        return null;
    }

    public void parseCommands() {
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
                        // Labels have already been added previously, skip now
                        continue;

                    default:
                        System.out.println("Invalid command type.");
                }
            } catch (Exception ex) {
                System.out.println("Failed to parse " + commandType + " command.");
                ex.printStackTrace();
            }

            if (commandHackCode != null) {
                this.addHackLine(commandHackCode);
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
        int value;
        boolean isNumeric = Pattern.matches("\\d+", symbol);

        if (isNumeric) {
            value = Integer.parseInt(symbol);
        } else if (symbolTable.contains(symbol)) {
            value = symbolTable.getAddress(symbol);
        } else {
            try {
                value = symbolTable.autoAddEntry(symbol);
            } catch (Exception ex) {
                ex.printStackTrace();
                return "";
            }
        }

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

    void addHackLine(String lineContent) {
        try {
            this.writer.write(lineContent);
            this.writer.newLine();
        } catch (IOException ex) {
            System.out.println("Failed to parse code to destination file.");
            ex.printStackTrace();
        }
    }
}
