using System;
using System.Collections.Generic;
using System.Linq;

namespace HackAssembler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<string>();
            list = Parser.OpenFile("selam");
            Console.WriteLine("satır sayısı: " + list.Count());
            Console.WriteLine("komut tipi: " + Parser.komuttipi());
            Console.WriteLine("lcommandx: " + Parser.lcommandx);
            Console.WriteLine("acommandx: " + Parser.acommandx);
            Console.WriteLine("ccommand türü: " + Parser.ccommandturu);
            Console.WriteLine("Dest: " + Parser.hedef());
            Console.WriteLine("İşlem: " + Parser.islem());
            Console.WriteLine("Jump: " + Parser.ziplama());
            
        }
    }
}
