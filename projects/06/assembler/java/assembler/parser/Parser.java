package assembler.parser;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.regex.Pattern;

public class Parser {
    private BufferedReader reader;
    private Command currentCommand;
    private String nextLine;

    public Parser(String filePath) {
        try {
            FileReader fileReader = new FileReader(filePath);
            this.reader = new BufferedReader(fileReader);
        } catch (FileNotFoundException ex) {
            ex.printStackTrace();
        }

        this.initParser();
    }

    public Parser(BufferedReader inputBuffer) {
        this.reader = inputBuffer;
        this.initParser();
    }

    void initParser() {
        this.readNextLine();
    }

    void readNextLine() {
        try {
            boolean isEmptyLine = false;
            boolean isCommentLine = false;
            String line;

            do {

                line = this.reader.readLine();
                if (line == null) {
                    this.nextLine = null;
                    return;
                }
                // Remove spaces as per specifications
                line = line.replace(" ", "");

                // Keep reading if we have empty line or comments
                isCommentLine = Pattern.matches("\\/\\/.*", line);
                isEmptyLine = Pattern.matches("^$", line);
            } while (isCommentLine || isEmptyLine);

            this.nextLine = line;
        } catch (IOException ex) {
            System.out.println("Failed to read next file line.");
            ex.printStackTrace();
        }
    }

    public boolean hasMoreCommands() {
        return this.nextLine != null;
    }

    public void advance() {
        if (nextLine != null) {
            currentCommand = CommandFactory.getCommand(nextLine);
        } else {
            try {
                reader.close();
            } catch (IOException ex) {
                ex.printStackTrace();
            }
        }

        try {
            currentCommand.parseCommand();
        } catch (Exception ex) {
            System.out.println("Failed to parse command " + nextLine);
            ex.printStackTrace();
        }

        this.readNextLine();
    }

    public CommandType commandType() {
        return currentCommand.getType();
    }

    public String symbol() throws Exception {
        CommandType type = commandType();
        var command = this.currentCommand;

        if (type != CommandType.A_COMMAND && type != CommandType.L_COMMAND) {
            throw new Exception("C instructions do not have a symbol.");
        }

        if (type == CommandType.A_COMMAND) {
            ACommand aCommand = (ACommand) command;
            return aCommand.getSymbol();
        } else {
            LCommand lCommand = (LCommand) command;
            return lCommand.getSymbol();
        }
    }

    public String dest() throws Exception {
        CommandType type = commandType();

        if (type != CommandType.C_COMMAND) {
            throw new Exception("Only C instructions have a dest.");
        }

        CCommand cCommand = (CCommand) currentCommand;

        return cCommand.getDest();
    }

    public String comp() throws Exception {
        CommandType type = commandType();

        if (type != CommandType.C_COMMAND) {
            throw new Exception("Only C instructions have a comp.");
        }

        CCommand cCommand = (CCommand) currentCommand;

        return cCommand.getComp();
    }

    public String jump() throws Exception {
        CommandType type = commandType();

        if (type != CommandType.C_COMMAND) {
            throw new Exception("Only C instructions have a jump.");
        }

        CCommand cCommand = (CCommand) currentCommand;

        return cCommand.getJump();
    }
}