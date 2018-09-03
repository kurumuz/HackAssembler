using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public class Parser
    {
        public static string currCommand = "NULL";
        public static string lValue = "NULL";
        public static string aValue = "NULL";
        public static string cType = "NULL";
        public static int currInst = 0;
        public static List<string> inslist = new List<string>();
        public static List<string> OpenFile(string asmpath)
        {
            var arrayLine = File.ReadAllLines(asmpath);
            for (var i=0; i < arrayLine.Length; i+=1)
            {
                inslist.Add(arrayLine[i]);
            }
            currCommand = inslist[0];
            return inslist;
        }

        public static bool dahaKomutVarmı() //look for comments and whitelines here.
        {
            if ((currInst-inslist.Count()) >= 0) 
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
            currInst++;
            if (inslist.Count() > currInst)
            {
                currCommand = inslist[currInst];
            }
        }

        public static string komuttipi() // "//" ile başlayan yada tamamı boşluk olan satırları algılamayı ekle. eğer a veya c komutu ise //den sonraki herşeyi regex ile sil.
        {
            Regex lcommand = new Regex(@"\(([^\)]*)\)");
            Match lmatch = lcommand.Match(currCommand);
            Regex acommand = new Regex(@"(?<!\w)@\w+");
            Match amatch = acommand.Match(currCommand);
            
            if (lmatch.Success & lmatch.Value != "()") //belki sadece 2 tane parantez olmalı?
            {lValue = lmatch.Groups[1].Value;
                
                aValue = "NULL";
                cType= "NULL";
                return "L_COMMAND";
            }

            else if (amatch.Success) //@ ten sonra bir @ daha olabilir mi?
            {
                aValue = amatch.Value.Substring(1);
                lValue = "NULL";
                cType= "NULL";
                return "A_COMMAND";
            }

            else if (currCommand.Contains('=') || currCommand.Contains(';')) 
            {
                aValue = "NULL";
                lValue = "NULL";

                if (currCommand.Contains('=') && !currCommand.Contains(';')) //
                {
                    cType = "destncomp";
                    return "C_COMMAND";
                }

                else if (currCommand.Contains(';') && !currCommand.Contains('='))
                {
                    cType = "compnjump";
                    return "C_COMMAND";
                }

                else if (currCommand.Contains('=') && currCommand.Contains(';'))
                {
                    cType = "all";
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

            if (lValue != "NULL")
            {
                return lValue;
            }

            if (aValue != "NULL")
            {
                return aValue;
            }

            else
            {
                return "NULL";
            } 
        }

        public static string hedef()
        {

            if (cType == "destncomp" || cType == "all")
            {
                Match destmatch = Regex.Match(currCommand, @"^.*?(?==)");
                return Regex.Replace(destmatch.Groups[0].Value, @"\s+", "");
            }

            else
            {
                return "NULL";
            }
        }

        public static string islem()
        {

            if (cType != "NULL")
            {

                if (cType == "compnjump") //sadece ; olan | comp;jump
                {
                    Match compmatch = Regex.Match(currCommand, @"^.*?(?=;)");
                    return Regex.Replace(compmatch.Groups[0].Value, @"\s+", "");
                }

                else if (cType == "destncomp") //sadece = olan |dest=comp | eşittirden sonraki herşey
                {
                    Match compmatch = Regex.Match(currCommand, @"=(.+)$");
                    string compmatchwospace = Regex.Replace(compmatch.Groups[1].Value, @"\s+", "");
                    return Regex.Replace(compmatchwospace, @"/(.+)$", ""); // deletes the
                }

                else if (cType == "all") //hem eşittir hemde virgül var | dest=comp;jump | = ve ; arasındaki alınmalı
                {
                    Match compmatch = Regex.Match(currCommand, @"\=([^\)]*)\;");
                    return Regex.Replace(compmatch.Groups[1].Value, @"\s+", "");
                }

                else
                {
                    return "NULL";
                }
            }

            else
            {
                return "NULL";   
            }

            //3 fonksiyonda yapılan iş tek fonksiyonda da yapılabilir aslında: eğer = varsa ; yoksa sadece = varsa ve sadece ; varsa olarak
            //if ifadeleri ile alabiliriz hepsini, ama 3ünü birden döndüremeyiz ondan 3 ayrı fonksiyon daha iyi bir fikir.      
        }

        public static string ziplama()
        {

            if ((cType != "NULL") && (cType == "compnjump" || cType == "all"))
            {
                Match ziplamatch = Regex.Match(currCommand, @";(.+)$");
                string ziplamatchwospace = Regex.Replace(ziplamatch.Groups[1].Value, @"\s+", "");
                return Regex.Replace(ziplamatchwospace, @"/(.+)$", "");    
            }

            else
            {
                return "NULL";
            }
        }
    }
}
