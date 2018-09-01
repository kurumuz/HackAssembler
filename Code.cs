using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public class Code
    {
        public static int hedef()
        {
            switch (Parser.hedef())
            {
                case "nothing":
                    return 000;
                    break;

                case "M":
                    return 001;
                    break;

                case "D":
                    return 010;
                    break;
                
                case "MD":
                    return 011;
                    break;
                
                case "A":
                    return 100;
                    break;
                
                case "AM":
                    return 101;
                    break;
                
                case "AD":
                    return 110;
                    break;
                
                case "AMD":
                    return 111;
                    break;
            }

            return 0;
        }

        public static int islem()
        {
            switch (Parser.islem())
            {
                case "0":
                    return 0101010;
                    break;
                
                case "1":
                    return 0111111;
                    break;
                
                case "-1":
                    return 0111010;
                    break;
                
                case "D":
                    return 0001100;
                    break;
                
                case "A":
                    return 0110000;
                    break;
                
                case "!D":
                    return 0001101;
                    break;
                
                case "!A":
                    return 0110001;
                    break;
                
                case "-D":
                    return 0001111;
                    break;
                
                case "-A":
                    return 0110011;
                    break;
                
                case "D+1":
                    return 0011111;
                    break;
                
                case "A+1":
                    return 0110111;
                    break;
                
                case "D-1":
                    return 0001110;
                    break;
                
                case "A-1":
                    return 0110010;
                    break;
                
                case "D+A":
                    return 0000010;
                    break;
                
                case "D-A":
                    return 0010011;
                    break;
                
                case "A-D":
                    return 0000111;
                    break;
                
                case "D&A":
                    return 0000000;
                    break;
                
                case "D|A":
                    return 0010101;
                    break;
                
                case "M":
                    return 1110000;
                    break;
                
                case "!M":
                    return 1110001;
                    break;
                
                case "-M":
                    return 1110011;
                    break;
                
                case "M+1":
                    return 1110111;
                    break;
                
                case "M-1":
                    return 1110010;
                    break;
                
                case "D+M":
                    return 1000010;
                    break;
                
                case "D-M":
                    return 1010011;
                    break;
                
                case "M-D":
                    return 1000111;
                    break;
                
                case "D&M":
                    return 1000000;
                    break;
                
                case "D|M":
                    return 1010101;
                    break;
            }

            return 0;
        }

        public static int ziplama()
        {
            switch (Parser.ziplama())
            {
                case "nothing":
                    return 000;
                    break;
                
                case "JGT":
                    return 001;
                    break;
                
                case "JEQ":
                    return 010;
                    break;
                
                case "JGE":
                    return 011;
                    break;
                
                case "JLT":
                    return 100;
                    break;
                
                case "JNE":
                    return 101;
                    break;
                
                case "JLE":
                    return 110;
                    break;
                
                case "JMP":
                    return 111;
                    break;
            }

            return 0;
        }

    }
}
