using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Kevin_Restaurant.Models;

namespace Kevin_Restaurant.Controllers
{
    internal class Reservations
    {
        public List<Reservation> _reservations;
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"Data/reservations.json"));
        public Reservations()
        {
            Load();
        }

        public void Load()
        {
            Console.WriteLine(path);
            string json = File.ReadAllText(path);

            _reservations = JsonSerializer.Deserialize<List<Reservation>>(json);
        }

        public void Write()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(_reservations, options);
            File.WriteAllText(path, json);
        }

        public void UpdateList(Reservation reservering)
        {
            int index = _reservations.FindIndex(s => s.Id == reservering.Id);

            if (index != -1)
            {
                _reservations[index] = reservering;
            }
            else
            {
                _reservations.Add(reservering);
            }
            Write();

        }

        public void DeleteReservation(Reservation reservering)
        {
            _reservations.Remove(reservering);
            Write();
        }



        public Reservation FindId(string id)
        {
            return _reservations.Find(i => i.Id == id);
        }


        public Reservation FindDate(DateTime date)
        {
            return _reservations.Find(i => i.Date == date);
        }

        public Reservation FindTime(string date)
        {
            return _reservations.Find(i => i.Time== date);
        }

        public Reservation FindUser(int Userid)
        {
            return _reservations.Find(i => i.UserId == Userid);
        }


        public int ChooseTable(int Groupsize)
        {
            Table_map A = new Table_map();
            DateTime DayForReservation = new DateTime(2020, 05, 05);
            List<Reservation> NotAvailableTables = FindAllAvailableTables(DayForReservation);
            foreach (Reservation index in NotAvailableTables)
            {
                A.Tables[index.Table - 1].available = false;
            }

            int indexchoice = A.Choice(Groupsize);
            return indexchoice;
        }

        public List<Reservation> FindAllAvailableTables(DateTime date)
        {
            return _reservations.FindAll((i => i.Date == date));
        }

        public Reservation make_reservation(Reservations reservations, int user_id)
        {
            Reservation res = new Reservation();

            res.Id = reservations.conformation_code();

            var date = get_date(14);
            res.Date = date;

            var time = get_time();
            res.Time = time;

            res.UserId = user_id;

            var diners = Reservations.diners();
            res.Diners = diners;

            OrderScreen order = new OrderScreen();
            res.meals = order.Start(diners);


            //Table_map x = new Table_map();
            //x.Show_Tables();
            //res.Table = x.Choice(diners);


            res.WriteToFile();

            return res;
        }
        public static DateTime get_date(int days_in_advance)
        {
            Console.Clear();
            DateTime today = DateTime.Today;

            var string_dates = new List<string>();
            var dates = new List<DateTime>();

            for (int i = 0; i < days_in_advance; i++)
            {
                if (today.AddDays(i).DayOfWeek != DayOfWeek.Tuesday)
                {
                    var string_date = $"{today.AddDays(i).DayOfWeek} {today.AddDays(i).ToString("M")}";
                    var date = today.AddDays(i);//.ToUniversalTime();

                    string_dates.Add(string_date);
                    dates.Add(date);
                }
            }
            string[] Strring_dates_array = string_dates.ToArray();
            DateTime[] dates_array = dates.ToArray();

            string promt = "the booking is for the entire day from 17:00 to 23:00 enjoy :);";


            ArrowMenu choose_date = new ArrowMenu(promt, Strring_dates_array, 1);
            int selectedIndex = choose_date.Move();

            return dates[selectedIndex];
        }
        public static int diners()
        {
            Console.Clear();
            Console.WriteLine("how many people are you planning to come?");
            var string_people = Console.ReadLine();

            while (!Int32.TryParse(string_people, out var date))
            {
                Console.WriteLine("WRONG......try again input must be an int");
                string_people = Console.ReadLine();
            }
            var people = Int32.Parse(string_people);

            return people;
        }

        public static string[] get_meals(int people)
        {
            var meals = new string[people + 1];
            for (int i = 1; i <= people; i++)
            {
                var promt = $"please select the dish for person{i}";
                string[] meal_options = { "meat", "fish", "vegatarian", "vegan" };
                ArrowMenu choose_date = new ArrowMenu(promt, meal_options, 0);
                int selectedIndex = choose_date.Move();
                meals[i] = meal_options[selectedIndex];

            }
            return meals;
        }
        public static string get_time()
        {
            Console.Clear();
            string promt = "what time do you expect you will be arriving?\n\n";
            string[] string_times = { "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45",
                "19:00", "19:15", "19:30", "19:45", "20:00", "20:15", "20:30", "20:45", "21:00", "21:15",
                "21:30", "21:45", "22:00", "22:15", "22:30", "22:45", "23:00" };

            ArrowMenu choose_date = new ArrowMenu(promt, string_times,0);
            int selectedIndex = choose_date.Move();

            Console.WriteLine(string_times[selectedIndex]);

            return string_times[selectedIndex];
        }

        public string conformation_code()
        {
            Random rnd = new Random();
            string code = string.Empty;
            string[] digits = { "1", "2", "3", "4","5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I",
                "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            for (int i = 0; i < 4; i++)
            {
                int index = rnd.Next(0, 35);
                var digit = digits[index];
                code += digit;
            }

            if (FindId(code) == null)
            {
                return code;
            }
            return conformation_code();

        }

        public bool filter_byday(DateTime date)
        {
            var avalible = false;

            if (FindDate(date) == null)
            {
                avalible = true;
            }
            return avalible;
        }

        public List<Reservation> FindAllReservations(User Currentuser)
        {
            return _reservations.FindAll((i => i.UserId == Currentuser.Id));
        }

        public List<string> DisplayAllReservations(List<Reservation> Reservationlist)
        {
            List<string> AllReservations = new List<string>();
            AllReservations.Add("Back");
            AllReservations.Add("Filter Options");
            foreach (Reservation reservation in Reservationlist)
            {
                AllReservations.Add($"{reservation.Id} | {reservation.Date.ToString("dddd, dd MMMM yyyy")} | {reservation.Time} | {reservation.Table} |");
            }
            return AllReservations;
        }
    }
}