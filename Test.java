public class Test {
    public static void main(String[] args) {
        String test = "0;JMP // jump to next";

        test = test.replace(" ", "").replaceAll("\\/\\/.*", "");
        System.out.println(test);
    }
}
