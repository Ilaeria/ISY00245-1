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
        //Create a struct template for the admissions data.
        public struct AdmissionsDetails
        {
            public string universityState; //The state the university is located in
            public string universityName; //The university name
            public int admissionsNumber2009; //Number of admissions in 2009
            public int admissionsNumber2010; //Number of admissions in 2010
            public int admissionsNumber2011; //Number of admissions in 2011
            public int admissionsNumber2012; //Number of admissions in 2012
            public int admissionsNumber2013; //Number of admissions in 2013
        }

        //Create a temporary holding template for the admissions data.
        public struct TempAdmissionsDetails
        {
            public string admissionsLine; //One line from the admissions file
        }

        static void Main(string[] args)
        {
            const string RAW_FILE_NAME =
                "undergraduateapplicationsoffersandacceptances"
            + "2013appendices.txt"; //Holds the raw data file name
            const string DATABASE_FILE_NAME =
                "applicationsdatabase2013.txt"; //Holds the database file name
            char menuChoice = 'q'; //Holds the user's menu choice

            //Create the database
            CreateDatabase(RAW_FILE_NAME, DATABASE_FILE_NAME); 
            
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
                        DisplayData(DATABASE_FILE_NAME);
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

       //Displays instructions to the user.
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

        //Prompts the user for their chosen menu option.
        public static char DisplayMenu()
        {
            char menuChoice = 'q'; //Holds the user's menu choice

            Console.WriteLine
              ("Enter the number of the task you wish to perform "
            + "or q to quit:");
            menuChoice = Convert.ToChar(Console.ReadLine());
            return menuChoice;
        }

        //Displays the data from the specified files.
        public static void DisplayData(string fileName)
        {
            string inputLine = ""; //Holds the contents of each file line
            
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

        //Finalises the program.
        public static void EndProgram()
        {
            Console.WriteLine("Press the enter key to exit");
            Console.ReadLine();
        }

        //Creates the database array.
        public static void CreateDatabase(string incomingFileName, 
            string outgoingFileName)
        {
            string fileInput = ""; //Holds the incoming file data
            int ArraySize = 0; //Admissions array size
            
            //Open a data file and load it into an array.
            try
            {
                StreamReader inFileCount = new StreamReader(incomingFileName);

                //Determine the size of the array required.
                while (fileInput != null)
                {
                    fileInput = inFileCount.ReadLine();
                    ArraySize++;
                }

                inFileCount.Close();

                StreamReader inFileRead = new StreamReader(incomingFileName);
                //Create the array.                
                TempAdmissionsDetails[] admissionsData =
                    new TempAdmissionsDetails[ArraySize];
                for (int count = 0; count < ArraySize; count++)
                {
                    admissionsData[count].admissionsLine = inFileRead.ReadLine();
                }

                inFileRead.Close();

                StreamWriter outFile = new StreamWriter(outgoingFileName);

                for (int count = 0; count < ArraySize; count++)
                {
                    outFile.WriteLine(admissionsData[count].admissionsLine);
                }

                outFile.Close();
            }
            catch
            {
                Console.WriteLine("File error");
            }
        }
    }
}
