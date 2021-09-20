using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Derivco;

namespace Derivco
{
    class Program
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
                this.FileName = "GoodMatchLogFile.txt";
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


        //Wrote an if statement in the Main for the user to choose which process they would like to go to
        public static void Main(string[] args)  
        {
            var logger = new Logger();


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to my Good Match App!      Created by Ratul Ramdhani");
            Console.WriteLine("");
            while (true)
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Please Indicate which option you would like by typing either 1 or 2: ");
                Console.WriteLine("Option 1: Good Match");
                Console.WriteLine("Option 2: CSV File");
                string charinput = Console.ReadLine();
                Console.WriteLine("");

                if (charinput.Equals("1"))
                {
                    GoodMatch();
                }
                else if (charinput.Equals("2"))
                {
                    var instance = new CSVFile();
                    instance.csvInput();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please use the correct Charcters ( 1 or 2 )");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press Enter Key to go back to back to Menu ....");
                    Console.ReadLine();
                }
            }
        }



        public static void GoodMatch()  //The Function for the Good Match Process
        {
            var logger = new Logger();
            var watch = new System.Diagnostics.Stopwatch();

            string firstName;
            string lastName;
            string sentence;
            List<int> noOfCount = new List<int>();


            Console.Write("Please enter the first name: ");
            firstName = Console.ReadLine();
            while (string.IsNullOrEmpty(firstName))  //While loop to make sure that a name gets inserted
            {
                Console.WriteLine("First Name Cant be empty, Input your name once more please");
                firstName = Console.ReadLine();
            }


            Console.Write("Please enter second name: ");
            lastName = Console.ReadLine();
            while (string.IsNullOrEmpty(lastName))  //While loop to make sure that a name gets inserted
            {
                Console.WriteLine("Last Name Cant be empty, Input your name once more please");
                lastName = Console.ReadLine();
            }


            sentence = (firstName + "matches" + lastName).ToLower();


            //Remove the empty spaces from the message
            sentence = sentence.Replace(" ", string.Empty);

            watch.Start();
            while (sentence.Length > 0)
            {
                Console.Write(sentence[0] + " : ");
                int count = 0;
                for (int j = 0; j < sentence.Length; j++)    //for loop that iterates through each letter in the loop, if that letter occurs again, it will increment the count for that letter
                {
                    if (sentence[0] == sentence[j])
                    {
                        count++;
                    }
                }                                           //once that letter is done, the number will get added to a list and then the count will be cleared for the next letter
                noOfCount.Add(count);
                Console.WriteLine(count);
                sentence = sentence.Replace(sentence[0].ToString(), string.Empty);
            }
            watch.Stop();
            logger.Log($"Execution Time of Good Match process: {watch.ElapsedTicks} Ticks, or {watch.ElapsedMilliseconds}ms");  //Logging how long this Good Match Process took to run

            int[] totalnum = noOfCount.ToArray();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Your array for this sentence is: " + String.Join(",", totalnum) + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press the Enter Key to find out the percentage match of your two names" + "\n");
            Console.ReadKey();

            PercentageCalc(totalnum, firstName, lastName);
        }





        static void PercentageCalc(int[] totalArray, string nameFirst, string nameLast)  //Function to calculate the percentage match
        {
            var logger = new Logger();
            var watch = new System.Diagnostics.Stopwatch();

            List<string> lisArray = new List<string>();
            List<int> intArray = new List<int>();
            int runvalue = totalArray.Length / 2;
            int first = 0;
            int last = 0;
            int sum = 0;
            string result = "";
            string finalResult = "";

            watch.Start(); //start watch
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
            watch.Stop();
            logger.Log($"Execution Time of Percentage Match Calculator: {watch.ElapsedTicks} Ticks, or {watch.ElapsedMilliseconds}ms");  //logging execution time of Percentage Calc

            sum = Convert.ToInt32(result);


            if (result.Length > 1)  //if statement to validate which output to print
            {
                if (sum >= 80)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(nameFirst + " matches " + nameLast + " " + sum + "%" + ", " + " Good Match!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(nameFirst + " matches " + nameLast + " " + sum + "%");
                }

            }
            else
            {
                Console.WriteLine("Your Inputs were not long enough for a calculation to perform, please try again, here is your result: " + result);
            }

            Console.ReadKey();
        }
    }
}