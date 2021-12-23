using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assessment_1
{
    class Login
    {
        // create variables for later use
        string userName, passWord;
        Boolean isValid = false;
        string passWdChar = "*";

        string filePath = "login.txt";

        public void LoginScreen()
        {
            try
            {
                // check whether login.txt exsists, create one if it doesn't exist
                if (!File.Exists(filePath))
                {
                    using (StreamWriter swUserPass = File.CreateText(filePath))
                    {
                        swUserPass.WriteLine("admin,admin");
                    }
                }

                // read the content of login.txt
                string[] loginCredential = System.IO.File.ReadAllLines(filePath);

                // use while loop so it will continue until login is successful
                while (!isValid)
                {
                    // reset the console and the passWord string at the start of each loop
                    Console.Clear();
                    passWord = "";

                    // print the interface
                    Console.WriteLine("\n\n\t\t||===================================||");
                    Console.WriteLine("\t\t|| WELCOME TO SIMPLE BANKING SYSTEM  ||");
                    Console.WriteLine("\t\t||===================================||");
                    Console.Write("\t\t||UserName: ");

                    // record cursor position
                    int CursorPosUserNameLeft = Console.CursorLeft;
                    int CursorPosUserNameTop = Console.CursorTop;
                    Console.Write("\t\t\t     ||");

                    Console.Write("\n\t\t||Password: ");

                    // record cursor position
                    int CursorPosPwdLeft = Console.CursorLeft;
                    int CursorPosPwdTop = Console.CursorTop;
                    Console.Write("\t\t\t     ||");

                    Console.WriteLine("\n\t\t||-----------------------------------||");

                    // set cursor to the right position and read user input
                    Console.SetCursorPosition(CursorPosUserNameLeft, CursorPosUserNameTop);
                    userName = Console.ReadLine();

                    Console.SetCursorPosition(CursorPosPwdLeft, CursorPosPwdTop);

                    // use do-while to read password
                    do
                    {
                        ConsoleKeyInfo Key = Console.ReadKey(true);

                        // hide the password using '*'
                        if (Key.Key != ConsoleKey.Backspace && Key.Key != ConsoleKey.Enter)
                        {
                            passWord += Key.KeyChar;
                            Console.Write(passWdChar);
                        }
                        // backspace key set up
                        else if (Key.Key == ConsoleKey.Backspace && passWord.Length > 0)
                        {
                            passWord = passWord.Substring(0, (passWord.Length - 1));
                            Console.Write("\b \b");
                        }
                        // enter key set up
                        else if (Key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    } while (true);

                    // split each value in loginCredential and compare them with user input
                    foreach (string set in loginCredential)
                    {
                        string[] splits = set.Split('|');

                        // validation
                        if (userName == splits[0] && passWord == splits[1])
                        {
                            isValid = true;
                            break;
                        }
                    }
                    if (!isValid)
                    {
                        // inform user when username or password doesn't match
                        Console.WriteLine("\n\n\t\tWrong username or password, please try again.");
                        Console.ReadKey();
                    }
                }

                // once username and password match, direct user to menu page
                Console.WriteLine("\n\n\t\tValid Credentials...");
                Console.ReadKey();
                Menu menu = new Menu();
                menu.MenuScreen();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
