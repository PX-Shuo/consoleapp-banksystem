using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assessment_1
{
    class DeleteAccount
    {
        public void DeleteScreen()
        {
            int accountTest;

            bool isFinish = false;
            string response;

            // create 2D-array for storing cursor positions
            int[,] cursorPos = new int[6, 2];
            while (!isFinish)
            {
                Console.Clear();
                Console.Write("\n\n\t\t||===================================||\n" +
                    "\t\t||         DELETE AN ACCOUNT         ||\n" +
                    "\t\t||===================================||\n" +
                    "\t\t||         ENTER THE DETAILS         ||\n" +
                    "\t\t||                                   ||" +
                    "\n\t\t||Account Number: ");
                int CursorPosANLeft = Console.CursorLeft;
                int CursorPosANTop = Console.CursorTop;
                Console.Write("                   ||");
                Console.Write("\n\t\t||===================================||\n");

                Console.SetCursorPosition(CursorPosANLeft, CursorPosANTop);
                string account = Console.ReadLine();

                // check whether input is integer
                if (Int32.TryParse(account, out accountTest))
                {
                    // check whether input is within 10 digits
                    if (accountTest.ToString().Length <= 10)
                    {
                        string fileName = account + ".txt";

                        // check whether account exist by checking the file names
                        if (File.Exists(fileName))
                        {
                            // print the account details in appropriate format
                            Console.Write("\n\n\t\tAccount found! Details displayed below...\n\n" +
                                "\t\t||===================================||\n" +
                                "\t\t||         ACCOUNT DETAILS           ||\n" +
                                "\t\t||===================================||\n" +
                                "\t\t||                                   ||" +
                                "\n\t\t||Account No: {0}               ||\n" +
                                "\t\t||Account Balance: $", account);
                            cursorPos[0, 0] = Console.CursorLeft;
                            cursorPos[0, 1] = Console.CursorTop;
                            Console.Write("                 ||");
                            Console.Write("\n\t\t||First Name: ");
                            cursorPos[1, 0] = Console.CursorLeft;
                            cursorPos[1, 1] = Console.CursorTop;
                            Console.Write("                       ||");
                            Console.Write("\n\t\t||Last Name: ");
                            cursorPos[2, 0] = Console.CursorLeft;
                            cursorPos[2, 1] = Console.CursorTop;
                            Console.Write("                        ||");
                            Console.Write("\n\t\t||Address: ");
                            cursorPos[3, 0] = Console.CursorLeft;
                            cursorPos[3, 1] = Console.CursorTop;
                            Console.Write("                          ||");
                            Console.Write("\n\t\t||Phone: ");
                            cursorPos[4, 0] = Console.CursorLeft;
                            cursorPos[4, 1] = Console.CursorTop;
                            Console.Write("                            ||");
                            Console.Write("\n\t\t||Email: ");
                            cursorPos[5, 0] = Console.CursorLeft;
                            cursorPos[5, 1] = Console.CursorTop;
                            Console.Write("                            ||");

                            string[] loginCredential = System.IO.File.ReadAllLines(fileName);

                            for (int loop = 0; loop < 6; loop++)
                            {
                                Console.SetCursorPosition(cursorPos[loop, 0], cursorPos[loop, 1]);
                                Console.Write((string)loginCredential[loop]);
                            }

                            // confirm whether user wants to delete this account
                            Console.Write("\n\t\t||-----------------------------------||\n\n" +
                                "\t\tDelete? (y/n)\n\t\t");
                            response = Console.ReadLine();
                            while (response != "y" && response != "n")
                            {
                                Console.Write("\t\tPlease respond using letter 'y' for yes or 'n' for no\n\t\t");
                                response = Console.ReadLine();
                            }
                            if (response == "y")
                            {
                                // delete the file when user typed 'y'
                                File.Delete(fileName);
                                Console.Write("\n\t\tAccount Deleted!...");
                                Console.ReadKey();
                                isFinish = true;
                                break;
                            }
                            else if (response == "n")
                            {
                                isFinish = true;
                                break;
                            }
                        }
                        else
                        {
                            // inform user when account cannot be found and ask for further instructions
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
                        // inform user when input exceeds 10 digits
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
