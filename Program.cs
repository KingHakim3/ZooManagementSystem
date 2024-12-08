using System.ComponentModel;
using System.Xml.Serialization;

namespace ZooManagementSystem
{
    class Animal
    {
        public string name; public string species; public int money;
        public Animal(string name, string species, int money)
        {
            this.name = name;
            this.species = species;
            this.money = money;
        }
        public void DisplayAnimal()
        {
            Console.WriteLine($"{name} the {species}, $/s: {money}");
        }
    }
    class Program
    {
        static List<Animal> animals = new List<Animal>
            {
            };
        //creates a class-scope list of all of the user's animals
        static void WelcomeMenu()
        {
            Console.WriteLine("Please choose an option. \na) Animal Database \nb) Shop \nc) Save");
            string menuChoice = Console.ReadLine().ToLower();
            while (menuChoice != "a" && menuChoice != "b" && menuChoice != "c")
            {
                Console.Write("Please input one of the options listed above.");
                menuChoice = Console.ReadLine().ToLower();
            }
            //Ensures that the user enters one of the options (a, A, b, B, c, C)
            switch (menuChoice)
            {
                case "a":
                    Database();
                    break;
                case "b":
                    Shop();
                    break;
                case "c":
                    SaveExit();
                    break;
                default:
                    break;
            }
            //calls the proper function based on the user's input
        }
        static void Database()
        {
            Console.WriteLine();
            if (animals.Count == 0)
                Console.WriteLine("You don't own any animals. Visit the shop.");
            else
            {
                for (int i = 0; i < animals.Count; i++)
                {
                    Console.Write($"{i}. ");
                    animals[i].DisplayAnimal();
                }
            }
            //prints the info of each of the user's animals if they have any
            Console.WriteLine("Press enter to return to the main menu.");
            Console.ReadLine();
            WelcomeMenu();
        }
        static void Shop()
        {
            Console.WriteLine("\nWelcome to the shop!");
            Console.WriteLine("a) Rainier the Rhino");
            //new Animal("Rainier", "Rhino", 3),
            //new Animal("George", "Monkey", 1),
            //new Animal("James", "Peach", 0)
            string menuChoice = Console.ReadLine().ToLower();
            while (menuChoice != "a" && menuChoice != "b" && menuChoice != "c" && menuChoice != "d")
            {
                Console.Write("Please input one of the options listed above.");
                menuChoice = Console.ReadLine().ToLower();
            }
            switch (menuChoice)
            {
                case "a":
                    break;
                case "b":
                    break;
                case "c":
                    break;
                case "d":
                    break;
                default:
                    break;
            }
        }
        static void SaveExit()
        {
            string saveInfo = SaveInfo();
            File.WriteAllText("save.txt", saveInfo);
            Console.WriteLine("\nSaving . . .");
            Console.WriteLine("Game successfully saved!");
        }
        static string SaveInfo()
        {
            string saveInfo = "";
            for (int i = 0; i < animals.Count; i++)
            {
                saveInfo += $"{animals[i].name}\n{animals[i].species}\n{animals[i].money}\n";
            }
            //saves all animal data to one string of separate lines
            return saveInfo;
        }
        static void LoadSave()
        {
            //reads save.txt line by line and reconstructs the animal objects
            string line;
            int loadCount = 0;
            if (File.Exists("save.txt"))
            {
                using (StreamReader sr = new StreamReader("save.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string name = line.Trim();

                        line = sr.ReadLine();
                        string species = line.Trim();

                        line = sr.ReadLine();
                        string mStr = line.Trim();
                        int money = int.Parse(mStr);

                        animals.Add(new Animal(name, species, money));
                        Console.Write($"Loaded: {name} the {species}, $/s: {money}");
                        loadCount++;
                    }
                }
            }
            if (loadCount == 0 || File.Exists("save.txt") == false)
                Console.WriteLine("No save was loaded.\n");
            else
                Console.WriteLine("Loading complete.\n");
        }
        static void Main(string[] args)
        {
            LoadSave();
            Console.Write("Welcome to the Zoo Management System! ");
            WelcomeMenu();
        }
    }
}

/*
- randomized animals in shop for set prices (3 options at a time, 1 small/cheap, 1 medium, 1 large/expensive)
    - when an animal is bought, another randomized animal should take its place in the shop
    - animal names randomized from a .txt file of a bunch of different options
    - use enums for animal species randomization
- implement money system
    - user should passively gain money from animals they own (the sum of all the owned animals' money field, per second)
    - shop should accurately remove money from user's account
*/