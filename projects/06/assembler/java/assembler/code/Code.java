package assembler.code;

public class Code {
    public static String dest(String asmDest) {
        String destValue = DestCode.valueOf(asmDest).getCode();

        return destValue;
    }

    public static String comp(String asmComp) {
        try {
            String compValue = CompCode.getCodeFor(asmComp);
            return compValue;
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return "";
    }

    public static String jump(String asmJump) {
        String jumpValue = JumpCode.valueOf(asmJump).getCode();

        return jumpValue;
    }

}
