using System;
using System.Collections.Generic;
using System.Text;

namespace Derivco
{
    class test
    {
        public static void Main(string[] args)
        {
            testcalc();
        }
        public static void testcalc()
        {
            int[] combo = { 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2 };

            //string comboString = "22211111112";

            List<string> lisArray = new List<string>();
            List<int> intArray = new List<int>();
            string newCombo = "";
            string Combo2 = "";
            int first = 0;
            int last = 0;
            int sum = 0;
            int runvalue = combo.Length/2;


            for (int j = 0; j < runvalue; j++)
            {
                first = combo[j];
                last = (combo[combo.Length - j - 1]);

                sum = first + last;

                newCombo += sum.ToString();

            }

            if (combo.Length % 2 != 0)
            {
                newCombo = newCombo + (combo[runvalue]).ToString();
                Console.WriteLine("This is before clear " + newCombo);
            }

            while(newCombo.Length != 2)
            {
                lisArray.Clear();
                for (int f = 0; f < newCombo.Length; f++)
                {
                    string number = newCombo[f].ToString();

                    lisArray.Add(number);
                }

                intArray.Clear();
                for (int g = 0; g < lisArray.Count; g++)
                {
                    int number = Convert.ToInt32(lisArray[g]);

                    intArray.Add(number);
                }

                for (int i = 0; i < intArray.Count / 2; i++)
                {
                    first = intArray[i];

                    last = (intArray[intArray.Count - i - 1]);

                    sum = first + last;

                    Combo2 += sum.ToString();

                    newCombo = "";
                }

                newCombo += Combo2;
                Combo2 = "";

                if (intArray.Count % 2 != 0)
                {
                    newCombo = newCombo + (intArray[intArray.Count/2]).ToString();
                }
            }

            Console.WriteLine("This is new answer: " + newCombo);



            //for (int i = 0; i < newCombo.Length / 2; i++)
            //{

            //    if (decimal.Round(Convert.ToDecimal(newCombo.Length), 2) == Convert.ToDecimal(newCombo.Length))
            //    {;

            //        for (int c = 0; c < newCombo.Length / 2; c++)
            //        {
            //            first = lisArray[c];
            //            last = (lisArray[lisArray.Count - c - 1]);

            //            sum = first + last;

            //            newCombo += sum.ToString();
            //        }

            //        if (newCombo.Length % 2 != 0)
            //        {
            //            newCombo = newCombo + (combo[newCombo.Length / 2]).ToString();
            //        }
            //    }
            //    else if (newCombo.Length % 2 == 0)
            //    {
            //        Console.WriteLine("This is the new number: " + newCombo);
            //    }
            //}



        }
    }
}


//Old Compute Code
//for (int i = 0; i < runvalue; i++)  //method to add the numbers in the list from the back and front till the middle
//{
//    first += totalArray[i];

//    last += (totalArray[totalArray.Length -i -1]);
//}

//sum = Convert.ToInt32(first + "" + last);

//result += sum;

//if (totalArray.Length % 2 != 0)  //to see if the array way odd or even, if it was odd then will add the number where the division occured as that number wouldnt be counted in the sum
//{
//    result = result + "" + totalArray[runvalue];
//}






//string secondCombo = "";
//List<int> thirdCombo = new List<int>();
//List<int> fourthCombo = new List<int>();
//List<int> finalCombo = new List<int>();
//string finalSum = "";
//int newcombo = 0;
//int first = 0;
//int last = 0;
//int sum = 0;
//int length = combo.Length;

//string[] strArray = Array.ConvertAll(combo, ele => ele.ToString());
//string stringCombo = string.Join("", strArray);

//int runvalue = stringCombo.Length / 2;


//Console.WriteLine("This is your string: " + stringCombo + "\n" + "Length of it: " + runvalue);

//for (int i = 0; i < runvalue; i++)
//{
//    first = Convert.ToInt32(stringCombo[i]);

//    last = stringCombo[stringCombo.Length - i - 1];


//    sum = (first + last);

//    secondCombo.add(i, sum);

//    sum = 0;
//}

//if (combo.Length % 2 != 0)
//{
//    secondCombo.Add(combo[runvalue]);
//}

//for (int j = 0; j < secondCombo.Count / 2; j++)
//{
//    first = secondCombo[j];

//    last = secondCombo[secondCombo.Count - j - 1];


//    sum = first + last;

//    thirdCombo.Add(sum);

//    sum = 0;
//}


//for (int f = 0; f < thirdCombo.Count / 2; f++)
//{
//    first = thirdCombo[f];

//    last = thirdCombo[thirdCombo.Count - f - 1];


//    sum = first + last;

//    fourthCombo.Add(sum);

//    sum = 0;
//}

//if (thirdCombo.Count % 2 != 0)
//{
//    fourthCombo.Add(thirdCombo[thirdCombo.Count / 2]);
//}


//finalSum = string.Join("", fourthCombo);

//for (int i = 0; i < finalSum.Length; i++)
//{
//    first = finalSum[i];
//    last = finalSum[finalSum.Length - i - 1];

//    sum = first + last;

//    finalCombo.Add(sum);
//}



//Console.WriteLine("This is the sum: " + String.Join("", finalCombo));
