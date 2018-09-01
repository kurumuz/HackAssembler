using System;
using System.Collections.Generic;
using System.Linq;

namespace HackAssembler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //assembler without symbol handling, gonna add that feature later.
            var list = new List<string>();
            list = Parser.OpenFile("txt.asm");
            Console.WriteLine("satır sayısı: " + list.Count());
            while(Parser.dahaKomutVarmı())
            {
                Console.WriteLine("komut tipi: " + Parser.komuttipi());
                Console.WriteLine("lcommandx: " + Parser.lcommandx);
                Console.WriteLine("acommandx: " + Parser.acommandx);
                Console.WriteLine("ccommand türü: " + Parser.ccommandturu);
                Console.WriteLine("Dest: " + Parser.hedef());
                Console.WriteLine("İşlem: " + Parser.islem());
                Console.WriteLine("Jump: " + Parser.ziplama());
                Console.WriteLine("----------------------------------------------");
                Parser.ilerle();
            }            
        }
    }
}
