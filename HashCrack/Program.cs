using HashCrack.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Markup;

namespace HashCrack
{
    internal static class Program
    {
        static List<string> ReadAllLinesFromTxt(string filename)
        {
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader strd = new StreamReader(filename))
                {
                    string line;
                    while ((line = strd.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch 
            {
                Console.WriteLine("Error Reading");
            }

            return lines;
        }


        private static string[] StartString = new string[]
        {
            @"$$\   $$\                     $$\        $$$$$$\                               $$\       ",
            @"$$ |  $$ |                    $$ |      $$  __$$\                              $$ |      ",
            @"$$ |  $$ | $$$$$$\   $$$$$$$\ $$$$$$$\  $$ /  \__| $$$$$$\  $$$$$$\   $$$$$$$\ $$ |  $$\ ",
            @"$$$$$$$$ | \____$$\ $$  _____|$$  __$$\ $$ |      $$  __$$\ \____$$\ $$  _____|$$ | $$  |",
            @"$$  __$$ | $$$$$$$ |\$$$$$$\  $$ |  $$ |$$ |      $$ |  \__|$$$$$$$ |$$ /      $$$$$$  / ",
            @"$$ |  $$ |$$  __$$ | \____$$\ $$ |  $$ |$$ |  $$\ $$ |     $$  __$$ |$$ |      $$  _$$<  ",
            @"$$ |  $$ |\$$$$$$$ |$$$$$$$  |$$ |  $$ |\$$$$$$  |$$ |     \$$$$$$$ |\$$$$$$$\ $$ | \$$\ ",
            @"\__|  \__| \_______|\_______/ \__|  \__| \______/ \__|      \_______| \_______|\__|  \__|",
            @"                                                            //Made by Obi/ProxNet/ProxFox",
            @"                                                                                         ",
            @"                                                                                         "
        };

        private static string[] HelpString = new string[]
        {
            @"/ˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉ\",
            @"|                                       USAGE:                                          |",
            @"|     .\HashCrack.exe [path to passlist] [hashedPass/path2hashfile] {-mLC | optional}   |",
            @"|                                                                                       |",
            @"|                    optional end args= '-mLC' - '--multiLineCrack'                     |",
            @"|                 Used when you want to crack multible hashes at once.                  |",
            @"|                                                                                       |",
            @"|                                                                                       |",
            @"|                    optional end args= '-p' - '--parse'                                |",
            @"|         Grabs the hash input directly from the args instead of from file.             |",
            @"|                                                                                       |",
            @"|                                 Contact Me:                                           |",
            @"|                                                                                       |",
            @"|                       Email   - obi@vnight-studios.xyz                                |",
            @"|                       Github  - https://github.com/Obimydudee                         |",
            @"|                       Twitter - https://twitter.com/obimydude                         |",
            @"|                       Bluesky - https://bsky.app/profile/veloxservers.lol             |",
            @"|                                                                                       |",
            @"\_______________________________________________________________________________________/",
            @"                                                                                         "
        };

        static void Main(string[] args)
        {
            
            if (args.Length == 0) {
            
                for (int i = 0; i < 19; i++) //10 meaning the amount of lines the string[] logo has
                {
                    Console.WriteLine(HelpString[i]);
                }
            }
            else
            {
                string[] cmdLineArgs = Environment.GetCommandLineArgs();

                if (cmdLineArgs.Contains("-h") || cmdLineArgs.Contains("--help"))
                {

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for (int i = 0; i < 19; i++) //10 meaning the amount of lines the string[] logo has
                    {
                        Console.WriteLine(HelpString[i]);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Clear();
                    string wordlist = cmdLineArgs[1];
                    string hashpath = cmdLineArgs[2];
                    string HashToCrack = null;
                    List<string> HashesToCrack = new List<string>();

                    
                    switch (cmdLineArgs) {
                        case string[] x when x.Contains("-p") || x.Contains("--parse"):
                            HashToCrack = hashpath;
                            var lines = File.ReadLines(wordlist);
                            foreach (string Passline in lines)
                            {
                                //Console.WriteLine($"{MD5(Passline)}");
                                if (Hasher(Passline) == HashToCrack)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Possible Password: {Passline} | HASH: {HashToCrack}");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;

                                }
                                else if (Hasher(Passline) == "ERROR")
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("FAILED");
                                    Console.ForegroundColor = ConsoleColor.White; 
                                    break;
                                }
                            }
                            break;
                        case string[] x when x.Contains("-mLC") || x.Contains("--multiLineCrack"):
                            List<string> HashLine = ReadAllLinesFromTxt(hashpath);
                            foreach (string line in HashLine)
                            {
                                HashesToCrack.Add(line);
                                Console.WriteLine($"Hashes2Crack: {line}");
                            }

                            foreach (string Hashes in HashesToCrack)
                            {
                                string tempHash = Hashes;
                                var Multilines = File.ReadLines(wordlist);
                                foreach (string Passline in Multilines)
                                {
                                    //Console.WriteLine($"{MD5(Passline)}");
                                    if (Hasher(Passline) == tempHash)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"Possible Password: {Passline} | HASH: {tempHash}");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        break;

                                    }
                                    
                                }
                                tempHash = string.Empty;
                                //Thread.Sleep(1000);
                            }
                            break;
                        default:
                            List<string> HashLinee = ReadAllLinesFromTxt(hashpath);
                            foreach (string line in HashLinee)
                            {
                                HashToCrack = line;
                                Console.WriteLine($"Hash2Crack: {line}");
                            }
                            var elines = File.ReadLines(wordlist);
                            foreach (string Passline in elines)
                            {
                                //Console.WriteLine($"{MD5(Passline)}");
                                if (Hasher(Passline) == HashToCrack)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Possible Password: {Passline} | HASH: {HashToCrack}");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;

                                }
                            }
                            break;
                    }

                    //if (cmdLineArgs.Contains("-p") || cmdLineArgs.Contains("--parse"))
                    //{
                    //    HashToCrack = hashpath;
                    //    Console.WriteLine($"HashCrack: {hashpath}");
                    //    var lines = File.ReadLines(wordlist);
                    //    foreach (string Passline in lines)
                    //    {
                    //        //Console.WriteLine($"{MD5(Passline)}");
                    //        if (MD5(Passline) == HashToCrack)
                    //        {
                    //            Console.ForegroundColor = ConsoleColor.Green;
                    //            Console.WriteLine($"Possible Password: {Passline} | HASH: {HashToCrack}");
                    //            Console.ForegroundColor = ConsoleColor.White;
                    //            break;
                    //
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    List<string> HashLine = ReadAllLinesFromTxt(hashpath);
                    //
                    //    if (cmdLineArgs.Contains("-mLC") || cmdLineArgs.Contains("--multiLineCrack")) //multiLineCrack
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Magenta;
                    //        for (int i = 0; i < 11; i++)
                    //        {
                    //            Console.WriteLine(StartString[i]);
                    //        }
                    //        Console.ForegroundColor = ConsoleColor.White;
                    //        foreach (string line in HashLine)
                    //        {
                    //            HashesToCrack.Add(line);
                    //            Console.WriteLine($"Hashes2Crack: {line}");
                    //        }
                    //
                    //        foreach (string Hashes in HashesToCrack)
                    //        {
                    //            string tempHash = Hashes;
                    //            var lines = File.ReadLines(wordlist);
                    //            foreach (string Passline in lines)
                    //            {
                    //                //Console.WriteLine($"{MD5(Passline)}");
                    //                if (MD5(Passline) == tempHash)
                    //                {
                    //                    Console.ForegroundColor = ConsoleColor.Green;
                    //                    Console.WriteLine($"Possible Password: {Passline} | HASH: {tempHash}");
                    //                    Console.ForegroundColor = ConsoleColor.White;
                    //                    break;
                    //
                    //                }
                    //            }
                    //            tempHash = string.Empty;
                    //            //Thread.Sleep(1000);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Console.ForegroundColor = ConsoleColor.Magenta;
                    //        for (int i = 0; i < 11; i++)
                    //        {
                    //            Console.WriteLine(StartString[i]);
                    //        }
                    //        Console.ForegroundColor = ConsoleColor.White;
                    //        foreach (string line in HashLine)
                    //        {
                    //            HashToCrack = line;
                    //            Console.WriteLine($"Hash2Crack: {line}");
                    //        }
                    //        var lines = File.ReadLines(wordlist);
                    //        foreach (string Passline in lines)
                    //        {
                    //            //Console.WriteLine($"{MD5(Passline)}");
                    //            if (MD5(Passline) == HashToCrack)
                    //            {
                    //                Console.ForegroundColor = ConsoleColor.Green;
                    //                Console.WriteLine($"Possible Password: {Passline} | HASH: {HashToCrack}");
                    //                Console.ForegroundColor = ConsoleColor.White;
                    //                break;
                    //
                    //            }
                    //        }
                    //    }
                    //}

                }
            }

            
            
            
            
        }

        

        public static string Hasher(string s)
        {
            string[] cmdLineArgs = Environment.GetCommandLineArgs();
            string ine = string.Join("", cmdLineArgs);
            string input = ine.Substring(ine.LastIndexOf("-m") + 1);

            if (cmdLineArgs.Contains("-m")) {
                
                //int index = Array.IndexOf(cmdLineArgs, "-m");
                //string input = cmdLineArgs[index + 1];
                if (input.Contains("MD5"))
                {
                    return Hashing.MD5(s);
                }
                else if (input.Contains("SHA256"))
                {
                    return Hashing.SHA256(s);
                }
                else
                {
                    return "ERROR";
                }
                
            }
            else
            {
                return Hashing.MD5(s);
            }
        }
    }
}
