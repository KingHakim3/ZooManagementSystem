using System.ComponentModel;
using System.Xml.Serialization;

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
            } //Returns a random line from names.txt
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
        } //Returns a random species from the Species enum
        public static Animal RandomAnimal(int exoticness)
        {
            string rName = RandomName();
            string rSpecies = RandomSpecies();
            int rIncome = rd.Next(1, 7) * exoticness; //Gives a random value from 1 to 6            
            return new Animal(rName, rSpecies, rIncome);
        }
    }
    class Program
    {
        static List<Animal> animals = new List<Animal>
            {
            }; //creates a list of all of the user's animals within the class scope. Purchased animals will be added to this list
        static void MainMenu()
        {
            Console.WriteLine("Welcome to the Zoo Management System! Please choose an option. \na) Animal Database \nb) Shop \nc) Save and Exit");
            string menuChoice = Console.ReadLine().ToLower();
            while (menuChoice != "a" && menuChoice != "b" && menuChoice != "c")
            {
                Console.Write("Please input one of the options listed above.");
                menuChoice = Console.ReadLine().ToLower();
            } //Prompts user for input. If they enter anything other than A, a, B, b, C, c, then it will return an error message and prompt them to enter another option.
            Console.Clear();
            switch (menuChoice)
            {
                case "a":
                    Database();
                    break;
                case "b":
                    if(animals.Count == 0)
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
            } //Calls the function that corresponds to the user's input
        }
        static void Database()
        {
            if (animals.Count == 0)
                Console.WriteLine("You don't own any animals. Visit the shop."); //If the list of animals is empty, the user is prompted to visit the shop.
            else
            {
                for (int i = 0; i < animals.Count; i++)
                {
                    Console.Write($"{i + 1}. ");
                    animals[i].DisplayAnimal();
                }
            } //If the user has animals, it displays their name, species, and income
            Console.WriteLine("Press enter to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
            MainMenu(); //Once the user hits the enter key, they are sent back to the main menu
        }
        static void Shop()
        {
            Animal shopAnimal1 = Animal.RandomAnimal(1);
            Animal shopAnimal2 = Animal.RandomAnimal(2);
            Animal shopAnimal3 = Animal.RandomAnimal(3);
            Console.WriteLine($"Welcome to the shop!" + 
                              $"\na) {shopAnimal1.name} the {shopAnimal1.species}, $50" +
                              $"\nb) {shopAnimal2.name} the {shopAnimal2.species}, $150" +
                              $"\nc) {shopAnimal3.name} the {shopAnimal3.species}, $250" +
                              $"\nd) Back to main menu");
            
            string menuChoice = Console.ReadLine().ToLower();
            while (menuChoice != "a" && menuChoice != "b" && menuChoice != "c" && menuChoice != "d")
            {
                Console.Write("Please input one of the options listed above.");
                menuChoice = Console.ReadLine().ToLower();
            } //Once again, requires the user to enter A, a, B, b, C, c, D, d to continue. Else, they are prompted to input a different option
            Console.Clear();
            switch (menuChoice)
            {
                case "a":
                    PurchaseConfirmation(50);
                    break;
                case "b":
                    PurchaseConfirmation(150);
                    break;
                case "c":
                    PurchaseConfirmation(250);
                    break;
                case "d":
                    MainMenu();
                    break;
                default: break;
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
                Console.Write("Please input one of the options listed above.");
                menuChoice = Console.ReadLine().ToLower();
            }
            switch(menuChoice)
            {
                case "a":
                    animals.Add(firstAnimal);
                    Console.WriteLine($"Purchased {firstAnimal.name} the {firstAnimal.species}.");
                    Console.ReadLine();
                    Console.Clear();
                    MainMenu();
                    break;
                case "b":
                    Console.Clear();
                    MainMenu();
                    break;
                default: break;
            }
        }
        static void PurchaseConfirmation(int price)
        {

            //Confirms the purchase, removes money from user's account and adds animal to the list of animals
            ShopRestock();
        }
        static void ShopRestock()
        {
            //Once an animal is purchased, a new, randomized one should take its place in the shop.
        }
        static void SaveExit()
        {
            string saveInfo = SaveInfo();
            File.WriteAllText("save.txt", saveInfo);
            //Writes the returned value of saveInfo to a .txt file

            Console.WriteLine("Saving . . .");
            Console.WriteLine("Data successfully saved!");
        }
        static string SaveInfo()
        {
            string saveInfo = "";
            for (int i = 0; i < animals.Count; i++)
            {
                saveInfo += $"{animals[i].name}\n{animals[i].species}\n{animals[i].income}\n";
            }
            //Saves all animal data to one string, with several lines
            return saveInfo;
        }
        static void LoadSave()
        {
            string line;
            int loadCount = 0;
            if (File.Exists("save.txt"))
            {
                using (StreamReader sr = new StreamReader("save.txt"))
                {
                    //Reads save.txt line by line and reconstructs the animal objects
                    int userMoney;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string name = line.Trim();

                        line = sr.ReadLine();
                        string species = line.Trim();

                        line = sr.ReadLine();
                        string mStr = line.Trim();
                        int income = int.Parse(mStr);

                        animals.Add(new Animal(name, species, income));
                        Console.Write($"Loaded: {name} the {species}, $/s: {income}\n");
                        loadCount++;
                    }
                }
            }
            if (loadCount == 0 || File.Exists("save.txt") == false)
                Console.WriteLine("No saved animals found.\n");
            else Console.WriteLine("Loading complete.\n");
        }
        static void Main(string[] args)
        {
            LoadSave();
            MainMenu();
        }
    }
}

/*
- randomized animals in shop for set prices (3 options at a time, 1 small/cheap, 1 medium, 1 large/expensive)
    - when an animal is bought, another randomized animal should take its place in the shop
    - animal names randomized from a .txt file of a bunch of different options
    - use enums for animal species randomization
- implement income system
    - user should passively gain income from animals they own (the sum of all the owned animals' incomeStat field, per second)
    - shop should accurately remove income from user's account
*/