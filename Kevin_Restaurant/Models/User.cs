using Kevin_Restaurant.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kevin_Restaurant.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("admin")]
        public bool Admin { get; set; }

        [JsonPropertyName("telephonenumber")]
        public string TelephoneNumber { get; set; }

        public void Writetofile()
        {
            Users newuser = new Users();
            newuser.Updatelist(this);
        }

        private void PromoteToAdminOrCostumor(User Currentuser) // promotes this instance of user to admin or costumor
        {
            if (this.Admin)
            {
                List<string> yesornolist = new List<string>()
                    {
                        "Yes, change to costumor",
                        "No"

                    };

                ArrowMenu yesorno = new ArrowMenu("Are you sure you want to change this account from Administrator to a normal costumor account?", yesornolist, 0);
                int index = yesorno.Move();
                if (index == 0)
                {
                    this.Admin = false;
                    Console.Clear();
                    Console.WriteLine("User succesfully promoted to Costumor. Press any key to continue");
                }
                else
                {
                    ChangeUserInfo(Currentuser);
                }
            }
            else
            {
                List<string> yesornolist = new List<string>()
                    {
                        "Yes, change to Administrator",
                        "No"

                    };

                ArrowMenu yesorno = new ArrowMenu("Are you sure you want to change this account from a normal costumor account to an Administrator?", yesornolist, 0);
                int index = yesorno.Move();
                if (index == 0)
                {
                    this.Admin = true;
                    Console.Clear();
                    Console.WriteLine("User succesfully promoted to Admin. Press any key to continue");
                }
                else
                {
                    ;
                }
            }
           
            this.Writetofile();
        }

        private void ChangeUsername(User CurrentUser) // changes username of this instance
        {
            Console.Clear();
            Users Usercontroller = new();
            string Usernameattempt = "";
            Console.WriteLine("Enter new username:");
            bool check = false;
            while (check == false)
            {
                Usernameattempt = Console.ReadLine();
                if (Usernameattempt == "")
                {
                    Console.WriteLine("You didn't enter a new username");
                }
                else if (Usernameattempt == this.Username)
                {
                    Console.WriteLine("The username you entered is the same as your current username");
                }
                else if (Usercontroller.CheckForUsername(Usernameattempt) == false)
                {
                    Console.WriteLine("This username is not available, try another");
                }
                else
                {
                    check = true;
                }
            }
            this.Username = Usernameattempt;
            this.Writetofile();
            Console.Clear();
            Console.WriteLine("Username succesfully changed! Press any key to continue");
            ConsoleKeyInfo keypress = Console.ReadKey();
            ChangeUserInfo(CurrentUser);
        }

        private void ChangePassword(User CurrentUser) // changes password of this instance
        {
            Console.Clear();
            string password = "";
            bool check = false;
            Console.WriteLine("Enter new password:");
            while (check == false)
            {
                password = Console.ReadLine();
                if (password == "")
                {
                    Console.WriteLine("You didn't enter a new password");
                }
                else if (password == this.Password)
                {
                    Console.WriteLine("this password is the same as your current pasword.");
                }
                else
                {
                    check = true;
                }
            }

            //writes to file
            this.Password = password;
            this.Writetofile();
            Console.Clear();
            Console.WriteLine("Password changed succesfully! Press any key to continue");
            ConsoleKeyInfo keypress = Console.ReadKey();
            ChangeUserInfo(CurrentUser);
        }

        private void ChangePhoneNumber(User CurrentUser) // changes phone number of this instance
        {
            Console.Clear();
            string phoneattempt = "";
            Users Usercontroller = new();
            bool check = false;
            Console.WriteLine("Enter new Telephone number:");
            while (check == false)
            {
                phoneattempt = Console.ReadLine();
                if (phoneattempt == "")
                {
                    Console.WriteLine("You didn't enter anything");
                }
                else if (phoneattempt == this.Username)
                {
                    Console.WriteLine("The number you entered is the same as your current number");
                }
                else
                {
                    check = (Usercontroller.CheckforPhone(phoneattempt));
                }
            }
            this.TelephoneNumber = phoneattempt;
            this.Writetofile();
            Console.Clear();
            Console.WriteLine("Phonenumber changed succesfully! Press any key to continue");
            ConsoleKeyInfo keypress = Console.ReadKey();
            ChangeUserInfo(CurrentUser);
        }

        private void DeleteThisUser(User CurrentUser) // deltes this instance of user.
        {
            Users Usercontroller = new();
            Reservations ReservationController = new();
            Console.Clear();
            List<string> Deleteornot = new List<string>
                    {
                        "Yes, Delete this account and all its open reservations",
                        "No, Keep this account and all it's reservations"
                    };
            ArrowMenu deleteornot = new("Are you sure you want to delete this account?", Deleteornot, 0);
            int index = deleteornot.Move();
            if (index == 0)
            {
                Console.Clear();
                ReservationController.DeleteAllReservationsofUser(this);
                Console.Clear();
                Console.WriteLine("Account and reservations succesfully deleted. Press any key to continue.");
                ConsoleKeyInfo keypress = Console.ReadKey();
                Startscreen X = new();
                Usercontroller.DeleteUser(this.Id);
                if (this == CurrentUser) // if current user is being deleted, log out
                {
                    X.Show_StartingScreen();
                }
                else // if admin deletes a user, go back to the overview of all users
                { 
                    Mainmenu A = new(CurrentUser);
                    Users Usercontrol = new();
                    A.OverviewAllUsers(Usercontrol._users);
                }
            }
            else
            {
                ;
            }
        }

        public void ChangeUserInfo(User CurrentUser) //allows user to change his own personal information
        {
            if (CurrentUser.Admin) //admin menu
            {
                string Adminstring;
                if (this.Admin)
                {
                    Adminstring = "Change to Custumor";
                }
                else
                {
                    Adminstring = "Promote to Admin";
                }
                string prompt = $"Selected User:\n\nID: {this.Id}\nUsername: {this.Username}\nPassword: {this.Password}\nTelephone Number: {this.TelephoneNumber}\nAdmin: {this.Admin}\n\n Change\n\n";
                List<string> filteroptions = new List<string>()
                    {
                        "Username",
                        "Password",
                        "Telephone Number",
                        Adminstring,
                        "Delete Account",
                        "Back"
                    };
                ArrowMenu filter = new ArrowMenu(prompt, filteroptions, 10);
                int selectedindex = filter.Move();
                switch (selectedindex)
                {

                    case 0:
                        ChangeUsername(CurrentUser);
                        break;
                    case 1:
                        ChangePassword(CurrentUser);
                        break;
                    case 2:
                        ChangePhoneNumber(CurrentUser);

                        break;
                    case 3:
                        PromoteToAdminOrCostumor(CurrentUser);
                        break;
                    case 4:
                        DeleteThisUser(CurrentUser);
                        break;
                    case 5:
                        Mainmenu X = new(this);
                        Users y = new();
                        X.OverviewAllUsers(y._users);
                        break;
                }

               
            }
            else
            {
                List<string> change_info_options = new()
                {
                "Change Username",
                "Change Password",
                "Change Phone number",
                "Delete Account",
                "Back"
                };
                ArrowMenu info_change_menu = new ArrowMenu($"Username: {this.Username}\nPassword: {this.Password}\nPhone Number: {this.TelephoneNumber}\n\nIf you want to change your personal information, select an option from the menu below.\n", change_info_options, 5);
                int index = info_change_menu.Move();
                switch (index)
                {
                    case 0:
                        ChangeUsername(CurrentUser);
                        break;
                    case 1:
                        ChangePassword(CurrentUser);
                        break;
                    case 2:
                        ChangePhoneNumber(CurrentUser);
                        break;
                    case 3:
                        DeleteThisUser(CurrentUser);
                        break;
                    case 4:
                        Mainmenu X = new(this);
                        X.StartMainMenu();
                        break;
                }
            }
        }

    }
}
