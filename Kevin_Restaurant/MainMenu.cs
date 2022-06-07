using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    public class Mainmenu
    {
        private readonly string[] user_options;
        private readonly string[] admin_options;
        private readonly string[] reservation_options;
        private readonly string[] change_info_options;

        // current user
        private readonly Users Usercontroller;
        private User Currentuser;

        //menu 
        private ArrowMenu main_menu;
        private ArrowMenu admin_main_menu;
        private ArrowMenu reservation_menu;
        public Startscreen beginscherm;

        //reservation
        private readonly Reservations ReservationController;

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

            //menus instantiate
            this.main_menu = new ArrowMenu($"Welcome {Currentuser.Username}", this.user_options, 0);
            this.admin_main_menu = new ArrowMenu($"Welcome {Currentuser.Username}", this.admin_options, 0);
            this.reservation_menu = new ArrowMenu("Manage Reservations", this.reservation_options, 0);
            this.beginscherm = new Startscreen();
        }

        public void StartMainMenu()//Main menu start
        {
            Console.Clear();
            int index;
            if (Currentuser.Admin) // menu for admins
            {
                index = this.admin_main_menu.Move();
                switch (index)
                {
                    case 0:
                        Reservationmenu();
                        break;
                    case 1:
                        OverviewAllUsers(Usercontroller._users);
                        break;
                    case 2:
                        ChangeMenu X = new(Currentuser);
                        X.ShowAllMenus();
                        break;
                    case 3:
                        OverviewAllUsers(Usercontroller._users);
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
                        Currentuser.ChangeUserInfo(Currentuser);
                        break;
                    case 2:
                        this.beginscherm.Show_StartingScreen();
                        break;
                }
            }
        }

        private void Reservationmenu() // Reservation menu options
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

        private void ViewAllReservations(List<Reservation> Reservationlist) //shows user all reservations he made ( or the admin all reservations in the system)
        {
            string prompt = " Overview of Reservations\n ID   | Date                 | Time  | Table";
            ArrowMenu AllReservMenu = new(prompt, ReservationController.DisplayAllReservations(Reservationlist, Currentuser.Admin), 1);
            int selectedindex = AllReservMenu.Move();
            if (Currentuser.Admin)
            {
                if (selectedindex == Reservationlist.Count+1)
                {
                    StartMainMenu();
                }
                else if (selectedindex == Reservationlist.Count )
                {
                    FilterAllReservations();
                }
                else
                {
                    ViewReservation(Reservationlist[selectedindex].Id);
                }
            }
            else
            {
                if (selectedindex == Reservationlist.Count)
                {
                    StartMainMenu();
                }
                else
                {
                    ViewReservation(Reservationlist[selectedindex].Id);
                }
            }
        }

        private void FilterAllReservations() // allows admin to filter on all reservations in the system
        {
            Console.Clear();
            string prompt = "Search for:";
            List<string> filteroptions = new List<string>()
                    {
                        "All Reservations from User",
                        "Date",
                        "Time",
                        "Code",
                        "Back"
                    };
            ArrowMenu filter = new ArrowMenu(prompt, filteroptions, 0);
            int selectedindex = filter.Move();
            List<Reservation> ReservationsFromUser = new List<Reservation>();
            switch (selectedindex)
            {
                case 0:
                    //Search by User ID
                    bool searchfound = false;
                    while (searchfound == false)
                    {
                        Console.Clear();
                        Console.WriteLine("ID of User:");
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
                                FilterAllReservations();
                                searchfound = true;
                            }
                        }
                        else
                        {
                            ReservationsFromUser = ReservationController.FindAllReservations(Usercontroller.GetId(searchid));
                            ViewAllReservations(ReservationsFromUser);
                        }
                    }
                    break;
                //Search by date
                case 1:

                    searchfound = false;
                    while (searchfound == false)
                    {
                        int day = 0;
                        int month = 0;
                        Console.Clear();
                        bool correctinput = false;
                     
                        //ask day
                        while(correctinput == false)
                        {
                            Console.WriteLine("Day (Example: 7 or 20): ");
                            int x = Convert.ToInt32(Console.ReadLine());
                            if(x <= 0 || x >= 31)
                            {
                                Console.WriteLine("invalid, try again");
                            }
                            else
                            {
                                day = x;
                                correctinput = true;
                               
                            }
                        }

                        correctinput = false;
                        while (correctinput == false)
                        {
                            Console.WriteLine("Month (Example: 3 or 11): ");
                            int y = Convert.ToInt32(Console.ReadLine());
                            if (y < 0 || y > 12)
                            {
                                Console.WriteLine("invalid, try again");
                            }
                            else
                            {
                                month = y;
                                correctinput = true;
                          
                            }
                        }

                        //year is hardcoded for now
                        DateTime SearchDate = new(2022, month , day);
                        if (ReservationController.FindDate(SearchDate) == null)
                        {
                            Console.WriteLine("No existing reservations on this date, Press enter to try again or Escape to go back");
                            if (PressEnter() == true)
                            {
                                ;
                            }
                            else
                            {
                                FilterAllReservations();
                            }
                        }
                        else
                        {
                            ReservationsFromUser.Add(ReservationController.FindDate(SearchDate));
                            ViewAllReservations(ReservationsFromUser);
                        }
                    }
                    break;
                    //Search by Time
                case 2:
                    searchfound = false;
                    while (searchfound == false)
                    {
                        Console.Clear();
                        string search = Reservations.get_time();
                    
                        if (ReservationController.FindTime(search) == null)
                        {
                            Console.SetCursorPosition(0, 30);
                            Console.WriteLine("No existing reservations on this time, Press enter to try again or Escape to go back");
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
                            ReservationsFromUser.Add(ReservationController.FindTime(search));
                            ViewAllReservations(ReservationsFromUser);
                        }
                    }
                    break;
                //search reservation with Confirmation Code
                case 3:
                    searchfound = false;
                    while (searchfound == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Search:");
                        string searchtry = Console.ReadLine();
                        if (ReservationController.FindId(searchtry) == null)
                        {
                            Console.WriteLine("No existing Reservation with this code, Press enter to try again or Escape to go back");
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
                            ReservationsFromUser.Add(ReservationController.FindId(searchtry));
                            ViewAllReservations(ReservationsFromUser);
                        }
                    }
                    break;
                //go back
                case 4:
                    if (Currentuser.Admin)
                    {
                        ViewAllReservations(ReservationController._reservations);
                        break;
                    }
                    else
                    {
                        ViewAllReservations(ReservationController.FindAllReservations(Currentuser));
                        break;
                    }
        
            }
        }

        private void ViewReservation(string reservationid) //individual reservation menu 
        {
            Console.Clear();
            Reservation CurrentReservation = ReservationController.FindId(reservationid);
            string tables = StringOfTables(CurrentReservation);
            string prompt = $" Reservation by {Usercontroller.GetId(CurrentReservation.UserId).Username} (ID : {Usercontroller.GetId(CurrentReservation.UserId).Id})\n\n Details \n Date: {CurrentReservation.Date:dddd, dd MMMM yyyy}\n Time: {CurrentReservation.Time}\n Table: {tables}\n Code: {CurrentReservation.Id}\n\n Change: \n\n";
            List<string> reservoptions = new List<string>()
                    {
                        "Date",
                        "Time",
                        "Table",
                        "Cancel Reservation",
                        "Back"
                    };
            ArrowMenu Reservation = new ArrowMenu(prompt, reservoptions, 10);
            int selectedindex = Reservation.Move();
            switch (selectedindex)
            {
                case 0:
                    CurrentReservation.Date = ReservationController.get_date(14);
                    CurrentReservation.WriteToFile();
                    ViewReservation(reservationid);
                    break;
                case 1:
                    CurrentReservation.Time = Reservations.get_time();
                    CurrentReservation.WriteToFile();
                    ViewReservation(reservationid);
                    break;
                case 2:
                    CurrentReservation.Table = ReservationController.ChooseTable(CurrentReservation);
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

        private string StringOfTables(Reservation X )  // returns a string of all tables reserved
        {
            string alltables = $"{X.Table[0]}";
            if (X.Table.Count > 1)
            {
                foreach (int tafel in X.Table.Skip(1))
                {
                    alltables += $", {tafel}";
                }
            }
            
            return alltables;
        }

        public void OverviewAllUsers(List<User> Userlist) // function that lets admin delete users, manually add users, make an user an admin
        {
            string prompt = "Overview of Users\n ID   Username    Password   PhoneNumber   Admin";
            ArrowMenu AllUsersMenu = new ArrowMenu(prompt, Usercontroller.DisplayAllusers(Userlist), 1);
            int selectedindex = AllUsersMenu.Move();
            if(selectedindex == Userlist.Count+1) //go back
            {
                StartMainMenu();
            }
            else if(selectedindex == Userlist.Count) // filter scree
            {
                FilterUsers();
            }
            else
            {
                Userlist[selectedindex].ChangeUserInfo(Currentuser);
            }

        }

        private void FilterUsers() // allows user or admin to select an option to filter on the list of users
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
                            OverviewAllUsers(Searcheduser);
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
                           OverviewAllUsers(Searcheduser);
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
                           OverviewAllUsers(Searcheduser);
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
                           OverviewAllUsers(Searcheduser);
                        }
                    }
                    break;
                case 4:
                   OverviewAllUsers(Usercontroller.FindAllAdminsorNot(false));
                    break;
                case 5:
                   OverviewAllUsers(Usercontroller.FindAllAdminsorNot(true));
                    break;
                case 6:
                   OverviewAllUsers(Usercontroller._users);
                    break;
            }
        } 

        private bool PressEnter() // waits for user to press enter,
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
