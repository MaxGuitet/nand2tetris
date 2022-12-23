package assembler.parser;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

class CCommand extends Command {
    int value;
    String dest;
    String comp;
    String jump;

    CCommand(String commandString) {
        super(commandString);
        type = CommandType.C_COMMAND;
    }

    void parseCommand() throws Exception {
        // if commandContent is only numbers, we simply extract its value
        Pattern groups = Pattern
                .compile("^((?<dest>[AMD]+)=)?((?<comp>[!ADM01|&+\\-]+);?)(?<jump>JGT|JGE|JEQ|JLT|JLE|JNE|JMP)?");
        Matcher groupsMatcher = groups.matcher(this.commandString);

        if (!groupsMatcher.matches()) {
            throw new Exception("Invalid C-instruction");
        }

        this.dest = groupsMatcher.group("dest");
        this.comp = groupsMatcher.group("comp");
        this.jump = groupsMatcher.group("jump");
    }

    CommandType getType() {
        return type;
    }

    String getComp() {
        return this.comp;
    }

    String getDest() {
        if (this.dest == null) {
            return "NONE";
        }
        return this.dest;
    }

    String getJump() {
        if (this.jump == null) {
            return "NONE";
        }

        return this.jump;
    }
}
