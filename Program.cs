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
        static List<string> writeList = new List<string>();
        private static void Main(string[] args)
        {
            Console.Write("enter the assembly file name: ");
            string asmfile = Console.ReadLine();
            Match filematch = Regex.Match(asmfile, @"^.*?(?=\.)");
            //assembler without symbol handling, gonna add that feature later.
            var list = new List<string>();
            list = Parser.OpenFile(asmfile);
            Console.WriteLine("satır sayısı: " + list.Count());
            while(Parser.dahaKomutVarmı())
            {
                Console.Write("Komut türü: [" + Parser.komuttipi() + "] ");
                if (Parser.komuttipi() == "L_COMMAND")
                {
                    Console.WriteLine("KOMUT: (" + Parser.lValue + ")" + " | SEMBOL: " + Parser.lValue);
                    //L_COMMAND PARSING: TODO;
                }

                else if (Parser.komuttipi() == "A_COMMAND") //for now only can use numbers, not sembols.
                {
                    string aBinary = Convert.ToString(Convert.ToInt32(Parser.aValue), 2);
                    
                    while (16 - aBinary.Length != 0)
                    {
                        aBinary = "0" + aBinary;
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

                else
                {
                    Console.WriteLine();
                }

                Parser.ilerle();
            } 

            foreach(string instruction in writeList)
            {
                Console.WriteLine(instruction);
            }
            File.WriteAllLines(filematch.Groups[0].Value + ".hack", writeList);

        }
    }
}
