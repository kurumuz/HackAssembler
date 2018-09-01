using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public class Parser
    {
        public static string command = "nothing";
        public static string lcommandx = "nothing";
        public static string acommandx = "nothing";
        public static string ccommandturu = "nothing";
        public static string dest = "nothing"; //muhtemelen dest comp ve jump stringlerine gerek kalmayacak
        public static string comp = "nothing";
        public static string jump = "nothing";
        public static int currinstlist = 0;
        public static List<string> inslist = new List<string>();
        public static List<string> OpenFile(string asmpath)
        {
            var arrayLine = File.ReadAllLines(asmpath);
            for (var i=0; i < arrayLine.Length; i+=1)
            {
                inslist.Add(arrayLine[i]);
            }
            command = inslist[0];
            return inslist;
        }

        public static bool dahaKomutVarmı() //look for comments and whitelines here.
        {
            if ((currinstlist-inslist.Count()) >= 0) 
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void ilerle()
        {
            currinstlist++; // 1 
            if (inslist.Count() > currinstlist)
            {
                command = inslist[currinstlist];
                
            }
        }

        public static string komuttipi() // "//" ile başlayan yada tamamı boşluk olan satırları algılamayı ekle. eğer a veya c komutu ise //den sonraki herşeyi regex ile sil.
        {
            Regex lcommand = new Regex(@"\(([^\)]*)\)");
            Match lmatch = lcommand.Match(command);
            Regex acommand = new Regex(@"(?<!\w)@\w+");
            Match amatch = acommand.Match(command);
            
            if (lmatch.Success & lmatch.Value != "()") //belki sadece 2 tane parantez olmalı?
            {lcommandx = lmatch.Groups[1].Value;
                
                acommandx = "nothing";
                ccommandturu= "nothing";
                return "L_COMMAND";
            }
            else if (amatch.Success) //@ ten sonra bir @ daha olabilir mi?
            {
                acommandx = amatch.Value.Substring(1);
                lcommandx = "nothing";
                ccommandturu= "nothing";
                return "A_COMMAND";
            }

            else if (command.Contains('=') || command.Contains(';')) 
            {
                acommandx = "nothing";
                lcommandx = "nothing";

                if (command.Contains('=') && !command.Contains(';'))
                {
                    ccommandturu = "sadeceeşit";
                    return "C_COMMAND";
                }

                else if (command.Contains(';') && !command.Contains('='))
                {
                    ccommandturu = "sadecevirgül";
                    return "C_COMMAND";
                }

                else if (command.Contains('=') && command.Contains(';'))
                {
                    ccommandturu = "ikiside";
                    return "C_COMMAND";
                }

                else
                {
                    return "Bilinmiyor";
                }

            }
            else
            {
                return "Bilinmiyor";
            }
        }

        public static string sembol()
        {
            if (lcommandx != "nothing")
            {
                return lcommandx;
            }
            if (acommandx != "nothing")
            {
                return acommandx;
            }
            else
            {
                return "nothing";
            } 
        }

        public static string hedef()
        {
            if (ccommandturu == "sadeceeşit" || ccommandturu == "ikiside")
            {
                Match destmatch = Regex.Match(command, @"^.*?(?==)");
                return Regex.Replace(destmatch.Groups[0].Value, @"\s+", "");
            }

            else
            {
                return "nothing";
            }
        }

        public static string islem()
        {

            if (ccommandturu != "nothing")
            {
                if (ccommandturu == "sadecevirgül") //sadece ; olan | comp;jump
                {
                    Match compmatch = Regex.Match(command, @"^.*?(?=;)");
                    return Regex.Replace(compmatch.Groups[0].Value, @"\s+", "");
                }
                else if (ccommandturu == "sadeceeşit") //sadece = olan |dest=comp | eşittirden sonraki herşey
                {
                    Match compmatch = Regex.Match(command, @"=(.+)$");
                    string compmatchwospace = Regex.Replace(compmatch.Groups[1].Value, @"\s+", "");
                    return Regex.Replace(compmatchwospace, @"/(.+)$", ""); // deletes the
                }
                else if (ccommandturu == "ikiside") //hem eşittir hemde virgül var | dest=comp;jump | = ve ; arasındaki alınmalı
                {
                    Match compmatch = Regex.Match(command, @"\=([^\)]*)\;");
                    return Regex.Replace(compmatch.Groups[1].Value, @"\s+", "");
                }
                else
                {
                    return "nothing";
                }
            }
            else
            {
                return "nothing";   
            }

            //3 fonksiyonda yapılan iş tek fonksiyonda da yapılabilir aslında: eğer = varsa ; yoksa sadece = varsa ve sadece ; varsa olarak
            //if ifadeleri ile alabiliriz hepsini, ama 3ünü birden döndüremeyiz ondan 3 ayrı fonksiyon daha iyi bir fikir.      
        }

        public static string ziplama()
        {
            if (ccommandturu != "nothing")
            {
                if (ccommandturu == "sadecevirgül" || ccommandturu == "ikiside") //sadece ; olan | comp;jump
                {
                    Match ziplamatch = Regex.Match(command, @";(.+)$");
                    string ziplamatchwospace = Regex.Replace(ziplamatch.Groups[1].Value, @"\s+", "");
                    return Regex.Replace(ziplamatchwospace, @"/(.+)$", "");
                }

                else
                {
                    return "nothing";
                }
            }
            else
            {
                return "nothing";
            }
        }
    }
}
