using System;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable<int> hash = new Hashtable<int>(5);
            int flag = 0, value;
            string key;
        redo:
            Console.Clear();
            ShowOptions();
            int choice = Choice();
            while (flag == 0)
            {
                switch (choice)
                {
                    case 1:
                        key = GetKey();
                        value = GetValue();
                        hash.Insert(key,value);
                        PressToContinue();
                        goto redo;

                    case 2:
                        key = GetKey();
                        hash.Delete(key);
                        PressToContinue();
                        goto redo;

                    case 3:
                        key = GetKey();
                        bool result = hash.Contains(key);
                        Console.WriteLine("\n" + result);
                        PressToContinue();
                        goto redo;

                    case 4:
                        key = GetKey();
                        try
                        {
                            Console.WriteLine("\nValue at key:{0} is " + hash.GetValueByKey(key),key);
                        }
                        catch(ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("\nNo Such key present in Hash Table");
                        }
                        PressToContinue();
                        goto redo;

                    case 5:
                        Console.WriteLine("\nSize is: "+ hash.Size());
                        PressToContinue();
                        goto redo;

                    case 6:
                        hash.Iterator();
                        PressToContinue();
                        goto redo;

                    case 7:
                        hash.Print();
                        PressToContinue();
                        goto redo;                    

                    case 0:
                        flag = 1;
                        break;

                    default:
                        goto redo;
                }
            }
            
        }

        public static void ShowOptions()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press 0 ---> Exit");
            Console.WriteLine("Press 1 ---> Insert");
            Console.WriteLine("Press 2 ---> Delete");
            Console.WriteLine("Press 3 ---> Contains");
            Console.WriteLine("Press 4 ---> Get Value By Key");
            Console.WriteLine("Press 5 ---> Size");
            Console.WriteLine("Press 6 ---> Iterator");
            Console.WriteLine("Press 7 ---> Print");
        }

        public static int GetValue()
        {
        redo:
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter Value to be Inserted: ");
                int value = int.Parse(Console.ReadLine());
                return value;
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: Invalid Type of Value Entered! Value can be of type Integer only");
                Console.ForegroundColor = ConsoleColor.White;
                goto redo;
            }
        }

        public static string GetKey()
        {
        redo:
            try
            {
                Console.Write("\nEnter key : ");
                string key = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(key)) { throw new Exception(); }
                return key;
            }

            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError:Key cannot be blank");
                Console.ForegroundColor = ConsoleColor.White;
                goto redo;
            }
            
        }

        public static int Choice()
        {
        redo:
            Console.Write("\nEnter Choice of Operation: ");
            int choice;
            try
            {
                choice = int.Parse(Console.ReadLine());
                while (choice < 0 || choice > 7)
                {
                    Console.Write("\nPlease enter valid choice: ");
                    choice = int.Parse(Console.ReadLine());
                }
                return choice;
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: Invalid Type! Please enter a integer from the given choices");
                Console.ForegroundColor = ConsoleColor.White;
                goto redo;
            }
        }

        public static void PressToContinue()
        {
            Console.WriteLine("\n\nPress Any Key to continue: ");
            Console.ReadKey();
        }
    }
}
