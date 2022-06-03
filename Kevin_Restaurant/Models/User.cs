using Kevin_Restaurant.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kevin_Restaurant.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("admin")]
        public bool Admin { get; set; }

        [JsonPropertyName("telephonenumber")]
        public string TelephoneNumber { get; set; }

        public void Writetofile()
        {
            Users newuser = new Users();
            newuser.Updatelist(this);
        }
    }
}
