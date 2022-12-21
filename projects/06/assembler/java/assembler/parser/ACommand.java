package assembler.parser;

class ACommand extends Command {
    int value;
    String symbol;
    boolean isSymbolic = false;

    ACommand(String commandString) {
        super(commandString);
        type = CommandType.A_COMMAND;
    }

    void parseCommand() {
        this.symbol = commandString.replace("@", "");

        // if commandContent is only numbers, we simply extract its value
        try {
            this.value = Integer.parseInt(this.symbol);
            this.isSymbolic = false;
        } catch (Exception ex) {
            this.isSymbolic = true;
        }
    }

    CommandType getType() {
        return type;
    }

    boolean getIsSymbolic() {
        return this.isSymbolic;
    }

    String getSymbol() {
        return this.symbol;
    }

    String getHackCode() {
        String rawCode = Integer.toBinaryString(this.value);
        return String.format("%16s", rawCode).replace(" ", "0");
    }
}
