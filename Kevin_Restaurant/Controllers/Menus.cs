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
    public class Menus
    {
        public List<Menu> _menus;
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data/Menu.json"));

        public Menus()
        {
            Load();
        }

        private void Load()
        {
            string json = File.ReadAllText(path);
            _menus = JsonSerializer.Deserialize<List<Menu>>(json);
        }


        public Menu GetById(int id)
        {
            return _menus.Find(x => x.Id == id);
        }

        public Menu GetByName(string name)
        {
            return _menus.Find(x => x.Name == name);
        }

        public void UpdateList(Menu m)
        {
            int index = _menus.FindIndex(s => s.Id == m.Id);

            if (index != -1)
            {
                _menus[index] = m;
            }
            else
            {
                _menus.Add(m);
            }
            Write();
        }

        private void Write()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(_menus);
            File.WriteAllText(path, json);
        }
    }
}
