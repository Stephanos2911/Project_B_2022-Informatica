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
    public class Users
    {
        public List<User> _users;
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"Data/userdatabase.json"));
        public Users()
        {
            Load();
        }

        private void Load()
        {
            string json = File.ReadAllText(path);

            _users = JsonSerializer.Deserialize<List<User>>(json);
        }

        private void Write()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(_users, options);
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

        public User GetbyPhone(string phonenumberinput)
        {
            return _users.Find(x => x.TelephoneNumber == phonenumberinput);
        }

        public User GetbyPassword(string inputpassword)
        {
            return _users.Find(x => x.Password == inputpassword);
        }

        public List<User> FindAllAdminsorNot(bool adminornot)
        {
            if(adminornot == true)
            {
                return _users.FindAll(i => i.Admin == true);
            }
            else
            {
                return _users.FindAll(x => x.Admin == false);
            }

        }

        public void Updatelist(User updateuser)
        {
            int index = _users.FindIndex(s => s.Id == updateuser.Id);

            if (index != -1)
            {
                _users[index] = updateuser;
            }
            else
            {
                _users.Add(updateuser);
            }
            Write();

        }

        public List<string> DisplayAllusers(List<User> Userlist)
        {
            List<string> AllUsers = new List<string>();
            foreach (User user in Userlist)
            {
                AllUsers.Add($"{user.Id} | {user.Username} | {user.Password} | {user.TelephoneNumber} | {user.Admin}");
            }
            AllUsers.Add("Filters");
            AllUsers.Add("Back");
            return AllUsers;
        }

        public void DeleteUser(int userid)
        {
            var itemToRemove = _users.Single(r => r.Id == userid);
            _users.Remove(itemToRemove);
            Write();
        }

        public bool CheckforPhone(string input) // checks if username/phone is already in use
        {
            bool check = true;
            if (input.Length > 15 || Startscreen.OnlyDigits(input) || input.Length < 9 || input == "")
            {
                Console.WriteLine("Please enter a valid phonenumber");
                check = false;

            }
            else
            {
                foreach (User x in _users)
                {
                    if (x.TelephoneNumber == input)
                    {
                        Console.WriteLine("This phone-number is already registered, try another number:");
                        check = false;

                    }
                }
            }
            return check;

        }

        public bool CheckForUsername(string input)
        {
            bool check = true;
            foreach (User x in _users)
            {
                if (x.Username == input)
                {
                    check = false;
                }
            }
            return check;
        }

    }
}
