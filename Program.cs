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
                Console.WriteLine("Error: File is missing.");
                Console.WriteLine("Usage: pax2tlu somefile.pax");
                Environment.Exit(-1);
            }
            else
            {
                inputFile = args[0];


            }
        }
    }
}
