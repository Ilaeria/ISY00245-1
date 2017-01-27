//Title:    ISY00245 Major Assignment 2014
//Purpose:  Allows a user to review and filter university applications data.
//Author:   Jennifer Doherty
//Date:     

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JDohertyMajor
{
    class Program
    {
        static void Main(string[] args)
        {
            const string RAW_FILE_NAME = 
                "undergraduateapplicationsoffersandacceptances"
            + "2013appendices.txt"; //Holds the raw data file name
            const string DATABASE_FILE_NAME = 
                "applicationsdatabase2013.txt"; //Holds the database file name
            int applicationsArraySize = 0; //Holds the size of the data array
            char menuChoice = 'q'; //Holds the user's menu choice
            
            DisplayInstructions(); //Show the instructions to the user

            do
            {
                menuChoice = DisplayMenu(); //Get the user's menu choice
                switch (menuChoice)
                {
                    case 'q':
                        EndProgram();
                        break;
                    case '1':
                        DisplayData(RAW_FILE_NAME);
                        break;
                    case '2':
                        Console.WriteLine("Display the database data");
                        Console.ReadLine();
                        break;
                    case '3':
                        Console.WriteLine("Display the highest data");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Please choose from the menu",
                                         menuChoice);
                        break;
                }
            } while (menuChoice != 'q');
        }

       //Display instructions to the user
        public static void DisplayInstructions()
        {
            Console.WriteLine("AUSTRALIAN UNIVERSITY APPLICATIONS DATA");
            Console.WriteLine("Ths program handles Australian university");
            Console.WriteLine("applications data for 2009-2013.");
            Console.WriteLine("It can do the following tasks:");
            Console.WriteLine("1. Display the raw data");
            Console.WriteLine("2. Display the data in database format");
            Console.WriteLine("3. Display the highest number of applications "
                + "for a given state and year");
        }

        //Prompts the user for their chosen menu option
        public static char DisplayMenu()
        {
            char menuChoice = 'q'; //Holds the user's menu choice

            Console.WriteLine
              ("Enter the number of the task you wish to perform "
            + "or q to quit:");
            menuChoice = Convert.ToChar(Console.ReadLine());
            return menuChoice;
        }

        //Displays the data from the specified file
        public static void DisplayData(string fileName)
        {
            string inputLine = ""; //Holds the contents of each file line
            
            Console.WriteLine("Display the raw data");
            try
            {
                StreamReader inFile = new StreamReader(fileName);
                inputLine = inFile.ReadLine();
                while (inputLine != null)
                {
                    inputLine = inFile.ReadLine();
                    Console.WriteLine(inputLine);
                }
                inFile.Close();

            }
            catch
            {
                Console.WriteLine("File error");
            }
        }

        //Finalise program
        public static void EndProgram()
        {
            Console.WriteLine("Press the enter key to exit");
            Console.ReadLine();
        }
    }
}
