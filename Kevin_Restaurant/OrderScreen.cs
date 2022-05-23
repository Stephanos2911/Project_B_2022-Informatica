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
        public int totallength;
        public Dish[] AllDishes;
        public int AppetizerCount;
        public int MainCourseCount;
        public int DessertCount;
        //public int currentPerson;
        public OrderScreen()
        {
            this.bill = 0;
            this.finalOrder = "";
            this.controller = new Dishes();
            this.totallength = controller._Dishes.Count;
            this.AppetizerCount = FindAllDish("appetizer").Count();
            this.MainCourseCount = FindAllDish("main course").Count();
            this.DessertCount = FindAllDish("Dessert").Count();

        }


        public string [] alldishestostring()
        {
            int index = 0;
            string [] str = new string[controller._Dishes.Count + 4];
            List<Dish> appetizers= FindAllDish("appetizer");
            List<Dish> Maindishes= FindAllDish("main course");
            List<Dish> desserts= FindAllDish("Dessert");
            str[index] = "Appetizers";    
            index++;  
            foreach(Dish i in appetizers)
            {
                str[index] = $"{i.Id}. [{i.Type}] {i.Gerecht} {i.Price},-";
                index++;
            }
            str[index] = "Main course";
            index++; 
            foreach (Dish i in Maindishes)
            {
                str[index] = $"{i.Id}. [{i.Type}] {i.Gerecht} {i.Price},-";
                index++;
            }
            str[index] = "Desserts";
            index++; 
            foreach (Dish i in desserts)
            {
                str[index] = $"{i.Id}. [{i.Type}] {i.Gerecht} {i.Price},-";
                index++;
            }
            str[index] = "Done";
            return str;
        }

        public List<Dish> FindAllDish(string sort)
        {
            return controller._Dishes.FindAll(i => i.Sort == sort);
        }

        public void Start(int groupsize)
        {
            int usersleft = groupsize;
            int currentPerson = 1;
            string prompt = $"Current person {currentPerson}";
            string [] options = alldishestostring();

            while (usersleft > 0)
            {
                prompt = $"Current person {currentPerson}";
                ArrowMenu OtherMenu = new ArrowMenu(prompt, options, 0);
                int selectedIndex = OtherMenu.Move();
            
                if (selectedIndex == AppetizerCount+1 || selectedIndex == 0 || selectedIndex == (options.Length - (DessertCount +2)))
                {
                    ;
                }
                else if (selectedIndex == options.Length - 1)
                {
                    usersleft--;
                    currentPerson++;
                }
                else 
                {
                    int index= 0 ;
                    if (selectedIndex <= AppetizerCount+1)
                    {
                        index = selectedIndex -1;
                    }
                    else if(selectedIndex < options.Length - (DessertCount+1) && selectedIndex > AppetizerCount+1)
                    {
                        index = selectedIndex -2;
                    }
                    else
                    {
                        index = selectedIndex -3;
                    }
                    Bill(controller.GetById(index).Price);
                    Order(index);
                }
                Console.Clear();
                Console.WriteLine($"You selected:\n{this.finalOrder}\n___________________________________________\n");
                Console.WriteLine($"Total: {this.bill},-");          
            }
        }
        public void Bill(int inputPrice)
        {
            this.bill += inputPrice;
        }

        public void Order(int i)
        {
            this.finalOrder += $"{controller._Dishes[i].Gerecht}, {controller._Dishes[i].Price},-\n";
        }
    }
}
