using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace InstantiatingClassAtRuntime
{
    class Program
    {
        static void Main(string[] args)
        {
            Type testType = typeof(TestClass);
            // Get default constructor
            ConstructorInfo ctor = testType.GetConstructor(Type.EmptyTypes);
            
            if (ctor != null)
            {
                // Null because constructor don't receive argumetns
                object instance = ctor.Invoke(null);

                // Invoking TestMethod
                MethodInfo testMethod = testType.GetMethod("TestMethod");
                // We pass the arguments to the method as a array of object
                int res = (int) testMethod.Invoke(instance, new object[] { 5 });
                Console.WriteLine("New Value: " + res);

                // Invoking SumArgs
                MethodInfo sumMethod = testType.GetMethod("SumArgs");
                res = (int)sumMethod.Invoke(instance, new object[] {
                    new int[] { 5, 10, 10 } // params int[] numbers
                });
                Console.WriteLine("Sum: " + res);

                // Invoking PrivMethod
                MethodInfo privMethod = testType.GetMethod("PrivMethod", BindingFlags.NonPublic | BindingFlags.Instance);
                string resStr = String.Empty;
                resStr = (string) privMethod.Invoke(instance, null);
                Console.WriteLine("PrivMethod " + resStr);
            }

            Console.WriteLine("");
            for (int i = 0; i < 40; i++)
                Console.Write("=");
            Console.WriteLine("");


            // Get defined constructor
            ConstructorInfo ctorArg = testType.GetConstructor(new Type[] { typeof(int) });
            if (ctorArg != null)
            {
                object instance2 = ctorArg.Invoke(new object[] { 20 });

                MethodInfo testMethod2 = testType.GetMethod("TestMethod");
                int res = (int)testMethod2.Invoke(instance2, new object[] { 30 });
                Console.WriteLine("Instance2.TestMethod(): " + res.ToString());
            }

            Console.ReadKey();
        }
    }

    class TestClass
    {
        private int TestValue = 10;

        public TestClass()
        {

        }

        public TestClass(int testValue)
        {
            this.TestValue = testValue;
        }

        public int TestMethod(int numberToAdd)
        {
            return this.TestValue + numberToAdd;
        }

        /// <summary>
        /// Sums all the given arguments
        /// </summary>
        /// <param name="numbers">The numbers which we are going to sum</param>
        /// <returns>The sum of all the given arguments</returns>
        public int SumArgs(params int[] numbers)
        {
            int res = 0;
            if (numbers.Length <= 0)
                return 0;

            foreach (int n in numbers)
                res += n;

            return res;
        }

        private string PrivMethod()
        {
            Console.WriteLine("Invoking private method");
            return "I am a private method";
        }
    }
}
