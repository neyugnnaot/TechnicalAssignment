using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TechnicalAssignment
{
    class Program
    {
        //Email Regex
        static readonly Regex PATTERN = new Regex(@"^[a-z]\w*(\.\w+)*@[\w]*[.][\w]*");

        static void PrintList(List<string> list)
        {
            list.ForEach(x => Console.WriteLine(x)); 
        }

        static void ValidateEmails()
        {
            //Asking user for file name
            Console.WriteLine("\nHello! Please enter the name of the .csv file:");
            string path = Console.ReadLine();

            List<string> valid = new List<string>();
            List<string> invalid = new List<string>();

            //Finding file
            try
            {
                //Parsing file
                var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(path);
                parser.SetDelimiters(new string[] { "," });
                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    //Validating emails
                    string[] line = parser.ReadFields();
                    if (PATTERN.IsMatch(line[2]))
                    {
                        valid.Add(line[2]);
                    }
                    else
                    {
                        invalid.Add(line[2]);
                    }
                }

                //Printing valid and invalid emails
                Console.WriteLine("\nValid Emails:");
                PrintList(valid);
                Console.WriteLine("\nInvalid Emails:");
                PrintList(invalid);

            }
            catch (Exception)
            {
                Console.WriteLine("\nThe file name you entered was not found");
            }
        }

        static void Main(string[] args)
        {
            ValidateEmails(); 
            Console.WriteLine("\nThank you for using this program\nPress any key to exit...");
            Console.ReadKey(true);
        }
    }
}
