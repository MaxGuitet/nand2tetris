package assembler.code;

public enum JumpCode {
    NONE("000"),
    JGT("001"),
    JEQ("010"),
    JGE("011"),
    JLT("100"),
    JNE("101"),
    JLE("110"),
    JMP("111");

    private String code = "";

    JumpCode(String code) {
        this.code = code;
    }

    public String getCode() {
        return code;
    }
}
