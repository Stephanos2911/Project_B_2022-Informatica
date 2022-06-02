using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Kevin_Restaurant.Controllers;

namespace Kevin_Restaurant.Models
{
    internal class Reservation
    {
        [JsonPropertyName("meals")]
        public List<string> meals { get; set; }

        [JsonPropertyName("diners")]
        public int Diners { get; set; }

        [JsonPropertyName("userid")]
        public int UserId{ get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("table")]
        public int Table { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }




        public void WriteToFile()
        {
            Reservations reservations = new Reservations();
            reservations.UpdateList(this);
        }
    }
}
