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
        //public Dish _Dishes;
        public ChangeMenu(User Currentuser)
        {
            this.controller = new Dishes();
            this.prompt = "Please select";
            this.AllDishes = controller._Dishes;
            this.NewDish = new Dish();
            this.CurrentUser = Currentuser;        }
        public List<Dish> FindAllDish(string sort)
        {
            return controller._Dishes.FindAll(i => i.Sort == sort);
        }

        public List<String> ListBuild()
        {
            List<string> AllDishList = new List<string>();
            foreach (Dish i in AllDishes)
            {
                AllDishList.Add($" [{i.Type}] {i.Gerecht} {i.Price},-");
            }
            AllDishList.Add("   Add    ");
            AllDishList.Add("   Back    ");
            return AllDishList;
        }

        public void Selection()
        {
            prompt = $"this month's theme: ";
            List<string> options = ListBuild();

            ArrowMenu OtherMenu = new ArrowMenu(prompt, options, 0);
            int selectedIndex = OtherMenu.Move();
            if (selectedIndex == AllDishes.Count + 1)
            {
                Mainmenu A = new Mainmenu(CurrentUser);
                A.StartMainMenu();
             
            }
            else if(selectedIndex == AllDishes.Count)
            {
                Add(AllDishes.Count);
            }
            else
            {
                Console.Clear();
                string[] stringArray= new string[] {
                "Adjust",
                "Remove"
                };
                //string[] options2 = stringArray;

                ArrowMenu OtherMenu2 = new ArrowMenu(prompt, stringArray, 0);
                int selectedIndex2 = OtherMenu2.Move();
                if (selectedIndex2 == 0)
                {
                    Adjust(AllDishes[selectedIndex].Id);
                }
                else
                {
                    controller.RemoveDish(AllDishes[selectedIndex].Id);
                    Selection();    
                }
            }
        }
        
        public void Adjust(int DishID)
        {
            Console.Clear();
            string[] adjustMenu = new string[]
            {
                "Type",
                "Name",
                "Price"
            };
            ArrowMenu OtherMenu = new ArrowMenu(prompt, adjustMenu, 0);
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
        public void Add(int IdNewDish)
        {
            Console.Clear();

            string prompt2 = "Please select the dish type.";
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
            Theme(IdNewDish, "Add");
            NewDish.WriteToFile();
        }

        public void Type(int DishId, string AddorChange)
        {
            Console.Clear();
            Console.WriteLine("What will be the new type? ");
            string newtype = Console.ReadLine();


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

            if(AddorChange == "Add")
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
                AllDishes[DishId].WriteToFile();

            }
        }
    }
}
