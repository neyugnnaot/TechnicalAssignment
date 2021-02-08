using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO; 

namespace TechnicalAssignment
{
    //Class to handle parsing emails
    class CSVParser
    {
        private TextFieldParser _parser; 
        private readonly List<string> _validEmails;
        private readonly List<string> _invalidEmails;

        public List<string> ValidEmails => _validEmails;
        public List<string> InvalidEmails => _invalidEmails;

        public CSVParser(string path)
        {
            _validEmails = new List<string>();
            _invalidEmails = new List<string>();
            _parser = new TextFieldParser(path);
        }

        public void ParseCSV()
        {
            _parser.SetDelimiters(new string[] { "," });
            _parser.ReadLine();

            while (!_parser.EndOfData)
            {
                //Validating emails
                string[] line = _parser.ReadFields();
                string email = line[2];

                if (EmailValidator.ValidateEmail(email))
                {
                    _validEmails.Add(email);
                }
                else
                {
                    _invalidEmails.Add(email);
                }
            }
        }
    }
    
    class EmailValidator
    {
                //Email Regex
        static readonly Regex PATTERN = new Regex(@"^[a-z]\w*(\.\w+)*@[\w]*[.][\w]*");

        public static bool ValidateEmail(string email)
        {
            return PATTERN.IsMatch(email); 
        }

    }

    //Class to handle outputs for data
    class OutputHandler
    {
        public static void PrintList(List<string> list)
        {
            list.ForEach(item => Console.WriteLine(item));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Asking user for file name
            Console.WriteLine("\nHello! Please enter the name of the .csv file:");
            string path = Console.ReadLine();

            try
            {
                CSVParser parser = new CSVParser(path);
                parser.ParseCSV();

                //Printing valid and invalid emails
                Console.WriteLine("\nValid Emails:");
                OutputHandler.PrintList(parser.ValidEmails);
                Console.WriteLine("\nInvalid Emails:");
                OutputHandler.PrintList(parser.InvalidEmails);

            }
            catch (Exception)
            {
                Console.WriteLine("\nThe file name you entered was not found");
            }
            finally
            {
                Console.WriteLine("\nThank you for using this program\nPress any key to exit...");
                Console.ReadKey(true);
            }

        }
    }
}
