using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace ZooManagementSystem
{
    class Animal
    {
        static Random rd = new Random();
        public string name; public string species; public int income;

        public Animal(string name, string species, int income)
        {
            this.name = name;
            this.species = species;
            this.income = income;
        }

        public void DisplayAnimal()
        {
            Console.WriteLine($"{name} the {species}, $/s: {income}");
        }

        static string RandomName()
        {
            if (File.Exists("names.txt"))
            {
                string[] lines = File.ReadAllLines("names.txt");
                int n = rd.Next(lines.Length);
                return lines[n];
            }
            else return "placeholder";
        }

        enum Species
        {
            Elephant,
            Rhino,
            Hippo,
            Giraffe,
            Cheetah,
            Bear,
            Eagle,
            Whale
        }

        static string RandomSpecies()
        {
            string[] species = Enum.GetNames(typeof(Species));
            int n = rd.Next(species.Length);
            return species[n];
        }

        public static Animal RandomAnimal(int exoticness)
        {
            string rName = RandomName();
            string rSpecies = RandomSpecies();
            int rIncome = rd.Next(1, 7) * exoticness;
            return new Animal(rName, rSpecies, rIncome);
        }
    }

    class Program
    {
        private static System.Timers.Timer incomeTimer; // Timer for passive income
        private static int userMoney = 500; // Starting money
        private static HashSet<Animal> animals = new HashSet<Animal>(new AnimalEqualityComparer());

        static void MainMenu()
        {
            Console.WriteLine($"Welcome to the Zoo Management System! Your current balance is ${userMoney}.\nPlease choose an option:" +
                              "\na) Animal Database" +
                              "\nb) Shop" +
                              "\nc) Save and Exit");
            string menuChoice = Console.ReadLine().ToLower();
            while (menuChoice != "a" && menuChoice != "b" && menuChoice != "c")
            {
                Console.Write("Please input one of the options listed above: ");
                menuChoice = Console.ReadLine().ToLower();
            }
            Console.Clear();
            switch (menuChoice)
            {
                case "a":
                    Database();
                    break;
                case "b":
                    if (animals.Count == 0)
                    {
                        FreeShop();
                    }
                    else Shop();
                    break;
                case "c":
                    SaveExit();
                    break;
                default:
                    break;
            }
        }

        static void StartPassiveIncome()
        {
            incomeTimer = new System.Timers.Timer(1000);
            incomeTimer.Elapsed += (sender, e) =>
            {
                int totalIncome = 0;
                foreach (var animal in animals)
                {
                    totalIncome += animal.income;
                }
                userMoney += totalIncome;
                Console.WriteLine($"You earned ${totalIncome}! Current Balance: ${userMoney}");
            };
            incomeTimer.Start();
        }

        static void Database()
        {
            if (animals.Count == 0)
            {
                Console.WriteLine("You don't own any animals. Visit the shop.");
            }
            else
            {
                int index = 1;
                foreach (var animal in animals)
                {
                    Console.Write($"{index}. ");
                    animal.DisplayAnimal();
                    index++;
                }
            }
            Console.WriteLine("Press enter to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
            MainMenu();
        }

        static void Shop()
        {
            Animal shopAnimal1 = Animal.RandomAnimal(1);
            Animal shopAnimal2 = Animal.RandomAnimal(2);
            Animal shopAnimal3 = Animal.RandomAnimal(3);

            while (true)
            {
                Console.WriteLine($"Welcome to the shop!" +
                                  $"\na) {shopAnimal1.name} the {shopAnimal1.species}, $50" +
                                  $"\nb) {shopAnimal2.name} the {shopAnimal2.species}, $150" +
                                  $"\nc) {shopAnimal3.name} the {shopAnimal3.species}, $250" +
                                  $"\nd) Back to main menu");
                string menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        PurchaseConfirmation(50, ref shopAnimal1, 1);
                        break;
                    case "b":
                        PurchaseConfirmation(150, ref shopAnimal2, 2);
                        break;
                    case "c":
                        PurchaseConfirmation(250, ref shopAnimal3, 3);
                        break;
                    case "d":
                        Console.Clear();
                        MainMenu();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void FreeShop()
        {
            Animal firstAnimal = new Animal("Rainier", "Rhino", 3);
            Console.WriteLine($"Welcome to the shop!" +
                              $"\na) {firstAnimal.name} the {firstAnimal.species}, FREE" +
                              $"\nb) Back to main menu");
            string menuChoice = Console.ReadLine().ToLower();
            while (menuChoice != "a" && menuChoice != "b")
            {
                Console.Write("Please input one of the options listed above: ");
                menuChoice = Console.ReadLine().ToLower();
            }
            if (menuChoice == "a")
            {
                animals.Add(firstAnimal);
                Console.WriteLine($"Purchased {firstAnimal.name} the {firstAnimal.species}.");
            }
            Console.Clear();
            MainMenu();
        }

        static void PurchaseConfirmation(int price, ref Animal shopAnimal, int exoticness)
        {
            if (userMoney >= price)
            {
                animals.Add(shopAnimal);
                userMoney -= price;
                Console.WriteLine($"You purchased {shopAnimal.name} the {shopAnimal.species} for ${price}!");
                shopAnimal = Animal.RandomAnimal(exoticness);
                Console.WriteLine($"New arrival: {shopAnimal.name} the {shopAnimal.species}, $/s: {shopAnimal.income}");
            }
            else
            {
                Console.WriteLine("You don't have enough money for this purchase.");
            }
        }

        static void SaveExit()
        {
            string saveInfo = $"{userMoney}\n";
            foreach (var animal in animals)
            {
                saveInfo += $"{animal.name}\n{animal.species}\n{animal.income}\n";
            }
            File.WriteAllText("save.txt", saveInfo);
            Console.WriteLine("Saving . . .");
            Console.WriteLine("Data successfully saved!");
        }

        static void LoadSave()
        {
            string line;
            int loadCount = 0;

            if (File.Exists("save.txt"))
            {
                using (StreamReader sr = new StreamReader("save.txt"))
                {
                    if ((line = sr.ReadLine()) != null)
                    {
                        userMoney = int.Parse(line.Trim());
                        Console.WriteLine($"Loaded balance: ${userMoney}");
                    }

                    while ((line = sr.ReadLine()) != null)
                    {
                        string name = line.Trim();
                        line = sr.ReadLine();
                        string species = line.Trim();
                        line = sr.ReadLine();
                        int income = int.Parse(line.Trim());

                        animals.Add(new Animal(name, species, income));
                        Console.WriteLine($"Loaded: {name} the {species}, $/s: {income}");
                        loadCount++;
                    }
                }
            }

            if (loadCount == 0)
            {
                Console.WriteLine("No saved animals found.");
            }
        }

        static void Main(string[] args)
        {
            LoadSave();
            StartPassiveIncome();
            MainMenu();
        }
    }

    class AnimalEqualityComparer : IEqualityComparer<Animal>
    {
        public bool Equals(Animal x, Animal y)
        {
            return x.name == y.name && x.species == y.species;
        }

        public int GetHashCode(Animal obj)
        {
            return HashCode.Combine(obj.name, obj.species);
        }
    }
}

/*
- Randomized animals in the shop with set prices (3 options at a time):
    - 1 small/cheap animal, 1 medium, and 1 large/expensive.
    - When an animal is bought, a new randomized animal replaces it in the shop.
- Animal names are randomized from a .txt file containing a list of potential names.
- Enum is used for animal species randomization.
- Shop animal offers dynamically restock using a ref-based approach.
- Passive income system implemented:
    - The user passively gains income from all owned animals (based on their income stat) every second.
    - This income is displayed and added to the user's balance in real-time.
- User's money is accurately deducted when purchasing animals from the shop.
- HashSet is used to prevent duplicate animals when saving or adding animals to the user's collection.
- Save and Load functionality:
    - User's balance and owned animals are saved to a file ("save.txt").
    - On loading, the file restores the user's money and recreates the animal objects.
*/
