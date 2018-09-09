using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public static class Parser
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

        public static bool dahaKomutVarmı() 
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
                currCommand = Regex.Replace(inslist[currInst], @"/(.+)$", "");

            }
        }

        public static string komuttipi()
        {
            Regex lcommand = new Regex(@"\(([^\)]*)\)");
            Match lmatch = lcommand.Match(currCommand);
            Regex acommand = new Regex(@"@(.+)$");
            Match amatch = acommand.Match(currCommand);
            
            if (lmatch.Success & lmatch.Value != "()")
            {
                lValue = lmatch.Groups[1].Value;
                aValue = "NULL";
                cType= "NULL";
                return "L_COMMAND";
            }

            else if (amatch.Success)
            {
                aValue = amatch.Groups[1].Value;
                lValue = "NULL";
                cType= "NULL";
                return "A_COMMAND";
            }

            else if (currCommand.Contains('=') || currCommand.Contains(';')) 
            {
                aValue = "NULL";
                lValue = "NULL";

                if (currCommand.Contains('=') && !currCommand.Contains(';')) 
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

                if (cType == "compnjump") // comp;jump
                {
                    Match compmatch = Regex.Match(currCommand, @"^.*?(?=;)");
                    return Regex.Replace(compmatch.Groups[0].Value, @"\s+", "");
                }

                else if (cType == "destncomp") //dest=comp
                {
                    Match compmatch = Regex.Match(currCommand, @"=(.+)$");
                    string compmatchwospace = Regex.Replace(compmatch.Groups[1].Value, @"\s+", "");
                    return Regex.Replace(compmatchwospace, @"/(.+)$", ""); // deletes the
                }

                else if (cType == "all") //dest=comp;jump
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
