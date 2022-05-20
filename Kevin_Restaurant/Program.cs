using Kevin_Restaurant.Controllers;
using System;

namespace Kevin_Restaurant
{

    internal class Program
    {
        public bool OnlyDigits(string str) // checkt of de string alleen getallen bevat
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return true;
            }

            return false;
        }
        static void Main(string[] args)
        {
            Startscreen beginscherm = new Startscreen();
            beginscherm.Show_StartingScreen();
            //Reservations test = new Reservations();
            //int A = test.ChooseTable(3);
        }
    }
}
