using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    public class OrderScreen
    {
        Dishes gcontroller = new Dishes();
        Dish gerecht;
        Dish gerecht1;
        Dish gerecht2;
        Dish gerecht3;
        Dish gerecht4;
        Dish gerecht5;
        Dish gerecht6;
        Dish gerecht7;
        Dish gerecht8;
        Dish gerecht9;
        Dish gerecht10;
        Dish gerecht11;
        Dishes pcontroller = new Dishes();
        Dish price;
        Dish price1;
        Dish price2;
        Dish price3;
        Dish price4;
        Dish price5;
        Dish price6;
        Dish price7;
        Dish price8;
        Dish price9;
        Dish price10;
        Dish price11;
        public string num;
        public string num1;
        public string num2;
        public string num3;
        public string num4;
        public string num5;
        public string num6;
        public string num7;
        public string num8;
        public string num9;
        public string num10;
        public string num11;
        public int bill;
        public string finalOrder;

        public OrderScreen()
        {
            this.gerecht = gcontroller.GetByNum("VG1");
            this.gerecht1 = gcontroller.GetByNum("VG2");
            this.gerecht2 = gcontroller.GetByNum("VG3");
            this.gerecht3 = gcontroller.GetByNum("VG4");
            this.gerecht4 = gcontroller.GetByNum("HG1");
            this.gerecht5 = gcontroller.GetByNum("HG2");
            this.gerecht6 = gcontroller.GetByNum("HG3");
            this.gerecht7 = gcontroller.GetByNum("HG4");
            this.gerecht8 = gcontroller.GetByNum("NG1");
            this.gerecht9 = gcontroller.GetByNum("NG2");
            this.gerecht10 = gcontroller.GetByNum("NG3");
            this.gerecht11 = gcontroller.GetByNum("NG4");
            this.num = "VG1";
            this.num1 = "VG2";
            this.num2 = "VG3";
            this.num3 = "VG4";
            this.num4 = "HG1";
            this.num5 = "HG2";
            this.num6 = "HG3";
            this.num7 = "HG4";
            this.num8 = "NG1";
            this.num9 = "NG2";
            this.num10 = "NG3";
            this.num11 = "NG4";
            this.price = pcontroller.GetByNum("HG1");
            this.price1 = pcontroller.GetByNum("HG2");
            this.price2 = pcontroller.GetByNum("HG3");
            this.price3 = pcontroller.GetByNum("HG4");
            this.price4 = pcontroller.GetByNum("VG1");
            this.price5 = pcontroller.GetByNum("VG2");
            this.price6 = pcontroller.GetByNum("VG3");
            this.price7 = pcontroller.GetByNum("VG4");
            this.price8 = pcontroller.GetByNum("NG1");
            this.price9 = pcontroller.GetByNum("NG2");
            this.price10 = pcontroller.GetByNum("NG3");
            this.price11 = pcontroller.GetByNum("NG4");
            this.bill = 0;
            this.finalOrder = "";
        }

        public void Start()
        {
            string prompt = $" Theme this month: {gerecht.Theme}";

            string[] options = { $"Appetizer\n",
                    $"{this.num}. [{this.gerecht.Type}] { this.gerecht.Gerecht } {this.gerecht.Price},-" ,
                    $"{this.num1}. [{this.gerecht1.Type}] { this.gerecht1.Gerecht } {gerecht1.Price},- ",
                    $"{this.num2}. [{this.gerecht2.Type}] { this.gerecht2.Gerecht } {gerecht2.Price},- ",
                    $"{this.num3}. [{this.gerecht3.Type}] { this.gerecht3.Gerecht } {gerecht3.Price},- ",
                    $"\nMain Course\n",
                    $"{this.num4}. [{this.gerecht4.Type}] { this.gerecht4.Gerecht } {gerecht4.Price},- ",
                    $"{this.num5}. [{this.gerecht5.Type}] { this.gerecht5.Gerecht } {gerecht5.Price},- ",
                    $"{this.num6}. [{this.gerecht6.Type}] { this.gerecht6.Gerecht } {gerecht6.Price},- ",
                    $"{this.num7}. [{this.gerecht7.Type}] { this.gerecht7.Gerecht } {gerecht7.Price},- ",
                    $"\nDessert\n",
                    $"{this.num8}. [{this.gerecht8.Type}] { this.gerecht8.Gerecht } {gerecht8.Price},- ",
                    $"{this.num9}. [{this.gerecht9.Type}] { this.gerecht9.Gerecht } {gerecht9.Price},- ",
                    $"{this.num10}. [{this.gerecht10.Type}] { this.gerecht10.Gerecht } {gerecht10.Price},- ",
                    $"{this.num11}. [{this.gerecht11.Type}] { this.gerecht11.Gerecht } {gerecht11.Price},- ",
                    $"\n            Done             "};

            ArrowMenu OtherMenu = new ArrowMenu(prompt, options, 0);
            int selectedIndex = OtherMenu.Move();
            switch (selectedIndex)
            {
                case 0:
                    Start();
                    break;
                case 1:
                    Bill(gerecht.Price);
                    Order(gerecht.Gerecht);
                    Start();
                    break;
                case 2:
                    Bill(gerecht1.Price);
                    Order(gerecht1.Gerecht);
                    Start();
                    break;
                case 3:
                    Bill(gerecht2.Price);
                    Order(gerecht2.Gerecht);
                    Start();
                    break;
                case 4:
                    Bill(gerecht3.Price);
                    Order(gerecht3.Gerecht);
                    Start();
                    break;
                case 5:
                    Start();
                    break;
                case 6:
                    Bill(gerecht4.Price);
                    Order(gerecht4.Gerecht);
                    Start();
                    break;
                case 7:
                    Bill(gerecht5.Price);
                    Order(gerecht5.Gerecht);
                    Start();
                    break;
                case 8:
                    Bill(gerecht6.Price);
                    Order(gerecht6.Gerecht);
                    Start();
                    break;
                case 9:
                    Bill(gerecht7.Price);
                    Order(gerecht7.Gerecht);
                    Start();
                    break;
                case 10:
                    Start();
                    break;
                case 11:
                    Bill(gerecht8.Price);
                    Order(gerecht8.Gerecht);
                    Start();
                    break;
                case 12:
                    Bill(gerecht9.Price);
                    Order(gerecht9.Gerecht);
                    Start();
                    break;
                case 13:
                    Bill(gerecht10.Price);
                    Order(gerecht10.Gerecht);
                    Start();
                    break;
                case 14:
                    Bill(gerecht11.Price);
                    Order(gerecht11.Gerecht);
                    Start();
                    break;
                case 15:
                    Console.WriteLine($"Your bill is {this.bill},-");
                    Console.WriteLine("You have selected:");
                    Console.WriteLine(this.finalOrder);
                    break;
            }
        }
        public void Bill(int inputPrice)
        {
            this.bill += inputPrice;
        }

        public void Order(string order)
        {
            this.finalOrder = $"{this.finalOrder}\n{order}";
        }
    }
}
