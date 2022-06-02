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
        public Dishes DishesController;

        public Mainmenu(User Currentuser)
        {

            this.Usercontroller = new Users();
            this.Currentuser = Currentuser;
            this.ReservationController = new Reservations();
            this.DishesController = new Dishes();

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
                "Manage Menu",
                "Account",
                "Log out"
            };
            this.reservation_options = new string[3]
            {
                "Make new reservation",
                "View reservations",
                "Back"
            };
            this.change_info_options = new string[5]
            {
                "Change Username",
                "Change Password",
                "Change Phone number",
                "Delete Account",
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
            if (Currentuser.Admin) // menu for admins
            {
                index = this.admin_main_menu.Move();
                switch (index)
                {
                    case 0:
                        Reservationmenu();
                        break;
                    case 1:
                        UserControlScreen(Usercontroller._users);
                        break;
                    case 2:
                        ChangeMenu X = new ChangeMenu(Currentuser);
                        X.ShowAllMenus();
                        break;
                    case 3:
                        ChangeUserInfo();
                        break;
                    case 4:
                        this.beginscherm.Show_StartingScreen();
                        break;
                }
            }
            else // menu for users
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

        public void Reservationmenu() // reservering menu 
        {
            Console.Clear();
            int index = this.reservation_menu.Move();
            switch (index)
            {
                case 0:
                    ReservationController.make_reservation(Currentuser);
                    Reservationmenu();
                    break;
                case 1:
                    if (Currentuser.Admin)
                    {
                        ViewAllReservations(ReservationController._reservations);
                    }
                    else
                    {
                        ViewAllReservations(ReservationController.FindAllReservations(Currentuser));
                    }
                    break;
                case 2:
                    StartMainMenu();
                    break;
            }
        }

        public void ViewAllReservations(List<Reservation> Reservationlist)
        {
            string prompt = " Overview of Reservations\n ID   | Date                 | Time  | Table";
            ArrowMenu AllReservMenu = new ArrowMenu(prompt, ReservationController.DisplayAllReservations(Reservationlist), 1);
            int selectedindex = AllReservMenu.Move();
            if (Currentuser.Admin)
            {
                if (selectedindex == Reservationlist.Count+1)
                {
                    StartMainMenu();
                }
                else if (selectedindex == Reservationlist.Count )
                {
                    FilterReservationsScreen();
                }
                else
                {
                    ViewReservation(Reservationlist[selectedindex].Id);
                }
            }
            else
            {
                if (selectedindex == Reservationlist.Count + 1)
                {
                    StartMainMenu();
                }
                else if (selectedindex == Reservationlist.Count )
                {
                    FilterReservationsScreen();
                }
                else
                {
                    ViewReservation(Reservationlist[selectedindex].Id);
                }
            }
        }
        
        public void ViewReservation(string reservationid) //individual reservation menu 
        {
            Console.Clear();
            Reservation CurrentReservation = ReservationController.FindId(reservationid);
            string prompt = $" Reservation by {Usercontroller.GetId(CurrentReservation.UserId).Username}\n\n Details \n Date: {CurrentReservation.Date.ToString("dddd, dd MMMM yyyy")}\n Time: {CurrentReservation.Time}\n Table: {CurrentReservation.Table}\n Code: {CurrentReservation.Id}\n\n Change: \n";
            List<string> reservoptions = new List<string>()
                    {
                        "Date",
                        "Time",
                        "Table",
                        "Cancel Reservation",
                        "Back"
                    };
            ArrowMenu Reservation = new ArrowMenu(prompt, reservoptions, 9);
            int selectedindex = Reservation.Move();
            switch (selectedindex)
            {
                case 0:
                    CurrentReservation.Date = ReservationController.get_date(14);
                    CurrentReservation.WriteToFile();
                    ViewReservation(reservationid);
                    break;
                case 1:
                    CurrentReservation.Time = ReservationController.get_time();
                    CurrentReservation.WriteToFile();
                    ViewReservation(reservationid);
                    break;
                case 2:
                    CurrentReservation.Table = ReservationController.ChooseTable(CurrentReservation.Diners, CurrentReservation.Date);
                    CurrentReservation.WriteToFile();
                    ViewReservation(reservationid);
                    break;
                case 3:
                    Console.Clear();
                    List<string> yesornolist = new List<string>()
                    {
                        "Yes, Cancel this reservation",
                        "No"
                    };

                    ArrowMenu yesorno = new ArrowMenu("Are you sure you want to cancel this reservation?", yesornolist, 0);
                    int ind = yesorno.Move();
                    if (ind == 0)
                    {
                        ReservationController.DeleteReservation(CurrentReservation);
                        ViewAllReservations(ReservationController.FindAllReservations(Currentuser));
                    }
                    else
                    {
                        ViewReservation(reservationid);
                    }
                    break;
                case 4:
                    if (Currentuser.Admin)
                    {
                        ViewAllReservations(ReservationController._reservations);
                    }
                    else
                    {
                        ViewAllReservations(ReservationController.FindAllReservations(Currentuser));
                    }
                    break;
            }
        }

        public void FilterReservationsScreen()
        {

        }

        public void UserControlScreen(List<User> Userlist) // function that lets admin delete users, manually add users, make an user an admin
        {
            string prompt = "Overview of Users\n ID   Username    Password   PhoneNumber   Admin";
            ArrowMenu AllUsersMenu = new ArrowMenu(prompt, Usercontroller.DisplayAllusers(Userlist), 1);
            int selectedindex = AllUsersMenu.Move();
            if(selectedindex == Userlist.Count+1)
            {
                StartMainMenu();
            }
            else if(selectedindex == Userlist.Count)
            {
                FilterUsers();
            }
            else
            {
                UserControl(Userlist[selectedindex].Id); ;
            }

        }

        public void FilterUsers()
        {
            Console.Clear();
            string prompt = "Search for:";
            List<string> filteroptions = new List<string>()
                    {
                        "ID",
                        "Username",
                        "Password",
                        "Telephone Number",
                        "Show all Costumors",
                        "Show all Administrators",
                        "Back"
                    };
            ArrowMenu filter = new ArrowMenu(prompt, filteroptions, 0);
            int selectedindex = filter.Move();
            List<User> Searcheduser = new List<User>();
            switch (selectedindex)
            {
                case 0:
                    bool searchfound = false;
                    while (searchfound == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Search:");
                        int searchid = Convert.ToInt32(Console.ReadLine());
                        if (Usercontroller.GetId(searchid) == null)
                        {
                            Console.WriteLine("No existing user with this ID, Press enter to try again or Escape to go back");
                            if (PressEnter() == true)
                            {
                                ;
                            }
                            else
                            {
                                FilterUsers();
                                searchfound = true;
                            }
                        }
                        else
                        {
                            Searcheduser.Add(Usercontroller.GetId(searchid));
                            UserControlScreen(Searcheduser);
                        }
                    }
                    break;
                case 1:
                    searchfound = false;
                    string searchtry;
                    while (searchfound == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Search:");
                        searchtry = Console.ReadLine();
                        if (Usercontroller.Getusername(searchtry) == null)
                        {
                            Console.WriteLine("No existing user with this username, Press enter to try again or Escape to go back");
                            if (PressEnter() == true)
                            {
                                ;
                            }
                            else
                            {
                                FilterUsers();
                                searchfound = true;
                            }
                        }
                        else
                        {
                            Searcheduser.Add(Usercontroller.Getusername(searchtry));
                            UserControlScreen(Searcheduser);
                        }
                    }
                    break;
                case 2:
                    searchfound = false;
                    while (searchfound == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Search:");
                        searchtry = Console.ReadLine();
                        if (Usercontroller.GetbyPassword(searchtry) == null)
                        {
                            Console.WriteLine("No existing user with this Password, Press enter to try again or Escape to go back");
                            if (PressEnter() == true)
                            {
                                ;
                            }
                            else
                            {
                                FilterUsers();
                            }
                        }
                        else
                        {
                            Searcheduser.Add(Usercontroller.GetbyPassword(searchtry));
                            UserControlScreen(Searcheduser);
                        }
                    }
                    break;
                case 3:
                    searchfound = false;
                    while (searchfound == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Search:");
                        searchtry = Console.ReadLine();
                        if (Usercontroller.GetbyPhone(searchtry) == null)
                        {
                            Console.WriteLine("No existing user with this Telephone number, Press enter to try again or Escape to go back");
                            if (PressEnter() == true)
                            {
                                ;
                            }
                            else
                            {
                                FilterUsers();
                                searchfound = true;
                            }
                        }
                        else
                        {
                            Searcheduser.Add(Usercontroller.GetbyPhone(searchtry));
                            UserControlScreen(Searcheduser);
                        }
                    }
                    break;
                case 4:
                    UserControlScreen(Usercontroller.FindAllAdminsorNot(false));
                    break;
                case 5:
                    UserControlScreen(Usercontroller.FindAllAdminsorNot(true));
                    break;
                case 6:
                    UserControlScreen(Usercontroller._users);
                    break;
            }
        } // filter Screen

        public void UserControl(int userid) // Selected user by admin, allows admin to change something about the user.
        {
            Console.Clear();
            User SelectedUser = Usercontroller._users[userid-1];
            string Adminstring;
            if (SelectedUser.Admin == true)
            {
                Adminstring = "Change to Custumor";
            }
            else
            {
                Adminstring = "Promote to Admin";
            }
            string prompt = $"Selected User:\n\nID: {SelectedUser.Id}\nUsername: {SelectedUser.Username}\nPassword: {SelectedUser.Password}\nTelephone Number: {SelectedUser.TelephoneNumber}\nAdmin: {SelectedUser.Admin}\n\n Change\n\n";
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
                    AdminChangeUsers("Username", SelectedUser);
                    break;
                case 1:
                    AdminChangeUsers("Password", SelectedUser);
                    break;
                case 2:
                    AdminChangeUsers("Telephone Number", SelectedUser);
                    break;
                case 3:
                    AdminChangeUsers(Adminstring, SelectedUser);
                    break;
                case 4:
                    AdminChangeUsers("Delete", SelectedUser);
                    break;
                case 5:
                    UserControlScreen(Usercontroller._users);
                    break;
            }
        }

        public void AdminChangeUsers(string option, User Currentuser)
        {
            switch (option)
            {
                case "Username":
                    Console.Clear();
                    Console.WriteLine($" Current Username:\n {Currentuser.Username}\n New Username:");
                    string newusername = "";
                    bool check = false;
                    while (check == false)
                    {
                        newusername = Console.ReadLine();
                        check = Checkdatabase(newusername, "username");
                    }
                    Currentuser.Username = newusername;
                    Currentuser.Writetofile();
                    Console.WriteLine("Write Succesful! Press enter to continue");
                    PressEnter();
                    UserControlScreen(Usercontroller._users);
                    break;
                case "Password":

                    Console.Clear();
                    Console.WriteLine($" Current Password:\n {Currentuser.Username}\n New Password:");
                    string newpass = Console.ReadLine();
                    Currentuser.Password = newpass;

                    Currentuser.Writetofile();
                    Console.WriteLine("Write Succesful! Press enter to continue");
                    PressEnter();
                    UserControlScreen(Usercontroller._users);
                    break;
                case "Telephone Number":

                    Console.Clear();
                    Console.WriteLine($" Current Phonenumber:\n {Currentuser.TelephoneNumber}\n New Phonenumber:");
                    bool check2 = false;
                    string newphone = "";

                    while (check2 == false)
                    {
                        newphone = Console.ReadLine();
                        check2 = Checkdatabase(newphone, "Phonenumber");
                    }

                    Currentuser.TelephoneNumber = newphone;
                    Currentuser.Writetofile();
                    Console.WriteLine("Write Succesful! Press enter to continue");
                    PressEnter();
                    UserControlScreen(Usercontroller._users);
                    break;
                case "Change to Custumor":
                    Console.Clear();
                    List<string> yesornolist = new List<string>()
                    {
                        "Yes, change to costumor",
                        "No"
             
                    };

                    ArrowMenu yesorno = new ArrowMenu("Are you sure you want to change this account from Administrator to a normal costumor account?", yesornolist, 0);
                    int index = yesorno.Move();
                    if(index == 0)
                    {
                        Currentuser.Admin = false;
                    }
                    else
                    {
                        ;
                    }

                    Currentuser.Writetofile();
                    Console.WriteLine($"{Currentuser.Username} doesn't have any Administrative capabilities. Press enter to continue");
                    PressEnter();
                    UserControlScreen(Usercontroller._users);
                    break;
                case "Promote to Admin":
                    Console.Clear();
                    List<string> yesornolist2 = new List<string>()
                    {
                        "Yes, promote to Admin",
                        "No"

                    };
                    ArrowMenu yesorno2 = new ArrowMenu("Are you sure you want to give this account Administrative priviliges?", yesornolist2, 0);
                    
                    int index2 = yesorno2.Move();
                    if (index2 == 0)
                    {
                        Currentuser.Admin = true;
                        Currentuser.Writetofile();
                        Console.WriteLine($"{Currentuser.Username} has been promoted to administrator. Press enter to continue");
                        PressEnter();
                    }
                    else
                    {
                        ;
                    }
                    UserControlScreen(Usercontroller._users);
                    break;
                case "Delete":
                    Console.Clear();
                    List<string> Deleteornot = new List<string>
                    {
                        "Yes, Delete this account and all it's open reservations",
                        "No, Keep this account and all it's reservations"
                    };
                    ArrowMenu deleteornot = new ArrowMenu("Are you sure you want to delete this account?", Deleteornot, 0);
                    int indexq = deleteornot.Move();
                    if (indexq == 0)
                    {
                        Console.Clear();
                        Usercontroller.DeleteUser(Currentuser.Id);
                        ReservationController.DeleteAllReservationsofUser(Currentuser);
                        this.Currentuser = null;
                        Console.WriteLine("Account and reservations succesfully deleted. Press  key to continue.");
                        ConsoleKeyInfo keypress = Console.ReadKey();
                        UserControlScreen(Usercontroller._users);
                    }

                    break;
            }
        }// actual input function for admin to change credentials

        public void ChangeUserInfo() //allows user to change personal information
        {
            Console.Clear();
            int choice = info_change_menu.Move();
            bool check = false;
            switch (choice) 
            {
    
                case 0: // Username changer with security
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
                        else if(Checkdatabase(Usernameattempt, "username") == false)
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

                    Console.WriteLine("Write Succesful! Press enter to continue");
                    PressEnter();
                    beginscherm.Show_StartingScreen();
                    break;
                case 1: // password changer for admin
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


                    Console.WriteLine("Write Succesful! Press enter to continue");
                    PressEnter();
                    beginscherm.Show_StartingScreen();
                    break;
                case 2: // phone number changer for admin
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
                        else
                        {
                            check = (Checkdatabase(phoneattempt, "phone"));
                        }
                    }

                    //writes to file
                    Currentuser.TelephoneNumber = phoneattempt;
                    Currentuser.Writetofile();


                    Console.WriteLine("Write Succesful! Press enter to continue");
                    PressEnter();
                    beginscherm.Show_StartingScreen();
                    break;
                case 3:
                    Console.Clear();
                    List<string> Deleteornot = new List<string>
                    {
                        "Yes, Delete this account and all its open reservations",
                        "No, Keep this account and all it's reservations"
                    };
                    ArrowMenu deleteornot = new ArrowMenu("Are you sure you want to delete this account?", Deleteornot, 0);
                    int index = deleteornot.Move();
                    if(index == 0)
                    {
                        Console.Clear();
                        Usercontroller.DeleteUser(Currentuser.Id);
                        ReservationController.DeleteAllReservationsofUser(Currentuser);
                        this.Currentuser = null;
                        Console.WriteLine("Account and reservations succesfully deleted. Press  key to continue.");
                        ConsoleKeyInfo keypress = Console.ReadKey();
                        beginscherm.Show_StartingScreen();
                        
                    }
                    else
                    {
                        ChangeUserInfo();
                    }
                    break;
                case 4:
                    StartMainMenu();
                    break;
            }
        }

        public bool Checkdatabase(string input, string version) // checks if username is already in use
        {
            if (version == "username")
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
                if (input.Length > 15 || OnlyDigits(input) || input.Length < 9 || input == "")
                {
                    Console.WriteLine("Please enter a valid phonenumber");
                    check = false;

                }
                else
                {
                    foreach (User x in Usercontroller._users)
                    {
                        if (x.TelephoneNumber == input)
                        {
                            Console.WriteLine("This phone-number is already registered, try another number:");
                            check = false;

                        }
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

        public bool PressEnter()
        {
            bool waiting = true;
            bool boolean = false;
            if (waiting == true)
            {
                ConsoleKeyInfo keypress = Console.ReadKey();
                if (keypress.Key == ConsoleKey.Enter)
                {
                    waiting = false;
                    boolean = true;
                }
                else
                {
                    waiting = false;
                }
            }
            return boolean;
        }

    }
}
