using System.Xml.Serialization;

namespace ZooManagementSystem
{
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