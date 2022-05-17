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
        public string [] user_options;
        public string[] admin_options;
        public string[] reservation_options;


        public Mainmenu(bool isadmin)
        {
            this.Admin = isadmin;
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

            this.reservation_options = new string[4]
            {
                "Make new reservation",
                "View confirmation codes",
                "Manage Dishes",
                "Back"
            };
        }

        public void StartMainMenu()
        {
            Console.Clear();

            //Main menu met arrowmenu
            ArrowMenu main_menu = new ArrowMenu("Main Menu", this.user_options);
            int index = main_menu.Move();
            switch (index)
            {
   
            }
        }

        public void Reservationmenu()
        {
            ArrowMenu reservation_menu = new ArrowMenu("Reservation Menu", this.reservation_options);
            int index = reservation_menu.Move();
            switch (index)
            {

            }
        }

        public void AdminMenu()
        {
            ArrowMenu admin_menu = new ArrowMenu("Admin Menu", this.admin_options);
            int index = admin_menu.Move();
            switch (index)
            {

            }
        } 
    }
}
