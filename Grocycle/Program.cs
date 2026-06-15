using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grocycle
{
    internal class Program
    {
        static string CurrentUser;
        static string UserFolder;

        static void Main(string[] args)
        {
            Directory.CreateDirectory("Users");

            StartMenu();
        }

        static void StartMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=========================================");
                Console.WriteLine("              GROCYCLE");
                Console.WriteLine(" Smart Grocery Planner (SDG 12)");
                Console.WriteLine("=========================================\n");

                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Sign Up");
                Console.WriteLine("[3] Exit");

                Console.Write("\nEnter Choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;

                    case "2":
                        SignUp();
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nInvalid Choice!");
                        Pause();
                        break;
                }
            }
        }

        static void SignUp()
        {
            Console.Clear();

            Console.WriteLine("=========================================");
            Console.WriteLine("                SIGN UP");
            Console.WriteLine("=========================================\n");

            Console.Write("Create Username: ");
            string username = Console.ReadLine().Trim();

            string userFolder = Path.Combine("Users", username);

            if (Directory.Exists(userFolder))
            {
                Console.WriteLine("\nUsername already exists!");
                Pause();
                return;
            }

            Console.Write("\nMonthly Grocery Budget ($): ");
            string budget = Console.ReadLine();

            Console.Write("Supermarket Visits Per Month: ");
            string visits = Console.ReadLine();

            Console.Write("Number of Family Members: ");
            string members = Console.ReadLine();

            Directory.CreateDirectory(userFolder);

            File.WriteAllLines(
                Path.Combine(userFolder, "Profile.txt"),
                new string[]
                {
                    $"Username|{username}",
                    $"Budget|{budget}",
                    $"Visits|{visits}",
                    $"Members|{members}"
                });

            File.WriteAllLines(
                Path.Combine(userFolder, "Points.txt"),
                new string[]
                {
                    "Points|0",
                    "Rank|Eco Beginner"
                });

            File.Create(
                Path.Combine(userFolder, "Inventory.txt"))
                .Close();

            File.Create(
                Path.Combine(userFolder, "Purchases.txt"))
                .Close();

            File.Create(
                Path.Combine(userFolder, "Waste.txt"))
                .Close();

            File.Create(
                Path.Combine(userFolder, "Reports.txt"))
                .Close();

            Console.WriteLine("\nAccount Created Successfully!");
            Pause();
        }

        static void Login()
        {

            Console.Clear();

            Console.WriteLine("=========================================");
            Console.WriteLine("                 LOGIN");
            Console.WriteLine("=========================================\n");

            Console.Write("Username: ");
            string username = Console.ReadLine().Trim();

            string userFolder = Path.Combine("Users", username);

            if (!Directory.Exists(userFolder))
            {
                Console.WriteLine("\nAccount not found!");
                Pause();
                return;
            }

            CurrentUser = username;
            UserFolder = userFolder;

            Console.WriteLine("\nLogin Successful!");
            Pause();

            Dashboard();
        }

        static void Dashboard()
        {
           while (true)
            {
                Console.Clear();

                string profilePath =
                    Path.Combine(UserFolder, "Profile.txt");

                string[] profile =
                    File.ReadAllLines(profilePath);

                string budget = "";
                string visits = "";
                string members = "";

                foreach (string line in profile)
                {
                    string[] data = line.Split('|');

                    switch (data[0])
                    {
                        case "Budget":
                            budget = data[1];
                            break;

                        case "Visits":
                            visits = data[1];
                            break;

                        case "Members":
                            members = data[1];
                            break;
                    }
                }

                Console.WriteLine("=========================================");
                Console.WriteLine("               GROCYCLE");
                Console.WriteLine("=========================================\n");

                Console.WriteLine($"Welcome, {CurrentUser}");
                Console.WriteLine();

                Console.WriteLine($"Monthly Budget : ${budget}");
                Console.WriteLine($"Store Visits   : {visits}");
                Console.WriteLine($"Family Members : {members}");

                Console.WriteLine("\n=========================================\n");

                Console.WriteLine("[1] Inventory Management");
                Console.WriteLine("[2] Grocery Planner");
                Console.WriteLine("[3] Expiry Monitor");
                Console.WriteLine("[4] Waste Tracker");
                Console.WriteLine("[5] Budget Analysis");
                Console.WriteLine("[6] Monthly Report");
                Console.WriteLine("[7] Logout");

                Console.Write("\nEnter Choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        InventoryMenu();
                        break;

                    case "2":  
                        GroceryPlanner();
                        break;

                    case "3":
                        Placeholder("Expiry Monitor");
                        break;

                   case "4":
                        Placeholder("Waste Tracker");
                       break;

                   case "5":
                        Placeholder("Budget Analysis");
                       break;

                   case "6":
                        Placeholder("Monthly Report");
                        break;

                    case "7":
                        CurrentUser = "";
                        UserFolder = "";
                       return;

                    default:
                       Console.WriteLine("\nInvalid Choice!");
                       Pause();
                      break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void Placeholder(string module)
        {
            Console.Clear();

            Console.WriteLine("=========================================");
            Console.WriteLine(module);
            Console.WriteLine("=========================================\n");

            Console.WriteLine("This module will be implemented in Part 2.");

            Pause();
        }

        static void InventoryMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=========================================");
                Console.WriteLine("         INVENTORY MANAGEMENT");
                Console.WriteLine("=========================================");

                Console.WriteLine("[1] View Inventory");
                Console.WriteLine("[2] Add Item");
                Console.WriteLine("[3] Update Quantity");
                Console.WriteLine("[4] Remove Item");
                Console.WriteLine("[5] Back");

                Console.Write("\nChoice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewInventory();
                        break;

                    case "2":
                        AddInventory();
                        break;

                    case "3":
                        UpdateInventory();
                        break;

                    case "4":
                        RemoveInventory();
                        break;

                    case "5":
                        return;
                }
            }
        }

        static void AddInventory()
        {
            Console.Clear();

            Console.Write("Item Name: ");
            string name = Console.ReadLine();

            Console.Write("Quantity: ");
            string quantity = Console.ReadLine();

            Console.Write("Expiry Date (MM/DD/YYYY): ");
            string expiry = Console.ReadLine();

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            File.AppendAllText(
                inventoryPath,
                $"{name}|{quantity}|{expiry}\n");

            Console.WriteLine("\nItem Added!");

            Pause();
        }

        static void ViewInventory()
        {
            Console.Clear();

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            string[] items =
                File.ReadAllLines(inventoryPath);

            Console.WriteLine("=========================================");
            Console.WriteLine("               INVENTORY");
            Console.WriteLine("=========================================\n");

            if (items.Length == 0)
            {
                Console.WriteLine("Inventory Empty.");
            }
            else
            {
                Console.WriteLine(
                    "Item\t\tQty\tExpiry");

                Console.WriteLine(
                    "-----------------------------------------");

                foreach (string item in items)
                {
                    string[] data = item.Split('|');

                    Console.WriteLine(
                        $"{data[0]}\t\t{data[1]}\t{data[2]}");
                }
            }

            Pause();
        }

        static void UpdateInventory()
        {
            Console.Clear();

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            List<string> items =
                File.ReadAllLines(inventoryPath).ToList();

            Console.Write("Item Name: ");
            string itemName = Console.ReadLine();

            bool found = false;

            for (int i = 0; i < items.Count; i++)
            {
                string[] data = items[i].Split('|');

                if (data[0].ToLower() ==
                    itemName.ToLower())
                {
                    Console.Write("New Quantity: ");

                    string qty =
                        Console.ReadLine();

                    data[1] = qty;

                    items[i] =
                        string.Join("|", data);

                    found = true;
                    break;
                }
            }

            if (found)
            {
                File.WriteAllLines(
                    inventoryPath,
                    items);

                Console.WriteLine(
                    "\nQuantity Updated!");
            }
            else
            {
                Console.WriteLine(
                    "\nItem Not Found.");
            }

            Pause();
        }

        static void RemoveInventory()
        {
            Console.Clear();

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            List<string> items =
                File.ReadAllLines(inventoryPath).ToList();

            Console.Write("Item Name: ");

            string name =
                Console.ReadLine();

            bool removed = false;

            for (int i = 0; i < items.Count; i++)
            {
                string[] data =
                    items[i].Split('|');

                if (data[0].ToLower() ==
                    name.ToLower())
                {
                    items.RemoveAt(i);

                    removed = true;
                    break;
                }
            }

            if (removed)
            {
                File.WriteAllLines(
                    inventoryPath,
                    items);

                Console.WriteLine(
                    "\nItem Removed!");
            }
            else
            {
                Console.WriteLine(
                    "\nItem Not Found.");
            }

            Pause();
        }


        static void GroceryPlanner()
        {
            Console.Clear();

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            string[] items =
                File.ReadAllLines(inventoryPath);

            Console.WriteLine("=========================================");
            Console.WriteLine("            GROCERY PLANNER");
            Console.WriteLine("=========================================\n");

            bool foundLowStock = false;

            foreach (string item in items)
            {
                string[] data = item.Split('|');

                int quantity =
                    Convert.ToInt32(data[1]);

                if (quantity <= 2)
                {
                    foundLowStock = true;

                    Console.WriteLine(
                        $"Buy More: {data[0]}");
                }
            }

            if (!foundLowStock)
            {
                Console.WriteLine(
                    "No low-stock items detected.");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Suggested purchases are based on low inventory.");

            Pause(); 
        }

        static void GenerateShoppingList()
        {
        }

        static void AnalyzeConsumption()
        {
        }

        static void ExpiryMenu()
        {
        }

        static void CheckExpiringItems()
        {
        }

        static void SuggestItemUsage()
        {
        }



        static void WasteMenu()
        {
        }

        static void RecordWaste()
        {
        }

        static void SuggestDisposal()
        {
        }


        static void AddPoints(int points)
        {
        }

        static void DeductPoints(int points)
        {
        }

        static void ViewPoints()
        {
        }



        static void BudgetAnalysis()
        {
        }

        static void CheckOverspending()
        {
        }

        static void DetectOverconsumption()
        {
        }



        static void GenerateMonthlyReport()
        {
        }

        static void ComparePreviousMonth()
        {
        }

   
        static void SaveUserData()
        {
        }

        static void LoadUserData()
        {
        }
    }
}