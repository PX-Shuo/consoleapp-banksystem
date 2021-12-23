using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assessment_1
{
    class Deposit
    {
        int accountTest;
        int amountNum, balanceNum;

        bool isFinish = false;
        string response;

        public void DepositScreen()
        {
            while (!isFinish)
            {
                Console.Clear();

                // print and read user input
                Console.Write("\n\n\t\t||===================================||\n" +
                    "\t\t||              Deposit              ||\n" +
                    "\t\t||===================================||\n" +
                    "\t\t||         ENTER THE DETAILS         ||\n" +
                    "\t\t||                                   ||" +
                    "\n\t\t||Account Number: ");
                int CursorPosANLeft = Console.CursorLeft;
                int CursorPosANTop = Console.CursorTop;
                Console.Write("                   ||");
                Console.Write("\n\t\t||Amount: $");
                int CursorPosAMLeft = Console.CursorLeft;
                int CursorPosAMTop = Console.CursorTop;
                Console.Write("                          ||");
                Console.Write("\n\t\t||-----------------------------------||\n");

                Console.SetCursorPosition(CursorPosANLeft, CursorPosANTop);
                string account = Console.ReadLine();

                // check whether input is integer
                if (Int32.TryParse(account, out accountTest))
                {
                    // check whether input is within 10 digits
                    if (accountTest.ToString().Length <= 10)
                    {
                        string fileName = account + ".txt";

                        // check whether account exist by checking file names
                        if (File.Exists(fileName))
                        {
                            // read user input for deposit amount
                            Console.Write("\n\n\n\t\tAccount found! Please enter the amount...\n");
                            Console.SetCursorPosition(CursorPosAMLeft, CursorPosAMTop);
                            string amount = Console.ReadLine();

                            // check whether input is integer
                            if(Int32.TryParse(amount, out amountNum))
                            {
                                // read all data from the file
                                string[] accountDetail = System.IO.File.ReadAllLines(fileName);
                                // known first value of accountDetail is the balance, fetch and save it to a int variable
                                try
                                {
                                    balanceNum = Int32.Parse(accountDetail[0]);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.ReadKey();
                                }
                                
                                // add the amount to the balance
                                balanceNum += amountNum;
                                // store value of the balance variable back to the array
                                accountDetail[0] = balanceNum.ToString();
                                // clean the file
                                File.WriteAllText(fileName, "");
                                // print all values of accountDetail to the file
                                foreach (var item in accountDetail)
                                {
                                    File.AppendAllText(fileName, item + "\n");
                                }
                                // inform user
                                Console.WriteLine("\n\n\n\t\tDeposit successfull!");
                                Console.ReadKey();
                                isFinish = true;
                                break;
                            }
                            else
                            {
                                // inform user when input is not integer
                                Console.WriteLine("\n\n\n\t\tAmount must be a number.");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            // inform user when account cannot be found, and ask for further instruction
                            Console.Write("\n\n\n\t\tAccount not found!\n" +
                                "\t\tRetry? (y/n)\n\t\t");
                            response = Console.ReadLine();
                            while (response != "y" && response != "n")
                            {
                                Console.Write("\t\tPlease respond using letter 'y' for yes or 'n' for no\n\t\t");
                                response = Console.ReadLine();
                            }
                            if (response == "y")
                            {
                                continue;
                            }
                            else if (response == "n")
                            {
                                isFinish = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        // inform user when input exceed 10 digits
                        Console.WriteLine("\n\t\tAccount number must be less or equals to 10 digits.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    // inform user when input is not integer
                    Console.WriteLine("\n\t\tAccount number must be integer.");
                    Console.ReadKey();
                }
            }
            // redirect user to menu screen
            Menu menu = new Menu();
            menu.MenuScreen();
        }
    }
}
