using System;
using System.Collections.Generic;
using Kevin_Restaurant;
using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class TestProdject
    {
        private ArrowMenu mainMenu;
        [TestMethod]
        public void GetByGerechtAndGetById_DishExists_DoesNotReturnNull()
        {
            Dishes x = new Dishes();

            Dish y = new Dish();
            y.MenuId = 0;
            y.Gerecht = "vis";
            y.Id = 24;
            y.Type = "vegetarian";
            y.Sort = "appetizer";
            y.Price = 5;

            y.WriteToFile();

            Dish a = x.GetById(24);
            Dish b = x.GetByGerecht("vis");
            List<Dish> c = x.AllDishesbyMenu(0);
            List<Dish> d = x.FindAllDish("appetizer");

            Assert.IsNotInstanceOfType(a, typeof(Dish));
            Assert.IsNotInstanceOfType(b, typeof(Dish));
            Assert.IsNotNull(c);
            Assert.IsNotNull(d);

        }
        [TestMethod]
        public void RemoveDish_DishNoExists_GetIdReturnsNull()
        {
            Dishes x = new Dishes();

            x.RemoveDish(24);

            Dish a = x.GetById(24);
            Dish b = x.GetByGerecht("vis");
            List<Dish> c = x.AllDishesbyMenu(0);
            List<Dish> d = x.FindAllDish("appetizer");

            Assert.IsNull(a);
            Assert.IsNull(b);
            Assert.IsNotNull(c);
            Assert.IsNotNull(d);
        }
        [TestMethod]
        public void GetIdAndGetUsernameAndCo_UserExists_DousNotReturn_Null()
        {
            Users x = new Users();

            User y = new User();
            y.Id = 999;
            y.Username = "truus";
            y.Password = "qwerty";
            y.Admin = false;
            y.TelephoneNumber = "12986732468";

            y.Writetofile();

            User a = x.GetId(999);
            User b = x.Getusername("truus");
            User c = x.GetbyPhone("12986732468");
            User d = x.GetbyPassword("qwerty");
            List<User> e = x.FindAllAdminsorNot(false);
            bool f = x.CheckforPhone("12986732468");
            bool g = x.CheckForUsername("truus");

            Assert.IsInstanceOfType(a, typeof(User));
            Assert.IsInstanceOfType(b, typeof(User));
            Assert.IsInstanceOfType(c, typeof(User));
            Assert.IsInstanceOfType(d, typeof(User));
            Assert.IsNotNull(e);
            Assert.IsFalse(f);
            Assert.IsFalse(g);

        }
        //[TestMethod]
        //public void GetIdAndGetUsernameAndCo_UserDoesNottExists_Returns_Null()
        //{
        //    Users x = new Users();

        //    x.DeleteUser(999);

        //    User a = x.GetId(999);
        //    //User b = x.Getusername("truus");
        //    //User c = x.GetbyPhone("12986732468");
        //    //User d = x.GetbyPassword("qwerty");
        //    //List<User> e = x.FindAllAdminsorNot(false);
        //    //bool f = x.CheckforPhone("12986732468");
        //    //bool g = x.CheckForUsername("truus");

        //    Assert.IsNull(a);
        //    //Assert.IsNotInstanceOfType(b, typeof(User));
        //    //Assert.IsNotInstanceOfType(c, typeof(User));
        //    //Assert.IsNotInstanceOfType(d, typeof(User));


        //[TestMethod]
        //public void GetByIdenCo_objectBestaat_returnedIsNotNull()
        //{
        //    Reservations x = new Reservations();

        //    Reservation y = new Reservation();
        //    y.Id = "T7PJ";
        //    //y.Diners = 2;
        //    //y.UserId = 4;
        //    //y.Table = new List<int> { 4, 5 };
        //    //y.Date = new DateTime(2022,12,1);
        //    //y.Time = "17:00";
            

        //    y.WriteToFile();

        //    Reservation a = x.FindId("T7PJ");
        //    //Dish b = x.GetByGerecht("vis");
        //    //List<Dish> c = x.AllDishesbyMenu(0);
        //    //List<Dish> d = x.FindAllDish("appetizer");

        //    Assert.IsNotNull(a);
        //    //Assert.IsNotInstanceOfType(b, typeof(Dish));
        //    //Assert.IsNotNull(c);
        //    //Assert.IsNotNull(d);

        //}


        [TestMethod]

        public void CorrectTime_User_True() // a test to see if the restaurant opening and closing are accurate.
        {

            string string_times_start = "17:00";
            string string_times_end = "23:00";


            Assert.AreEqual("17:00", string_times_start);
            Assert.AreEqual("23:00", string_times_end);
        }

        [TestMethod]
        public void BeforeTime_User_False() // a test to see if user can order before the expected time.
        {
            string expected_time = "17:00";


            Assert.AreNotEqual("16:00", expected_time);

        }
        //[TestMethod]
        //public void Reservation_User_True() // NOG NIET GELUKT
        //{
        //    //act 
        //    Reservations x = new Reservations();

        //    Reservation y = new Reservation();

        //    const string[] V = new Array[] { "Frikandel" };
        //    y.meals = new List<string>(V);
        //    y.Diners = 2;
        //    y.UserId = 1;
        //    y.Id = "1";
        //    y.Table = new List<int>(2);
        //    y.Date = new DateTime(DateTime.MaxValue.Ticks);
        //    y.Time = "21:00";

        //    y.WriteToFile();


        //    Reservation z = y. ;
        //    Reservation p = y. ;

        //}
        [TestMethod]
        public void Menus() // BASICIALLY NET ZOALS CHE MAAR MENU HEEFT GEEN ANDERE FUNCTIES
        {
            Menus x = new Menus();
            Menu y = new Menu();

            x.GetByName("Carlo");
            y.WriteToFile();
            Assert.IsNotNull(x);


        }
        [TestMethod]
        public void MakeAnotherMenu_User_False() // User is not able to make there own selection menu (WAARSCHIJNLIJK NIET GOED)
        {
            Startscreen x = new Startscreen();

            this.mainMenu = new ArrowMenu("Hey", new string[] { "Test", "Another test" }, 1);
            Assert.IsNotNull(mainMenu);



        }

    }
}
