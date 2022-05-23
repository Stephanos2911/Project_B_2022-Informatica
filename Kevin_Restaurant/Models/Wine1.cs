using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using wijn_en_courses.Controllers;

namespace wijn_en_courses.Models
{
    internal class Wine1
    {
        [JsonPropertyName("wine_name")]
        public string Wine_name {  get; set; }
        [JsonPropertyName("wine_id")]
        public int Wine_id { get; set; }
        [JsonPropertyName("wine_price")]
        public double Wine_price { get; set; }
        [JsonPropertyName("wine_type")]
        public string Wine_type { get; set;}

        public void WriteToFile()
        {
            AllWine c = new AllWine();
            c.UpdateList(this);
        }
    }
}
