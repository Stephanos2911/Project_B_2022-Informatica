using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using wijn_en_courses.Models;

namespace wijn_en_courses.Controllers
{
    internal class AllWine
    {
        private List<Wine1> _allwine;
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data/wine.json"));

        public AllWine()
        {
            Load();
        }

        public void Load()
        {
            
            string json = File.ReadAllText(path);
            _allwine = JsonSerializer.Deserialize<List<Wine1>>(json);
        }

        public Wine1 GetById(int id)
        {
            return _allwine.Find(x => x.Wine_id == id);
        }
        public Wine1 GetByName(string name)
        {
            return _allwine.Find(x => x.Wine_name == name);
        }

        public void UpdateList(Wine1 m)
        {
            int index = _allwine.FindIndex(s => s.Wine_id == m.Wine_id);
            if (index == -1)
            {
                _allwine.Add(m);
            }
            else
            {
                _allwine[index] = m;
            }
            Write();
        }

        public void Write()
        {
            string json = JsonSerializer.Serialize(_allwine);

            File.WriteAllText(path, json);
            Console.WriteLine("Write done");

        }
    }
}
