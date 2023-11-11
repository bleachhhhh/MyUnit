using System;
using System.Arithmetic.Tests;

namespace MyUnit.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MyTestRunner.TestFailed += MyTestRunner_TestFailed;
            MyTestRunner.TestPassed += MyTestRunner_TestPassed;
            MyTestRunner.Run(typeof(SumOperationTest));
            
        }

        private static void MyTestRunner_TestPassed(string testName,string obj)
        {

            PrintResult(testName, obj, true);
            
        }

        private static void PrintResult(string testName, string obj, bool success)
        {
            if (!success)
            {
                System.Console.WriteLine($"{testName}:{obj}");
                System.Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                System.Console.WriteLine($"{testName}");
                System.Console.BackgroundColor = ConsoleColor.Green;
            }
            System.Console.WriteLine(success? "пройден":"провален");
            System.Console.ResetColor();
            
        }

        private static void MyTestRunner_TestFailed(string testName,string obj)
        {
            PrintResult(testName, obj, false);
            
        }
    }
}
