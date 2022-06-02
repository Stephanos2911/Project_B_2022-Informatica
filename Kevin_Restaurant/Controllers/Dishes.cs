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

        public void Load()
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
        public Dish GetByPrice(int price)
        {
            return _Dishes.Find(x => x.Price == price);
        }
        public Dish GetByType(string type)
        {
            return _Dishes.Find(x => x.Type == type);
        }
        public Dish GetBySort(string sort)
        {
            return _Dishes.Find(x => x.Sort == sort);
        }

        public List<Dish> AllDishesbyMenu(int Searchmenuid)
        {
            return _Dishes.FindAll(i => i.MenuId == Searchmenuid);
        }

        public List<Dish> FindAllDish(string sort)
        {
            return _Dishes.FindAll(i => i.Sort == sort);
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

        public void RemoveDish(int dishid)
        {
            var itemToRemove = _Dishes.Single(r => r.Id == dishid);
            _Dishes.Remove(itemToRemove);
            Write();
        }

        public void Write()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(_Dishes);
            File.WriteAllText(path, json);
        }
    }
}
