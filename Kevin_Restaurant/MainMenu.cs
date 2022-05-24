using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    internal class Mainmenu
    {
        public string [] user_options;
        public string[] admin_options;
        public string[] reservation_options;
        public string[] change_info_options;

        // current user
        public Users Usercontroller;
        public User Currentuser;

        //menu 
        public ArrowMenu main_menu;
        public ArrowMenu admin_main_menu;
        public ArrowMenu reservation_menu;
        public ArrowMenu info_change_menu;
        public Startscreen beginscherm;

        //reservation
        public Reservations ReservationController;

        public Mainmenu(User Currentuser)
        {

            this.Usercontroller = new Users();
            this.Currentuser = Currentuser;
            this.ReservationController = new Reservations();

            //string of options
            this.user_options = new string[3]
            {
                "Reservations",
                "Account",
                "Log out"
            };
            this.admin_options = new string[5]
            {
                "Manage Reservations",
                "Manage Users",
                "Manage Dishes",
                "Account",
                "Log out"
            };
            this.reservation_options = new string[3]
            {
                "Make new reservation",
                "View reservations",
                "Back"
            };
            this.change_info_options = new string[4]
            {
                "Change Username",
                "Change Password",
                "Change Phone number",
                "Back"
            };

            //menus instantiate
            this.main_menu = new ArrowMenu($"Welcome {Currentuser.Username}", this.user_options, 0);
            this.admin_main_menu = new ArrowMenu($"Welcome {Currentuser.Username}", this.admin_options, 0);
            this.reservation_menu = new ArrowMenu("Manage Reservations", this.reservation_options, 0);
            this.beginscherm = new Startscreen();
            this.info_change_menu = new ArrowMenu($"Username: {Currentuser.Username}\nPassword: {Currentuser.Password}\nPhone Number: {Currentuser.TelephoneNumber}\n If you want to change your personal information, select an option from the menu below.", change_info_options, 3);
        }

        public void StartMainMenu()//Main menu start
        {
            Console.Clear();
            int index = 0;
            if (Currentuser.Admin)
            {
                index = this.admin_main_menu.Move();
                switch (index)
                {
                    case 3:
                        ChangeUserInfo();
                        break;
                    default:
                        this.beginscherm.Show_StartingScreen();
                        break;
                }
            }
            else
            {
                index = this.main_menu.Move();
                switch (index)
                {
                    case 0:
                        Reservationmenu();
                        break;
                    case 1:
                        ChangeUserInfo();
                        break;
                    case 2:
                        this.beginscherm.Show_StartingScreen();
                        break;
                }
            }
        }

        public void Reservationmenu() // reservering menu voor normale user
        {
            int index = this.reservation_menu.Move();
            switch (index)
            {
                case 0:
                    Reservation NewReservation = ReservationController.make_reservation(this.ReservationController, this.Currentuser.Id);
                    Reservationmenu();
                    break;
                default:
                    this.ReservationController.ViewReservations(this.Currentuser);
                    break;
            }
        }

        public void ChangeUserInfo() //allows user to change personal information
        {
            Console.Clear();
            int choice = info_change_menu.Move();
            bool check = false;
            switch (choice) // checks if everything is correct
            {
    
                case 0:
                    Console.Clear();                 
                    string Usernameattempt = "";
                    Console.WriteLine("Enter new username:");
                    while(check == false)
                    {
                        Usernameattempt = Console.ReadLine();
                        if (Usernameattempt == "")
                        {
                            Console.WriteLine("You didn't enter a new username");
                        }
                        else if (Usernameattempt == Currentuser.Username)
                        {
                            Console.WriteLine("The username you entered is the same as your current username");
                        }
                        else if(checkdatabase(Usernameattempt, 0) == false)
                        {
                            Console.WriteLine("This username is not available, try another");
                        }
                        else
                        {
                            check = true;
                        }
                    }
                    
                    //writes to file
                    Currentuser.Username = Usernameattempt;
                    Currentuser.Writetofile();

                    beginscherm.Show_StartingScreen();
                    break;
                case 1:
                    Console.Clear();
                    string password = "";
                    Console.WriteLine("Enter new password:");
                    while (check == false)
                    {
                        password = Console.ReadLine();
                        if (password == "")
                        {
                            Console.WriteLine("You didn't enter a new password");
                        }
                        else if (password == Currentuser.Password)
                        {
                            Console.WriteLine("this password is the same as your current pasword.");
                        }
                        else
                        {
                            check = true;
                        }
                    }

                    //writes to file
                    Currentuser.Password = password;
                    Currentuser.Writetofile();

                    beginscherm.Show_StartingScreen();
                    break;
                case 2:
                    Console.Clear();
                    string phoneattempt = "";
                    Console.WriteLine("Enter new Telephone number:");
                    while (check == false)
                    {
                        phoneattempt = Console.ReadLine();
                        if (phoneattempt == "")
                        {
                            Console.WriteLine("You didn't enter anything");
                        }
                        else if (phoneattempt == Currentuser.Username)
                        {
                            Console.WriteLine("The number you entered is the same as your current number");
                        }
                        else if (checkdatabase(phoneattempt, 1) == false)
                        {
                            Console.WriteLine("This phone number is already in use");
                        }
                        else if (phoneattempt.Length > 15 || OnlyDigits(phoneattempt) || phoneattempt.Length < 9)
                        {
                            Console.WriteLine("Please enter a valid phonenumber");
                        }       
                        else
                        {
                            check = true;
                        }
                    }

                    //writes to file
                    Currentuser.TelephoneNumber = phoneattempt;
                    Currentuser.Writetofile();

                    beginscherm.Show_StartingScreen();
                    break;
                case 3:
                    StartMainMenu();
                    break;
            }
        }

        public bool checkdatabase(string input, int version)
        {
            if (version == 0)
            {
                bool check = true;
                foreach (User x in Usercontroller._users)
                {
                    if (x.Username == input)
                    {
                        check = false;
                    }
                }
                return check;
            }
            else
            {
                bool check = true;
                foreach (User x in Usercontroller._users)
                {
                    if (x.TelephoneNumber== input)
                    {
                        check = false;
                    }
                }
                return check;
            }

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
