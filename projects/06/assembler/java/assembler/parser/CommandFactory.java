package assembler.parser;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

class CommandFactory {

    static Command getCommand(String commandString) {
        Pattern atPattern = Pattern.compile("^@");
        Matcher atMatch = atPattern.matcher(commandString);

        if (atMatch.matches()) {
            return new ACommand(commandString);
        }

        Pattern lPattern = Pattern.compile("^(");
        Matcher lMatch = lPattern.matcher(commandString);

        if (lMatch.matches()) {
            return new LCommand(commandString);
        }

        return new CCommand(commandString);
    }
}
