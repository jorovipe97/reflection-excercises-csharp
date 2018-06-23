using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace ReflectionBasedSettingClass
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonSettings person = new PersonSettings();
            person.Name = "Jose Romualdo";
            person.Age = 20;
            person.Id = "000294087";

            person.Save();
            Console.WriteLine(person.ToString());

            person.Name = "Bender Siglat";
            person.Age = 100;
            person.Id = "00030303030";
            Console.WriteLine(person.ToString());

            person.Load();
            Console.WriteLine(person.ToString());

            Console.ReadKey();
        }
    }

    class PersonSettings
    {
        private string savedFileName = "person.txt";

        private string name = String.Empty;
        private int age = -1;
        private string id = String.Empty;

        public void Save()
        {
            Type type = this.GetType();
            PropertyInfo[] properties = type.GetProperties();

            using (TextWriter tw = new StreamWriter(savedFileName))
            {
                foreach (PropertyInfo prop in properties)
                {
                    tw.WriteLine(prop.Name+"|"+prop.GetValue(this, null));
                }

                tw.Close();
            }
        }

        public void Load()
        {
            if (File.Exists(savedFileName))
            {
                string[] lines = File.ReadAllLines(savedFileName).ToArray<string>();
                Type type = this.GetType();

                foreach (string line in lines)
                {
                    string propertyName = String.Empty;
                    string propertyValue = String.Empty;

                    string[] args = line.Split('|');

                    if (args.Length == 2)
                    {
                        propertyName = args[0];
                        propertyValue = args[1];

                        PropertyInfo prop = type.GetProperty(propertyName);
                        SetProperty(prop, propertyValue);
                    }
                }
            }
        }

        public override string ToString()
        {
            return this.Name + " is " + this.Age.ToString() + " years old; id: " + this.id;
        }

        /// <summary>
        /// Sets the properties values for the actual instance
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="val">Usually it is a string</param>
        public void SetProperty(PropertyInfo prop, object val)
        {
            switch (prop.PropertyType.Name)
            {
                case "Int32":
                    prop.SetValue(this, Convert.ToInt32(val), null);
                    break;
                case "String":
                    prop.SetValue(this, val.ToString(), null); // TODO: Put null in third argument
                    break;
            }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; } // TODO: Put validations here
        }

        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}
