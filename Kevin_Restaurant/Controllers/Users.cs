﻿using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kevin_Restaurant.Controllers
{
    internal class Users
    {
        private List<User> _users;
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"Data/userdatabase.json"));
        public Users()
        {
            Load();
        }

        public void Load()
        {
            string json = JsonSerializer.Serialize(_users);
            //Console.WriteLine(json);
            File.WriteAllText(path, json);
        }

        public void Write()
        {
            string json = JsonSerializer.Serialize(_users);
            File.WriteAllText(path, json);
        }

        public User GetId(int id)
        {
            return _users.Find(x => x.Id == id);
        }

        public User Getusername(string inputname)
        {
            return _users.Find(x => x.Username == inputname);
        }

        public void Updatelist(User updateuser)
        {
            int index = _users.FindIndex(s => s.Id == updateuser.Id);

            if (index != -1)
            {
                _users.Add(updateuser);
            }
            else
            {
                _users[index] = updateuser;
            }
            Write();

        }
    }
}
