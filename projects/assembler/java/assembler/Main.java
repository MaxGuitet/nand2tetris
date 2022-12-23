package assembler;

import java.nio.file.Path;

public class Main {
    public static void main(String[] args) {

        if (args.length == 0) {
            System.out.println("Missing input script arg.");
            return;
        }

        String inputFilePath = args[0];
        Path path = Path.of(inputFilePath, args);
        String fileName = path.getFileName().toString();

        if (!fileName.endsWith(".asm")) {
            System.out.println("Input file must be a .asm file.");
            return;
        }

        String destinationFilePath = inputFilePath.replace(".asm", ".hack");

        Assembler assembler = new Assembler(inputFilePath, destinationFilePath);
        assembler.parseFile();
    }
}
