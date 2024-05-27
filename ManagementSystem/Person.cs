using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
    class Person
    {
        public string Name { get; protected set; }
        public int Age { get; protected set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void PrintUserDetails()
        {
            Console.WriteLine($"Name: {Name} | Age: {Age}");
        }

        public void SetName(string newName)
        {
            Name = newName;
        }

        public void SetAge(int newAge)
        {
            Age = newAge;
        }
        public String GetName()
        {
            return Name;
        }
        public int GetAge()
        {
            return Age;
        }
    }
}
