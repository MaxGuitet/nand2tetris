package assembler.parser;

class LCommand extends Command {
    String symbol;

    LCommand(String commandString) {
        super(commandString);
        type = CommandType.L_COMMAND;
    }

    void parseCommand() {
        this.symbol = commandString.replace("(", "").replace(")", "");
    }

    CommandType getType() {
        return type;
    }

    String getSymbol() {
        return this.symbol;
    }

    String getHackCode() {
        return "";
    }
}
