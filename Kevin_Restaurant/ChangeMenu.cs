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
        public string prompt;
        List<Dish> AllDishes;
        public Dish NewDish;
        User CurrentUser;
        List<string> AllDishList;

        public ChangeMenu(User CurrentUser)
        {
            this.controller = new Dishes();
            this.prompt = "Please select an option";
            this.AllDishes = controller._Dishes;
            this.NewDish = new Dish();
            this.CurrentUser = CurrentUser;
            AllDishList = new List<string>();
        }
        public List<Dish> FindAllDish(string sort)
        {
            return controller._Dishes.FindAll(i => i.Sort == sort);
        }

        public List<String> ListBuild()
        {
            List<string> AllDishList = new List<string>();
            foreach (Dish i in AllDishes)
            {
                AllDishList.Add($" -{i.Theme}- [{i.Type}] {i.Gerecht} {i.Price},-");
            }
            AllDishList.Add("   Add    ");
            AllDishList.Add("   Filter    ");
            AllDishList.Add("   Back    ");
            return AllDishList;
        }

        public void Selection()
        {
            List<string> options = ListBuild();

            ArrowMenu OtherMenu = new ArrowMenu(prompt, options, 0);
  
            int selectedIndex = OtherMenu.Move();
            if (selectedIndex == AllDishes.Count + 1)
            {
                //Filter();
            }
            else if (selectedIndex == AllDishes.Count + 2)
            {
                Mainmenu A = new Mainmenu(CurrentUser);
                A.StartMainMenu();
            }
            else if (selectedIndex == AllDishes.Count)
            {
                Add(AllDishes.Count);
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
                }
                else if (selectedIndex2 == 1)
                {
                    controller.RemoveDish(AllDishes[selectedIndex].Id);
                    this.prompt = "Removal succesfull";
                    Selection();                 
                }
                else
                {
                    Selection();
                }
            }
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
                Type(dish, "change");
            }
            else if (selectedIndex == 1)
            {
                Name(dish, "change");
            }
            else
            {
                Price(DishID, "change");
            }
        }
        public void Add(int IdNewDish)
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
            Name(NewDish, "Add");
            Type(NewDish, "Add");
            Price(IdNewDish, "Add");
            Theme(IdNewDish, "Add");
            NewDish.Id = AllDishes[AllDishes.Count - 1].Id + 1;
            //Id(controller._Dishes.Count);
            controller.UpdateList(NewDish);
            //NewDish.WriteToFile();
        }
        public void Name(Dish DishtoAdjust, string AddorChange)
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
                DishtoAdjust.Gerecht = newname;
                controller.UpdateList(DishtoAdjust);
                //AllDishes[DishId].Gerecht = newname;
                //AllDishes[DishId].WriteToFile();
            }
        }
        public void Type(Dish DishtoAdjust, string AddorChange)
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
                DishtoAdjust.Type = newtype;
                controller.UpdateList(DishtoAdjust);
            }
        }
        public void Price(int DishId, string AddorChange)
        {
            Console.Clear();
            Console.WriteLine("What will be the new price? ");
            int newprice = Int32.Parse(Console.ReadLine());
            //while (!int.TryParse(Console.ReadLine(), out newprice))
            //{
            //    Console.WriteLine("That was invalid. Enter a valid Grid Size.");
            //}

            if (AddorChange == "Add")
            {
                NewDish.Price = newprice;
            }
            else
            {
                AllDishes[DishId].Price = newprice;
                //controller.UpdateList(DishtoAdjust);
                //controller.UpdateList(AllDishes[DishId].Price);
                AllDishes[DishId].WriteToFile();
            }
        }

        public void Theme(int DishId, string AddorChange)
        {
            Console.Clear();
            Console.WriteLine("What will be the new theme? ");
            string newtheme = Console.ReadLine();


            if (AddorChange == "Add")
            {
                NewDish.Theme = newtheme;
            }
            else
            {
                AllDishes[DishId].Theme = newtheme;
                //AllDishes[DishId].WriteToFile();
            }
        }
    }
}
