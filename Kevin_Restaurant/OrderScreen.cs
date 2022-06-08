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
        private Dishes controller;
        private Menus MenuController;
        private int bill;
        private string finalOrder;

        private Menu currentMenu;
        private List<Dish> AllDishesInCurrentMenu;
        private int AppetizerCount;
        private int MainCount;
        private int DessertCount;

        public OrderScreen()
        {
            this.bill = 0;
            this.MenuController = new();
            this.currentMenu = MenuController.CurrentMenu();
            this.finalOrder = "";
            this.controller = new Dishes();
            this.AllDishesInCurrentMenu = controller.AllDishesbyMenu(currentMenu.Id);
            this.AppetizerCount = AllDishesInCurrentMenu.Count(x => x.Sort == "appetizer");
            this.MainCount = AllDishesInCurrentMenu.Count(x => x.Sort == "main course");
            this.DessertCount = AllDishesInCurrentMenu.Count(x => x.Sort == "Dessert");
        }

        public List<string> Start(int groupsize)
        {
            int usersleft = groupsize;
            int currentPerson = 1;
            string prompt;
            List<string> options = controller.alldishestostring(this.currentMenu);
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
        private void Bill(int inputPrice)
        {
            this.bill += inputPrice;
        }
        //makes the final order after all users have selected their order
        private void Order(Dish X)
        {
            this.finalOrder += $"{X.Gerecht}, {X.Price},-\n";
        }
    }
}