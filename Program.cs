using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace pax2tlu
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "";

            if (args.Length == 0)
            {
                Console.WriteLine("Error: File is missing from the commandline.");
                Console.WriteLine("Usage: pax2tlu somefile.pax");
                Environment.Exit(-1);
            }
            else
            {
                inputFile = args[0];
                if (File.Exists(inputFile))
                {
                    if (inputFile.Contains(".pax"))
                    {
                        XDocument paxFile new XDocument.Load(inputFile);
                        XDocument tluFile new XDocument();

                    }
                    else
                    {
                        Console.WriteLine("Error: File is not of type .pax");
                        Console.WriteLine("Usage: pax2tlu somefile.pax");
                        Environment.Exit(-1);
                    }
                }
                else
                {
                    Console.WriteLine("Error: Given filename does not exist");
                    Console.WriteLine("Usage: pax2tlu somefile.pax");
                    Environment.Exit(-1);
                }

            }
        }
    }
}
