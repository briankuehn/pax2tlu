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
                        XElement paxFile = XElement.Load(inputFile);
                        XDocument tluFile = new XDocument(
                            new XDeclaration("1.0", "ISO-8859-1",""),
                            new XElement("SalaryData", new XAttribute("ProgramName", "pax2tlu"),
                                                       new XAttribute("Version", "1.03"),
                                                       new XAttribute("ExportVersion", "1.2"),
                                                       new XAttribute("Created", DateTime.Today.ToString("d")))
                            
                            );
                        tluFile.Save(inputFile.Replace(".pax", ".tlu"));
                        XElement paxInnerElement = paxFile.Element("foretagnamn");
                        Console.WriteLine(paxInnerElement.Value);
                        Console.WriteLine(tluFile.FirstNode.ToString());
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
