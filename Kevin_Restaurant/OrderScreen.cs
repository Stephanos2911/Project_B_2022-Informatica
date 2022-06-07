using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    public class OrderScreen
    {
        public Dishes controller;
        public int bill;
        public string finalOrder;

        public Menu currentMenu;
        List<Dish> AllDishesInCurrentMenu;
        public int AppetizerCount;
        public int MainCount;
        public int DessertCount;

        public OrderScreen()
        {
            this.bill = 0;
            this.currentMenu = CurrentMenu();
            this.finalOrder = "";
            this.controller = new Dishes();
            this.AllDishesInCurrentMenu = controller.AllDishesbyMenu(currentMenu.Id);
            this.AppetizerCount = AllDishesInCurrentMenu.Count(x => x.Sort == "appetizer");
            this.MainCount = AllDishesInCurrentMenu.Count(x => x.Sort == "main course");
            this.DessertCount = AllDishesInCurrentMenu.Count(x => x.Sort == "Dessert");
        }

        private Menu CurrentMenu()
        {
            Menu CurrentMenu = new Menu();
            bool found = false;
            Menus X = new Menus();
            while (found == false)
            {
                foreach (Menu menu in X._menus)
                {
                    if (DateTime.Now > menu.StartingDate && DateTime.Now < menu.EndDate)
                    {
                        CurrentMenu = menu;
                        found = true;
                    }
                }
            }
            return CurrentMenu;
        }

        public List<string> alldishestostring()
        {
            Menu currentMenu = CurrentMenu();
            List<String> str = new List<string>();
            List<Dish> AllDishesInCurrentMenu = controller.AllDishesbyMenu(currentMenu.Id);

            str.Add("Appetizers");
            //puts all objects with an "appetizer" attribute in an array
            foreach (Dish i in AllDishesInCurrentMenu)
            {
                if (i.Sort == "appetizer")
                {
                    str.Add($"{i.Id}. [{i.Type}] {i.Gerecht} {i.Price},-");
                }
            }
            str.Add("Main course");
            foreach (Dish i in AllDishesInCurrentMenu)
            {
                if (i.Sort == "main course")
                {
                    str.Add($"{i.Id}. [{i.Type}] {i.Gerecht} {i.Price},-");
                }
            }
            str.Add("Desserts");
            foreach (Dish i in AllDishesInCurrentMenu)
            {
                if (i.Sort == "Dessert")
                {
                    str.Add($"{i.Id}. [{i.Type}] {i.Gerecht} {i.Price},-");
                }
            }
            str.Add("Done");
            return str;
        }

        public List<string> Start(int groupsize)
        {
            int usersleft = groupsize;
            int currentPerson = 1;
            string prompt;
            List<string> options = alldishestostring();
            List<string> ChosenDishes = new List<string>();


            //makes the program work until all users have selcted their dishes
            while (usersleft > 0)
            {
                prompt = $"This month's theme: {currentMenu.Name}\nCurrent person {currentPerson}\n";
                ArrowMenu OtherMenu = new ArrowMenu(prompt, options, 2);
                int selectedIndex = OtherMenu.Move();

                //if selectedIndex is equal to "appetizer", "main course" or "dessert" the program has to do nothing
                if (selectedIndex == AppetizerCount + 1 || selectedIndex == 0 || selectedIndex == (options.Count - (DessertCount + 2)))
                {
                    ;
                }
                //makes the "done" button work
                else if (selectedIndex == options.Count - 1)
                {
                    usersleft--;
                    currentPerson++;
                }
                //makes all buttons of the dishes work
                else
                {
                    int index = 0;
                    if (selectedIndex <= AppetizerCount + 1)
                    {
                        index = selectedIndex - 1;
                    }
                    else if (selectedIndex < options.Count - (DessertCount + 1) && selectedIndex > AppetizerCount + 1)
                    {
                        index = selectedIndex - 2;
                    }
                    else
                    {
                        index = selectedIndex - 3;
                    }
                    ChosenDishes.Add(AllDishesInCurrentMenu[index].Gerecht);
                    Bill(AllDishesInCurrentMenu[index].Price);
                    Order(AllDishesInCurrentMenu[index]);
                }
                //gives the final bill
                Console.Clear();
                Console.WriteLine($"You selected:\n{this.finalOrder}\n___________________________________________\n");
                Console.WriteLine($"Total: {this.bill},-");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.WriteLine("This is the End");
            return ChosenDishes;
        }


        //makes the final bill
        public void Bill(int inputPrice)
        {
            this.bill += inputPrice;
        }
        //makes the final order after all users have selected their order
        public void Order(Dish X)
        {
            this.finalOrder += $"{X.Gerecht}, {X.Price},-\n";
        }
    }
}