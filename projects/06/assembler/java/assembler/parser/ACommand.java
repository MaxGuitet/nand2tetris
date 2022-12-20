package assembler.parser;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

class ACommand extends Command {
    int value;
    String symbol;
    boolean isSymbolic = true;

    ACommand(String commandString) {
        super(commandString);
        type = CommandType.A_COMMAND;
    }

    void parseCommand() {
        this.symbol = commandString.replace("@", "");

        // if commandContent is only numbers, we simply extract its value
        Pattern numbers = Pattern.compile("^[0-9]+$");
        Matcher numbersMatcher = numbers.matcher(this.symbol);

        if (numbersMatcher.matches()) {
            value = Integer.getInteger(this.symbol);
            this.isSymbolic = false;
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

    int getValue() {
        return this.value;
    }
}
