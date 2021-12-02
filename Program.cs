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

        static void ExitOnError(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Usage: pax2tlu somefile.pax employee.xml");
            Environment.Exit(-1);
        }

        static void Main(string[] args)
        {

            string inputFile, employeeFile;
            XDocument paxFile, tluFile, employeeList;
            
            if (args.Length == 0)
            {
                ExitOnError("Error: Files are missing from the commandline.");               
            }
            else
            {
                inputFile = args[0];
                employeeFile = args[1];
                if (File.Exists(inputFile))
                {
                    if (inputFile.Contains(".pax"))
                    {
                        if (File.Exists(employeeFile) && employeeFile.Contains(".xml"))
                        {

                        
                        paxFile = XDocument.Load(inputFile);
                        tluFile = new XDocument(
                                            new XDeclaration("1.0", "ISO-8859-1", ""),
                                            new XElement("SalaryData", 
                                                new XAttribute("ProgramName", "pax2tlu"),
                                                new XAttribute("Version", "1.03"),
                                                new XAttribute("ExportVersion", "1.2"),
                                                new XAttribute("Created", DateTime.Today.ToString("d")),
                                                new XAttribute("CompanyName", paxFile.Element("paxml").Element("header").Element("foretagnamn").Value),
                                                new XAttribute("OrgNo", paxFile.Element("paxml").Element("header").Element("foretagorgnr").Value.Replace("16","").Insert(6,"-")))
                                                );

                        tluFile.Save(inputFile.Replace(".pax", ".tlu"));
                        }
                        else
                        {
                            ExitOnError("Error: employee.xml file is missing");
                        }
                    }
                    else
                    {
                        ExitOnError("Error: File is not of type .pax");
                    }
                }
                else
                {
                    ExitOnError("Error: Given filename does not exist");
                }

            }
        }
    }
}
