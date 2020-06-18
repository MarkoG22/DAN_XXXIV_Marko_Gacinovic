using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        public static int BankAmount = 10000;
        public static readonly object locker = new object();

        static void Main(string[] args)
        {
            Menu();

            Console.ReadLine();
        }

        public static void ATM()
        {
            Thread.Sleep(10);

            lock (locker)
            {
                if (BankAmount== 0)
                {
                    Console.WriteLine("\nSorry, there is no money on the bank account. Please try later.");
                    return;
                }                

                Random rnd = new Random();
                int a = rnd.Next(100, 10000);

                Console.WriteLine("\n{0} trying to raise the amount: {1}", Thread.CurrentThread.Name, a);

                if (a <= BankAmount)
                {
                    BankAmount = BankAmount - a;
                    Console.WriteLine("\nPayment succeeded.");
                    Console.WriteLine("Current bank amount is: {0}", BankAmount);
                }
                else
                {
                    Console.WriteLine("\n{0} did not succeed to raise the amount: {1}", Thread.CurrentThread.Name, a);
                    Console.WriteLine("There is not enough money on the bank amount, please try smaller amount.");
                }
            }
        }

        public static void Menu()
        {
            uint atm1clients = 0;
            uint atm2clients = 0;

            Console.WriteLine("Welcome");
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

            uint clients = atm1clients + atm2clients;

            for (uint i = 0; i < clients; i++)
            {
                Thread t = new Thread(() => ATM());
                t.Name = string.Format("Client_{0}", i + 1);
                t.Start();
                t.Join();
            }
        }
    }
}
