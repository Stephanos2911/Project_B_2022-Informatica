using System.Collections.Generic;
using Kevin_Restaurant.Controllers;
using Kevin_Restaurant.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class TestProdject
    {
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
            y.Id = 9;
            y.Username = "truus";
            y.Password = "qwerty";
            y.Admin = false;
            y.TelephoneNumber = "12986732468";

            y.Writetofile();

            User a = x.GetId(9);
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
        [TestMethod]
        public void GetIdAndGetUsernameAndCo_UserDoesNottExists_Returns_Null()
        {
            Users x = new Users();

            x.DeleteUser(9);

            User a = x.GetId(9);
            ////    User b = x.Getusername("truus");
            ////    User c = x.GetbyPhone("12986732468");
            ////    User d = x.GetbyPassword("qwerty");
            ////    List<User> e = x.FindAllAdminsorNot(false);
            ////    bool f = x.CheckforPhone("12986732468");
            ////    bool g = x.CheckForUsername("truus");

            Assert.IsNull(a);
            //    Assert.IsNotInstanceOfType(b, typeof(User));
            //    Assert.IsNotInstanceOfType(c, typeof(User));
            //    Assert.IsNotInstanceOfType(d, typeof(User));


        }

    }
}
