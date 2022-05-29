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
        public Dish CurrentDish;
        //public Dish _Dishes;
        public ChangeMenu()
        {
            this.controller = new Dishes();
            this.prompt = "Please select";
            this.CurrentDish = null;
        }
        public List<Dish> FindAllDish(string sort)
        {
            return controller._Dishes.FindAll(i => i.Sort == sort);
        }

        public string[] ArrayBuild()
        {
            int index = 0;
            string[] menustring = new string[controller._Dishes.Count+2];
            List<Dish> appetizers = FindAllDish("appetizer");
            List<Dish> Maindishes = FindAllDish("main course");
            List<Dish> desserts = FindAllDish("Dessert");
            foreach (Dish i in appetizers)
            {
                menustring[index] = $" [{i.Type}] {i.Gerecht} {i.Price},-";
                index++;
            }
            foreach (Dish i in Maindishes)
            {
                menustring[index] = $" [{i.Type}] {i.Gerecht} {i.Price},-";
                index++;
            }
            foreach (Dish i in desserts)
            {
                menustring[index] = $" [{i.Type}] {i.Gerecht} {i.Price},-";
                index++;
            }
            menustring[index] = "   Add    ";
            index++;
            menustring[index] = "   Back    ";
            return menustring;
        }

        public void Selection()
        {
            prompt = $"this month's theme: ";
            string[] options = ArrayBuild();

            ArrowMenu OtherMenu = new ArrowMenu(prompt, options, 0);
            int selectedIndex = OtherMenu.Move();
            if (selectedIndex == options.Length - 2)
            {
                Add();
            }
            else
            {
                Console.Clear();
                //string[] stringArray = new string[2];
                string[] stringArray= new string[] {
                "Adjust",
                "Remove"
                };
                //string[] options2 = stringArray;

                ArrowMenu OtherMenu2 = new ArrowMenu(prompt, stringArray, 0);
                int selectedIndex2 = OtherMenu2.Move();
                if (selectedIndex2 == 0)
                {
                    Adjust();
                }
                else
                {
                    //Remove();
                }
            }
        }
        
        public void Adjust()
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
                Type();
            }
            else if (selectedIndex == 1)
            {
                Name();
            }
            else
            {
                Price();
            }

        }
        public void Add()
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
                CurrentDish.Sort = "appetizer";
                CurrentDish.WriteToFile();
            }
            else if (selectedIndex == 1)
            {
                CurrentDish.Sort = "main course";
                CurrentDish.WriteToFile();
            }
            else
            {
                CurrentDish.Sort = "Dessert";
                CurrentDish.WriteToFile();
            }
            Console.Clear();
            Name();
            Type(options[index]);
            Price();
            Theme();
        }
        public void Remove(Dish dish)
        {
            //_Dishes.Remove(dish);
            //Write();
        }
        public void Type()
        {
            Console.Clear();
            Console.WriteLine("What will be the new type? ");
            string newtype = Console.ReadLine();
            
            CurrentDish.Type = newtype;
            CurrentDish.WriteToFile();
        }
        public void Price()
        {
            Console.Clear();
            Console.WriteLine("What will be the new price? ");
            int newprice = Int32.Parse(Console.ReadLine());

            CurrentDish.Price = newprice;
            CurrentDish.WriteToFile();
        }
        public void Name()
        {
            Console.Clear();
            Console.WriteLine("What will be the new name? ");
            string newname = Console.ReadLine();

            CurrentDish.Gerecht = newname;
            CurrentDish.WriteToFile();
        }
        public void Theme()
        {
            Console.Clear();
            Console.WriteLine("What will be the new theme? ");
            string newtheme = Console.ReadLine();

            CurrentDish.Theme = newtheme;
            CurrentDish.WriteToFile();
        }
    }
}
