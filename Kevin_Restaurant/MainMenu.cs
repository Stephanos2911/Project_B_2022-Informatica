using Kevin_Restaurant.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    internal class Mainmenu
    {
        public bool Admin;
        public string name;
        public string [] user_options;
        public string[] admin_options;
        public string[] reservation_options;
        public ArrowMenu main_menu;
        public ArrowMenu admin_main_menu;
        public ArrowMenu reservation_menu;

        public Mainmenu(bool isadmin, string name)
        {
            this.Admin = isadmin;
            this.name = name;
            //string of options
            this.user_options = new string[2]
            {
                "Reservations",
                "Log out"
            };
            this.admin_options = new string[4]
            {
                "Manage Reservations",
                "Manage Users",
                "Manage Dishes",
                "Back"
            };
            this.reservation_options = new string[3]
            {
                "Make new reservation",
                "View confirmation codes",
                "Back"
            };

            //menus instantiate
            this.main_menu = new ArrowMenu($"Welcome {this.name}", this.user_options,0);
            this.admin_main_menu = new ArrowMenu($"Welcome {this.name}", this.admin_options,0);
            this.reservation_menu = new ArrowMenu("Manage Reservations", this.reservation_options,0);
        }

        public void StartMainMenu()//Main menu start
        {
            Console.Clear();
            if (this.Admin)
            {

                this.AdminMainMenu();
            }
            else
            {

                this.UserMainMenu();
            }
        }

        public void Reservationmenu() // reservering menu voor normale user
        {
            int index = this.reservation_menu.Move();
            switch (index)
            {
                case 0:
                    Reservations ReservationController = new Reservations();
                    ReservationController.make_reservation(ReservationController);
                    break;
                default:
                    this.StartMainMenu();
                    break;
            }
        }

        public void UserMainMenu()
        {
            int index = this.main_menu.Move();
            switch (index)
            {
                case 0:
                    Reservationmenu();
                    break;
                case 1:
                    Startscreen beginscherm = new Startscreen();
                    beginscherm.Show_StartingScreen();
                    break;
            }
        }

        public void AdminMainMenu() //main menu voor admin
        {
            int index = this.admin_main_menu.Move();
            switch (index)
            {

            }
        } 
    }
}
