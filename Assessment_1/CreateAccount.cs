using System;
using System.Linq;
using System.Collections;
using System.IO;
using System.Net.Mail;

namespace Assessment_1
{
    class CreateAccount
    {
        // use arraylist and 2D-array to store user input and cursor positions
        ArrayList accountInfo = new ArrayList();
        int[,] cursorPos = new int[5,2];

        bool isPhoneInt = false;
        bool isPhoneLengthValid = false;
        bool isEmailValid = false;
        int phoneTest;
        string email;

        bool isUnique = false;
        int accountNum;

        public void CAScreen()
        {
            while (!isPhoneInt | !isPhoneLengthValid | !isEmailValid)
            {
                // clean the arraylist at the beginning of each loop
                accountInfo.Clear();
                // add 0 to the empty arraylist, 0 represents the current balance
                accountInfo.Add("0");

                Console.Clear();

                // print the interface and record cursor positions
                Console.Write("\n\n\t\t||===================================||\n" +
                    "\t\t||       CREATE A NEW ACCOUNT        ||\n" +
                    "\t\t||===================================||\n" +
                    "\t\t||     PLEASE ENTER THE DETAILS      ||\n" +
                    "\t\t||                                   ||" +
                    "\n\t\t||First Name: ");
                cursorPos[0, 0] = Console.CursorLeft;
                cursorPos[0, 1] = Console.CursorTop;
                Console.Write("                       ||");
                Console.Write("\n \t\t||Last Name: ");
                cursorPos[1, 0] = Console.CursorLeft;
                cursorPos[1, 1] = Console.CursorTop;
                Console.Write("                        ||");
                Console.Write("\n \t\t||Address: ");
                cursorPos[2, 0] = Console.CursorLeft;
                cursorPos[2, 1] = Console.CursorTop;
                Console.Write("                          ||");
                Console.Write("\n \t\t||Phone: ");
                cursorPos[3, 0] = Console.CursorLeft;
                cursorPos[3, 1] = Console.CursorTop;
                Console.Write("                            ||");
                Console.Write("\n \t\t||Email: ");
                cursorPos[4, 0] = Console.CursorLeft;
                cursorPos[4, 1] = Console.CursorTop;
                Console.Write("                            ||");
                Console.WriteLine("\n \t\t||-----------------------------------||\n");

                // store user input into the arraylist
                for (int loop = 0; loop < 5; loop++)
                {
                    Console.SetCursorPosition(cursorPos[loop, 0], cursorPos[loop, 1]);
                    accountInfo.Add(Console.ReadLine());
                }

                // known the 6th value in the arraylist is the email, store it into a variable for later use
                email = (string)accountInfo[5];

                // check whether the phone number is integer
                if (Int32.TryParse((string)accountInfo[4], out phoneTest))
                {
                    // set boolean value to true
                    isPhoneInt = true;

                    // check whether phone number is within 10 digits
                    if (phoneTest.ToString().Length <= 10)
                    {
                        isPhoneLengthValid = true;

                        // check whether email value contains '@'
                        if (email.ToLower().Contains('@'))
                        {
                            isEmailValid = true;

                            // confirm the input
                            Console.Write("\n\n \t\tIs the information correct? (y/n)\n\t\t");
                            string confirm = Console.ReadLine();

                            // handle different user input and guide the user when input is not valid
                            while(confirm!="y" && confirm != "n")
                            {
                                Console.Write("\t\tPlease respond using letter 'y' for yes or 'n' for no\n\t\t");
                                confirm = Console.ReadLine();
                            }
                            if (confirm == "y")
                            {
                                // break out of the loop when user typed 'y'
                                break;
                            }
                            else if(confirm == "n")
                            {
                                // continue to the next loop when user typed 'n'
                                isPhoneInt = false;
                                isPhoneLengthValid = false;
                                isEmailValid = false;
                                continue;
                            }
                        }
                    }
                }

                // display message for each condition respectively
                if (!isPhoneInt)
                {
                    Console.WriteLine("\n\t\tPlease make sure phone number only contains integer.\n");
                    Console.ReadKey();
                }
                if (!isPhoneLengthValid)
                {
                    Console.WriteLine("\n\t\tPlease make sure phone number is less or equal to 10 digits.\n");
                    Console.ReadKey();
                }
                if (!isEmailValid)
                {
                    Console.WriteLine("\n\t\tPlease enter a valid email address.\n");
                    Console.ReadKey();
                }
            }

            // generating unique account number, use it as the file name, and save the user input to this file
            Random rand = new Random();
            while (!isUnique)
            {
                accountNum = rand.Next(100000,99999999);
                string fileName = accountNum + ".txt";
                if (!File.Exists(fileName))
                {
                    isUnique = true;
                    using (StreamWriter swNewAcc = File.CreateText(fileName))
                    {
                        foreach (var item in accountInfo)
                        {
                            swNewAcc.WriteLine((string)item);
                        }
                    }
                }
            }

            // email the account detail to the registerd email address
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("consolebank@gmail.com");
                mail.To.Add((string)accountInfo[5]);
                mail.Subject = "Your new bank account";
                mail.Body = "Dear " + (string)accountInfo[1] + "," +
                    "\n\n\tYour account number is: " + accountNum + "." +
                    "\n\n\tPlease confirm your details:" +
                    "\n\t\tFirst name: " + (string)accountInfo[1] + 
                    "\n\t\tLast name: " + (string)accountInfo[2] + 
                    "\n\t\tAddress: " + (string)accountInfo[3] + 
                    "\n\t\tPhone: " + (string)accountInfo[4] + 
                    "\n\t\tEmail: " + (string)accountInfo[5] + 
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

            // inform the user the account creation is successful and redirect user to menu page.
            Console.WriteLine("\n\n\t\tAccount created! Details will be provided via email." +
                "\n\t\tYour account number: {0}", accountNum);
            Console.ReadKey();
            Menu menu = new Menu();
            menu.MenuScreen();
        }
    }
}
