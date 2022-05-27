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
    class Users
    {
        public List<User> _users;
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"Data/userdatabase.json"));
        public Users()
        {
            Load();
        }

        public void Load()
        {
            string json = File.ReadAllText(path);

            _users = JsonSerializer.Deserialize<List<User>>(json);
        }

        public void Write()
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
            AllUsers.Add("Back");
            AllUsers.Add("Filter Options");
            foreach (User user in Userlist)
            {
                AllUsers.Add($"{user.Id} | {user.Username} | {user.Password} | {user.TelephoneNumber} | {user.Admin}");
            }
            return AllUsers;
        }
    }
}
