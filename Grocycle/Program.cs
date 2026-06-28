using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp7
{
    internal class Program
    {

        static string CurrentUser = "";
        static string UserFolder = "";

        static double Budget = 0;
        static double SavingsGoal = 0;

        static string Visits = "";
        static string Members = "";

        static double CurrentTotal = 0;
        static double RemainingBudget = 0;
        static double CurrentSavings = 0;

        static double SavingsUsed = 0;

        static double RecommendedSpending = 0;

        

        static void Main(string[] args)
        {
            Directory.CreateDirectory("Users");

            StartMenu();
        }

        //==================================================
        // MENUS
        //==================================================

        static void StartMenu()
        {
            while (true)
            {
                Header("HOME");

                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Sign Up");
                Console.WriteLine("[3] Exit");

                Console.Write("\nChoice: ");

                switch (Console.ReadLine())
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

                        Error("Invalid choice.");
                        break;
                }
            }
        }

        static void Dashboard()
        {
            Header("DASHBOARD");

            while (true)
            {
                LoadProfile();

                CalculateInventory();

                Console.WriteLine($"Welcome, {CurrentUser}!");

                Console.WriteLine();

                Console.WriteLine($"Members         : {Members}");

                Console.WriteLine($"Visits          : {Visits}");

                Console.WriteLine($"Budget          : ₱{Budget}");

                Console.WriteLine($"Savings Goal    : ₱{SavingsGoal}"); 

                Console.WriteLine($"Current Savings : ₱{CurrentSavings}");

                Console.WriteLine("--------------------------------------------------------------------------------------------------");

                Console.WriteLine("[1] Account");

                Console.WriteLine("[2] Grocery Planner");

                Console.WriteLine("[3] Inventory");

                Console.WriteLine("[4] Savings Tracker");

                Console.WriteLine("[5] Logout");

                Console.Write("\nChoice: ");

                switch (Console.ReadLine())
                {
                    case "1":

                        AccountInformation();
                        break;

                    case "2":

                        PlannerConfirmation();
                        break;

                    case "3":

                        InventoryMenu();
                        break;

                    case "4":

                        SavingsTracker();
                        break;

                    case "5":

                        CurrentUser = "";

                        UserFolder = "";

                        Budget = 0;

                        SavingsGoal = 0;

                        CurrentSavings = 0;

                        CurrentTotal = 0;

                        return;

                    default:

                        Error("Invalid choice.");
                        break;
                }
            }
        }

        static void PlannerConfirmation()
        {
            
            Header("PLANNER");

            Console.Write("Would you like GROCAP to generate your grocery planner? (Y/N): ");

            string choice =
                Console.ReadLine().ToUpper();

            if (choice == "Y")
            {
                GroceryPlanner();

                InventoryMenu();
            }

            else if (choice == "N")
            {
                InventoryMenu();
            }

            else
            {
                Error("Invalid choice.");
            }


        }

        static void InventoryMenu()
        {
            while (true)
            {
                Header("INVENTORY");

                ViewInventory();

                Console.WriteLine();

                Console.WriteLine("[1] Add Item");

                Console.WriteLine("[2] Update Quantity");

                Console.WriteLine("[3] Remove Item");

                Console.WriteLine("[4] Finalize Grocery Plan");

                Console.WriteLine("[5] Back to Dashboard");

                Console.WriteLine("\nChoice: ");
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

                       FinalizeGroceryPlan();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void SavingsTracker()
        {
            string historyPath = Path.Combine(UserFolder, "SavingsHistory.txt");

            string[] history = File.ReadAllLines(historyPath);

            Header("SAVINGS TRACKER");

            Console.WriteLine($"Current Savings: ₱{CurrentSavings}\n");

            Console.WriteLine("--------------------------------");

            Console.WriteLine("Savings History:");

            Console.WriteLine("-----------------");

            Console.WriteLine("Date\t\tSavings");



            foreach (string line in history)
            {
                string[] data = line.Split('|');
                string date = data[0];
                double savings = double.Parse(data[1]);
                Console.WriteLine($"{date}: ₱{savings}");
            }

            Pause();
        }

        static void AccountInformation()
        {
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
                string savings = "";
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

                Console.Write("Target Savings: $");
                string newsavings = Console.ReadLine();
                double testsavings;

                if (string.IsNullOrEmpty(budget))
                {
                    savings = newsavings;
                }

                else if (!double.TryParse(newsavings, out testsavings))
                {
                    Console.WriteLine("Please enter a valid budget.");
                    Pause();
                    continue;
                }



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
                SaveProfile(gmail, budget, savings, visits, members);
            }
        }

        //==================================================
        // LOGIN
        //==================================================

        static void Login()
        {
            while (true)
            {
                Header("LOGIN");

                Console.Write("Username (B = Back): ");
                string username = Console.ReadLine().Trim();

                if (username.ToUpper() == "B")
                    return;

                string userFolder =
                    Path.Combine("Users", username);

                if (!Directory.Exists(userFolder))
                {
                    Error("Account not found.");
                    continue;
                }

                Console.Write("Password: ");
                string password = Console.ReadLine();

                string profilePath =
                    Path.Combine(userFolder, "Profile.txt");

                string savedPassword = "";

                foreach (string line in File.ReadAllLines(profilePath))
                {
                    string[] data = line.Split('|');

                    if (data[0] == "Password")
                    {
                        savedPassword = data[1];
                        break;
                    }
                }

                if (password != savedPassword)
                { 

                    Console.Write("\nForgot Password? (Y/N): ");

                    string answer = Console.ReadLine().ToUpper();

                    if (answer == "Y")
                    {
                        ChangePassword();
                    }

                    else if (answer == "N")
                    {
                        Console.WriteLine("\nPlease retry password.");
                        continue;
                    }

                    else
                    {
                        Error("Incorrect password.");
                    }
                }

                CurrentUser = username;
                UserFolder = userFolder;

                LoadProfile();

                Dashboard();

                return;
            }
        }

        static void SignUp()
        {
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

                Header("SIGN UP");
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

        static void ChangePassword()
        {
            Header("CHANGE PASSWORD");

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
                Header("CHANGE PASSWORD");

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

        //==================================================
        // GROCERY PLANNER
        //==================================================

        static void GroceryPlanner()
        {
            string inventoryPath = Path.Combine(UserFolder, "Inventory.txt");

            List<string> planner = new List<string>();

            // Clear old planner
            File.WriteAllText(inventoryPath, "");

            double spendingLimit = Budget - SavingsGoal;

           

            if (Members == "1-3")
            {
                planner.Add("Rice|50|5|Essential");
                planner.Add("Eggs|8|12|Essential");
                planner.Add("Bread|40|2|Essential");
                planner.Add("Water|25|6|Essential");
            }

            else if (Members == "4-7")
            {
                planner.Add("Rice|50|10|Essential");
                planner.Add("Eggs|8|30|Essential");
                planner.Add("Bread|40|4|Essential");
                planner.Add("Water|25|10|Essential");
            }

            else
            {
                planner.Add("Rice|50|15|Essential");
                planner.Add("Eggs|8|60|Essential");
                planner.Add("Bread|40|6|Essential");
                planner.Add("Water|25|15|Essential");
            }

            if (Budget >= 8000)
            {
                planner.Add("Chicken|220|5|Essential");
                planner.Add("Milk|95|5|Essential");
                planner.Add("Fish|180|4|Essential");
                planner.Add("Fruits|120|6|Optional");
            }

            else if (Budget >= 4000)
            {
                planner.Add("Chicken|220|3|Essential");
                planner.Add("Milk|95|2|Essential");
                planner.Add("Vegetables|100|5|Essential");
            }

            else
            {
                planner.Add("Vegetables|80|5|Essential");
                planner.Add("Fish|150|2|Essential");
            }

            File.WriteAllLines(

                Path.Combine(UserFolder,"Inventory.txt"),planner);

            Console.WriteLine();

            Console.WriteLine("Planner generated!");

            CalculateInventory();

            Pause();
        }

        static void FinalizeGroceryPlan()
        {
            CalculateInventory();

            if (CheckBudgetStatus())
                return;

            if (CheckOverConsumption())
                return;

            if (CheckMissingEssentialItems())
                return;


            RecordSavings();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\nFinal Grocery Plan Saved!");

            Console.WriteLine("\n================================");
            Console.WriteLine(" FINAL GROCERY PLAN GENERATED");
            Console.WriteLine("================================");

            Console.ResetColor();

            Console.WriteLine($"Total Cost: ₱{CurrentTotal}");
            Console.WriteLine($"Savings: ₱{CurrentSavings}");
            Console.WriteLine($"Remaining Budget: ₱{RemainingBudget}");


            Console.ResetColor();

            Pause();
        }

        //==================================================
        // INVENTORY
        //==================================================

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
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine($"{"ITEM",-15}{"PRICE",-8}{"QTY",-10}{"TOTAL"}");
                Console.WriteLine("----------------------------------------------");

                foreach (string item in items)
                {
                    string[] data = item.Split('|');

                    string name = data[0];

                    double price = double.Parse(data[1]);

                    int qty = int.Parse(data[2]);

                    double total = price * qty;

                    grandTotal += total;

                    Console.WriteLine(
                        $"{name,-15}{price,-8:C}{qty,-10}{total:C}");
                }

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine($"{"", -33}Grand Total: {grandTotal:C}");
            }
        }

        static void AddInventory()
        {
            Header("ADD ITEM");

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
            $"{name}|{price}|{quantity}|Optional\n");

            CalculateInventory();

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

                    string qty = Console.ReadLine();

                    data[2] = qty;

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

                Console.WriteLine("\nQuantity Updated!");
            }
            else
            {
                Console.WriteLine("\nItem Not Found.");
            }

            CalculateInventory();

            Pause();
        }

        static void RemoveInventory()
        {
            string inventoryPath = Path.Combine(UserFolder, "Inventory.txt");

            List<string> items = File.ReadAllLines(inventoryPath).ToList();

            Console.Write("Item Name: ");

            string name =
                Console.ReadLine();

            bool removed = false;

            for (int i = 0; i < items.Count; i++)
            {
                string[] data = items[i].Split('|');

                if (data[0].Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    // Item belongs to the planner (essential)
                    if (data[3] == "Essential")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine("\n=========================================");
                        Console.WriteLine("           ESSENTIAL PRODUCT");
                        Console.WriteLine("=========================================\n");

                        Console.ResetColor();

                        Console.WriteLine($"{data[0]} is part of your recommended grocery planner.");

                        Console.WriteLine("Removing it may affect your grocery plan.");

                        Console.Write("\nRemove anyway? (Y/N): ");

                        string confirm =
                            Console.ReadLine().ToUpper();

                        if (confirm != "Y")
                        {
                            Console.WriteLine("\nRemoval cancelled.");

                            Pause();
                            return;
                        }
                    }

                    items.RemoveAt(i);

                    removed = true;

                    if (removed)
                    {
                        File.WriteAllLines(inventoryPath, items);

                        CalculateInventory();

                        Console.WriteLine("Item Removed!");
                    }

                    break;
                }
            }
        }

        //==================================================
        // CHECKING 
        //==================================================

        static int GetRecommendedQuantity(string itemName)
        {
            switch (Members)
            {
                case "1-3":

                    switch (itemName)
                    {
                        case "Rice": return 5;
                        case "Eggs": return 30;
                        case "Bread": return 2;
                        case "Chicken": return 2;
                        case "Fish": return 2;
                        case "Milk": return 2;
                        case "Water": return 12;
                        case "Cooking Oil": return 1;
                        case "Salt": return 1;
                        case "Sugar": return 1;
                        case "Vegetables": return 8;
                    }

                    break;

                case "4-7":

                    switch (itemName)
                    {
                        case "Rice": return 10;
                        case "Eggs": return 60;
                        case "Bread": return 4;
                        case "Chicken": return 4;
                        case "Fish": return 4;
                        case "Milk": return 4;
                        case "Water": return 24;
                        case "Cooking Oil": return 2;
                        case "Salt": return 2;
                        case "Sugar": return 2;
                        case "Vegetables": return 15;
                    }

                    break;

                case "8-10":

                    switch (itemName)
                    {
                        case "Rice": return 15;
                        case "Eggs": return 90;
                        case "Bread": return 6;
                        case "Chicken": return 6;
                        case "Fish": return 6;
                        case "Milk": return 6;
                        case "Water": return 36;
                        case "Cooking Oil": return 3;
                        case "Salt": return 3;
                        case "Sugar": return 3;
                        case "Vegetables": return 20;
                    }

                    break;

                default:        //10+
                    switch (itemName)
                    {
                        case "Rice": return 20;
                        case "Eggs": return 120;
                        case "Bread": return 8;
                        case "Chicken": return 8;
                        case "Fish": return 8;
                        case "Milk": return 8;
                        case "Water": return 48;
                        case "Cooking Oil": return 4;
                        case "Salt": return 4;
                        case "Sugar": return 4;
                        case "Vegetables": return 30;
                    }

                    break;
            }

            return int.MaxValue;     // Optional items are never considered overconsumed
        }

        static bool IsOverconsumed(string itemName, int quantity)
        {
            int recommended =
                GetRecommendedQuantity(itemName);

            return quantity > recommended;
        }


        static bool CheckOverConsumption()
        {
            bool found = false;

            string[] inventory =
                File.ReadAllLines(Path.Combine(UserFolder, "Inventory.txt"));

            foreach (string line in inventory)
            {
                string[] data = line.Split('|');

                string item = data[0];

                int qty = int.Parse(data[2]);

                if (IsOverconsumed(item, qty))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine("\n==================================");
                    Console.WriteLine(" POTENTIAL OVERCONSUMPTION");
                    Console.WriteLine("==================================");

                    Console.WriteLine(item);

                    Console.WriteLine($"Current Quantity : {qty}");

                    Console.WriteLine($"Recommended      : {GetRecommendedQuantity(item)}");

                    Console.ResetColor();

                    found = true;
                }
            }

            if (found)
                Pause();

            return found;
        }


        static bool CheckMissingEssentialItems()
        {
            bool missing = false;

            string inventoryPath =
                Path.Combine(UserFolder, "Inventory.txt");

            List<string> inventory =
                File.ReadAllLines(inventoryPath).ToList();

            foreach (string essential in inventory)
            {
                bool exists = false;

                foreach (string line in inventory)
                {
                    string[] data = line.Split('|');

                    string itemName = data[0];

                    if (itemName.Equals(essential,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine($"\nMissing Essential Item: {essential}");

                    Console.ResetColor();

                    missing = true;
                }
            }

            if (missing)
                Pause();

            return missing;
        }
        

        static bool CheckBudgetStatus()
        {
            CalculateInventory();

            if (CurrentTotal <= Budget)
                return false;

          
                double needed = CurrentTotal - Budget;

            if (needed <= SavingsGoal)
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine("==================================");
                Console.WriteLine("       SAVINGS AT RISK");
                Console.WriteLine("==================================");

                Console.ResetColor();

                Console.WriteLine($"Budget           : ₱{Budget}");
                Console.WriteLine($"Current Basket   : ₱{CurrentTotal}");
                Console.WriteLine($"Savings Goal     : ₱{SavingsGoal}");
                Console.WriteLine($"Savings Needed   : ₱{needed}");

                Console.WriteLine();

                Console.WriteLine("Adding this item exceeds your grocery budget.");
                Console.WriteLine("Your savings will be used to complete this purchase.");

                Pause();

                return false;
            }

            else
            {
                double excess = CurrentTotal - (Budget + SavingsGoal);

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine();
                Console.WriteLine("==================================");
                Console.WriteLine("      ERROR: BUDGET BLOWN");
                Console.WriteLine("==================================");

                Console.ResetColor();

                Console.WriteLine($"Current Basket : ₱{CurrentTotal}");
                Console.WriteLine($"Budget         : ₱{Budget}");
                Console.WriteLine($"Savings Goal   : ₱{SavingsGoal}");

                Console.WriteLine();

                Console.WriteLine($"You are ₱{excess} over your available funds.");

                Console.WriteLine();

                Console.WriteLine($"Remove approximately ₱{Math.Ceiling(excess / 100) * 100:N0} worth of items.");

                Pause();

                return true;
            }
       }
        
        

        static void Checker()
        {
            CheckOverConsumption();
            CheckMissingEssentialItems();
            CheckBudgetStatus();
        }

        //==================================================
        // SAVINGS
        //==================================================


        static void CalculateInventory()
            {
                CurrentTotal = 0;

                string inventoryPath =
                    Path.Combine(UserFolder, "Inventory.txt");

                foreach (string line in File.ReadAllLines(inventoryPath))
                {
                    string[] data = line.Split('|');

                    double price = double.Parse(data[1]);

                    int qty = int.Parse(data[2]);

                    CurrentTotal += price * qty;
                }

                RecommendedSpending = Budget - SavingsGoal;

                if (CurrentTotal <= RecommendedSpending)
                {
                    SavingsUsed = 0;

                    CurrentSavings = Budget - CurrentTotal;
                }
                else
                {
                    SavingsUsed = CurrentTotal - RecommendedSpending;

                    CurrentSavings = SavingsGoal - SavingsUsed;

                    if (CurrentSavings < 0)
                        CurrentSavings = 0;
                }
            }
        

        static void RecordSavings()
        {
            string historyPath =
        Path.Combine(UserFolder, "SavingsHistory.txt");

            File.AppendAllText(
                historyPath,
                $"{DateTime.Now:MM/dd/yyyy}|{CurrentTotal}|{CurrentSavings}\n");
        }

        //==================================================
        // FILE METHODS
        //==================================================

        static void LoadProfile()
        {
            string profilePath =
        Path.Combine(UserFolder, "Profile.txt");

            foreach (string line in File.ReadAllLines(profilePath))
            {
                string[] data = line.Split('|');

                switch (data[0])
                {
                    case "Budget":

                        Budget =
                            double.Parse(data[1]);

                        break;

                    case "Savings":

                        SavingsGoal =
                            double.Parse(data[1]);

                        break;

                    case "Visits":

                        Visits =
                            data[1];

                        break;

                    case "Members":

                        Members =
                            data[1];

                        break;

                    case "RecommendedSpending":
                        RecommendedSpending =
                            Budget - SavingsGoal;
                        break;  
                }
            }
        }

        static void SaveProfile(string gmail,string budget,string savings,string visits,string members)
        {
            string profilePath =
                Path.Combine(UserFolder, "Profile.txt");

            string[] profile =
                File.ReadAllLines(profilePath);

            for (int i = 0; i < profile.Length; i++)
            {
                string[] data = profile[i].Split('|');

                switch (data[0])
                {
                    case "Gmail":

                        if (!string.IsNullOrWhiteSpace(gmail))
                            data[1] = gmail;

                        break;

                    case "Budget":

                        if (!string.IsNullOrWhiteSpace(budget))
                            data[1] = budget;

                        break;

                    case "Savings":

                        if (!string.IsNullOrWhiteSpace(savings))
                            data[1] = savings;

                        break;

                    case "Visits":

                        if (!string.IsNullOrWhiteSpace(visits))
                            data[1] = visits;

                        break;

                    case "Members":

                        if (!string.IsNullOrWhiteSpace(members))
                            data[1] = members;

                        break;
                }

                profile[i] = string.Join("|", data);
            }

            File.WriteAllLines(profilePath, profile);

            LoadProfile();
        }

        //==================================================
        // UTILITIES
        //==================================================

        static void Header(string title)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(@"

              ██████╗ ██████╗  ██████╗  ██████╗ █████╗ ██████╗
             ██╔════╝ ██╔══██╗██╔═══██╗██╔════╝██╔══██╗██╔══██╗
             ██║  ███╗██████╔╝██║   ██║██║     ███████║██████╔╝
             ██║   ██║██╔══██╗██║   ██║██║     ██╔══██║██╔═══╝
             ╚██████╔╝██║  ██║╚██████╔╝╚██████╗██║  ██║██║
              ╚═════╝ ╚═╝  ╚═╝ ╚═════╝  ╚═════╝╚═╝  ╚═╝╚═╝

");

            Console.ResetColor();

            Console.WriteLine($"==================== {title} ====================\n");
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"\n{message}");

            Console.ResetColor();

            Pause();
        }
    }
}
