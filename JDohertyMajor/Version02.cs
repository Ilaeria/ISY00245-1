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
            
            DisplayInstructions(); //Show the instructions to the user

            DisplayMenu(); //Prompts the user for the menu option

            Console.ReadLine();
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
        public static void DisplayMenu()
        {
            char menuChoice = 'q'; //Holds the user's menu choice
            
            do
            {
                Console.WriteLine
                  ("Enter the number of the task you wish to perform "
                + "or q to quit");
                menuChoice = Convert.ToChar(Console.ReadLine());
                switch (menuChoice)
                {
                    case 'q':
                        Console.WriteLine("Ending program");
                        Console.ReadLine();
                        break;
                    case '1':
                        Console.WriteLine("Display the raw data");
                        Console.ReadLine();
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
    }
}
