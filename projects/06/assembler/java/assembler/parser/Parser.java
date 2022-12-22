package assembler.parser;

import java.io.BufferedReader;
import java.io.IOException;
import java.util.regex.Pattern;

public class Parser {
    private BufferedReader reader;
    private Command currentCommand;
    private String nextLine;

    public Parser(BufferedReader inputBuffer) {
        this.reader = inputBuffer;
        this.initParser();
    }

    void initParser() {
        this.readNextCommand();
    }

    void readNextCommand() {
        try {
            boolean isEmptyLine = false;
            String line;

            do {

                line = this.reader.readLine();

                if (line == null) {
                    this.nextLine = null;
                    return;
                }
                // Remove spaces as per specifications
                line = line.replace(" ", "").replaceAll("\\/\\/.*$", "");

                // Keep reading if we have empty line
                // Comment lines will have been emptied above
                isEmptyLine = Pattern.matches("^$", line);
            } while (isEmptyLine);

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

        this.readNextCommand();
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

    public void reset() throws IOException {
        this.reader.reset();
        this.initParser();
    }
}