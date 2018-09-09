using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public class SymbolTable
    {
        public static Dictionary<string, int> hashTable = new Dictionary<string, int>();         	

        public static void addEntry(string symbol, int address)
        {
        	hashTable.Add(symbol, address);
        }

        public static bool contains(string symbol)
        {
        	return hashTable.ContainsKey(symbol);
        }

        public static int GetAddress(string symbol)
        {

        	if (contains(symbol))
        	{
        		return hashTable[symbol];
        	}
        	else
        	{
        		return 0;
        	}
        }
	}
}

