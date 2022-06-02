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
    class ChangeMenu
    {
        public Dishes controller;
        public Menus Menucontroller;
        public string prompt;
        public User CurrentUser;

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

        public List<string> ListBuildMenu() // makes a String list of all menus
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


        public List<String> DishListBuild(int MenuId) //given a menu id gives a string with all dishes from that menu
        {
            List<string> AllDishList = new List<string>();
            List<Dish> DishesofMenu = controller.AllDishesbyMenu(MenuId);
            foreach (Dish i in DishesofMenu)
            {
                AllDishList.Add($" [{i.Type}] {i.Gerecht} {i.Price},-");
            }
            AllDishList.Add("   Add    ");
            AllDishList.Add("   Back    ");
            return AllDishList;
        }

        public void Selection(Menu thismenu) // shows all dishes of a chosen menu
        {
            List<Dish> DishesOfMenu = controller.AllDishesbyMenu(thismenu.Id);
            string prompt1 = $"All dishes of the {thismenu.Name} Menu";
            List<string> options = DishListBuild(thismenu.Id);

            ArrowMenu OtherMenu = new ArrowMenu(prompt1, options, 0);
            int selectedIndex = OtherMenu.Move();


            if(options.Count > 2)
            {
                if (selectedIndex == DishesOfMenu.Count + 1)
                {
                    ShowAllMenus();

                }
                else if (selectedIndex == DishesOfMenu.Count)
                {
                    Add(thismenu);
                    Selection(thismenu);
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
                    ArrowMenu OtherMenu2 = new ArrowMenu(prompt, stringArray, 0);
                    int selectedIndex2 = OtherMenu2.Move();
                    if (selectedIndex2 == 0) // adjust dish
                    {
                        Adjust(DishesOfMenu[selectedIndex2].Id);
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
            else
            {
                if (selectedIndex == 1)
                {
                    ShowAllMenus();

                }
                else
                {
                    Add(thismenu);
                    Selection(thismenu);
                }
            }

        }

        public void AddMenu()
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

            if(Menucontroller._menus.Count == 0)
            {
                newMenu.Id = 0;
            }
            else
            {
                newMenu.Id = Menucontroller._menus[Menucontroller._menus.Count - 1].Id + 1 ;
            }
            

            //ask which month
            string[] options = ShowAvailableMonths();
            Console.Clear();
            ArrowMenu NewDate = new ArrowMenu("Choose in which month this menu will be present.", options, 0);
            int ind = NewDate.Move();
            DateTime X = new DateTime(2022, ind + 1, 15);
            newMenu.StartingDate = FirstDayOfMonth(X);
            newMenu.EndDate = newMenu.StartingDate.AddMonths(1).AddDays(-1);

            Menucontroller.UpdateList(newMenu);
            Console.Clear();
            Console.WriteLine("Menu succesfully added! Press any key to continue");
            Console.ReadKey();
        }

        public string[] ShowAvailableMonths()
        {
            string[] months = DateTimeFormatInfo.CurrentInfo.MonthNames;
            return months;
        }

        public DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }



        public void Adjust(int DishID) // allows an admin to adjust a dish or delete it
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
        public void Add(Menu CurrentMenu) //adds a dish to a chosen menu
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

        public string Type()
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
        public int Price()
        {
            Console.Clear();
            Console.WriteLine("What will be the new price? ");
            int newprice = Int32.Parse(Console.ReadLine());
            return newprice;
        }
        public string Name()
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
