using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{ // deze class is net nieuw. aangemaakt door gwn ADD te drukken 
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
            ArrowMenu mainMenu = new ArrowMenu(intro, options);
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
            ArrowMenu OtherMenu = new ArrowMenu(prompt, options);
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
            Console.Clear();
            Console.WriteLine("Enter username:");
            this.Username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            this.Password = Console.ReadLine();


        }

        public void Register() // asks for username, password, phonenumber then registers to json.
        {
            Console.Clear();
            Console.WriteLine("What do you want your username to be?");
            this.Username = Console.ReadLine();
            Console.WriteLine("What do you want your password to be? ");
            this.Password = Console.ReadLine();
            Console.WriteLine("What is your phonenumber?");
            this.Telephonenumber = Console.ReadLine();
            int phonenumber_length = this.Telephonenumber.Length;
            if (phonenumber_length < 10)
            {
                while (phonenumber_length < 10)
                {
                    Console.WriteLine("Please enter a valid phonenumber");
                    Console.WriteLine("What is your phonenumber?");
                    this.Telephonenumber = Console.ReadLine();
                    phonenumber_length = this.Telephonenumber.Length;
                }
            }
        }
    }
}
