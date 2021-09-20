using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Derivco;

namespace Derivco
{

    public abstract class LogBase                                   //abstract class for our Logger
    {
        public abstract void Log(string Message);
    }

    public class Logger : LogBase                                   //Creating the filpath for our Good Match Log File     
    {
        private string CurrentDirectory { get; set; }

        private string FileName { get; set; }

        private string FilePath { get; set; }

        public Logger()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FileName = "CSVLogFile.txt";
            this.FilePath = this.CurrentDirectory + "/" + this.FileName;
        }

        public override void Log(string Message)                   //Creating Log Template that we can use to Populate the Message
        {
            using (System.IO.StreamWriter streamwriter = System.IO.File.AppendText(this.FilePath))
            {
                streamwriter.Write("\r\nLog Entry: ");
                streamwriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                streamwriter.WriteLine("  :{0}", Message);
                streamwriter.WriteLine("-------------------------------------");
            }
        }
    }




    public class CSVFile
    {
        
        public void csvInput()  //CSV File input and conversion to list
        {
            var logger = new Logger();  //Initializing the Logger and StopWatch
            var watch = new System.Diagnostics.Stopwatch();


            Regex regexCSV = new Regex(@"(.csv)");

            watch.Start(); //Starting stop watch to measure execution time
            Console.WriteLine("Input CSV file path: ");
            string path = Console.ReadLine();
            while(string.IsNullOrEmpty(path)) //Validation to make sure that the User Inputs a filepath
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter a File Path");
                path = Console.ReadLine();

                Console.WriteLine("");

            }



            if (regexCSV.IsMatch(path.ToLower()))     //Using Regular expressions to run a check to see if a .csv file is being input or not
            {
                try
                {
                    using (var reader = new StreamReader(path))  //Using StreamReader to read the input file
                    {
                        List<string> People = new List<string>();
                        List<string> duplicate = new List<string>();


                        while (!reader.EndOfStream)  //Runs until there isnt anymore data to process, this takes the data and splits them into rows
                        {
                            var line = reader.ReadLine();
                            var values = line.Split('\r');
                            var count = values.Count();

                            People.Add(values[0].ToLower());
                            People.Sort();
                            duplicate = People.Distinct().ToList(); //Removes Duplicates from the list
                        }
                        Console.WriteLine("Original Data: " + "\n" + string.Join("; ", People));
                        Console.WriteLine("");
                        Console.WriteLine("People with Duplicates removed: " + "\n" + string.Join(", ", duplicate));
                        Console.WriteLine("");

                        seperateArray(duplicate);
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("CSV file was not found, please enter the correct path" + "\n");
                }
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CSV file was not found, please enter the correct path" + "\n");
            }

            watch.Stop();
            logger.Log($"Execution Time of to Process CSV File while running Sort and Duplicate remover: {watch.ElapsedTicks} Ticks, or {watch.ElapsedMilliseconds}ms");

        }




        static void seperateArray(List<string> peopleArray)  //Function to seperate Sorted List into Male and Female
        {
            var logger = new Logger();
            var watch = new System.Diagnostics.Stopwatch();

            List<string> male = new List<string>();
            List<string> female = new List<string>();

            Regex regexM = new Regex(@",m");  //^(?:;m|M|male|Male|f|F|female|Female)$
            Regex regexF = new Regex(@",f");



            watch.Start(); //Start stopwatch to measure execution time
            for (int i = 0; i < peopleArray.Count; i++)  //Used Regular Expressions to search through Lists for Males and Females
            {
                
                if (regexM.IsMatch(peopleArray[i]))  //If Regex matches Male, then it will add the name to the male list
                {
                    male.Add(peopleArray[i]);
                }

                if (regexF.IsMatch(peopleArray[i])) //If Regex matches Female, then it will add the name to the Female list
                {
                    female.Add(peopleArray[i]);
                }
               
            }

            if (male.Count == 0 && female.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Seems like something went wrong, please make sure that your CSV file has the right delimiters (,)");
            }

            watch.Stop();
            logger.Log($"Execution Time of to Seperate CSV List into Male and Female: {watch.ElapsedTicks} Ticks, or {watch.ElapsedMilliseconds}ms");



            Console.WriteLine("Females: " + string.Join(", ", female));                 //Printing the Female and Male Groups
            Console.WriteLine("Males: " + string.Join(", ", male) + "\n" + "\n");


                goodMatchNames(peopleArray, male, female);
            
        }




        //Modified Good Match Process to run the Male and Female Lists through
        public static void goodMatchNames(List<string> peopleNumber ,List<string> maleName, List<string> femaleName)
        {
            var logger = new Logger();
            var watch = new System.Diagnostics.Stopwatch();

            List<string> mName = new List<string>();
            mName = maleName;
            List<string> fName = new List<string>();
            fName = femaleName;
            string sentence;
            List<int> noOfCount = new List<int>();
            string output = "";
            int Counter = 0; ;

            Console.WriteLine("Good Match Process: ");


            watch.Start();
            if(maleName.Count < femaleName.Count)  //if statement to remove an error that occured as sometimes the amount of males are not the same amount as females and vice versa
            {
                Counter += maleName.Count;
            }
            else
            {
                Counter += femaleName.Count;
            }



            for (int k = 0; k < Counter ; k++)  //For Loop to Loop through each Male and Female name and run the Good Match process on it to return an array of occurence numbers
            {
                sentence = (mName[k] + " matches " + fName[k]).ToLower();

                //Remove the empty spaces and all the variantions of Male and Female for a more accurate Calculation
                sentence = sentence.Replace(" ", string.Empty).Replace(";m", string.Empty).Replace(";f", string.Empty).Replace(";M", string.Empty).Replace(";M", string.Empty);
                sentence = sentence.Replace(",m", string.Empty).Replace(",f", string.Empty).Replace(",M", string.Empty).Replace(",F", string.Empty);
                sentence = sentence.Replace(",male", string.Empty).Replace(",female", string.Empty).Replace(",Male", string.Empty).Replace(",Female", string.Empty);

                while (sentence.Length > 0) //Check to make sure that the sentence isnt in a negative Index
                {
                    int count = 0;
                    for (int j = 0; j < sentence.Length; j++)
                    {
                        if (sentence[0] == sentence[j])
                        {
                            count++;
                        }
                    }
                    noOfCount.Add(count);
                    sentence = sentence.Replace(sentence[0].ToString(), string.Empty);  //Clears sentence (which is the occurence number) to get ready for the next letter occurences
                }

                int[] totalnum = noOfCount.ToArray();
                Console.WriteLine("Your array for this sentence is: " + String.Join(",", totalnum));
                

                PercentageCalc(totalnum, mName, fName);


                noOfCount.Clear();



                //Modified PercentageCalc Process
                void PercentageCalc(int[] totalArray, List<string> nameFirst, List<string> nameLast)
                {
                    List<string> lisArray = new List<string>();
                    List<int> intArray = new List<int>();
                    int runvalue = totalArray.Length / 2;
                    int first = 0;
                    int last = 0;
                    int sum = 0;
                    string result = "";
                    string finalResult = "";
                    string saveOutput = "";



                    for (int j = 0; j < runvalue; j++)                          //first loop that loops through the array being passed in and the outcome is stored as a string
                    {
                        first = totalArray[j];
                        last = (totalArray[totalArray.Length - j - 1]);

                        sum = first + last;

                        result += sum.ToString();

                    }



                    if (totalArray.Length % 2 != 0)                             //this is a check to see if the array was odd or even, if it was odd then it will add the number where the division occured to the end 
                    {                                                           //as that number wouldnt be counted in the sum
                        result = result + (totalArray[runvalue]).ToString();
                    }



                    while (result.Length != 2)                                  //this is a while loop that runs until the result is a double digit number
                    {
                        lisArray.Clear();
                        for (int f = 0; f < result.Length; f++)                 //Loop 1 takes the result and adds it into a string Array
                        {
                            string number = result[f].ToString();

                            lisArray.Add(number);
                        }


                        intArray.Clear();
                        for (int g = 0; g < lisArray.Count; g++)                //Loop 2 takes the string Array and converts it into a Integer Array
                        {
                            int number = Convert.ToInt32(lisArray[g]);

                            intArray.Add(number);
                        }


                        for (int i = 0; i < intArray.Count / 2; i++)            //Loop 3 takes the Integer Array and runs the Modified Good Match Process on it
                        {                                                       //at the end of the loop, the previous result is cleared so the new result can be assigned to it
                            first = intArray[i];

                            last = (intArray[intArray.Count - i - 1]);

                            sum = first + last;

                            finalResult += sum.ToString();

                            result = "";
                        }


                        result += finalResult;                                  //The finalResult of the Good Match Process gets assigned to the result
                        finalResult = "";


                        if (intArray.Count % 2 != 0)                            //Similar to above, this runs a check on the arrays length, if odd this executes
                        {
                            result = result + (intArray[intArray.Count / 2]).ToString();
                        }
                    }

                    sum = Convert.ToInt32(result);



                    if (result.Length > 1)      //Here you will see that I put an extra variable "saveOutput", I used this to copy the message that would be outputted to the Console
                                                //that way I can use the same messaged to store in the output.txt file
                    {
                        if (sum >= 80)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine((string.Join(", ", nameFirst[k])) + " matches " + (string.Join(", ", nameLast[k])) + " " + sum + "%" + ", " + " Good Match!" + "\n");
                            saveOutput += (string.Join(", ", nameFirst[k])) + " matches " + (string.Join(", ", nameLast[k])) + " " + sum + "%" + ", " + " Good Match!" + "\n";
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine((string.Join(", ", nameFirst[k])) + " matches " + (string.Join(", ", nameLast[k])) + " " + sum + "%" + "\n");
                            saveOutput += (string.Join(", ", nameFirst[k])) + " matches " + (string.Join(", ", nameLast[k])) + " " + sum + "%" + "\n";
                        }
                        output += saveOutput; //assign "saveOutput" to "output" as it will be easies to call when passing it into the next method
                    }
                    else
                    {
                        Console.WriteLine("Your Inputs in your CSV file was not long enough for the calculation to execute, please try again, here is your result: " + result);
                    }

                }

                watch.Stop();
                logger.Log($"Execution Time of to Process CSV File Good Match process on Each Male and Female Pair: {watch.ElapsedTicks} Ticks, or {watch.ElapsedMilliseconds}ms");
            }

            exportTxt(output);


            static void exportTxt(string outputText)  //Method to export to the output.txt file
            {
                try
                {

                    StreamWriter tw = new StreamWriter("output.txt");
                    tw.WriteLine("Good Match Process: ");
                    tw.WriteLine(outputText);
                    tw.Close();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Awesome! your output.txt file has been exported, look in the bin folder located in the Application Directory");
                }
                catch (Exception e)
                {

                    Console.Write("Exception: " + e.Message);
                }

                Console.Read();
            }

            
        }




        

    }
}


