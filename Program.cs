﻿using System;
using System.Collections; 
using System.Text.RegularExpressions;

namespace TechnicalAssignment
{
    class Program
    {

        static void Main(string[] args)
        {
            //Asking user for file name
            Console.WriteLine("\nHello! Please enter the name of the .csv file...");
            string path = Console.ReadLine(); ;
            
            ArrayList valid = new ArrayList();
            ArrayList invalid = new ArrayList();
            // string path = "test.csv";

            //Finding file
            try 
            {
                var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(path); 
                parser.SetDelimiters(new string[] {";"});
                parser.ReadLine();

                //Parsing file
                Regex pattern = new Regex(@"^[a-z]\w*(\.\w+)*@[\w]*[.][\w]*");
                while(!parser.EndOfData) 
                {
                    //Validating emails
                    string[] line = parser.ReadLine().Split(",");
                    if(pattern.IsMatch(line[2]))
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
                foreach(string mail in valid)
                {
                    Console.WriteLine(mail);
                }
                Console.WriteLine("\nInvalid Emails:");
                foreach(string mail in invalid)
                {
                    Console.WriteLine(mail);
                }

            } 
            catch(Exception) 
            {
                Console.WriteLine("\nThe name you entered was invalid");
            }
            finally 
            {
                Console.WriteLine("\nThank you for using this program\nPress any key to exit...");
                Console.ReadKey(true);
            }
        }
    }
}