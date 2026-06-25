using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


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

        static void Header(string title)
        {
            Console.Clear();


            Console.WriteLine("=======================================================================================================================");
            Console.ForegroundColor =
                ConsoleColor.Green;

            Console.WriteLine(@"

                    ██████╗ ██████╗  ██████╗  ██████╗ █████╗ ██████╗
                   ██╔════╝ ██╔══██╗██╔═══██╗██╔════╝██╔══██╗██╔══██╗
                   ██║  ███╗██████╔╝██║   ██║██║     ███████║██████╔╝
                   ██║   ██║██╔══██╗██║   ██║██║     ██╔══██║██╔═══╝
                   ╚██████╔╝██║  ██║╚██████╔╝╚██████╗██║  ██║██║
                    ╚═════╝ ╚═╝  ╚═╝ ╚═════╝  ╚═════╝╚═╝  ╚═╝╚═╝

                                      G R O C A P
                        Grocery Consumption Analysis Planner

                           SDG 12: Responsible Consumption & Production");

            Console.ResetColor();

            Console.WriteLine("=======================================================================================================================");


            Console.WriteLine(title);

            Console.WriteLine("=======================================================================================================================\n");

        }

        static void StartMenu()
        {
            while (true)
            {
                Console.Clear();

                Header("");

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
                        SignUpConfirmation();
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nPlease input a valid option.");
                        Pause();
                        break;
                }
            }
        }

        static void SignUpConfirmation()
        {

            Console.Clear();

            Header("Already have an account?(Y/N): ");
            string answer = Console.ReadLine().ToLower();

            Console.WriteLine("\n=======================================================================================================================");

            if (answer == "y")
            {
                Console.WriteLine("\nLogin Here");
                Pause();
                Console.Clear();

                Login();
                return;
            }

            else if (answer == "n")
            {
                Console.WriteLine("\nCreate an account");
                Pause();
                Console.Clear();

                SignUp();
                return;
            }

            Console.WriteLine("\nInvalid choice.");
            Pause();

        }

        static void SignUp()
        {
            Console.Clear();
            while (true)
            {

                Header("SIGN UP");

                Console.Write("Enter Email: ");
                string gmail = Console.ReadLine().Trim().ToLower();

                if (!Regex.IsMatch(gmail, @"^[a-zA-Z][a-zA-Z0-9._]{2,}@gmail\.com$"))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid Gmail format.");
                    Console.WriteLine("Example: juan123@gmail.com");
                    Pause();
                    Console.Clear();
                    continue;
                }

                if (string.IsNullOrEmpty(gmail))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter email account.\n");
                    Pause();
                    Console.Clear();
                    continue;
                }

                if (!gmail.EndsWith("@gmail.com"))
                {
                    Console.WriteLine("Invalid Gmail address!");
                    Pause();
                    Console.Clear();
                    continue;
                }


                Console.Write("Create Username: ");
                string username = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a username.\n");
                    Pause();
                    Console.Clear();
                    continue;
                }

                string userFolder = Path.Combine("Users", username);

                if (Directory.Exists(userFolder))
                {
                    Console.WriteLine();
                    Console.WriteLine("Username already exists!\n");
                    Pause();
                    Console.Clear();
                    continue;
                }

                Console.Write("Password: ");
                string password = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a password.\n" +
                        "");
                    Pause();
                    Console.Clear();
                    continue;
                }

                Console.Write("Confirm Password: ");
                string repassword = Console.ReadLine();

                if (password != repassword)
                {

                    Console.WriteLine("\nPasswords do not match.\n");
                    Pause();
                    Console.Clear();
                    continue;

                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nWELCOME!");
                Console.ResetColor();

                Console.WriteLine("Before we set up your account, we would like to ask a few brief questions to customize your experience. Please take a moment to review our Terms and Conditions below.\n\n");

                Console.WriteLine("I consent to the collection, use, storage, sharing, and processing of my personal data by the Social Security System (SSS) in accordance with the Data Privacy Act (DPA) and its Implementing Rules and Regulations (IRR). I affirm my rights as a data subject, including the rights to be informed, object, access, correct or dispute inaccuracies, suspend or withdraw my data, data portability, and to be indemnified for damages. I also understand my right to file a complaint with the National Privacy Commission (NPC) for any violation of my data privacy rights.\n\n");
                Console.WriteLine("[1]I do not consent.\n[2] I consent.");
                Console.WriteLine("---------------------------");
                string consent = Console.ReadLine();

                switch (consent)
                {
                    case "1":
                        Console.WriteLine("You must consent to create an account.");
                        Pause();
                        Console.Clear();
                        return;


                    case "2":
                        Pause();
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        Console.Clear();
                        continue;

                }

                Console.Write("\nTarget Grocery Budget ($): ");
                string budget = Console.ReadLine();

                if (string.IsNullOrEmpty(budget))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter an amount.\n ");
                    Pause();
                    Console.Clear();
                    continue;
                }

                Console.Write("\nTarget Grocery Savings ($): ");
                string savings = Console.ReadLine();

                if (string.IsNullOrEmpty(savings))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter an amount.\n");
                    Pause();
                    Console.Clear();
                    continue;
                }

                string visits = "";
                string members = "";

                while (true)
                {
                    Console.WriteLine("\nHow often do you go to the supermarket?\n");

                    Console.WriteLine("[1] Every Week");
                    Console.WriteLine("[2] Every 2 Weeks");
                    Console.WriteLine("[3] Every 3 Weeks");
                    Console.WriteLine("[4] Once a Month");

                    Console.Write("\nChoice: ");

                    string visitChoice = Console.ReadLine();

                    switch (visitChoice)
                    {
                        case "1":
                            visits = "Every Week";
                            break;

                        case "2":
                            visits = "Every 2 Weeks";
                            break;

                        case "3":
                            visits = "Every 3 Weeks";
                            break;

                        case "4":
                            visits = "Once a Month";
                            break;

                        default:
                            Console.WriteLine("\nInvalid choice.");
                            Pause();
                            continue;
                    }

                    break;
                }


                while (true)
                {
                    Console.WriteLine("\nHow many family members are in your household?\n");

                    Console.WriteLine("[1] 1 - 3 Family Members");
                    Console.WriteLine("[2] 4 - 7 Family Members");
                    Console.WriteLine("[3] 8 - 10 Family Members");
                    Console.WriteLine("[4] More than 10 Family Members");

                    Console.Write("\nChoice: ");

                    string memberChoice = Console.ReadLine();

                    switch (memberChoice)
                    {
                        case "1":
                            members = "1-3";
                            break;

                        case "2":
                            members = "4-7";
                            break;

                        case "3":
                            members = "8-10";
                            break;

                        case "4":
                            members = "10+";
                            break;

                        default:
                            Console.WriteLine("\nInvalid choice.");
                            Pause();
                            continue;
                    }

                    break;
                }

                Directory.CreateDirectory(userFolder);

                File.WriteAllLines(Path.Combine(userFolder, "Profile.txt"), new string[]
                    {
                    $"Username|{username}",
                    $"Password|{password}",
                    $"Gmail|{gmail}",
                    $"Budget|{budget}",
                    $"Savings|{savings}",
                    $"Visits|{visits}",
                    $"Members|{members}"
                    });

                File.Create(Path.Combine(userFolder, "Inventory.txt")).Close();


                File.Create(Path.Combine(userFolder, "SavingsHistory.txt")).Close();


                Console.WriteLine("\nAccount Created Successfully!");
                Pause();
                Console.Clear();
                return;
            }
        }

        static void Login()
        {
            Console.Clear();
            while (true)
            {

                Header("LOG IN");

                Console.Write("Username: ");
                string username = Console.ReadLine().Trim();

                string userFolder = Path.Combine("Users", username);

                if (!Directory.Exists(userFolder))
                {
                    Console.WriteLine("\nAccount not found!");
                    Pause();
                    Console.Clear();
                    continue;
                }

                Console.Write("Password: ");
                string password = Console.ReadLine().Trim();

                string profilePath =
                    Path.Combine(userFolder, "Profile.txt");

                string[] profile = File.ReadAllLines(profilePath);

                string savedPassword = "";

                foreach (string pass in profile)
                {
                    string[] data = pass.Split('|');

                    if (data[0] == "Password")
                    {
                        savedPassword = data[1];
                    }
                }

                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine();
                    Console.Write("Forgot Password?(Y/N): ");
                    string choice = Console.ReadLine().ToLower();

                    if (choice == "y")
                    {
                        Console.Clear();
                        ChangePassword();
                    }

                    else if (choice == "n")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please retry password.");
                        Pause();
                        Console.Clear();
                        continue;
                    }
                }

                if (password != savedPassword)
                {
                    Console.WriteLine("\nIncorrect Password!");
                    Pause();
                    Console.Clear();
                    continue;
                }



                CurrentUser = username;
                UserFolder = userFolder;

                Console.WriteLine("\nLogin Successful!");
                Pause();

                Dashboard();
                break;
            }
        }

        static void Dashboard()
        {
            while (true)
            {
                Console.Clear();

                double budget = 0;
                double savings = 0;

                string members = "";
                string visits = "";

                string profilePath =
                Path.Combine(UserFolder, "Profile.txt");

                string[] profile =
                    File.ReadAllLines(profilePath);

                foreach (string line in profile)
                {
                    string[] data = line.Split('|');

                    switch (data[0])
                    {
                        case "Budget":
                            budget = double.Parse(data[1]);
                            break;

                        case "Savings":
                            savings = double.Parse(data[1]);
                            break;

                        case "Members":
                            members = data[1];
                            break;

                        case "Visits":
                            visits = data[1];
                            break;
                    }
                }

                Header("MENU");

                Console.WriteLine($"Welcome, {CurrentUser}");
                Console.WriteLine();

                Console.WriteLine($"Monthly Budget: ${budget}");
                Console.WriteLine($"Savings: ${savings}");
                Console.WriteLine($"Store Visits: {visits}");
                Console.WriteLine($"Family Members: {members}");

                Console.WriteLine("\n=======================================================================================================================\n");

                Console.WriteLine("[1] Account Information");
                Console.WriteLine("[2] Grocery Planner");
                Console.WriteLine("[3] Inventory Management");
                Console.WriteLine("[4] Savings Tracker");
                Console.WriteLine("[5] Log out");

                Console.Write("\nEnter Choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {

                    case "1":

                        AccountInformation();
                        break;

                    case "2":
                        PlannerConfirmation();
                        break;

                    case "3":
                        InventoryMenu(members, visits, budget, savings);
                        break;

                    case "4":
                        RecordSavings();
                        break;

                    case "5":
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

        static void AccountInformation()
        {
            Console.Clear();

            string profilePath =
            Path.Combine(UserFolder, "Profile.txt");

            string[] profile =
                File.ReadAllLines(profilePath);

            string savedPassword = "";


            Header("ACCOUNT INFOFRMATION");

            foreach (string line in profile)
            {
                Console.WriteLine(
                    line.Replace("|", ": "));

                string[] data = line.Split('|');

                if (data[0] == "Password")
                {
                    savedPassword = data[1];
                }
            }

            Console.WriteLine();
            Console.Write("Update household information? (Y/N): ");

            string choice =
                Console.ReadLine().ToUpper();

            if (choice != "Y")
                return;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\n=================================");
                Console.WriteLine(" UPDATE HOUSEHOLD INFORMATION" +
                    "\n(Leave blank to keep current value.)");
                Console.WriteLine("=================================\n");

                string gmail = "";
                string budget = "";
                string visits = "";
                string members = "";
                


                Console.Write("Gmail: ");
                string newgmail = Console.ReadLine();

                if (string.IsNullOrEmpty(gmail))
                {
                    gmail = newgmail;
                }

                else if (!gmail.EndsWith("@gmail.com"))
                {
                    Console.WriteLine("Invalid Gmail address!");
                    Pause();
                    Console.Clear();
                    continue;
                }

                Console.Write("Monthly Grocery Budget: $");
                string newbudget = Console.ReadLine();
                double testBudget;

                if (string.IsNullOrEmpty(budget))
                {
                   budget = newbudget;
                }

                else if (!double.TryParse(newbudget, out testBudget))
                {
                    Console.WriteLine("Please enter a valid budget.");
                    Pause();
                    continue;
                }

                while (true)
                {

                    Console.WriteLine("\nHow often do you go to the supermarket?\n");

                    Console.WriteLine("[1] Every Week");
                    Console.WriteLine("[2] Every 2 Weeks");
                    Console.WriteLine("[3] Every 3 Weeks");
                    Console.WriteLine("[4] Once a Month");

                    Console.Write("\nChoice: ");

                    string newvisits = Console.ReadLine();

                    if (string.IsNullOrEmpty(newvisits))
                    {
                        visits = newvisits;
                    }

                    switch (newvisits)
                    {
                        case "1":
                            newvisits = "Every Week";
                            break;

                        case "2":
                            newvisits = "Every 2 Weeks";
                            break;

                        case "3":
                            newvisits = "Every 3 Weeks";
                            break;

                        case "4":
                            newvisits = "Once a Month";
                            break;

                        default:
                            Console.WriteLine("\nInvalid choice.");
                            Pause();
                            continue;
                    }
                    break;
                }

                while (true)
                {
                    
                    Console.WriteLine("\nHow many family members are in your household?\n");

                    Console.WriteLine("[1] 1 - 3 Family Members");
                    Console.WriteLine("[2] 4 - 7 Family Members");
                    Console.WriteLine("[3] 8 - 10 Family Members");
                    Console.WriteLine("[4] More than 10 Family Members");

                    Console.Write("\nChoice: ");

                    string newmember = Console.ReadLine();

                    if (string.IsNullOrEmpty(newmember))
                    {
                        members = newmember;
                    }

                    switch (newmember)
                    {
                        case "1":
                            newmember = "1-3";
                            break;

                        case "2":
                            newmember = "4-7";
                            break;

                        case "3":
                            newmember = "8-10";
                            break;

                        case "4":
                            newmember = "10+";
                            break;

                        default:
                            Console.WriteLine("\nInvalid choice.");
                            Pause();
                            continue;
                    }

                    break;
                }

                string[] newProfile =
                {
                $"Username|{CurrentUser}",
                $"Password|{savedPassword}",
                $"Gmail|{gmail}",
                $"Budget|{budget}",
                //$"Savings|{savedSavings}",
                $"Visits|{visits}",
                $"Members|{members}"
    };

                File.WriteAllLines(
                    profilePath,
                    newProfile);

                Console.WriteLine();
                Console.WriteLine(
                    "Household information updated successfully!");

                Pause();
                break;
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void InventoryMenu(string members, string visits, double budget, double savings)
        {
            while (true)
            {
                Console.Clear();


                Header("INVENTORY MANAGEMENT");


                ViewInventory();

                Console.WriteLine("\n[1] Add Item");
                Console.WriteLine("[2] Update Quantity");
                Console.WriteLine("[3] Remove Item");
                Console.WriteLine("[4] Back");

                Console.Write("\nChoice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddInventory();
                        break;

                    case "2":
                        UpdateInventory();
                        break;

                    case "3":
                        RemoveInventory();
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Please input a valid option...");
                        break;

                }
            }
        }

        static void ChangePassword()
        {

            while (true)
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                string profilePath = Path.Combine("Users", username, "Profile.txt");

                if (!File.Exists(profilePath))
                {
                    Console.WriteLine("\nAccount not found please check username.");
                    Pause();
                    Console.Clear();
                    continue;
                }


                Console.Write("Enter Gmail: ");
                string gmail = Console.ReadLine();

                if (!gmail.EndsWith("@gmail.com"))
                {
                    Console.WriteLine();
                    Console.WriteLine("\nInvalid Gmail address!");
                    Pause();
                    Console.Clear();
                    continue;
                }

                string[] profile =
                    File.ReadAllLines(profilePath);

                string savedGmail = "";

                foreach (string line in profile)
                {
                    string[] data = line.Split('|');

                    if (data[0] == "Gmail")
                    {
                        savedGmail = data[1];
                    }
                }

                if (gmail.ToLower() != savedGmail.ToLower())
                {
                    Console.WriteLine();
                    Console.WriteLine("\nGmail does not match account.");
                    Pause();
                    Console.Clear();
                    continue;
                }

                Console.Write("\nConfirm Gmail account(Y/N): ");
                string confirmation = Console.ReadLine().ToLower();

                if (confirmation != "y")
                {
                    Console.WriteLine("\nVerification cancelled.");
                    Pause();
                    Console.Clear();
                    continue;
                }

                Random rnd = new Random();
                int numcode = rnd.Next(1000, 9999);

                Console.WriteLine($"\nAn OTP has been sent to {gmail}");
                Console.ReadKey();
                Console.WriteLine($"\nHello {username}, your OTP is {numcode}");

                Pause();
                Console.Clear();

                Console.WriteLine("=========================================");
                Console.WriteLine("    PLEASE ENTER VERIFICATION CODE");
                Console.WriteLine("=========================================\n");

                Console.Write("Enter code: ");
                string code = Console.ReadLine();
                Console.WriteLine("----");

                if (code != numcode.ToString())
                {
                    Console.WriteLine("Incorrect verification code. Would you like to try again?:");

                }

                Console.Write("Enter New Password: ");
                string newpass = Console.ReadLine();

                Console.Write("Confirm New Password: ");
                string confirmnewpass = Console.ReadLine();

                if (newpass != confirmnewpass)
                {

                    Console.WriteLine("\nPasswords do not match.\n");
                    Pause();
                    Console.Clear();
                    continue;

                }



                for (int i = 0; i < profile.Length; i++)
                {
                    if (profile[i].StartsWith("Password|"))
                    {
                        profile[i] =
                            $"Password|{newpass}";
                    }
                }

                File.WriteAllLines(profilePath, profile);

                Console.WriteLine("\nPassword Updated Successfully!");
                Pause();
                Console.Clear();
                break;
            }
        }

        static void AddInventory()
        {
            Console.Clear();

            Console.Write("Item Name: ");
            string name = Console.ReadLine();

            Console.Write("Price: $");
            string price = Console.ReadLine();

            Console.Write("Quantity: ");
            string quantity = Console.ReadLine();

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(quantity))
            {
                Console.WriteLine();
                Console.WriteLine("No information entered. Returning to menu...");
                Console.ReadKey();
                return;
            }



            Console.WriteLine("\nItem Added!");

            File.AppendAllText(
            inventoryPath,
            $"{name}|{price}|{quantity}\n");



            Pause();
        }

        static void ViewInventory()
        {

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            string[] items =
                File.ReadAllLines(inventoryPath);

            double grandTotal = 0;

            if (items.Length == 0)
            {
                Console.WriteLine("Inventory Empty.");
            }
            else
            {
                Console.WriteLine(
                    "Item\t\tQty\tPrice\t\tTotal");

                Console.WriteLine(
                    "-------------------------------------------------------");

                foreach (string item in items)
                {
                    string[] data = item.Split('|');

                    string itemName = data[0];

                    int quantity =
                        int.Parse(data[1]);

                    double price =
                        double.Parse(data[2]);

                    double total =
                        quantity * price;

                    grandTotal += total;

                    Console.WriteLine("------------------------------------------------------------");

                    Console.WriteLine(
                    $"{"ITEM",-15}{"QTY",-10}{"PRICE",-12}{"TOTAL"}");

                    Console.WriteLine("------------------------------------------------------------");
                }

                Console.WriteLine("\n-------------------------------------------");

                Console.WriteLine(
                $"Total Grocery Cost: ${grandTotal}");
            }


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


        static void GroceryPlanner(string members, string visits, double budget, double savings)
        {

            string inventoryPath = Path.Combine(UserFolder, "Inventory.txt");

            List<string> planner = new List<string>();

            double total = 0;

            if (members == "1-3")
            {
                planner.Add("Rice|1|55");
                planner.Add("Eggs|12|10");
                planner.Add("Bread|2|90");

                total += 55;
                total += 120;
                total += 180;
            }

            else if (members == "4-7")
            {
                planner.Add("Rice|2|55");
                planner.Add("Eggs|24|10");
                planner.Add("Bread|4|90");

                total += 110;
                total += 240;
                total += 360;
            }

            else
            {
                planner.Add("Rice|3|55");
                planner.Add("Eggs|36|10");
                planner.Add("Bread|6|90");

                total += 165;
                total += 360;
                total += 540;
            }


            if (budget >= 8000)
            {
                planner.Add("Chicken|5|200");
                planner.Add("Milk|5|75");

                total += 1000;
                total += 375;
            }

            else if (budget >= 4000)
            {
                planner.Add("Chicken|3|200");
                planner.Add("Vegetables|5|100");

                total += 600;
                total += 500;
            }

            else
            {
                planner.Add("Vegetables|5|100");
                planner.Add("Fish|2|180");

                total += 500;
                total += 360;
            }

            File.WriteAllLines(inventoryPath, planner);

            Console.Clear();

            Console.WriteLine("=========================================");
            Console.WriteLine("          GROCYCLE PLANNER");
            Console.WriteLine("=========================================\n");

            Console.WriteLine($"Family Size: {members}");
            Console.WriteLine($"Budget: ${budget}");
            Console.WriteLine($"Target Savings: ${savings}");
            Console.WriteLine($"Shopping Frequency: {visits}");

            Console.WriteLine("\nSuggested Grocery List");
            Console.WriteLine("----------------------------------------");

            foreach (string item in planner)
            {
                string[] data = item.Split('|');

                Console.WriteLine(
                    $"{data[0],-15} Qty:{data[1],-5} Price: ${data[2]}");
            }

            Console.WriteLine("----------------------------------------");

            Console.WriteLine($"Total Cost: ${total}");

            double spendingLimit = budget - savings;

            Console.WriteLine($"Recommended Spending Limit: ${spendingLimit}");

            if (total <= spendingLimit)
            {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nWithin Budget!\n");
                Console.ResetColor();
                Console.WriteLine($"Remaining: ${spendingLimit - total}");
            }
            else
            {
                Console.WriteLine(
                    $"Overspent: ${total - spendingLimit}");
            }

            Console.WriteLine("Would you like ");

            Pause();
        }

        static void PlannerConfirmation()
        {
            Console.Clear();

            double budget = 0;
            double savings = 0;

            string members = "";
            string visits = "";

            string profilePath =
            Path.Combine(UserFolder, "Profile.txt");

            string[] profile =
                File.ReadAllLines(profilePath);

            foreach (string line in profile)
            {
                string[] data = line.Split('|');

                switch (data[0])
                {
                    case "Budget":
                        budget = double.Parse(data[1]);
                        break;

                    case "Savings":
                        savings = double.Parse(data[1]);
                        break;

                    case "Members":
                        members = data[1];
                        break;

                    case "Visits":
                        visits = data[1];
                        break;
                }
            }

            Console.Clear();

            Console.WriteLine("=======================================================================================================================");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(@"

                    ██████╗ ██████╗  ██████╗  ██████╗ █████╗ ██████╗
                   ██╔════╝ ██╔══██╗██╔═══██╗██╔════╝██╔══██╗██╔══██╗
                   ██║  ███╗██████╔╝██║   ██║██║     ███████║██████╔╝
                   ██║   ██║██╔══██╗██║   ██║██║     ██╔══██║██╔═══╝
                   ╚██████╔╝██║  ██║╚██████╔╝╚██████╗██║  ██║██║
                    ╚═════╝ ╚═╝  ╚═╝ ╚═════╝  ╚═════╝╚═╝  ╚═╝╚═╝

                                      G R O C A P
                        Grocery Consumption Analysis Planner

                           SDG 12: Responsible Consumption & Production

");

            Console.ResetColor();
            Console.WriteLine("=======================================================================================================================\n");

            Console.Write("Want us to make your planner? (Y/N): ");
            string choice = Console.ReadLine().ToUpper();
            Console.WriteLine("\n=======================================================================================================================");

            if (choice == "Y")
            {
                GroceryPlanner(members, visits, budget, savings);

                InventoryMenu(members, visits, budget, savings);
            }

            else
            {
                InventoryMenu(members, visits, budget, savings);
            }

        }

        static void RecordSavings()
        {
            Console.Clear();

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            string profilePath =
                Path.Combine(UserFolder, "Profile.txt");

            string savingsPath =
                Path.Combine(UserFolder, "SavingsHistory.txt");

            double budget = 0;

            string[] profile =
                File.ReadAllLines(profilePath);

            foreach (string line in profile)
            {
                string[] data = line.Split('|');

                if (data[0] == "Budget")
                {
                    budget =
                        double.Parse(data[1]);
                }
            }

            double total = 0;

            string[] items =
                File.ReadAllLines(inventoryPath);

            foreach (string item in items)
            {
                string[] data = item.Split('|');

                int quantity =
                    int.Parse(data[1]);

                double price =
                    double.Parse(data[2]);

                total += quantity * price;
            }

            double saved =
                budget - total;

            Header("SAVINGS TRACKER");

            Console.WriteLine($"Budget: ${budget}");
            Console.WriteLine($"Current Grocery Total: ${total}");

            if (saved >= 0)
            {
                Console.ForegroundColor =
                    ConsoleColor.Green;

                Console.WriteLine(
                    $"Money Saved: ${saved}");

                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor =
                    ConsoleColor.Red;

                Console.WriteLine(
                    $"Overspent: ${Math.Abs(saved)}");

                Console.ResetColor();
            }

            File.AppendAllText(
                savingsPath,
                $"{DateTime.Now:yyyy-MM-dd}|{saved}\n");

            Pause();
        }
    }
}
