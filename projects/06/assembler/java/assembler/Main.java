package assembler;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Path;
import java.util.List;

public class Main {
    public static void main(String[] args) {

        if (args.length == 0) {
            System.out.println("Missing input script arg.");
            return;
        }
        String filepath = args[0];
        Path path = Path.of(filepath, args);
        String fileName = path.getFileName().toString();

        if (!fileName.endsWith(".asm")) {
            System.out.println("Input file must be a .asm file.");
            return;
        }

        Assembler assembler = new Assembler(filepath);
        List<String> output = assembler.parseFile();

        String destinationFile = filepath.replace(".asm", ".hack");
        try {

            FileWriter fileWriter = new FileWriter(destinationFile, false);
            BufferedWriter bufferedWriter = new BufferedWriter(fileWriter);

            for (String line : output) {
                bufferedWriter.write(line);
                bufferedWriter.newLine();
            }

            bufferedWriter.close();
        } catch (IOException ex) {
            System.out.println("Failed to write result to output file.");
            ex.printStackTrace();
        }
    }
}
