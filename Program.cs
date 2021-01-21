using System;
using System.Collections; 
using System.Text.RegularExpressions;

namespace TechnicalAssignment
{
    class Program
    {

        static void Main(string[] args)
        {
            //Asking user for file name
            Console.WriteLine("\nHello! Please enter the name of the .csv file:");
            string path = Console.ReadLine();
            
            ArrayList valid = new ArrayList();
            ArrayList invalid = new ArrayList();

            //Finding file
            try 
            {
                //Parsing file
                var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(path); 
                parser.SetDelimiters(new string[] {","});
                parser.ReadLine();

                Regex pattern = new Regex(@"^[a-z]\w*(\.\w+)*@[\w]*[.][\w]*");
                while(!parser.EndOfData) 
                {
                    //Validating emails
                    string[] line = parser.ReadFields();
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
