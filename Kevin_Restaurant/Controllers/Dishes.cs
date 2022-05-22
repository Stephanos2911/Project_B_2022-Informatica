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
    internal class Dishes
    {
        private List<Dish> _gerechtenpt2;
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data/menu.json"));

        public Dishes()
        {
            Load();
        }

        public void Load()
        {
            string json = File.ReadAllText(path);
            _gerechtenpt2 = JsonSerializer.Deserialize<List<Dish>>(json);
        }

        public Dish GetByGerecht(string gerecht)
        {
            return _gerechtenpt2.Find(x => x.Gerecht == gerecht);
        }
        public Dish GetByNum(string num)
        {
            return _gerechtenpt2.Find(x => x.Num == num);
        }
        public Dish GetByPrice(int price)
        {
            return _gerechtenpt2.Find(x => x.Price == price);
        }
        public Dish GetByType(string type)
        {
            return _gerechtenpt2.Find(x => x.Type == type);
        }

        public void UpdateList(Dish m)
        {
            int index = _gerechtenpt2.FindIndex(s => s.Gerecht == m.Gerecht);
            if (index != -1)
            {
                _gerechtenpt2.Add(m);
            }
            else
            {
                _gerechtenpt2[index] = m;
            }
            Write();
        }

        public void Write()
        {
            string json = JsonSerializer.Serialize(_gerechtenpt2);
            File.WriteAllText(path, json);
            Console.WriteLine("done");
        }
    }
}
