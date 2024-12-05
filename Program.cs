using System.ComponentModel;
using System.Xml.Serialization;

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
    class Program
    {
        static Animal Rhino;
        static Animal Elephant;
        static Animal Giraffe;
        static void WelcomeMenu()
        {
            Console.WriteLine("Please choose an option. \na) Animal Database \nb) Trainers");
            string menuChoice = Console.ReadLine();
            while (menuChoice != "a" && menuChoice != "b")
            {
                Console.Write("Please input one of the options listed above.");
                menuChoice = Console.ReadLine();
            }
            switch (menuChoice)
            {
                case "a":
                    MenuChoiceA();
                    break;
                case "b":
                    MenuChoiceB();
                    break;
                default:
                    break;
            }
        }
        static void MenuChoiceA()
        {
            Console.WriteLine("a) Rhino \nb) Elephant \nc) Giraffe \nd) Back");
            string menuChoice = Console.ReadLine();
            while (menuChoice != "a" && menuChoice != "b" && menuChoice != "c" && menuChoice != "d")
            {
                Console.Write("Please input one of the options listed above.");
                menuChoice = Console.ReadLine();
            }
            switch (menuChoice)
            {
                case "a":
                    Rhino.DisplayAnimal();
                    break;
                case "b":
                    Elephant.DisplayAnimal();
                    break;
                case "c":
                    Giraffe.DisplayAnimal();
                    break;
                case "d":
                    WelcomeMenu();
                    break;
                default:
                    break;
            }
        }
        static void MenuChoiceB()
        {
            Console.WriteLine("a) Jeremy \n b) Hilbert");
            string menuChoice = Console.ReadLine();
            while (menuChoice != "a" && menuChoice != "b")
            {
                Console.Write("Please input one of the options listed above.");
                menuChoice = Console.ReadLine();
            }
            switch (menuChoice)
            {
                case "a":
                    Rhino.DisplayAnimal();
                    break;
                case "b":
                    Elephant.DisplayAnimal();
                    break;
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {
            Rhino = new Animal("Rainier", "F", 15, 360);
            Elephant = new Animal("Eddy", "M", 23, 850);
            Giraffe = new Animal("Rod", "M", 8, 240);
            Console.Write("Welcome to the Zoo Management System! ");
            WelcomeMenu();
        }
    }
}

/*
- make it more game-like. shop, animal database, trainers with specific boosts, tasks to make money (stopwatch typing speed game, memory game, catch-the-animal luck game)
- animal names randomized from a .txt file of a bunch of different options
- rename feature for non-void method?
- use enums for menu options
- use interfaces & public/private access modifiers for animal-related methods

Meaningful comments
Class (in addition to the class that contains Main)
Arrays or Enums 
Loops (any one of while, do-while and for loops)
Methods (one returning void, and one returning non-void; one instance and one static method, and these can be combined, e.g., void/static, and non-void/instance)
Constructor (at least one non-default constructor)
Private and public access modifiers
Properties
Interfaces
File read/write
Random number generators
Collections
Github

Recursion
Inheritance
*/