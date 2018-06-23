using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TheRightType
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();

            foreach (Type t in types)
                Console.WriteLine(t.Name + ((t.IsValueType)? " is a value type." : " is a reference type"));

            Console.ReadKey();
        }
    }

    // Reference type
    class DummyClass
    {

    }

    // Value type
    enum DummyEnum
    {
        nothing,
        run,
        pause,
        end
    }

    // Value type
    struct DummyStruct
    {
        public int age;
        public string name;
    }

    // Reference type
    delegate void DummyDelegate(int a);
}
