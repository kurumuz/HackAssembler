using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    class Program
    {
        static string cBinary = "NULL";
        static int AandLcounter = 0;
        static int varramaddr = 16;
        static List<string> writeList = new List<string>();
        private static void Main(string[] args)
        {
            SymbolTable.Construct();
            Console.Write("enter the assembly file name: ");
            string asmfile = Console.ReadLine();
            Match filematch = Regex.Match(asmfile, @"^.*?(?=\.)");
            var list = new List<string>();
            list = Parser.OpenFile(asmfile);
            Console.WriteLine("satır sayısı: " + list.Count());
            while(Parser.dahaKomutVarmı()) //first pass
            {
                
                if (Parser.komuttipi() == "L_COMMAND")
                {
                    string lBinary = Convert.ToString(AandLcounter, 2);
                    while (16 - lBinary.Length != 0)
                    {
                        lBinary = "0" + lBinary;
                    }
                    if (!SymbolTable.contains(Parser.lValue))
                    SymbolTable.addEntry(Parser.lValue, lBinary);
                }   

                else if (Parser.komuttipi() == "A_COMMAND")
                {
                    AandLcounter++;
                }

                else if (Parser.komuttipi() == "C_COMMAND")
                {
                    AandLcounter++;
                }

                Parser.ilerle();
            }

            Parser.currInst = 0;
            Parser.currCommand = Parser.inslist[0];
            
            while(Parser.dahaKomutVarmı()) //second pass
            {
                Console.WriteLine("Komut türü: [" + Parser.komuttipi() + "] ");
                if (Parser.komuttipi() == "L_COMMAND")
                {
                    Console.WriteLine("KOMUT: (" + Parser.lValue + ")" + " | SEMBOL: " + Parser.lValue + " | Binary: " + SymbolTable.GetAddress(Parser.lValue));
                }   

                else if (Parser.komuttipi() == "A_COMMAND") //"TODO:" SYMBOL HANDLING
                {
                    string aBinary = "NULL";

                    if (Regex.IsMatch(Parser.aValue, @"^\d+$"))
                    {
                        aBinary = Convert.ToString(Convert.ToInt32(Parser.aValue), 2);
                    
                        while (16 - aBinary.Length != 0)
                        {
                            aBinary = "0" + aBinary;
                        }
                    }
                    else
                    {
                        if (!SymbolTable.contains(Parser.aValue))
                        {
                            aBinary = Convert.ToString(varramaddr, 2);

                            while (16 - aBinary.Length != 0)
                            {
                                aBinary = "0" + aBinary;
                            }

                            SymbolTable.addEntry(Parser.aValue, aBinary);
                            varramaddr++;
                        }

                        else
                        {
                            aBinary = SymbolTable.GetAddress(Parser.aValue);
                        }
                    }
                    Console.WriteLine("KOMUT: @" + Parser.aValue + " | SEMBOL: " + Parser.aValue + " | " + "Binary: " + aBinary);
                    writeList.Add(aBinary);

                }

                else if (Parser.komuttipi() == "C_COMMAND")
                {
                    cBinary = "111" + Convert.ToString(Code.islem()) + Convert.ToString(Code.hedef()) + Convert.ToString(Code.ziplama());
                    Console.WriteLine("DEST: " + Parser.hedef() + " | " + "COMP: " + Parser.islem() + " | " + "JUMP: " + Parser.ziplama());
                    Console.WriteLine("Binary: " + cBinary + " | " + "DEST: " + Code.hedef() + " |" + " COMP: " + Code.islem() + " |" + " JUMP: " + Code.ziplama());
                    writeList.Add(cBinary);
                }

                Parser.ilerle();
            }
            
            File.WriteAllLines(filematch.Groups[0].Value + ".hack", writeList);
        }
    }
}
