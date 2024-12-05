using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooManagementSystem
{
    class Animal
    {
        string name; string sex; int age; int weight;
        public Animal(string name, string sex, int age, int weight)
        {
            this.name = name;
            this.sex = sex;
            this.age = age;
            this.weight = weight;
        }
        public void DisplayAnimal()
        {
            Console.WriteLine($"Name: {name}, Sex: {sex}, Age: {age}, Weight: {weight}");
        }
    }
}
