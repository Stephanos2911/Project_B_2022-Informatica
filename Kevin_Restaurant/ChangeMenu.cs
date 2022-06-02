using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
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
        List<Dish> AllDishes;
        public Dish NewDish;
        User CurrentUser;
        List<string> AllDishList;

        public ChangeMenu(User CurrentUser)
        {
            this.controller = new Dishes();
            this.Menucontroller = new Menus();
            this.prompt = "Please select";
            this.AllDishes = controller._Dishes;
            this.NewDish = new Dish();
            this.CurrentUser = CurrentUser;
            AllDishList = new List<string>();
        }
        public List<Dish> FindAllDish(string sort)
        {
            return controller._Dishes.FindAll(i => i.Sort == sort);
        }///?????????????????????????????????????????????????????????

        public void ShowAllMenus()
        {
            List<string> options = ListBuildMenu();
            ArrowMenu OtherMenu = new ArrowMenu("Overview of All Menu's", options, 0);
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

        public List<string> ListBuildMenu()
        {
            List<string> AllMenuList = new List<string>();
            List<Menu> AllMenus = Menucontroller._menus;
            foreach (Menu i in AllMenus)
            {
                AllMenuList.Add($" [{i.Name}] -  {i.Id},-");
            }
            AllDishList.Add("   Add Menu   ");
            AllDishList.Add("   Back    ");
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

        public void Selection(Menu thismenu)
        {
            string prompt1 = $"All dishes of the {thismenu.Name} Menu";
            List<string> options = DishListBuild(thismenu.Id);

            ArrowMenu OtherMenu = new ArrowMenu(prompt1, options, 0);
            int selectedIndex = OtherMenu.Move();


            if(options.Count > 2)
            {
                if (selectedIndex == AllDishes.Count + 1)
                {
                    ShowAllMenus();

                }
                else if (selectedIndex == AllDishes.Count)
                {
                    Add(AllDishes.Count, thismenu);
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
                    if (selectedIndex2 == 0)
                    {
                        Adjust(AllDishes[selectedIndex].Id);
                        Selection(thismenu);
                    }
                    else if (selectedIndex2 == 1)
                    {
                        controller.RemoveDish(AllDishes[selectedIndex].Id);
                        Selection(thismenu);
                    }
                    else
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
                    Add(AllDishes.Count, thismenu);
                }
            }

        }

        public void AddMenu()
        {

        }



        public void Adjust(int DishID)
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
                Type(DishID, "change");
            }
            else if (selectedIndex == 1)
            {
                Name(DishID, "change");
            }
            else
            {
                Price(DishID, "change");
            }

        }
        public void Add(int IdNewDish, Menu CurrentMenu)
        {
            Console.Clear();
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
            Name(IdNewDish, "Add");
            Type(IdNewDish, "Add");
            Price(IdNewDish, "Add");
            NewDish.MenuId = CurrentMenu.Id;
            Id(IdNewDish);
            NewDish.WriteToFile();
        }

        public void Type(int DishId, string AddorChange)
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

            if (AddorChange == "Add")
            {

                NewDish.Type = newtype;
            }
            else
            {
                AllDishes[DishId].Type = newtype;
                AllDishes[DishId].WriteToFile();
            }
        }
        public void Price(int DishId, string AddorChange)
        {
            Console.Clear();
            Console.WriteLine("What will be the new price? ");
            int newprice = Int32.Parse(Console.ReadLine());

            if (AddorChange == "Add")
            {
                NewDish.Price = newprice;
            }
            else
            {
                AllDishes[DishId].Price = newprice;
                AllDishes[DishId].WriteToFile();
            }
        }
        public void Name(int DishId, string AddorChange)
        {
            Console.Clear();
            Console.WriteLine("What will be the new name? ");
            string newname = Console.ReadLine();


            if (AddorChange == "Add")
            {
                NewDish.Gerecht = newname;
            }
            else
            {
                AllDishes[DishId].Gerecht = newname;
                AllDishes[DishId].WriteToFile();
            }
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
        public void Id(int DishId)
        {
            int newid = AllDishList.Count+1;
            NewDish.Id = newid;
        }
    }
}
