//Title:    ISY00245 Major Assignment 2014
//Purpose:  Allows a user to review and manipulate university applications data
//Author:   Jennifer Doherty
//Date:     11 September 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace JDohertyMajor
{
    class Program
    {
        //Create a struct template for the admissions data.
        public struct AdmissionsDetails
        {
            public string universityState; //The state the university is in
            public string universityName; //The university name
            public int admissionsNumber2009; //Number of admissions in 2009
            public int admissionsNumber2010; //Number of admissions in 2010
            public int admissionsNumber2011; //Number of admissions in 2011
            public int admissionsNumber2012; //Number of admissions in 2012
            public int admissionsNumber2013; //Number of admissions in 2013
        }

        static void Main(string[] args)
        {
            const string RAW_FILE_NAME =
                "undergraduateapplicationsoffersandacceptances"
            + "2013appendices.txt"; //Holds the raw data file name
            const string DATABASE_FILE_NAME =
                "applicationsdatabase2013.txt"; //Holds the database file name
            char menuChoice = 'x'; //Holds the user's menu choice

            //Create the database file.
            CreateDatabase(RAW_FILE_NAME, DATABASE_FILE_NAME);

            //Show the instructions to the user.
            DisplayInstructions();

            //Get the user's menu choice and perform that task.
            do
            {
                menuChoice = DisplayMenu(menuChoice); 
                switch (menuChoice)
                {
                    case 'q':
                        EndProgram(); //Finalise the program
                        break;
                    case '1':
                        DisplayData(RAW_FILE_NAME); //Output the raw data
                        break;
                    case '2':
                        DisplayData(DATABASE_FILE_NAME); //Output the database
                        break;
                    case '3':
                        HighestData(DATABASE_FILE_NAME); //Output highest data
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

        //Prompts the user for their chosen menu option and returns it.
        public static char DisplayMenu(char menuChoice)
        { 
            Console.WriteLine
                ("Enter the number of the task you wish to perform "
            + "or q to quit:");
            try
            {
                menuChoice = Convert.ToChar(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Enter the number of the task you wish to perform "
                + "or q to quit:");
            }
            return menuChoice;
        }

        //Opens the specified file and displays the data.
        public static void DisplayData(string fileName)
        {
            string inputLine = ""; //To read and display the file line-by-line
            
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
                Console.WriteLine("File read error");
            }
        }

        //Finalises the program.
        public static void EndProgram()
        {
            Console.WriteLine("Press the enter key to exit");
            Console.ReadLine();
        }

        //Displays the highest number of applications for a given state
        //and year based on user input.
        public static void HighestData(string inFileName)
        {
            const int YEAR_ARRAY_SIZE = 5; //Number of years in file data
            AdmissionsDetails[] admissionsData; //Admissions data - initialised
            string[] stateList; //State list - initialised and loaded
            int admissionsArraySize = 0; //Size required for admissions array
            int stateArraySize = 0; //Number of states in stateList array
            int highestNumber = 0; //The highest number of admissions
            string universityName = ""; //The univeristy with highest number
            string fileInput = ""; //To read the data file line-by-line
            string stateChoice = ""; //The state chosen by the user
            string yearChoice = ""; //The year chosen by the user

            try
            {
                //Determine the size of the admissions array required.
                StreamReader inFileCount = new StreamReader(inFileName);
                fileInput = inFileCount.ReadLine();
                while (fileInput != null)
                {
                    admissionsArraySize++;
                    fileInput = inFileCount.ReadLine();
                }
                inFileCount.Close();
            }
            catch
            {
                Console.WriteLine("File read error");
            }

            //Get the admissions array.
            AdmissionsData(admissionsArraySize, out admissionsData);

            //Get the state array.
            StateArray(out stateList);

            ////Determine the size of the state array.
            stateArraySize = stateList.Length;

            //Create an array of the year options for the data file.
            string[] yearList = new string[YEAR_ARRAY_SIZE];
            {
                yearList[0] = "2009";
                yearList[1] = "2010";
                yearList[2] = "2011";
                yearList[3] = "2012";
                yearList[4] = "2013";
            }

            //Load the admissions array.
            try
            {
                StreamReader inFileRead = new StreamReader(inFileName);
                for (int count = 0; count < admissionsArraySize; count++)
                {
                    fileInput = inFileRead.ReadLine();
                    string[] fileInputParser;
                    fileInputParser = fileInput.Split(';');
                    admissionsData[count].universityState = fileInputParser[0];
                    admissionsData[count].universityName = fileInputParser[1];
                    admissionsData[count].admissionsNumber2009 =
                        Convert.ToInt32(fileInputParser[2]);
                    admissionsData[count].admissionsNumber2010 =
                        Convert.ToInt32(fileInputParser[3]);
                    admissionsData[count].admissionsNumber2011 =
                        Convert.ToInt32(fileInputParser[4]);
                    admissionsData[count].admissionsNumber2012 =
                        Convert.ToInt32(fileInputParser[5]);
                    admissionsData[count].admissionsNumber2013 =
                        Convert.ToInt32(fileInputParser[6]);
                }
                inFileRead.Close();
            }
            catch
            {
                Console.WriteLine("File read error");
            }

            //Get the user's state choice.
            Console.WriteLine("Please choose which state you wish to "
            + "view the highest data for:");
            for (int count = 0; count < stateArraySize; count++)
            {
                Console.WriteLine(count + ". " + stateList[count]);
            }
            try
            {
                stateChoice = (stateList[Convert.ToInt16(Console.ReadLine())]);
            }
            catch
            {
                stateChoice = stateList[0];
                Console.WriteLine("Invalid choice. Default choice: "
                + "{0} selected", stateChoice);
            }

            //Get the user's year choice.
            Console.WriteLine("Please choose which year you wish to "
                + "view the highest data for:");
            for (int count = 0; count < YEAR_ARRAY_SIZE; count++)
            {
                Console.WriteLine(count + ". " + yearList[count]);
            }
            try
            {
                yearChoice = (yearList[Convert.ToInt16(Console.ReadLine())]);
            }
            catch
            {
                yearChoice = yearList[0];
                Console.WriteLine("Invalid choice. Default choice: "
                + "{0} selected", yearChoice);
            }

            foreach (AdmissionsDetails admissions in admissionsData)
            {
                if (admissions.universityState == stateChoice)
                {
                    switch (yearChoice)
                    {
                        case "2009":
                            if (admissions.admissionsNumber2009 > highestNumber)
                            {
                                highestNumber = admissions.admissionsNumber2009;
                                universityName = admissions.universityName;
                            }
                            break;
                        case "2010":
                            if (admissions.admissionsNumber2010 > highestNumber)
                            {
                                highestNumber = admissions.admissionsNumber2010;
                                universityName = admissions.universityName;
                            }
                            break;
                        case "2011":
                            if (admissions.admissionsNumber2011 > highestNumber)
                            {
                                highestNumber = admissions.admissionsNumber2011;
                                universityName = admissions.universityName;
                            }
                            break;
                        case "2012":
                            if (admissions.admissionsNumber2012 > highestNumber)
                            {
                                highestNumber = admissions.admissionsNumber2012;
                                universityName = admissions.universityName;
                            }
                            break;
                        case "2013":
                            if (admissions.admissionsNumber2013 > highestNumber)
                            {
                                highestNumber = admissions.admissionsNumber2013;
                                universityName = admissions.universityName;
                            }
                            break;
                        default:
                            Console.WriteLine
                                ("HighestData failed to calculate");
                            break;
                    }
                }
            }
            
            //Display the result to the user.
            Console.WriteLine
                ("The highest number of applications for {0} in {1} is: "
                + universityName + " with " + highestNumber + " admissions",
                stateChoice, yearChoice);
        }

        //Sets up and loads the array of state options.
        public static void StateArray(out string[] stateArray)
        {
            const int STATE_ARRAY_SIZE = 9; //Number of states in file
            
            //Create an array of the state options for the data file.
            stateArray = new string[STATE_ARRAY_SIZE];
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
        }

        //Sets up and initialises the admissions data array.
        public static void AdmissionsData(int admissionsArraySize, 
            out AdmissionsDetails[] admissionsData)
        {
            admissionsData = new AdmissionsDetails[admissionsArraySize];

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
        }

        //Creates the database file.
        public static void CreateDatabase(string incomingFileName, 
            string outgoingFileName)
        {
            const int ADMISSIONS_ARRAY_STARTING_LINE = 5; //Start of good data
            const int ADMISSIONS_ARRAY_ENDING_LINE = 51; //End of good data
            AdmissionsDetails[] admissionsData; //Admissions data - initialised
            string[] stateList; //State list - initialised and loaded
            string fileInput = ""; //To read the data file line-by-line
            string currentState = ""; //Holds which state is currently selected
            int stateArraySize = 0; //Number of states in file
            int admissionsArraySize = 0; //Admissions array size required
            int lineCounter = 0; //Holds which line in the file the loop is on
            int stateCounter = 0; //Holds the index of the state array
            int admissionsCounter = 0; //Holds index of the admissions array

            //Get the state array.
            StateArray(out stateList);
 
            //Determine the size of the state array.
            stateArraySize = stateList.Length;

            //Determine the admissions array size required by subtracting
            //the extra data.
            admissionsArraySize = ADMISSIONS_ARRAY_ENDING_LINE -
                ADMISSIONS_ARRAY_STARTING_LINE - stateArraySize;

            //Get the admissions array.
            AdmissionsData(admissionsArraySize, out admissionsData);
            
            //Open the incoming data file and load it 
            //into the admissions array.
            try
            {
                StreamReader inFileRead = new StreamReader(incomingFileName);
            
                //Ignore the opening useless data.
                do
                {
                    inFileRead.ReadLine();
                    lineCounter++;
                } while (lineCounter < ADMISSIONS_ARRAY_STARTING_LINE);

                //Load the admissions array.
                for (int count = 0; count < ADMISSIONS_ARRAY_ENDING_LINE; 
                    count++)
                {
                    fileInput = inFileRead.ReadLine();
                    string[] fileInputParser;
                    fileInputParser = fileInput.Split('\t');
                    if (admissionsCounter < admissionsArraySize)
                    {
                        if (stateCounter < stateArraySize)
                        {
                            if (fileInputParser[0] == stateList[stateCounter])
                            {
                                currentState = fileInputParser[0];
                                stateCounter++;
                            }
                            else
                            {
                                admissionsData[admissionsCounter].
                                    universityState =
                                    currentState;
                                admissionsData[admissionsCounter].
                                    universityName =
                                    fileInputParser[0];
                                admissionsData[admissionsCounter].
                                    admissionsNumber2009 =
                                    Int32.Parse(fileInputParser[1].
                                    Replace("\"", ""), 
                                    NumberStyles.AllowThousands);
                                admissionsData[admissionsCounter].
                                    admissionsNumber2010 =
                                    Int32.Parse(fileInputParser[2].
                                    Replace("\"", ""), 
                                    NumberStyles.AllowThousands);
                                admissionsData[admissionsCounter].
                                    admissionsNumber2011 =
                                    Int32.Parse(fileInputParser[3].
                                    Replace("\"", ""), 
                                    NumberStyles.AllowThousands);
                                admissionsData[admissionsCounter].
                                    admissionsNumber2012 =
                                    Int32.Parse(fileInputParser[4].
                                    Replace("\"", ""), 
                                    NumberStyles.AllowThousands);
                                admissionsData[admissionsCounter].
                                    admissionsNumber2013 =
                                    Int32.Parse(fileInputParser[5].
                                    Replace("\"", ""), 
                                    NumberStyles.AllowThousands);
                                admissionsCounter++;
                            }
                        }
                        else
                        {
                            admissionsData[admissionsCounter].
                                    universityState =
                                    currentState;
                            admissionsData[admissionsCounter].
                                universityName =
                                fileInputParser[0];
                            admissionsData[admissionsCounter].
                                admissionsNumber2009 =
                                Int32.Parse(fileInputParser[1].
                                Replace("\"", ""),
                                NumberStyles.AllowThousands);
                            admissionsData[admissionsCounter].
                                admissionsNumber2010 =
                                Int32.Parse(fileInputParser[2].
                                Replace("\"", ""),
                                NumberStyles.AllowThousands);
                            admissionsData[admissionsCounter].
                                admissionsNumber2011 =
                                Int32.Parse(fileInputParser[3].
                                Replace("\"", ""),
                                NumberStyles.AllowThousands);
                            admissionsData[admissionsCounter].
                                admissionsNumber2012 =
                                Int32.Parse(fileInputParser[4].
                                Replace("\"", ""),
                                NumberStyles.AllowThousands);
                            admissionsData[admissionsCounter].
                                admissionsNumber2013 =
                                Int32.Parse(fileInputParser[5].
                                Replace("\"", ""),
                                NumberStyles.AllowThousands);
                            admissionsCounter++;
                        }
                    }
                    else
                    {
                        inFileRead.ReadLine();
                    }
                }
                inFileRead.Close();
            }
            catch
            {
                Console.WriteLine("File read error");
            }

            try
            {
            //Write the admissions array to the outgoing file.
                StreamWriter outFile = new StreamWriter(outgoingFileName);

                for (int count = 0; count < admissionsArraySize; count++)
                {
                    outFile.Write(admissionsData[count].
                        universityState + "; ");
                    outFile.Write(admissionsData[count].
                        universityName + "; ");
                    outFile.Write(admissionsData[count].
                        admissionsNumber2009 + "; ");
                    outFile.Write(admissionsData[count].
                        admissionsNumber2010 + "; ");
                    outFile.Write(admissionsData[count].
                        admissionsNumber2011 + "; ");
                    outFile.Write(admissionsData[count].
                        admissionsNumber2012 + "; ");
                    outFile.WriteLine(admissionsData[count].
                        admissionsNumber2013);
                }
                outFile.Close();
            }
            catch
            {
                Console.WriteLine("File write error");
            }
        }
    }
}