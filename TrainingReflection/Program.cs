using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HelloReflection
{
    class Program
    {
        // Variables
        private static int a = 5, b = 5, c = 5;

        static void Main(string[] args)
        {

            Console.WriteLine("a + b + c = " + (a+b+c));
            Console.WriteLine("Write the variable name that you want to change: ");
            string varName = Console.ReadLine();
            Type i = typeof(Program);
            FieldInfo field = i.GetField(varName, BindingFlags.NonPublic | BindingFlags.Static);
            Console.WriteLine("Write the new value for " + varName);
            string newValStr = Console.ReadLine();
            int newVal;
            if (int.TryParse(newValStr, out newVal))
            {
                // The first argument is null because the fields are static
                field.SetValue(null, newVal);
                Console.WriteLine("a + b + c = " + (a+b+c));
            }
            Console.ReadKey();
        }
    }
}
