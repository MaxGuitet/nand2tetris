package assembler.symboltable;

import java.util.HashMap;
import java.util.Map;

public class SymbolTable {
    int nextAddress = 16;
    Map<String, Integer> table = new HashMap<String, Integer>();

    public SymbolTable() {
        this.initPredefinedSymbols();
    }

    void initPredefinedSymbols() {
        for (PredefinedSymbols symbol : PredefinedSymbols.values()) {
            String hackName = symbol.name();
            int value = symbol.getValue();

            this.table.put(hackName, value);
        }
    }

    public void addEntry(String symbol, int value) throws Exception {
        if (this.contains(symbol)) {
            throw new Exception("Symbol \"" + symbol + "\" already exists.");
        }

        this.table.put(symbol, value);
    }

    public int autoAddEntry(String symbol) throws Exception {
        if (this.contains(symbol)) {
            throw new Exception("Symbol \"" + symbol + "\" already exists.");
        }

        int returnValue = this.nextAddress;
        this.table.put(symbol, this.nextAddress);

        this.nextAddress += 1;

        return returnValue;
    }

    public boolean contains(String symbol) {
        return this.table.get(symbol) != null;
    }

    public int getAddress(String symbol) {
        return this.table.get(symbol);
    }
}
