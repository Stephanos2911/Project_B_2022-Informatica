using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//registreren
// kunnen meerdere usernames dezelfde wachtwoord hebben
namespace Kevin_Restaurant
{
    internal class Startscreen
    {
        public string Username;
        public string Password;
        public string Telephonenumber;

        public Startscreen()
        {
            this.Username = null;
            this.Password = null;
            this.Telephonenumber = null;
        }


        public void Show_StartingScreen() // prints all prompts and menus for the user at the beginning of the program
        {
            string intro = @"
 _  __          _       _           _ _                 
| |/ /         (_)     ( )         | (_)                
| ' / _____   ___ _ __ |/ ___    __| |_ _ __   ___ _ __ 
|  < / _ \ \ / / | '_ \  / __|  / _` | | '_ \ / _ \ '__|
| . \  __/\ V /| | | | | \__ \ | (_| | | | | |  __/ |_  
|_|\_\___| \_/ |_|_| |_| |___/  \__,_|_|_| |_|\___|_(_)
Welcome to Kevin’s diner, a place where you can taste the world. (uhh maybe ?)
Every month we have a brand-new cultural theme, this month’s theme is ….
Next month’s theme is ….
---------------------------------------------------------------------------------------------------------------------- 
L O C A T I O N

Our restaurant is located at Wijnhaven 107, 3011 WN in Rotterdam.
---------------------------------------------------------------------------------------------------------------------
O P E N I N G

We are open on Monday, Wednesday till Sunday. On Tuesdays we are closed. 
Kevin’s diner is open from 17.00-23.00. 
----------------------------------------------------------------------------------------------------------------------
H E L P

Use the up and down arrow to select what you want to do. (maybe) 
----------------------------------------------------------------------------------------------------------------------
Have fun dining out and thank you for choosing us. -K";

            string[] options = { "Sign up", "Log in", "Exit", "More / Extra" };
            ArrowMenu mainMenu = new ArrowMenu(intro, options, 24);
            int SelectedIndex = mainMenu.Move();


            switch (SelectedIndex)
            {
                case 0:
                    this.Register();
                    break;
                case 1:
                    this.Login();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                case 3:
                    Extras_Menu();
                    break;
            }
        }


        private void Extras_Menu() // menu with extra information for a costumor
        {

            string prompt = "Another menu";
            string[] options = { "Menu Card", "Map", "Reservation" };
            ArrowMenu OtherMenu = new ArrowMenu(prompt, options, 0);
            int extra_selectedIndex = OtherMenu.Move();

            switch (extra_selectedIndex)
            {
                case 0:
                    Console.WriteLine("hier komt het menu");
                    break;

            }

        }


        public void Login()
        {
            //setup
            Console.Clear();
            Console.WriteLine("Login");
            bool check = false;
            string Usernameattempt = "";
            string passwordattempt = "";

            while(check == false)
            { 
                Console.WriteLine("Enter username:");
                string username = Console.ReadLine();
                if(username == "")
                {
                    Console.WriteLine("You have didn't enter a username");
                }
                else
                {
                    Usernameattempt = username;
                    check = true;
                }
            }
            check = false;

            while (check == false)
            {
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                if (password == "")
                {
                    Console.WriteLine("You have didn't enter a password");
                }
                else
                {
                    passwordattempt = password;
                    check = true;
                }
            }

            Users usercontroller = new Users();

            //check if credentials are valid to an account
            User logintry = usercontroller.Getusername(Usernameattempt);
            if (logintry != null && logintry.Password == passwordattempt && logintry.Username == Usernameattempt)
            {
                Mainmenu mainMenu = new Mainmenu(logintry.Admin, logintry.Username);
                mainMenu.StartMainMenu();
            }

            else
            {
                Console.WriteLine("Your username and password do not correspond to any User in our database, Press enter to try again");
                this.KeytoContinue();
                this.Login();
            }
        }


        public void Register() // asks for username, password, phonenumber then registers to json.
        {
            Console.Clear();

            //setup nieuwe user om te writen
            Users usercontroller = new Users();

            User NewUser = CheckCredentials(usercontroller._users, usercontroller._users.Count + 1);

            NewUser.Writetofile();

            //registration is succesfull, wait for key press to continue
            Console.WriteLine("\nRegistration successful, press enter to continue to login.");
            this.KeytoContinue();
            this.Show_StartingScreen();

        }


        public void KeytoContinue() // wacht tot enter gedrukt word om door te gaan
        {
            ConsoleKeyInfo keypress = Console.ReadKey();
            bool waiting = true;
            if (waiting == true)
            {
                if (keypress.Key == ConsoleKey.Enter)
                {
                    waiting = false;
                }
            }
        }


        public User CheckCredentials(List<User> users, int ID) // function to keep asking for correct inputs
        {
            User writeuser = new User();
            writeuser.Id = ID;
            writeuser.Admin = false;


            //check username
            Console.WriteLine("What do you want your username to be?");
            bool usercheck = false;
            while (usercheck == false)
            {
                writeuser.Username = Console.ReadLine();
                usercheck = AlreadyExists(users, "Username", writeuser.Username);
            }

            //ask password
            Console.WriteLine("What do you want your password to be?");
            writeuser.Password = Console.ReadLine();

            //check phonenumber
            Console.WriteLine("Enter your phone-number:");
            usercheck = false;
            while (usercheck == false)
            {
                writeuser.TelephoneNumber = Console.ReadLine();
                usercheck = AlreadyExists(users, "Phonenumber", writeuser.TelephoneNumber);
            }

            return writeuser;
        }


        public bool AlreadyExists(List<User> users, string type, string input)
        {
            bool stoploop = false;
            bool check = true;
            //check if username exists in database
            if (stoploop == false)
            {
                if (type == "Username")
                {
                    foreach (User x in users)
                    {
                        if (x.Username == input)
                        {
                            Console.WriteLine("This username already exists, try again:");
                            check = false;
                            stoploop = true;
                        }
                    }
                    if (input == "")
                    {
                        Console.WriteLine("No username was typed:");
                        check = false;
                        stoploop = true;
                    }
                }
                //check if telephonenumber is already in database
                else
                {
                    if (input.Length > 15 || OnlyDigits(input) || input.Length < 9 || input == "")
                    {
                        Console.WriteLine("Please enter a valid phonenumber");
                        check = false;
                        stoploop = true;
                    }
                    else
                    {
                        foreach (User x in users)
                        {
                            if (x.TelephoneNumber == input)
                            {
                                Console.WriteLine("This phone-number is already registered, try another number:");
                                check = false;
                                stoploop = true;
                            }
                        }
                    }
                }
            }
            return check;
        }


        public bool OnlyDigits(string str) // checkt of de string alleen getallen bevat
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return true;
            }

            return false;
        }

    }
}
