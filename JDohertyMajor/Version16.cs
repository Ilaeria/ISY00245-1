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
using System.Globalization;

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

        static void Main(string[] args)
        {
            const string RAW_FILE_NAME =
                "undergraduateapplicationsoffersandacceptances"
            + "2013appendices.txt"; //Holds the raw data file name
            const string DATABASE_FILE_NAME =
                "applicationsdatabase2013.txt"; //Holds the database file name
            char menuChoice = 'q'; //Holds the user's menu choice

            //Create the database.
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
                        EndProgram();
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

        //Prompts the user for their chosen menu option.
        public static char DisplayMenu(char menuChoice)
        { 
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

        //Displays the highest number of applications for a given state
        //and year based on user input.
        public static void HighestData(string inFileName)
        {
            int admissionsArraySize = 0; //Holds the size of the admissions array
            string fileInput = ""; //Holds the incoming file data
            AdmissionsDetails[] admissionsData; //Holds the admissions data - already initialised
            string[] stateList; //Holds the state array - already initialised
            int stateArraySize = 0; //Number of states in file
            string stateChoice = ""; //Holds the value of the chosen state
            string yearChoice = ""; //Holds the value of the chosen year
            const int YEAR_ARRAY_SIZE = 5; //Number of years in file
            int stateYearArraySize = 0; //Holds how many entries for a given state and year
            int stateYearArrayIndex = 0; //Holds the index of the stateYearArray

            try
            {
                //Determine the size of the array required.
                StreamReader inFileCount = new StreamReader(inFileName);
                fileInput = inFileCount.ReadLine();
                while (fileInput != null)
                {
                    admissionsArraySize++;
                    fileInput = inFileCount.ReadLine();
                }

                inFileCount.Close();

                //Get the admissions array.
                AdmissionsData(admissionsArraySize, out admissionsData);

                //Get the state array.
                StateArray(out stateList);

                //Determine the size of the state array.
                foreach (string state in stateList)
                    stateArraySize++;

                //Create an array of the year options for the data file.
                string[] yearList = new string[YEAR_ARRAY_SIZE];
                {
                    yearList[0] = "2009";
                    yearList[1] = "2010";
                    yearList[2] = "2011";
                    yearList[3] = "2012";
                    yearList[4] = "2013";
                }

                //Load the array.
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

                //Get the user's state choice.
                Console.WriteLine("Please choose which state you wish to "
                + "view the highest data for:");
                for (int count = 0; count < stateArraySize; count++)
                {
                    Console.WriteLine(count + ". " + stateList[count]);
                }
                stateChoice = (stateList[Convert.ToInt16(Console.ReadLine())]);

                //Get the user's year choice.
                Console.WriteLine("Please choose which year you wish to "
                    + "view the highest data for:");
                for (int count = 0; count < YEAR_ARRAY_SIZE; count++)
                {
                    Console.WriteLine(count + ". " + yearList[count]);
                }
                yearChoice = (yearList[Convert.ToInt16(Console.ReadLine())]);

                //Set up an array of the chosen state and year values.
                for (int count = 0; count < admissionsArraySize; count++)
                    if (admissionsData[count].universityState == stateChoice)
                    {
                        stateYearArraySize++; //Determine the array size needed
                    }

                int[] stateYearArray = new int[stateYearArraySize];
                for (int count = 0; count < stateYearArraySize; count++)
                {
                    stateYearArray[count] = 0; //Initialise the array
                }

                //Load the array with the data for the chosen state and year.
                for (int count = 0; count < admissionsArraySize; count++)
                    if (admissionsData[count].universityState == stateChoice)
                    {
                        switch (yearChoice)
                        {
                            case "2009":
                                stateYearArray[stateYearArrayIndex] = admissionsData[count].admissionsNumber2009;
                                stateYearArrayIndex++;
                                break;
                            case "2010":
                                stateYearArray[stateYearArrayIndex] = admissionsData[count].admissionsNumber2010;
                                stateYearArrayIndex++;
                                break;
                            case "2011":
                                stateYearArray[stateYearArrayIndex] = admissionsData[count].admissionsNumber2011;
                                stateYearArrayIndex++;
                                break;
                            case "2012":
                                stateYearArray[stateYearArrayIndex] = admissionsData[count].admissionsNumber2012;
                                stateYearArrayIndex++;
                                break;
                            case "2013":
                                stateYearArray[stateYearArrayIndex] = admissionsData[count].admissionsNumber2013;
                                stateYearArrayIndex++;
                                break;
                            default:
                                Console.WriteLine("stateYearArray failed to complete");
                                break;
                        }
                    }
            }
            catch
            {
                Console.WriteLine("File read error");
            }
        }

        //Set up the array of state options.
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

        //Set up the admissions data array.
        public static void AdmissionsData(int admissionsArraySize, out AdmissionsDetails[] admissionsData)
        {
            admissionsData = new AdmissionsDetails[admissionsArraySize];
            {
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
        }

        //Creates the database file.
        public static void CreateDatabase(string incomingFileName, 
            string outgoingFileName)
        {
            int stateArraySize = 0; //Number of states in file
            const int ADMISSIONS_ARRAY_STARTING_LINE = 5; //Start of good data
            const int ADMISSIONS_ARRAY_ENDING_LINE = 51; //End of good data
            string fileInput = ""; //Holds the incoming file data
            string currentState = ""; //Holds which state is currently selected
            int admissionsArraySize = 0; //Admissions array size required
            int lineCounter = 0; //Holds which line in the file the loop is on
            int stateCounter = 0; //Holds the current state in the state array
            int admissionsCounter = 0; //Holds the admissions array counter
            string[] stateList; //Holds the state array - already initialised
            AdmissionsDetails[] admissionsData; //Holds the admissions data - already initialised

            //Get the state array.
            StateArray(out stateList);
 
            //Determine the size of the state array.
            foreach (string state in stateList)
                stateArraySize++;

            //Determine the array size required by subtracting
            //the extra data.
            admissionsArraySize = ADMISSIONS_ARRAY_ENDING_LINE -
                ADMISSIONS_ARRAY_STARTING_LINE - stateArraySize;

            //Get the admissions array.
            AdmissionsData(admissionsArraySize, out admissionsData);
            
            //Open the incoming data file and load it into an array.
            try
            {
                StreamReader inFileRead = new StreamReader(incomingFileName);

                //Ignore the opening useless data.
                do
                {
                    inFileRead.ReadLine();
                    lineCounter++;
                } while (lineCounter < ADMISSIONS_ARRAY_STARTING_LINE);

                //Load the array.
                for (int count = 0; count < ADMISSIONS_ARRAY_ENDING_LINE; count++)
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
                                admissionsData[admissionsCounter].universityState =
                                    currentState;
                                admissionsData[admissionsCounter].universityName =
                                    fileInputParser[0];
                                admissionsData[admissionsCounter].admissionsNumber2009 =
                                    Int32.Parse(fileInputParser[1].Replace("\"", ""), NumberStyles.AllowThousands);
                                admissionsData[admissionsCounter].admissionsNumber2010 =
                                    Int32.Parse(fileInputParser[2].Replace("\"", ""), NumberStyles.AllowThousands);
                                admissionsData[admissionsCounter].admissionsNumber2011 =
                                    Int32.Parse(fileInputParser[3].Replace("\"", ""), NumberStyles.AllowThousands);
                                admissionsData[admissionsCounter].admissionsNumber2012 =
                                    Int32.Parse(fileInputParser[4].Replace("\"", ""), NumberStyles.AllowThousands);
                                admissionsData[admissionsCounter].admissionsNumber2013 =
                                    Int32.Parse(fileInputParser[5].Replace("\"", ""), NumberStyles.AllowThousands);
                                admissionsCounter++;
                            }
                        }
                        else
                        {
                            admissionsData[admissionsCounter].universityState =
                                currentState;
                            admissionsData[admissionsCounter].universityName =
                                fileInputParser[0];
                            admissionsData[admissionsCounter].admissionsNumber2009 =
                                Int32.Parse(fileInputParser[1].Replace("\"", ""), NumberStyles.AllowThousands);
                            admissionsData[admissionsCounter].admissionsNumber2010 =
                                Int32.Parse(fileInputParser[2].Replace("\"", ""), NumberStyles.AllowThousands);
                            admissionsData[admissionsCounter].admissionsNumber2011 =
                                Int32.Parse(fileInputParser[3].Replace("\"", ""), NumberStyles.AllowThousands);
                            admissionsData[admissionsCounter].admissionsNumber2012 =
                                Int32.Parse(fileInputParser[4].Replace("\"", ""), NumberStyles.AllowThousands);
                            admissionsData[admissionsCounter].admissionsNumber2013 =
                                Int32.Parse(fileInputParser[5].Replace("\"", ""), NumberStyles.AllowThousands);
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
            //Write the array to the outgoing file.
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
