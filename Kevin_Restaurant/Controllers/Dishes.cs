using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kevin_Restaurant.Controllers
{
    public class Dishes
    {
        public List<Dish> _Dishes;
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data/Dishes.json"));

        public Dishes()
        {
            Load();
        }

        private void Load()
        {
            string json = File.ReadAllText(path);
            _Dishes = JsonSerializer.Deserialize<List<Dish>>(json);
        }

        public Dish GetByGerecht(string gerecht)
        {
            return _Dishes.Find(x => x.Gerecht == gerecht);
        }
        public Dish GetById(int id)
        {
            return _Dishes.Find(x => x.Id == id);
        }

        public List<Dish> AllDishesbyMenu(int Searchmenuid)
        {
            return _Dishes.FindAll(i => i.MenuId == Searchmenuid);
        }

        public List<Dish> FindAllDish(string sort)
        {
            return _Dishes.FindAll(i => i.Sort == sort);
        }

        public void DeleteAllDishesofMenu(int MenuID)
        {
            List<Dish> dishestodelete = AllDishesbyMenu(MenuID);
            foreach(Dish dish in dishestodelete)
            {
                RemoveDish(dish.Id);
            }
        }

        public void UpdateList(Dish m)
        {
            int index = _Dishes.FindIndex(s => s.Id == m.Id);

            if (index != -1)
            {
                _Dishes[index] = m;
            }
            else
            {
                _Dishes.Add(m);
            }
            Write();

        }

        public void RemoveDish(int id)
        {
            var itemToRemove = _Dishes.Single(r => r.Id == id);
            _Dishes.Remove(itemToRemove);
            Write();
        }

        public List<string> alldishestostring(Menu CurrentMenu)
        {
            Menu currentMenu = new();
            List<String> str = new List<string>();
            List<Dish> AllDishesInCurrentMenu = AllDishesbyMenu(currentMenu.Id);

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

        private void Write()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(_Dishes);
            File.WriteAllText(path, json);
        }
    }
}
