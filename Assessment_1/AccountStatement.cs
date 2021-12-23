using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;

namespace Assessment_1
{
    class AccountStatement
    {
        public void StatementScreen()
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
                    "\t\t||             STATEMENT             ||\n" +
                    "\t\t||===================================||\n" +
                    "\t\t||        ENTER THE DETAILS          ||\n" +
                    "\t\t||                                   ||" +
                    "\n\t\t||Account Number: ");
                int CursorPosANLeft = Console.CursorLeft;
                int CursorPosANTop = Console.CursorTop;
                Console.Write("                   ||");
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

                        try
                        {
                            // check whether account exist by checking the file names
                            if (File.Exists(fileName))
                            {
                                // print statement to the screen in appropriate format
                                Console.Write("\n\n\t\tAccount found! The Statement is displayed below...\n\n" +
                                    "\t\t||===================================||\n" +
                                    "\t\t||       SIMPLE BANKING SYSTEM       ||\n" +
                                    "\t\t||===================================||\n" +
                                    "\t\t||        Account Statement          ||\n" +
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

                                // ask whether the user wants to receive the statement via email
                                Console.Write("\n\t\t||-----------------------------------||\n" +
                                    "\n\n\t\tEmail Statement? (y/n)\n\t\t");
                                response = Console.ReadLine();
                                while (response != "y" && response != "n")
                                {
                                    Console.Write("\t\tPlease respond using letter 'y' for yes or 'n' for no\n\t\t");
                                    response = Console.ReadLine();
                                }
                                if (response == "y")
                                {
                                    // send email containing the statement when user typed 'y'
                                    try
                                    {
                                        MailMessage mail = new MailMessage();
                                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                                        mail.From = new MailAddress("consolebank@gmail.com");
                                        mail.To.Add((string)loginCredential[5]);
                                        mail.Subject = "Your Bank Statement";
                                        mail.Body = "Dear " + (string)loginCredential[1] + "," +
                                            "\n\n\tHere is your statement.\n" +
                                            "\n\t||===========================================================||" +
                                            "\n\t||                                        SIMPLE BANKING SYSTEM                                      ||" +
                                            "\n\t||===========================================================||" +
                                            "\n\t                        Account Statement                      " +
                                            "\n\n\t\tAccount No: " + account +
                                            "\n\t\tAccount Balance: $" + (string)loginCredential[0] +
                                            "\n\t\tFirst name: " + (string)loginCredential[1] +
                                            "\n\t\tLast name: " + (string)loginCredential[2] +
                                            "\n\t\tAddress: " + (string)loginCredential[3] +
                                            "\n\t\tPhone: " + (string)loginCredential[4] +
                                            "\n\t\tEmail: " + (string)loginCredential[5] +
                                            "\n\t||-------------------------------------------------------------------------------------------------------||" +
                                            "\n\nregards," +
                                            "\n\nConsoleBank";

                                        SmtpServer.Port = 587;
                                        SmtpServer.Credentials = new System.Net.NetworkCredential("consolebank@gmail.com", "abcd1234zxcv.");
                                        SmtpServer.EnableSsl = true;

                                        SmtpServer.Send(mail);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }

                                    // inform user that email is sent
                                    Console.Write("\t\tEmail sent successfully!...");
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
                                // inform user when account not found and ask for further instructions
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
                        catch (FileNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
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
