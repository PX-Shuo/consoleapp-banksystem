using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_1
{
    class Menu
    {
        string choice;
        int choiceno;
        bool isNum = false;

        public void MenuScreen()
        {
            while (!isNum) {
                Console.Clear();

                // print interface
                Console.Write("\n\n\t\t||===================================||\n" +
                    "\t\t|| WELCOME TO SIMPLE BANKING SYSTEM  ||\n" +
                    "\t\t||===================================||\n" +
                    "\t\t||1. Create a new account            ||\n" +
                    "\t\t||2. Search for an account           ||\n" +
                    "\t\t||3. Deposit                         ||\n" +
                    "\t\t||4. Withdraw                        ||\n" +
                    "\t\t||5. A/C statement                   ||\n" +
                    "\t\t||6. Delete account                  ||\n" +
                    "\t\t||7. Exit                            ||\n" +
                    "\t\t||8. Logout                          ||\n" +
                    "\t\t||-----------------------------------||\n" +
                    "\t\t||Enter your choice (1-7): ");

                // record cursor position
                int CursorPosChoiceLeft = Console.CursorLeft;
                int CursorPosChoiceTop = Console.CursorTop;
                Console.Write("          ||");

                Console.WriteLine("\n\t\t||-----------------------------------||\n");

                // read user input
                Console.SetCursorPosition(CursorPosChoiceLeft, CursorPosChoiceTop);
                choice = Console.ReadLine();

                // check whether user input is a integer
                if (Int32.TryParse(choice, out choiceno))
                {
                    // use switch to activate the function according to user input
                    switch (choiceno)
                    {
                        case 1:
                            CreateAccount create = new CreateAccount();
                            create.CAScreen();
                            break;
                        case 2:
                            Search search = new Search();
                            search.SearchScreen();
                            break;
                        case 3:
                            Deposit deposit = new Deposit();
                            deposit.DepositScreen();
                            break;
                        case 4:
                            Withdraw withdraw = new Withdraw();
                            withdraw.WithdrawScreen();
                            break;
                        case 5:
                            AccountStatement statement = new AccountStatement();
                            statement.StatementScreen();
                            break;
                        case 6:
                            DeleteAccount delete = new DeleteAccount();
                            delete.DeleteScreen();
                            break;
                        case 7:
                            // exit this console application when user typed 7
                            Environment.Exit(0);
                            break;
                        case 8:
                            Login login = new Login();
                            login.LoginScreen();
                            break;
                        default:
                            // inform user if the input number is out of range (not within 1-7)
                            Console.Write("\n\t\tPlease enter a number between 1 to 7");
                            Console.ReadKey();
                            continue;
                    }
                }
                else
                {
                    // inform user when they enters non-integer value
                    Console.WriteLine("\n \t\tPlease enter a number between 1 to 7.");
                    Console.ReadKey();
                }
            }
        }
    }
}
