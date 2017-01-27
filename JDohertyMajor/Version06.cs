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
                menuChoice = DisplayMenu(menuChoice); //Get the user's menu choice
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
        public static char DisplayMenu(char menuChoice)
        {
            menuChoice = 'q'; //Holds the user's menu choice

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
            const int STATE_ARRAY_SIZE = 9; //Number of states in file
            string fileInput = ""; //Holds the incoming file data
            int admissionsArraySize = 0; //Admissions array size

            //Create an array of the state options for the data file.
            string[] stateArray = new string[STATE_ARRAY_SIZE];
            {
                stateArray[0] = "New South Wales";
                stateArray[1] = "Victoria";
                stateArray[2] = "Queensland";
                stateArray[3] = "Western Australia";
                stateArray[4] = "South Australia";
                stateArray[5] = "Northern Territory";
                stateArray[6] = "Tasmania";
                stateArray[7] = "Australian Capital Territory";
                stateArray[8] = "Multi-State";
            }
            
            //Open the incoming data file and load it into an array.
            try
            {
                StreamReader inFileCount = new StreamReader(incomingFileName);

                //Determine the size of the array required.
                while (fileInput != null)
                {
                    fileInput = inFileCount.ReadLine();
                    admissionsArraySize++;
                }

                inFileCount.Close();

                ////Create the array. 
                //StreamReader inFileRead = new StreamReader(incomingFileName);     
                //TempAdmissionsDetails[] admissionsData =
                //    new TempAdmissionsDetails[admissionsArraySize];
                //for (int count = 0; count < admissionsArraySize; count++)
                //{
                //    admissionsData[count].admissionsLine = inFileRead.ReadLine();
                //}

                //inFileRead.Close();

                //Create and initialise the array.
                AdmissionsDetails[] admissionsData = 
                    new AdmissionsDetails[admissionsArraySize];
                for (int count = 0; count < admissionsArraySize; count++)
                {
                    admissionsData[count].universityState = "x"; 
                    admissionsData[count].universityName = "x"; 
                    admissionsData[count].admissionsNumber2009 = 0; 
                    admissionsData[count].admissionsNumber2010 = 0; 
                    admissionsData[count].admissionsNumber2011 = 0; 
                    admissionsData[count].admissionsNumber2012 = 0; 
                    admissionsData[count].admissionsNumber2013 = 0; 
                }

                ////Load the array with the incoming file data.
                //StreamReader inFileRead = new StreamReader(incomingFileName);

                //Write the array to the outgoing file.
                StreamWriter outFile = new StreamWriter(outgoingFileName);

                for (int count = 0; count < admissionsArraySize; count++)
                {
                    outFile.Write(admissionsData[count].
                        universityState + ", ");
                    outFile.Write(admissionsData[count].
                        universityName + ", ");
                    outFile.Write(admissionsData[count].
                        admissionsNumber2009 + ", ");
                    outFile.Write(admissionsData[count].
                        admissionsNumber2010 + ", ");
                    outFile.Write(admissionsData[count].
                        admissionsNumber2011 + ", ");
                    outFile.Write(admissionsData[count].
                        admissionsNumber2012 + ", ");
                    outFile.WriteLine(admissionsData[count].
                        admissionsNumber2013);
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
