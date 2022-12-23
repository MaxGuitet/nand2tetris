package assembler.parser;

import java.util.regex.Pattern;

class CommandFactory {
    static Command getCommand(String commandString) {
        boolean isACommand = Pattern.matches("@.*", commandString);

        if (isACommand) {
            return new ACommand(commandString);
        }

        boolean isLCommand = Pattern.matches("^\\(.*", commandString);

        if (isLCommand) {
            return new LCommand(commandString);
        }

        return new CCommand(commandString);
    }
}
