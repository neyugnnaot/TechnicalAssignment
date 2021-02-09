using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO; 

namespace TechnicalAssignment
{
    //Class to represent CSV file
    class CSVFile
    {
        public CSVParser Parser { get; set; }
        public CSVData Data { get; set; }

        public CSVFile(string path)
        {
            //passing file path into parser
            Parser = new CSVParser(path);
            Data = new CSVData(); 
        }

        public void ParseCSV()
        {
            Parser.ParseEmails(Data.ValidEmails, Data.InvalidEmails); 
        }
    }

    //Class to handle parsing csv files
    class CSVParser
    {
        public TextFieldParser Parser { get; set; }

        public CSVParser(string path)
        {
            Parser = new TextFieldParser(path); 
        }

        public void ParseEmails(List<string> valid, List<string> invalid)
        {
            Parser.SetDelimiters(new string[] { "," });
            //To ignore the first line
            Parser.ReadLine();

            while (!Parser.EndOfData)
            {
                //Validating emails
                string[] line = Parser.ReadFields();
                string email = line[2];

                if (EmailValidator.ValidateEmail(email))
                {
                    valid.Add(email);
                }
                else
                {
                    invalid.Add(email);
                }
            }
        }

    }

    //Class to store data of CSV
    class CSVData
    {
        //Unused but added to represent full rows in case for future 
        public List<(string first, string last, string email)> Entries { get; set; }
        
        public List<string> ValidEmails { get; set; }
        public List<string> InvalidEmails { get; set; }

        public CSVData()
        {
            Entries = new List<(string, string, string)>();
            ValidEmails = new List<string>();
            InvalidEmails = new List<string>();
        }
    }
    
    //Class to handle email validations
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
                //Parsing file with path given by user
                CSVFile csv = new CSVFile(path);
                csv.ParseCSV();

                //Printing valid and invalid emails
                Console.WriteLine("\nValid Emails:");
                OutputHandler.PrintList(csv.Data.ValidEmails);
                Console.WriteLine("\nInvalid Emails:");
                OutputHandler.PrintList(csv.Data.InvalidEmails);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\nThe file name you entered was not found");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("\nYou did not enter a file name");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("\nThank you for using this program\nPress any key to exit...");
                Console.ReadKey(true);
            }

        }
    }
}
