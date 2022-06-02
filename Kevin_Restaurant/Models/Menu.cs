using Kevin_Restaurant.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kevin_Restaurant.Models
{
    public class Menu
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("start")]
        public DateTime StartingDate { get; set; }

        [JsonPropertyName("end")]
        public DateTime EndDate{ get; set; }

        public void WriteToFile()
        {
            Menus NewMenu = new Menus();
            NewMenu.UpdateList(this);
        }
    }
}
