using System;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        // static variable for the bank account
        static int BankAmount = 10000;

        // static object for the lock
        static readonly object locker = new object();
        
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            string option = null;

            // loop for the Main Menu
            do
            {
                // thread sleep for the main menu
                Thread.Sleep(100);

                Console.WriteLine("\nWELCOME");
                Console.WriteLine("1. Start the application");
                Console.WriteLine("2. Exit");
                Console.Write("Please choose an option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        BankAmount = 10000;
                        Menu();                        
                        break;
                    case "2": Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please choose option 1 or 2.");
                        break;
                }
            } while (!option.Equals("2"));

            Console.ReadLine();
        }

        /// <summary>
        /// method for ATM payment system
        /// </summary>
        static void ATM()
        {
            // lock to ensure alternately threads working
            lock (locker)
            {
                if (BankAmount== 0)
                {
                    Console.WriteLine("\nSorry, there is no money on the bank account. Please try later.");
                    return;
                }                

                // getting the random amount               
                int a = rnd.Next(100, 10000);

                Console.WriteLine("\n{0} trying to raise the amount: {1} RSD", Thread.CurrentThread.Name, a);

                // loop for checking if the random amount isn't bigger then the bank amount and displaying messages
                if (a <= BankAmount)
                {
                    BankAmount = BankAmount - a;
                    Console.WriteLine("\nPayment succeeded.");
                    Console.WriteLine("Current bank amount is: {0} RSD", BankAmount);
                }
                else
                {
                    Console.WriteLine("\n{0} did not succeed to raise the amount: {1} RSD", Thread.CurrentThread.Name, a);
                    Console.WriteLine("Sorry, there is not enough money on the bank amount.");
                }
            }
        }

        /// <summary>
        /// method for the Menu
        /// </summary>
        static void Menu()
        {
            // two variables for the ATM's clients
            uint atm1clients = 0;
            uint atm2clients = 0;

            // inputs and validations           
            Console.Write("Please input the number of clients for first ATM: ");
            bool first = uint.TryParse(Console.ReadLine(), out atm1clients);

            while (!first)
            {
                Console.WriteLine("Wrong input, please try again.");
                first = uint.TryParse(Console.ReadLine(), out atm1clients);
            }

            Console.Write("Please input the number of clients for second ATM: ");
            bool second = uint.TryParse(Console.ReadLine(), out atm2clients);

            while (!second)
            {
                Console.WriteLine("Wrong input, please try again.");
                second = uint.TryParse(Console.ReadLine(), out atm2clients);
            }

            // sum of clients
            uint clients = atm1clients + atm2clients;

            // loop for creating threads for clients, giving them names and starting threads
            for (uint i = 0; i < clients; i++)
            {
                Thread t = new Thread(() => ATM());
                t.Name = string.Format("Client_{0}", i + 1);
                t.Start();                
            }
        }
    }
}
