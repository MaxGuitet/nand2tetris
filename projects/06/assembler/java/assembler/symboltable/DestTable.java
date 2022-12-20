package assembler.symboltable;

public enum DestTable {
    NONE("000"),
    M("001"),
    D("010"),
    DM("011"),
    A("100"),
    AM("101"),
    AD("110"),
    ADM("111");

    private String code = "";

    DestTable(String code) {
        this.code = code;
    }

    public String getCode() {
        return code;
    }
}
