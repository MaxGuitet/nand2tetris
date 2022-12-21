package assembler.code;

public enum DestCode {
    NONE("000"),
    M("001"),
    D("010"),
    MD("011"),
    A("100"),
    AM("101"),
    AD("110"),
    AMD("111");

    private String code = "";

    DestCode(String code) {
        this.code = code;
    }

    public String getCode() {
        return code;
    }
}
