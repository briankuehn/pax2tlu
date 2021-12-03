﻿using System;
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

                            employeeList = XDocument.Load(employeeFile);
                            paxFile = XDocument.Load(inputFile);
                            tluFile = new XDocument(
                                            new XDeclaration("1.0", "ISO-8859-1", ""),
                                            new XElement("SalaryData", 
                                                new XAttribute("ProgramName", "pax2tlu"),
                                                new XAttribute("Version", "1.03"),
                                                new XAttribute("ExportVersion", "1.2"),
                                                new XAttribute("Created", DateTime.Today.ToString("d")),
                                                new XAttribute("CompanyName", paxFile.Element("paxml").Element("header").Element("foretagnamn").Value),
                                                new XAttribute("OrgNo", paxFile.Element("paxml").Element("header").Element("foretagorgnr").Value.Replace("16","").Insert(6,"-")),
                                                new XElement("SalaryDataEmployee", new XAttribute("FromDate","2021-01-01"), new XAttribute("ToDate","2021-12-31")),
                                                    new XElement("NormalWorkingHours"),
                                                    new XElement("Times"),
                                                    new XElement("RegOutlays"))
                                            );

                            XDocument normalWorkingHoursList = employeeList;
                            XDocument timesList = employeeList;
                            XDocument regOutlaysList = employeeList;

                            foreach (XElement element in employeeList.Descendants("Employee"))
                            {
                                Console.WriteLine(element.Element("EmploymentNo").Value);

                                tluFile.Element("SalaryData").Element("SalaryDataEmployee").Add(new XElement("Employee",
                                                    new XAttribute("EmploymentNo", element.Element("EmploymentNo").Value),
                                                    new XAttribute("FirstName", element.Element("FirstName").Value),
                                                    new XAttribute("Name", element.Element("Name").Value),
                                                    new XAttribute("PersonalNo", element.Element("PersonalNo").Value),
                                                    new XAttribute("FromDate", "2021-01-01"),
                                                    new XAttribute("ToDate", "2021-12-01"))
                                                    );
                                //Need to look into how to select records with Linq and searching through an attribute
                                foreach (XElement el in normalWorkingHoursList.Descendants("schematransaktioner"))
                                {

                                }

                            }
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
