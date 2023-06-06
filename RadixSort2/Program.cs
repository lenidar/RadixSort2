using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixSort2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 29, 72, 98, 13, 87, 66, 52, 51, 36 };
            int[] LSD = new int[arr.Length];

            int insertIndex = -1;
            bool insertFlag = false;
            int toInsertValue = 0;
            int toInsertLSD = 0;

            int baseNum = 1;

            bool sorted = false; // should find a way to get what the most number of digits are 

            // for the stupid display logic only, this might make the code rather slow but still
            // it makes the output look pretty so what the hell
            int charToDisplay = 0;
            // char to display is what character needs to have color in the number
            // 0 means its going to display the ones column
            // 1 means its going to display the tens column
            // 2 means its going to display the hundreds column
            // and so on and so forth 
            string temp = "";
            // the string temp is just for display purposes
            int tempVal = 0;
            // the int tempVal is just for display purposes too

            while (!sorted)
            {
                Console.Clear();
                // get the LSD (Least Significant Digit) of the array based on baseNum
                // the LSD starts from the once digit, followed by the tens, followed by the hundreds and so on
                sorted = true;
                for (int x = 0; x < arr.Length; x++)
                {
                    LSD[x] = arr[x] / baseNum % 10;
                    // if all LSD are 0, this means that the sorting algorithm, if coded correctly, would have sorted
                    // everything by now
                    if (LSD[x] > 0)
                        sorted = false;
                }

                // this just breaks the while loop if all LSD are 0
                if (sorted)
                    break;

                //foreach (int a in LSD)
                //    Console.Write(a + "\t");
                //Console.WriteLine();

                // we will be performing the insertion sort
                // basing everything in the LSD array but reflecting the changes in both
                // the LSD array and the number array
                Console.WriteLine("The number array to be sorted in base {0}", baseNum);
                foreach (int a in arr)
                    Console.WriteLine("\t" + a);
                Console.ReadKey();
                Console.Clear();

                // beginning the insertion sort...
                // this is just regular insertion sort but instead of comparing values, we are comparing LSD
                for (int x = 1; x < arr.Length; x++)
                {
                    insertIndex = -1;
                    insertFlag = false;

                    #region Highlighting all LSDs
                    // display
                    Console.WriteLine("Base {0} Getting all LSDs", baseNum);
                    for (int a = 0; a < arr.Length; a++)
                    {
                        temp = arr[a] + ""; // converts the int into a string
                        Console.Write("\t");
                        for (int b = 0; b < temp.Length; b++)
                        {
                            // this code only works for this example because all of them has a length of 2
                            // since this specific logic is only to display and nothing more
                            // please do not dwell on this logic too much
                            // if I made this more dynamic there would be too much uncessary logic
                            // the uncessary logic would be, but not limited to
                            // scanning of the numbers, converting them to string, finding how many characters
                            // the longest number is and using that number in this logic
                            tempVal = temp.Length - 1 - b;

                            if (tempVal < 0) // logic for absolute value
                                tempVal *= -1;

                            if (tempVal == charToDisplay)
                                Console.BackgroundColor = ConsoleColor.Red;
                            else
                                Console.ResetColor();

                            Console.Write(temp[b]);
                        }
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                    Console.ReadKey();
                    Console.Clear(); 
                    #endregion

                    for (int y = 0; y < x; y++)
                    {
                        #region Highlight all LSD selections and comparisons
                        // display
                        Console.ResetColor();
                        Console.WriteLine("Base {0} : Comparing {1} and {2}", baseNum, LSD[x], LSD[y]);
                        for (int a = 0; a < arr.Length; a++)
                        {
                            temp = arr[a] + "";
                            Console.Write("\t");
                            for (int b = 0; b < temp.Length; b++)
                            {
                                tempVal = temp.Length - 1 - b;

                                if (tempVal < 0)
                                    tempVal *= -1;

                                Console.ResetColor();

                                if (tempVal == charToDisplay)
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    if (a == x)
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                    else if (a == y)
                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                }

                                Console.Write(temp[b]);
                            }
                            Console.WriteLine();
                            Console.ResetColor();
                        }
                        Console.ReadKey();
                        Console.Clear();
                        Console.ResetColor(); 
                        #endregion
                        if (LSD[x] < LSD[y])
                        {
                            insertFlag = true;
                            insertIndex = y;
                            break;
                        }
                    }

                    if (insertFlag)
                    {
                        toInsertValue = arr[x];
                        toInsertLSD = LSD[x];
                        arr[x] = -1;
                        LSD[x] = -1;


                        #region Highlight Removal of value to be moved
                        // display
                        Console.ResetColor();
                        Console.WriteLine("Base {0} : Removing {1}.", baseNum, toInsertValue);
                        for (int a = 0; a < arr.Length; a++)
                        {
                            temp = arr[a] + "";
                            Console.Write("\t");
                            if (arr[a] > 0)
                            {
                                for (int b = 0; b < temp.Length; b++)
                                {
                                    tempVal = temp.Length - 1 - b;

                                    if (tempVal < 0)
                                        tempVal *= -1;

                                    Console.ResetColor();

                                    if (tempVal == charToDisplay)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Red;
                                    }

                                    Console.Write(temp[b]);
                                }
                                Console.WriteLine();
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("  ");
                                Console.ResetColor();
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
                        Console.ResetColor();
                        #endregion

                        for (int y = x - 1; y >= insertIndex; y--)
                        {
                            arr[y + 1] = arr[y];
                            LSD[y + 1] = LSD[y];
                            arr[y] = -1;
                            LSD[y] = -1;

                            #region Highlight moving of values
                            // display
                            Console.ResetColor();
                            Console.WriteLine("Base {0} : Moving {1}.", baseNum, arr[y + 1]);
                            for (int a = 0; a < arr.Length; a++)
                            {
                                temp = arr[a] + "";
                                Console.Write("\t");
                                if (arr[a] > 0)
                                {
                                    for (int b = 0; b < temp.Length; b++)
                                    {
                                        tempVal = temp.Length - 1 - b;

                                        if (tempVal < 0)
                                            tempVal *= -1;

                                        Console.ResetColor();

                                        if (tempVal == charToDisplay)
                                        {
                                            Console.BackgroundColor = ConsoleColor.Red;
                                        }

                                        Console.Write(temp[b]);
                                    }
                                    Console.WriteLine();
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("  ");
                                    Console.ResetColor();
                                }
                            }
                            Console.ReadKey();
                            Console.Clear();
                            Console.ResetColor();
                            #endregion
                        }

                        arr[insertIndex] = toInsertValue;
                        LSD[insertIndex] = toInsertLSD;
                    }
                }

                Console.WriteLine("The Sorted Number array base {0}", baseNum);
                foreach (int a in arr)
                    Console.WriteLine("\t" + a);
                Console.ReadKey();


                baseNum *= 10;
                charToDisplay++;
            }
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
