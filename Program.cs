using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.VisualBasic.FileIO; 

namespace TechnicalAssignment
{
    //Class to represent the CSV file 
    class CSVFile
    {
        public CSVParser Parser { get; }
        public CSVData Data { get; }

        public CSVFile(string path)
        {
            //passing file path into parser
            Parser = new CSVParser(path);
            Data = new CSVData(); 
        }

        //Method to initiate parsing of the CSV file
        public void ParseCSV()
        {
            Parser.Parse(Data); 
        }
    }

    //Class to handle parsing csv files
    class CSVParser
    {
        public TextFieldParser Parser { get; }

        public CSVParser(string path)
        {
            Parser = new TextFieldParser(path); 
        }

        //Parses the CSV and adds the data into the CSVData object that is passed in
        public void Parse(CSVData data)
        {
            Parser.SetDelimiters(new string[] { "," });
            //Setting Fields in CSVData object
            data.Fields = Parser.ReadFields().ToList();

            int index = 0;
            while (!Parser.EndOfData)
            {
                //adding each entry into the CSVData object and validating emails
                string[] line = Parser.ReadFields();
                List<string> entry = line.ToList();
                data.AddEntry(entry);
                EmailValidator.ValidateEmail(data, index++, entry[2]);
            }
        }

    }

    //Class to handle, store, and classify data of CSV
    //Allows for data of more than three columns if needed
    class CSVData
    {
        public List<string> Fields { get; set; }
        public List<List<string>> Entries { get; }   
        public List<int> ValidEmails { get; }
        public List<int> InvalidEmails { get; }

        public CSVData()
        {
            Entries = new List<List<string>>();
            ValidEmails = new List<int>();
            InvalidEmails = new List<int>();
        }

        public void AddEntry(List<string> entry)
        {
            Entries.Add(entry); 
        }

        public void AddValidEmail(int index)
        {
            ValidEmails.Add(index); 
        }

        public void AddInvalidEmail(int index)
        {
            InvalidEmails.Add(index);
        }
    }
    
    //Class to handle email validations
    class EmailValidator
    {
        //Email Regex to match with emails
        static readonly Regex PATTERN = new Regex(@"^[a-z]\w*(\.\w+)*@[\w]*[.][\w]*");

        // Validates an email and adds the index of the email in the appropriate list
        public static void ValidateEmail(CSVData data, int index, string email)
        {
            if (PATTERN.IsMatch(email))
            {
                data.AddValidEmail(index);
            }
            else
            {
                data.AddInvalidEmail(index);
            }
        }

    }

    //Class to handle outputs for data 
    class OutputHandler
    {
        //Print a column filtered by a given list of indices
        public static void PrintColumnByIndices(CSVData data, int column, List<int> indices)
        {
            indices.ForEach(index => Console.WriteLine(data.Entries[index][column]));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Asking user for file name
            Console.WriteLine("\nHello! Please enter the name or path of the .csv file:");
            string path = Console.ReadLine();

            try
            {
                //Parsing file with path given by user
                CSVFile csv = new CSVFile(path);
                csv.ParseCSV();

                //Printing valid and invalid emails
                Console.WriteLine("\nValid Emails:");
                OutputHandler.PrintColumnByIndices(csv.Data, 2, csv.Data.ValidEmails);
                Console.WriteLine("\nInvalid Emails:");
                OutputHandler.PrintColumnByIndices(csv.Data, 2, csv.Data.InvalidEmails);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\nThe file name entered was not found");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("\nA file name was not entered");
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
