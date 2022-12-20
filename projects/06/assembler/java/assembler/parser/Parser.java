package assembler.parser;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;

public class Parser {
    private BufferedReader reader;
    private Command currentCommand;
    private String nextLine;

    public Parser(String filePath) {
        try {

            FileReader fileReader = new FileReader(filePath);
            BufferedReader reader = new BufferedReader(fileReader);
            new Parser(reader);
        } catch (FileNotFoundException ex) {
            ex.printStackTrace();
        }

    }

    public Parser(BufferedReader inputBuffer) {
        this.reader = inputBuffer;
        try {
            this.nextLine = this.reader.readLine();
        } catch (IOException ex) {
            System.out.println("Empty input.");
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
            nextLine = reader.readLine();
        } catch (IOException ex) {
            ex.printStackTrace();
            try {
                reader.close();
            } catch (IOException closeEx) {
                closeEx.printStackTrace();
            }
        }
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