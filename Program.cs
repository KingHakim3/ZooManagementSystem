﻿using System.ComponentModel;
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
                new Animal("Rainier", "Rhino", 3),
                new Animal("George", "Monkey", 1)
            };
        //creates a class-scope list of all of the user's animals
        static void WelcomeMenu()
        {
            Console.WriteLine("Please choose an option. \na) Animal Database \nb) Trainers");
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
        public static void Database()
        {
            Console.WriteLine();
            for (int i = 0; i < animals.Count; i++)
            {
                Console.Write($"{i}. ");
                animals[i].DisplayAnimal();
            }
            Console.WriteLine("Press enter to return to the main menu.");
            Console.ReadLine();
            WelcomeMenu();
        }
        static void Shop()
        {

        }
        static void SaveExit()
        {
            string saveInfo = SaveInfo();
            File.WriteAllText("save.txt", saveInfo);
        }
        static string SaveInfo()
        {
            string saveInfo = "";
            for (int i = 0; i < animals.Count; i++)
            {
                saveInfo += $"{animals[i].name} {animals[i].species} {animals[i].money}\n";
            }
            Console.WriteLine(saveInfo);
            return saveInfo;
        }
        static void Main(string[] args)
        {
            Console.Write("Welcome to the Zoo Management System! ");
            WelcomeMenu();
        }
    }
}

/*
- make it more game-like. shop, animal database, trainers with specific boosts, tasks to make money (stopwatch typing speed game, memory game, catch-the-animal luck game)
- animal names randomized from a .txt file of a bunch of different options
- save function with .txt file
- rename feature for non-void method?
- use enums for animal species randomization
- use lists to compile all user animals into one object
- use interfaces & public/private access modifiers for animal-related methods

Meaningful comments
Arrays or Enums 
Methods (one returning void, and one returning non-void; one instance and one static method, and these can be combined, e.g., void/static, and non-void/instance)
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