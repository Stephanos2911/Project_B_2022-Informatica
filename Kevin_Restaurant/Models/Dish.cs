using Kevin_Restaurant.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kevin_Restaurant.Models
{
    public class Dish
    {
        [JsonPropertyName("theme")]
        public string Theme { get; set; }
        [JsonPropertyName("gerecht")]
        public string Gerecht { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("sort")]
        public string Sort { get; set; }

        public void WriteToFile()
        {
            Dishes variabel = new Dishes();
            variabel.UpdateList(this);
        }
    }
}
