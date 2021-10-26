using System;
using System.Text;

namespace LISPValidation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var introTextBuilder = new StringBuilder();
            introTextBuilder.Append("This console application takes in a single line of LISP code and validates");
            introTextBuilder.Append("\nthe parentheses. If all of the parentheses in the string are properly");
            introTextBuilder.Append("\nclosed and nested, the application will return \"True\".");
            
            Console.WriteLine(introTextBuilder.ToString());

            var lispInput = Console.ReadLine();

            var isValid = ValidationLogic.Parse(lispInput);
            
            Console.WriteLine(isValid ? "True" : "False");
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            Console.WriteLine("\nGoodbye!");
        }
    }
}