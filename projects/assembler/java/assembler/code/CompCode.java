package assembler.code;

public enum CompCode {
    ZERO("0101010"),
    ONE("0111111"),
    MINUS_ONE("0111010"),
    D("0001100"),
    A("0110000"),
    M("1110000"),
    NOT_D("0001101"),
    NOT_A("0110001"),
    NOT_M("1110001"),
    MINUS_D("0001111"),
    MINUS_A("0110011"),
    MINUS_M("1110011"),
    D_PLUS_1("0011111"),
    A_PLUS_1("0110111"),
    M_PLUS_1("1110111"),
    D_MINUS_1("0001110"),
    A_MINUS_1("0110010"),
    M_MINUS_1("1110010"),
    D_PLUS_A("0000010"),
    D_PLUS_M("1000010"),
    D_MINUS_A("0010011"),
    D_MINUS_M("1010011"),
    A_MINUS_D("0000111"),
    M_MINUS_D("1000111"),
    D_AND_A("0000000"),
    D_AND_M("1000000"),
    D_OR_A("0010101"),
    D_OR_M("1010101");

    private String code = "";

    CompCode(String code) {
        this.code = code;
    }

    public String getCode() {
        return code;
    }

    public static String getCodeFor(String asmCode) throws Exception {
        switch (asmCode) {
            case "0":
                return ZERO.getCode();

            case "1":
                return ONE.getCode();

            case "-1":
                return MINUS_ONE.getCode();

            case "D":
                return D.getCode();

            case "A":
                return A.getCode();

            case "M":
                return M.getCode();

            case "!D":
                return NOT_D.getCode();

            case "!A":
                return NOT_A.getCode();

            case "!M":
                return NOT_M.getCode();

            case "-D":
                return MINUS_D.getCode();

            case "-A":
                return MINUS_A.getCode();

            case "-M":
                return MINUS_M.getCode();

            case "D+1":
                return D_PLUS_1.getCode();

            case "A+1":
                return A_PLUS_1.getCode();

            case "M+1":
                return M_PLUS_1.getCode();

            case "D-1":
                return D_MINUS_1.getCode();

            case "A-1":
                return A_MINUS_1.getCode();

            case "M-1":
                return M_MINUS_1.getCode();

            case "D+A":
                return D_PLUS_A.getCode();

            case "D+M":
                return D_PLUS_M.getCode();

            case "D-A":
                return D_MINUS_A.getCode();

            case "D-M":
                return D_MINUS_M.getCode();

            case "A-D":
                return A_MINUS_D.getCode();

            case "M-D":
                return M_MINUS_D.getCode();

            case "D&A":
                return D_AND_A.getCode();

            case "D&M":
                return D_AND_M.getCode();

            case "D|A":
                return D_OR_A.getCode();

            case "D|M":
                return D_OR_M.getCode();

            default:
                throw new Exception("Unknown ASM code \"" + asmCode + "\"");
        }
    }
}
