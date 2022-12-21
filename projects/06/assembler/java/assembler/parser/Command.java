package assembler.parser;

abstract class Command {
    protected CommandType type;
    protected String commandString;

    Command(String commandString) {
        this.commandString = commandString;

        try {
            parseCommand();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    abstract void parseCommand() throws Exception;

    CommandType getType() {
        return type;
    }
}
