using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    public class ChangeMenu
    {
        private Dishes controller;
        private Menus Menucontroller;
        private string prompt;
        private User CurrentUser;

        public ChangeMenu(User CurrentUser)
        {
            this.controller = new Dishes();
            this.Menucontroller = new Menus();
            this.prompt = "Please select";
            this.CurrentUser = CurrentUser;
        }


        public void ShowAllMenus()
        {
            List<string> options = ListBuildMenu();
            ArrowMenu OtherMenu = new ArrowMenu("Overview of All Menu's\nID | Start Date | End Date   | Name", options, 1);
            int selectedIndex = OtherMenu.Move();
            if (selectedIndex == Menucontroller._menus.Count + 1)
            {
                Mainmenu A = new Mainmenu(CurrentUser);
                A.StartMainMenu();
            }
            else if (selectedIndex == Menucontroller._menus.Count)
            {
                AddMenu();
                ShowAllMenus();
            }
            else
            {
                Selection(Menucontroller._menus[selectedIndex]);
            }
        }

        private List<string> ListBuildMenu() // makes a String list of all menus
        {
            List<string> AllMenuList = new List<string>();
            List<Menu> AllMenus = Menucontroller._menus;
            foreach (Menu i in AllMenus)
            {
                AllMenuList.Add($"{i.Id} | {i.StartingDate.ToString("MM/dd/yyyy")} | {i.EndDate.ToString("MM/dd/yyyy")} | {i.Name} |");
            }
            AllMenuList.Add("   Add Menu   ");
            AllMenuList.Add("   Back    ");
            return AllMenuList;
        }


        private List<String> DishListBuild(int MenuId) //given a menu id gives a string with all dishes from that menu
        {
            List<string> AllDishList = new List<string>();
            List<Dish> DishesofMenu = controller.AllDishesbyMenu(MenuId);
            foreach (Dish i in DishesofMenu)
            {
                AllDishList.Add($" [{i.Type}] {i.Gerecht} {i.Price},-");
            }
            AllDishList.Add("   Add Dish   ");
            AllDishList.Add("   Delete this Menu   ");
            AllDishList.Add("   Back    ");
            return AllDishList;
        }

        private void Selection(Menu thismenu) // shows all dishes of a chosen menu
        {
            List<Dish> DishesOfMenu = controller.AllDishesbyMenu(thismenu.Id);
            string prompt1 = $"All dishes of the {thismenu.Name} Menu";
            List<string> options = DishListBuild(thismenu.Id);

            ArrowMenu OtherMenu = new ArrowMenu(prompt1, options, 0);
            int selectedIndex = OtherMenu.Move();


            if(options.Count > 2) // if menu has already one dish
            {
                if (selectedIndex == DishesOfMenu.Count + 2) // go back
                {
                    ShowAllMenus();

                }
                else if (selectedIndex == DishesOfMenu.Count) // add dish
                {
                    AddDish(thismenu);
                    Selection(thismenu);
                }
                else if(selectedIndex == DishesOfMenu.Count + 1) // delete menu
                {
                    DeleteMenu(thismenu);
                }
                else
                {
                    Console.Clear();
                    string[] stringArray = new string[]
                    {
                    "Adjust",
                    "Remove",
                    "Back"
                    };
                    ArrowMenu OtherMenu2 = new ArrowMenu(prompt, stringArray, 0); // Adjust, Remove, Back
                    int selectedIndex2 = OtherMenu2.Move();
                    if (selectedIndex2 == 0) // adjust dish
                    {
                        Adjust(DishesOfMenu[selectedIndex].Id);
                        Selection(thismenu);
                    }
                    else if (selectedIndex2 == 1) // delete dish
                    {
                        controller.RemoveDish(controller._Dishes[selectedIndex].Id);
                        Selection(thismenu);
                    }
                    else // go back to all dishes overview
                    {
                        Selection(thismenu);
                    }
                }

            }
            else // if Menu is empty
            {
                if (selectedIndex == 2)
                {
                    ShowAllMenus();

                }
                else if(selectedIndex == 1)
                {
                    DeleteMenu(thismenu);
                }
                else
                {
                    AddDish(thismenu);
                    Selection(thismenu);
                }
            }

        }

        private void DeleteMenu(Menu MenutoDelete)
        {
            Console.Clear();
            List<string> yesorno = new List<string>()
            {
                "Yes",
                "No"
            };
            ArrowMenu AreYouSure = new("Are you sure you want to Delete this menu and all it's dishes?", yesorno, 0);
            int index = AreYouSure.Move();
            if(index == 0)
            {
                Menucontroller.DeleteMenu(MenutoDelete.Id);
                controller.DeleteAllDishesofMenu(MenutoDelete.Id);
                Console.Clear();
                Console.WriteLine("Menu deleted succesfully, press any key to continue");
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                ShowAllMenus();
            }
            else
            {
                Selection(MenutoDelete);
            }
        }

        private void AddMenu() // Admin can add a menu 
        {
            Menu newMenu = new Menu();
            Console.Clear();

            //ask menu name
            bool correctname = false;
            while(correctname == false)
            {
                Console.WriteLine("Name of new Menu:\n");
                newMenu.Name = Console.ReadLine();
                bool check = true;
                foreach(Menu S in Menucontroller._menus)
                {
                    if(S.Name == newMenu.Name)
                    {
                        check = false;
                    }
                }
                if (check == false)
                {
                    Console.WriteLine("There is already an other existing menu with this name, try again.");
                }
                correctname = check;
            }

            if(Menucontroller._menus.Count == 0) // if this is the first menu, menu id =0
            {
                newMenu.Id = 0;
            }
            else
            {
                newMenu.Id = Menucontroller._menus[Menucontroller._menus.Count - 1].Id + 1 ;
            }


            //ask which month
            Dictionary <int,string> Availablemonths = AvailableMonths();
            List<string> monthsformenu = Availablemonths.Values.ToList();
            List<int> MonthstoInt = Availablemonths.Keys.ToList();
            
            Console.Clear();

            ArrowMenu NewDate = new ArrowMenu("Choose one month out of the available monthes below for this menu.\n", monthsformenu, 1);
            int ind = NewDate.Move();
            DateTime X = new DateTime(2022, MonthstoInt[ind], 15);
            newMenu.StartingDate = FirstDayOfMonth(X);
            newMenu.EndDate = newMenu.StartingDate.AddMonths(1).AddDays(-1);

            Menucontroller.UpdateList(newMenu);
            Console.Clear();
            Console.WriteLine("Menu succesfully added! Press any key to continue");
            Console.ReadKey();
        }

        private Dictionary<int, string> AvailableMonths()  // returns list of integers of months that are available
        {
            Dictionary<int, string> months = new Dictionary<int, string>()
{
                {1, "January"},
                {2, "February"},
                {3, "March"},
                {4, "April"},
                {5, "May"},
                {6, "June" },
                {7,"July"},
                {8, "August"},
                {9, "September"},
                {10 , "October"},
                {11 ,"November"},
                { 12, "December"},
};
            foreach (Menu X in Menucontroller._menus)
            {
                months.Remove(X.StartingDate.Month);
            }
            return months;
        }

        private DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }



        private void Adjust(int DishID) // allows an admin to adjust a dish or delete it
        {
            Console.Clear();
            Dish dish = controller.GetById(DishID);
            string adjustInfo = ($"Name: {dish.Gerecht}\n" +
            $"Type: {dish.Type}\n" +
            $"Price: {dish.Price}\n" +
            $"If you want to change your dish information, select an option from the menu below.");

            string[] adjustMenu = new string[]
            {
                "Type",
                "Name",
                "Price"
            };
            ArrowMenu OtherMenu = new ArrowMenu(adjustInfo, adjustMenu, 3);
            int selectedIndex = OtherMenu.Move();
            if (selectedIndex == 0)
            {
                dish.Type = Type();
                dish.WriteToFile();
            }
            else if (selectedIndex == 1)
            {
                dish.Gerecht = Name();
                dish.WriteToFile();
            }
            else
            {
                dish.Price = Price();
                dish.WriteToFile();
            }
            Console.Clear();
            Console.WriteLine("Dish succesfully updated! Press any key to continue");
            Console.ReadKey();

        }
        private void AddDish(Menu CurrentMenu) //adds a dish to a chosen menu
        {
            Console.Clear();
            Dish NewDish = new Dish();
            string prompt2 = "Please select the dish sort.";
            string[] addMenu = new string[]
            {
                "Appetizer",
                "Main Course",
                "Dessert"
            };
            ArrowMenu OtherMenu = new ArrowMenu(prompt2, addMenu, 0);


            int selectedIndex = OtherMenu.Move();
            if (selectedIndex == 0)
            {
                NewDish.Sort = "appetizer";

            }
            else if (selectedIndex == 1)
            {
                NewDish.Sort = "main course";
            }
            else
            {
                NewDish.Sort = "Dessert";
            }
            Console.Clear();

            //ask for everything
            NewDish.Gerecht = Name();
            NewDish.Type = Type();
            NewDish.Price = Price();
            NewDish.MenuId = CurrentMenu.Id;
            NewDish.Id = controller._Dishes[controller._Dishes.Count - 1].Id + 1; //id of the last dish + 1

            //write new dish to json
            controller.UpdateList(NewDish);
            Console.Clear();
            Console.WriteLine("New Dish succesfully added! Press any key to continue");
            Console.ReadKey();

        }

        private string Type()
        {
            Console.Clear();
            string prompt2 = "Please select the dish type.";
            string[] typeMenu = new string[]
            {
                "Meat",
                "Vegan",
                "Vegetarian",
                "Fish",
                "Ice cream",
                "Pudding",
                "Pie"
            };
            ArrowMenu OtherMenu = new ArrowMenu(prompt2, typeMenu, 0);
            int selectedIndex = OtherMenu.Move();

            string newtype = typeMenu[selectedIndex];
            return newtype;
        }
        private int Price()
        {
            Console.Clear();
            int newprice;
            Console.WriteLine("What will be the new price? ");
            while (!int.TryParse(Console.ReadLine(), out newprice))
            {
                Console.Clear();
                Console.WriteLine("That was invalid. Enter a valid number.");
            }
            return newprice;
        }
        private string Name()
        {
            Console.Clear();
            Console.WriteLine("What will be the new name? ");
            string newname = Console.ReadLine();
            return newname; 
        }

        //public void Theme(int DishId, string AddorChange)
        //{
        //    Console.Clear();
        //    Console.WriteLine("What will be the new theme? ");
        //    string newtheme = Console.ReadLine();


        //    if (AddorChange == "Add")
        //    {
        //        NewDish.Theme = newtheme;
        //    }
        //    else
        //    {
        //        AllDishes[DishId].Theme = newtheme;
        //        AllDishes[DishId].WriteToFile();
        //    }
        //}

    }
}
