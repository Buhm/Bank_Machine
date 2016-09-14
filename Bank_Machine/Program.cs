using System;
using System.Collections.Generic;
using System.Linq;
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

                Console.WriteLine("Failed Attempt: {0}", FailedLoginAttempts);

                //register login attempts and log in user
                bool UserLoginsuccess = UserLogin(FailedLoginAttempts);

                if (UserLoginsuccess== true) 
                {

                    GetBalance();
                    MainMenu();
                    return;

                }
                else
                {



                }

            }

        }


        //due to lack of plastic card system, AccountNumber has been hardcoded to (user)id 1 "Hugo DaBoss"
        public static bool UserLogin(int FailedLoginAttempts)
        {

            var DBconnect = new DBClass();

            Console.WriteLine("  Please provide your Personal Identification Number (PIN):");
            string UserPinCode = Console.ReadLine();

            //looking up actual user PIN in database
            string QueriedUserPin = DBconnect.SelectSingle("PIN");

            //compare if provided PIN matches registered PIN 
            bool IsPINsuccess = ComparePinCodes(UserPinCode, QueriedUserPin, FailedLoginAttempts);

            //layout purposes to end screen making it look like new screen (also removes pin code from the screen)
            System.Threading.Thread.Sleep(3500);
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
            //System.Threading.Thread.Sleep(5000);
            //Console.Clear();

            return UserCurrentBalance;

        }


        private static void CalcAttemptsRemaining(int FailedLoginAttempts)
        {

            //hardcoded to 2 so I know for sure I always have the right amount of attempts left in output. Tested and works without flaw
            int RemainingAttempts = 2 - FailedLoginAttempts;

            if (RemainingAttempts == 1)
            {

                Console.Write("  Incorrect PIN has been provided. Please remember you only have " + RemainingAttempts + " attempt left before your account gets locked! \n");

            }
            else if (RemainingAttempts == 0)
            {

                Console.WriteLine("  Your account has been locked due to security reasons. If you wish to unlock your account, \n" +
                                  "  please contact our staff at: Tel: 00(XX)+ XXXX XXXX \n");
                System.Threading.Thread.Sleep(5000);

            }
            else
            {

                Console.Write("  Incorrect PIN has been provided. \n " + 
                              "  Please remember you only have " + RemainingAttempts + " attempts left before your account gets locked! \n");

            }

        }


        private static void MainMenu()
        {
            //TODO build in a response timer, logout after X seconds of no user input

            //offer choise to user to Withdraw, Deposit or Exit
            Console.WriteLine("  Please make a choice out of the following options:\n\n");
            Console.WriteLine("  1 - Withdraw");
            Console.WriteLine("  2 - Deposit");
            Console.WriteLine("  3 - Balance");
            Console.WriteLine("  4 - Print");
            Console.WriteLine("  0 - Exit\n\n");


            string UserChoice_MainMenu = Console.ReadLine();

            //TODO create function to export checks if userinput = 1 2 3 4 5.

            // validation check to catch if userinput is not 1, 2, 3 4 5


            

            int ParsedUserInput = 0;
            if (Int32.TryParse(UserChoice_MainMenu, out ParsedUserInput))
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
                        Console.WriteLine("  You choose 4 - Print");
                        Print();
                        break;
                    case 0:
                        Console.WriteLine("  You choose 0, Exiting now (still to add in the switch statement) \n\n");
                        break;

                    default:
                        //Validation failed
                        Console.WriteLine("  Switch statement Validation failed");
                        Console.WriteLine("  The input:\" " + UserChoice_MainMenu + " \" is not an available option");

                        System.Threading.Thread.Sleep(3500);
                        Console.Clear();
                        MainMenu();
                        break;
                }

            }
            else
            {

                //Validation failed
                Console.WriteLine("  Parse Validation failed");
                Console.WriteLine("  The input:\" " + UserChoice_MainMenu + " \" is not an available option");

                System.Threading.Thread.Sleep(3500);
                Console.Clear();
                MainMenu();

            }

        }

        private static void Withdraw()
        {

            Console.Clear();
            Console.WriteLine("Please make a selection out of 1 of the following options:\n\n");
            Console.WriteLine("  1 - 10");
            Console.WriteLine("  2 - 20");
            Console.WriteLine("  3 - 50");
            Console.WriteLine("  4 - 100");
            Console.WriteLine("  5 - 250");
            Console.WriteLine("  6 - Enter your amount manually");
            Console.WriteLine("  0 - Exit\n\n");

            string UserChoice_Withdraw_Amount = Console.ReadLine();

            int ParsedUserInput = 0;
            if (Int32.TryParse(UserChoice_Withdraw_Amount, out ParsedUserInput))
            {

                switch (ParsedUserInput)
                {
                    case 1:
                        float Amount = 10;
                        Console.WriteLine("  You choose 1.  EUR: " + Amount + " ");
                        TransferMin(Amount, false);
                        break;
                    case 2:
                        Amount = 20;
                        Console.WriteLine("  You choose 2.  EUR: " + Amount + " ");
                        TransferMin(Amount, false);
                        break;
                    case 3:
                        Amount = 50;
                        Console.WriteLine("  You choose 3.  EUR: " + Amount + " ");
                        TransferMin(Amount, false);
                        break;
                    case 4:
                        Amount = 100;
                        Console.WriteLine("  You choose 4.  EUR: " + Amount + " ");
                        TransferMin(Amount, false);
                        break;
                    case 5:
                        Amount = 250;
                        Console.WriteLine("  You choose 5.  EUR: " + Amount + " ");
                        TransferMin(Amount, false);
                        break;
                    case 6:
                        Console.WriteLine("  You choose 6:  Enter your amount manually");
                        EnterManualWithdrawAmount(); //TODO fill function so it works
                        break;
                    case 0:
                        Console.WriteLine("  You choose 0, Exiting now (still to add in the switch statement) \n\n");
                        MainMenu();
                        break;

                    default:
                        //Validation failed
                        Console.WriteLine("  Switch statement Validation failed");
                        Console.WriteLine("  The input:\" " + UserChoice_Withdraw_Amount + " \" is not an available option");
                        System.Threading.Thread.Sleep(3500);
                        Console.Clear();
                        Withdraw();
                        break;
                }

            }
            else
            {

                //Validation failed
                Console.WriteLine("  Parse statement Validation failed");
                Console.WriteLine("  The input:\" " + UserChoice_Withdraw_Amount + " \" is not an available option");

                System.Threading.Thread.Sleep(3500);
                Console.Clear();
                Withdraw();

            }

        }


        private static void Deposit()
        {
            
            Console.Clear();

            MainMenu();

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

            Console.WriteLine("  Network Error: No Printer Found. \n\n" +
                  "  Our service team has automatically been informed and an engineer " +
                  "  will be dispatched within 30 minutes\n"); //TODO insert time and date 30 minutes away from CURRENTTIME
            Console.WriteLine("  You are now being returned to the Main Menu...");
            System.Threading.Thread.Sleep(4500);
            Console.Clear();

        }


        private static bool TransferPlus(int Amount) //TODO
        {

            return false;

        }


        private static bool TransferMin(float Amount, bool isAmountPositive) //TODO
            {

                DBClass DBconnect = new DBClass();

                string StringAmount = Amount.ToString();
                string userId = DBconnect.SelectSingle("id");
                int intUserId;
                int.TryParse(userId, out intUserId);
                string userName = DBconnect.SelectSingle("UserName");
                string Previousbalance = DBconnect.SelectSingle("balance");
                float floatPreviousBalance = float.Parse(Previousbalance);
            
                try
                {

                    float NewBalance = floatPreviousBalance - Amount;

                    DBconnect.Update(intUserId, NewBalance);
                    Console.WriteLine("  Action complete.\n");

                    //TODO does user want receipt?
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

                            //nextCustomer();

                        };

                    };

                    return true;

                }
                catch
                {

                    Console.WriteLine("INSERT FAILED!!!!");

                };  

                return false; //default 

            }


        private static int EnterManualWithdrawAmount() //TODO
        {

            return 0;

        }


        private static bool CheckIfUserWantsReceipt() //TODO
        {

            Console.WriteLine("  Would you like a receipt? Please choose out of the following options: \n\n");
            Console.WriteLine("  1 - Yes\n");
            Console.WriteLine("  2 - No \n\n");
            string userInputReceiptPreference = Console.ReadLine();
            int intUserReceiptPreference = Int32.Parse(userInputReceiptPreference);
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
                        MainMenu();
                        return false;
                }

            }
            else
            {

                //Validation failed
                Console.WriteLine("Hit a parse exception");
                Console.WriteLine("  The input:\" " + userInputReceiptPreference + " \" is not an available option");

                System.Threading.Thread.Sleep(3500);
                Console.Clear();
                CheckIfUserWantsReceipt();

                return false;
            }

            //if (intUserReceiptPreference == 1)
            //{
                    
            //    PretendToConnect();
            //    MainMenu();
                
            //}
            //else if(intUserReceiptPreference == 2)
            //{
            //    if(CheckIfUserWantsMainMenu())
            //    {

            //        Console.Clear();
            //        MainMenu();
            //}
            //else
            //{

            //    Console.WriteLine("Weird exception in Main menu choice");

            //    }
                    

            //}
            
        }


        private static bool CheckIfUserWantsMainMenu() //TODO
        {

            Console.WriteLine("  Would you like to return to Main Menu? Please choose out of the following options: \n\n");
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
                        return UserBackToMainMenuPref;

                    default:
                        Console.WriteLine("I do not understand the input. I am returning you to Main Menu");
                        MainMenu();
                        break;
                }

            }
            else
            {

                //Validation failed
                Console.WriteLine("  The input:\" " + BackToMainMenuPref + " \" is not an available option");

                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                MainMenu();

            }
            
            return false;

        }
 
    }

}





