using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public class SymbolTable
    {
        public static Dictionary<string, string> hashTable = new Dictionary<string, string>();  

        public static void Construct()
        {
            string binary = "NULL";
            SymbolTable.addEntry("SP", "0000000000000000");
            SymbolTable.addEntry("LCL", "0000000000000001");
            SymbolTable.addEntry("ARG", "0000000000000010");
            SymbolTable.addEntry("THIS", "0000000000000011");
            SymbolTable.addEntry("THAT", "0000000000000100");
            SymbolTable.addEntry("SCREEN", Convert.ToString(16384, 2));
            SymbolTable.addEntry("KBD", Convert.ToString(24576, 2));
            for (int i = 0; i <= 15; i++)
            {
                binary = Convert.ToString(i, 2);
                while (16 - binary.Length != 0)
                {
                    binary = "0" + binary;
                }
                SymbolTable.addEntry("R" + Convert.ToString(i), binary);
            }

        }           

        public static void addEntry(string symbol, string address)
        {
            hashTable.Add(symbol, address);
        }

        public static bool contains(string symbol)
        {
            return hashTable.ContainsKey(symbol);
        }

        public static string GetAddress(string symbol)
        {

            if (contains(symbol))
            {
                return hashTable[symbol];
            }
            else
            {
                return "NULL";
            }
        }
    }
}

