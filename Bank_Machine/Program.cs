using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace BankMachine
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int FailedLoginAttempts = 0; FailedLoginAttempts < 3; FailedLoginAttempts++)
            {
                bool UserLoginsuccess = UserLogin(FailedLoginAttempts);
                if (UserLoginsuccess== true) 
                {
                    MainMenu();
                    return;
                }
                else
                {
                    // no else needed as all "else" is caught in the exceptions based on FailedLoginAttempts in the UserLoginProcess
                }

            }

            RestartProgram();

        }


        //due to lack of plastic card system, AccountNumber has been hardcoded to user id 1 "Hugo DaBoss"
        private static bool UserLogin(int FailedLoginAttempts)
        {

            var DBconnect = new DBClass();
            Console.WriteLine("  Please provide your Personal Identification Number (PIN):");
            Console.WriteLine("  Please confirm your PIN when ready by pressing the ENTER key once\n\n");

            string UserPin = Orb.App.Console.ReadPassword();

            string QueriedUserPin = DBconnect.SelectSingle("PIN");
            bool IsPINsuccess = ComparePinCodes(UserPin, QueriedUserPin, FailedLoginAttempts);

            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            return IsPINsuccess;

        }


        private static bool ComparePinCodes(string UserPinCode, string QueriedUserPin, int FailedLoginAttempts)
        {
            if (UserPinCode == QueriedUserPin)
            {
                 return true;
            }
            else
            {
                CalcAttemptsRemaining(FailedLoginAttempts);
                return false;
            }

        }


        private static string GetBalance()
        {

            var DBconnect = new DBClass();
            string UserCurrentBalance = DBconnect.SelectSingle("Balance");
            string UserName = DBconnect.SelectSingle("UserName");

            Console.Clear();
            Console.WriteLine("\n  Welcome, {0}\n\n", UserName);
            Console.WriteLine("  Your Balance is: \n\n" + "  EUR:  " + UserCurrentBalance + "\n\n");

            return UserCurrentBalance;

        }


        private static void CalcAttemptsRemaining(int FailedLoginAttempts)
        {

            int RemainingAttempts = 2 - FailedLoginAttempts;
            if (RemainingAttempts == 1)
            {
                Console.Write("  Incorrect PIN has been provided. \n" + 
                              "  Please remember you only have " + RemainingAttempts + " attempt left before your account gets locked! \n");
            }
            else if (RemainingAttempts == 0)
            {
                Console.WriteLine("  Sorry, your account has been locked due to security reasons. \n" +
                                  "  If you wish to unlock your account, \n" +
                                  "  please contact our staff at: Tel: 00(XX)+ XXXX XXXX \n");
                System.Threading.Thread.Sleep(4000);
                //RestartProgram();

            }
            else
            {
                Console.Write("  Incorrect PIN has been provided. \n" + 
                              "  Please remember you only have " + RemainingAttempts + " attempts left before your account gets locked! \n");
            }

        }


        private static void MainMenu()
        {
            //TODO build in a response timer, logout after X seconds of no user input

            Console.WriteLine("  Please make a choice out of the following options:");
            Console.WriteLine("  Please confirm your choice by pressing the ENTER key once\n\n");
            Console.WriteLine("  1 - Withdraw");
            Console.WriteLine("  2 - Deposit");
            Console.WriteLine("  3 - Balance");
            Console.WriteLine("  4 - Print Statement");
            Console.WriteLine("  0 - Exit\n\n");

            string UserChoiceMainMenu = Console.ReadLine();

            // validation check to catch if userinput is not 1 2 3 4 5
            int ParsedUserInput = 0;
            if (Int32.TryParse(UserChoiceMainMenu, out ParsedUserInput))
            {
                switch (ParsedUserInput)
                {
                    case 1:
                        Console.WriteLine("  You choose 1 - Withdraw");
                        Withdraw();
                        break;
                    case 2:
                        Console.WriteLine("  You choose 2 - Deposit");
                        Deposit();
                        break;
                    case 3:
                        Console.WriteLine("  You choose 3 - Display Balance");
                        GetBalance();
                        MainMenu();
                        break;
                    case 4:
                        Console.WriteLine("  You choose 4 - Print Statement");
                        Print();
                        break;
                    case 0:
                        Console.WriteLine("  You choose 0, Exiting now.. \n\n");
                        RestartProgram();
                        break;
                    default:
                        Console.WriteLine("  Switch statement Validation failed");
                        Console.WriteLine("  Sorry, the input:\" " + UserChoiceMainMenu + " \" is not an available option");
                        System.Threading.Thread.Sleep(3500);
                        Console.Clear();
                        MainMenu();
                        break;
                }

            }
            else
            {
                Console.WriteLine("  Parse Validation failed");
                Console.WriteLine("  The input:\" " + UserChoiceMainMenu + " \" is not an available option");

                System.Threading.Thread.Sleep(3500);
                Console.Clear();
                MainMenu();
            }

        }


        private static void Withdraw()
        {

            Console.Clear();
            Console.WriteLine("Please make a selection out of 1 of the following options:");
            Console.WriteLine("  Please Confirm your choice by pressing the ENTER key once\n\n");
            Console.WriteLine("  1 - 10");
            Console.WriteLine("  2 - 20");
            Console.WriteLine("  3 - 50");
            Console.WriteLine("  4 - 100");
            Console.WriteLine("  5 - 250");
            Console.WriteLine("  6 - Enter your amount manually");
            Console.WriteLine("  0 - RestartProgram\n\n");
            string UserChoiceWithdrawAmount = Console.ReadLine();

            int ParsedUserInput = 0;
            if (Int32.TryParse(UserChoiceWithdrawAmount, out ParsedUserInput))
            {
                switch (ParsedUserInput)
                {
                    case 1:
                        float Amount = 10;
                        Console.WriteLine("  You choose 1.  EUR: " + Amount + " ");
                        Transfer(Amount, false);
                        break;
                    case 2:
                        Amount = 20;
                        Console.WriteLine("  You choose 2.  EUR: " + Amount + " ");
                        Transfer(Amount, false);
                        break;
                    case 3:
                        Amount = 50;
                        Console.WriteLine("  You choose 3.  EUR: " + Amount + " ");
                        Transfer(Amount, false);
                        break;
                    case 4:
                        Amount = 100;
                        Console.WriteLine("  You choose 4.  EUR: " + Amount + " ");
                        Transfer(Amount, false);
                        break;
                    case 5:
                        Amount = 250;
                        Console.WriteLine("  You choose 5.  EUR: " + Amount + " ");
                        Transfer(Amount, false);
                        break;
                    case 6:
                        Console.WriteLine("  You choose 6:  Enter your amount manually");
                        Amount = EnterManualWithdrawAmount(); 
                        if(CheckIfManualWithdrawValid(Amount))
                        {
                            Transfer(Amount, false);
                        }
                        else
                        {
                            Withdraw();
                        }
                        break;
                    case 0:
                        Console.WriteLine("  You choose 0, Exiting now..");
                        RestartProgram();
                        break;
                    default:
                        Console.WriteLine("  Switch statement Validation failed");
                        Console.WriteLine("  The input:\" " + UserChoiceWithdrawAmount + " \" is not an available option");
                        System.Threading.Thread.Sleep(3500);
                        Console.Clear();
                        Withdraw();
                        break;
                }

            }
            else
            {
                Console.WriteLine("  Parse statement Validation failed");
                Console.WriteLine("  The input:\" " + UserChoiceWithdrawAmount + " \" is not an available option");
                System.Threading.Thread.Sleep(3500);
                Console.Clear();
                Withdraw();
            }

        }


        private static void Deposit()
        {

            Console.WriteLine("Please enter the amount you would like to Deposit:");
            Console.WriteLine("Your limit is set to 1000");

            string UserChoiceDepositAmount = Orb.App.Console.ReadPassword();
            float ParsedUserInput;
            if (float.TryParse(UserChoiceDepositAmount, out ParsedUserInput))
            {

                float remainder = ParsedUserInput % 5; //needs testing if works
                Console.WriteLine("Remainder Deposit: {0}", remainder);

                if ((ParsedUserInput <= 1000) && (remainder == 0))
                {
                    Console.WriteLine("  Your deposit was Successful!!"); 
                    Transfer(ParsedUserInput, true);
                }
                else
                {
                    Console.WriteLine("  That is not a valid amount to Deposit ", UserChoiceDepositAmount); 
                    System.Threading.Thread.Sleep(500);
                    Deposit();
                }

            }
        }


        private static void Print()
        {

            // due to lack of printer hardware no connection can be established, therefor PretendToConnect
            Console.Clear();
            PretendToConnect();
            MainMenu();

        }


        private static void PretendToConnect()
        {

            for(int i = 0; i <= 5; i++ )
            {
                Console.Clear();
                Console.WriteLine("  Connecting to Printer...");
                System.Threading.Thread.Sleep(500);
                Console.Clear();
                System.Threading.Thread.Sleep(500);
            }

            DateTime FourHoursFromNow = GetFourHoursFromNow();
            Console.WriteLine("  Network Error: No Printer Found. \n\n\n\n" +
                              "  Our service team has automatically been informed.\n\n" +
                              "  An engineer will be dispatched before {0}\n\n\n\n", FourHoursFromNow.ToString("g")); 

            Console.WriteLine("  You are now being returned to the Main Menu...");
            System.Threading.Thread.Sleep(10000);
            Console.Clear();

        }


        private static bool Transfer(float Amount, bool isAmountPositive) 
        {

            DBClass DBconnect = new DBClass();

            string StringAmount = Amount.ToString();
            string userId = DBconnect.SelectSingle("id");
            int intUserId;
            int.TryParse(userId, out intUserId);
            string userName = DBconnect.SelectSingle("UserName");
            string Previousbalance = DBconnect.SelectSingle("balance");
            float floatPreviousBalance = float.Parse(Previousbalance);
            float NewBalance;
            if (isAmountPositive)
            {
                NewBalance = floatPreviousBalance + Amount;
            }
            else
            {
                NewBalance = floatPreviousBalance - Amount;
            }

            try
            {
                DBconnect.Update(intUserId, NewBalance);
                DBconnect.Insert(userId, StringAmount, isAmountPositive);
                Console.WriteLine("  Action complete.\n");
                if (CheckIfUserWantsReceipt())
                {
                    PretendToConnect();
                    MainMenu();
                }
                else
                {
                    Console.Clear();
                    if (CheckIfUserWantsMainMenu())
                    {
                        Console.Clear();
                        MainMenu();
                    }
                    else
                    {
                        RestartProgram();
                    }
                }

                return true;

            }
            catch
            {
                Console.WriteLine("CAUGHT: INSERT FAILED!!!!");
            } 

            return false; 

        }


        private static float EnterManualWithdrawAmount()
        {

            string UserChoiceWithdrawAmount = Orb.App.Console.ReadPassword();
            float ParsedUserInput;
            if (float.TryParse(UserChoiceWithdrawAmount, out ParsedUserInput))
            {
                return ParsedUserInput;
            }
            else
            {
                Console.WriteLine("  Could not parse that withdrawal amount! ", UserChoiceWithdrawAmount); 
                System.Threading.Thread.Sleep(500);
                MainMenu();
                return 0;
            }

        }


        private static bool CheckIfUserWantsReceipt() 
        {

            Console.WriteLine("  Would you like a receipt? Please choose out of the following options: ");
            Console.WriteLine("  Please confirm your choice by pressing the ENTER key once\n\n");
            Console.WriteLine("  1 - Yes\n");
            Console.WriteLine("  2 - No \n\n");
            string userInput = Console.ReadLine();
            int intUserReceiptPreference = Int32.Parse(userInput);
            if (intUserReceiptPreference == 1 || intUserReceiptPreference == 2)
            {

                bool userReceiptPreference = false;

                switch (intUserReceiptPreference)
                {
                    case 1:
                        userReceiptPreference = true;
                        return userReceiptPreference;
                    case 2:
                        userReceiptPreference = false;
                        return userReceiptPreference;

                    default:
                        Console.WriteLine("Hit a switch exception");
                        System.Threading.Thread.Sleep(500);
                        MainMenu();
                        return false;
                }

            }
            else
            {
                Console.WriteLine("Hit a parse exception");
                Console.WriteLine("  The input:\" " + userInput + " \" is not an available option");
                System.Threading.Thread.Sleep(3500);
                Console.Clear();
                CheckIfUserWantsReceipt();

                return false;
            }
      
        }


        private static bool CheckIfUserWantsMainMenu() 
        {

            Console.WriteLine("  Would you like to return to Main Menu? Please choose out of the following options: \n\n");
            Console.WriteLine("  Please confirm your choice by pressing the ENTER key once\n\n");
            Console.WriteLine("  1 - Yes\n");
            Console.WriteLine("  2 - No \n\n");
            string BackToMainMenuPref = Console.ReadLine();
            bool UserBackToMainMenuPref;
            int intUserInputBackToMainMenuPref = Int32.Parse(BackToMainMenuPref);
            if (intUserInputBackToMainMenuPref == 1 || intUserInputBackToMainMenuPref == 2)
            {
                switch (intUserInputBackToMainMenuPref)
                {
                    case 1:
                        UserBackToMainMenuPref = true;
                        return UserBackToMainMenuPref;
                    case 2:
                        UserBackToMainMenuPref = false;
                        RestartProgram();
                        return UserBackToMainMenuPref;
                    default:
                        Console.WriteLine("I do not understand the input. I am returning you to Main Menu");
                        MainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("  The input:\" " + BackToMainMenuPref + " \" is not an available option");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                MainMenu();
            }
            
            return false;

        }


        private static DateTime GetFourHoursFromNow()
        {

            System.DateTime today = System.DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(0, 4, 0, 0);
            System.DateTime answer = today.Add(duration);
            return answer;

        }


        private static bool CheckIfManualWithdrawValid(float amount)
        {
            float remainder = amount % 5;
            if ((amount <= 1000 && amount >=5) && (remainder == 0))
            {
                return true;
            }
            else
            {
                return false;
            }

            throw new NotImplementedException();
        }


        private static void RestartProgram()
        {

            Console.WriteLine("Restarting Machine\n");
            System.Threading.Thread.Sleep(1000);
            var fileName = Assembly.GetExecutingAssembly().Location;
            System.Diagnostics.Process.Start(fileName);
            Application.Exit();

        }


    }


    namespace Orb.App // Created this removed from the original namespace and masked the userinput, output **** on screen 
    {

        static public class Console
        {

            public static string ReadPassword(char mask)
            {
                const int ENTER = 13, BACKSP = 8, CTRLBACKSP = 127;
                int[] FILTERED = { 0, 27, 9, 10 /*, 32 space, if you care */ }; // const

                var pass = new Stack<char>();
                char chr = (char)0;

                while ((chr = System.Console.ReadKey(true).KeyChar) != ENTER)
                {
                    if (chr == BACKSP)
                    {
                        if (pass.Count > 0)
                        {
                            System.Console.Write("\b \b");
                            pass.Pop();
                        }
                    }
                    else if (chr == CTRLBACKSP)
                    {
                        while (pass.Count > 0)
                        {
                            System.Console.Write("\b \b");
                            pass.Pop();
                        }
                    }
                    else if (FILTERED.Count(x => chr == x) > 0) { }
                    else
                    {
                        pass.Push((char)chr);
                        System.Console.Write(mask);
                    }
                }

                System.Console.WriteLine();

                return new string(pass.Reverse().ToArray());
            }


            public static string ReadPassword()
            {
                return Orb.App.Console.ReadPassword('*');
            }

        }

    }

}





