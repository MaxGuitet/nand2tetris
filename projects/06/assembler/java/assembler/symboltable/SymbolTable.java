package assembler.symboltable;

public class SymbolTable {
    public static String dest(String asmDest) {
        String destValue = DestTable.valueOf(asmDest).getCode();

        return destValue;
    }

    public static String comp(String asmComp) {
        try {

            String compValue = CompTable.getCodeFor(asmComp);
            return compValue;
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        return "";
    }
}
