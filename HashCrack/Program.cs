using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

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
            @"|     .\HashCrack.exe [path to passlist] [the path to the hashedPass] {-mLC | optional} |",
            @"|                                                                                       |",
            @"|                    optional end args= '-mLC' - '--multiLineCrack'                     |",
            @"|                                                                                       |",
            @"|                 Used when you want to crack multible hashes at once.                  |",
            @"|                                                                                       |",
            @"|                                 Contact Me:                                           |",
            @"|                                                                                       |",
            @"|                       Email - obi@vnight-studios.xyz                                  |",
            @"|                       Github - https://github.com/Obimydudee                          |",
            @"|                       Twitter - https://twitter.com/obimydude                         |",
            @"|                       Bluesky - https://bsky.app/profile/veloxservers.lol             |",
            @"|                                                                                       |",
            @"\_______________________________________________________________________________________/",
            @"                                                                                         "
        };

        static void Main(string[] args)
        {
            
            if (args.Length == 0) {
                Console.WriteLine("usage: [path to passlist] [the path to the hashedPass]");
            }

            string[] cmdLineArgs = Environment.GetCommandLineArgs();

            if (cmdLineArgs.Contains("-h") || cmdLineArgs.Contains("--help"))
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                for (int i = 0; i < 17; i++) //10 meaning the amount of lines the string[] logo has
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
                List<string> HashLine = ReadAllLinesFromTxt(hashpath);





                if (cmdLineArgs.Contains("-mLC") || cmdLineArgs.Contains("--multiLineCrack")) //multiLineCrack
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    for (int i = 0; i < 11; i++)
                    {
                        Console.WriteLine(StartString[i]);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (string line in HashLine)
                    {
                        HashesToCrack.Add(line);
                        Console.WriteLine($"Hashes2Crack: {line}");
                    }

                    foreach (string Hashes in HashesToCrack)
                    {
                        string tempHash = Hashes;
                        var lines = File.ReadLines(wordlist);
                        foreach (string Passline in lines)
                        {
                            //Console.WriteLine($"{MD5(Passline)}");
                            if (MD5(Passline) == tempHash)
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
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    for (int i = 0; i < 11; i++)
                    {
                        Console.WriteLine(StartString[i]);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    foreach (string line in HashLine)
                    {
                        HashToCrack = line;
                        Console.WriteLine($"Hash2Crack: {line}");
                    }
                    var lines = File.ReadLines(wordlist);
                    foreach (string Passline in lines)
                    {
                        //Console.WriteLine($"{MD5(Passline)}");
                        if (MD5(Passline) == HashToCrack)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Possible Password: {Passline} | HASH: {HashToCrack}");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        }
                    }
                }
            }
            
            
            
        }

        public static string MD5(string s)
        {
            var provider = System.Security.Cryptography.MD5.Create();
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }
    }
}
