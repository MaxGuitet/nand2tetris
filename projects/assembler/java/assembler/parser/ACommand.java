package assembler.parser;

class ACommand extends Command {
    int value;
    String symbol;

    ACommand(String commandString) {
        super(commandString);
        type = CommandType.A_COMMAND;
    }

    void parseCommand() {
        this.symbol = commandString.replace("@", "");
    }

    CommandType getType() {
        return this.type;
    }

    String getSymbol() {
        return this.symbol;
    }
}
