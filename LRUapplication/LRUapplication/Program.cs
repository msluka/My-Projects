using System;
using System.Threading;
using ConsoleApplication8;

namespace LRUapplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Author: Shota Mamulashvili");
            Console.WriteLine("Date: 18-08-2017");
            Console.WriteLine("Warsaw, Poland");

            Console.WriteLine("-----------*****-----------" + Environment.NewLine);

            Console.WriteLine("Please set the memory size (e.g. 3; 5; 10 itc.) :");
            Console.WriteLine("(Only numbers are allowed: 0-9;)" + Environment.NewLine);
            Start:

            string line = Console.ReadLine();
            int memory;

            if (int.TryParse(line, out memory))
            {
            }
            else
            {
                Console.WriteLine("Not a number!!! Please enter a number:");
                Console.WriteLine();
                goto Start;
            }


            var list = new LRUprogram(memory);
            Console.WriteLine();
            Console.WriteLine("The memory has been successfully configured");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("Memory size: " + list.maxLengthOfDictionary);
            Console.WriteLine("Records in Memory: " + list.CheckDicLength());
            Console.WriteLine("--------------------" + Environment.NewLine);
            Thread.Sleep(1000);
            Console.WriteLine("Let's start adding the records" + Environment.NewLine);
            Thread.Sleep(1000);

            Adding:
            do
            {
                Console.WriteLine("Please enter the key:");
                var key = Console.ReadLine();
                Console.WriteLine("Please enter the value:");
                var value = Console.ReadLine();
                list.AddRecord(key, value);
                Console.WriteLine();
                Console.WriteLine("Memory size: " + list.maxLengthOfDictionary);
                Console.WriteLine("Records in Memory: " + list.CheckDicLength());
                Console.WriteLine("--------------------");
                Console.WriteLine();
                Thread.Sleep(1000);
                Console.WriteLine("Do you want to add more records to the memory?");
                Console.WriteLine("Yes = enter, No = 0");
                Console.WriteLine();


            } while (Console.ReadLine() != "0");


            Console.WriteLine();
            Console.WriteLine("The list of records:" + Environment.NewLine);
            foreach (var l in list.Dic)
            {
                Console.WriteLine("Key: " + l.Key + ", " + "Value: " + l.Value);
            }

            Console.WriteLine("---Time---");

            foreach (var l in list.Log)
            {
                Console.WriteLine("Key: " + l.Key + ", " + "Value: " + l.Value);
            }

            Console.WriteLine();
            Thread.Sleep(1000);

            do
            {
                Console.WriteLine();
                Console.WriteLine("If you want to get the value by key, press: enter");
                Console.WriteLine("If you want to add more records to the memory, press: 1");

                var response = Console.ReadLine();
                if (response == "1")
                {
                    goto Adding;

                }
                else
                {
                    Console.WriteLine("Please enter the key you want to find:");
                    var keyname = Console.ReadLine();
                    if (list.Dic.ContainsKey(keyname))
                    {
                        list.GetValue(keyname);
                        Console.WriteLine("Key: " + keyname + ", Value: " + list.Dic[keyname]);
                    }
                }

            } while (true);
        }
    }
}
