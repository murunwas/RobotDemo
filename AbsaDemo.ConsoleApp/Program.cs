using AbsaDemo.Library;
using System;

namespace AbsaDemo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your directions");
            var direction = Console.ReadLine();

            try
            {
                Robot robot = new Robot();
                robot.StartMovement(direction);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
