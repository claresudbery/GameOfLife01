using System;
using System.Threading;
using GameOfLife.Classes;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowIntroAndLegend();

            string newInput = ProcessInput();

            while (null != newInput && "END" != newInput.ToUpper())
            {
                newInput = ProcessInput();
            }

            Console.WriteLine("OK, then, if I really must. Goodbye [sob].");

            Thread.Sleep(System.TimeSpan.FromSeconds(2));
        }

        private static string ProcessInput()
        {
            string input = Console.ReadLine();
            Console.WriteLine("Still here... (type 'End' to quit)");

            return input;
        }

        private static void ShowIntroAndLegend()
        {
            Console.WriteLine("Hello (type 'End' to quit)");
        }
    }
}
