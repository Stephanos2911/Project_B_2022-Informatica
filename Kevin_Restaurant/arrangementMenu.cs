using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wijn_en_courses.Controllers;
using wijn_en_courses.Models;

namespace Kevin_Restaurant
{
    internal class arrangementMenu
    {
        List<String> coursecart = new List<string>();
        AllWine wineController = new AllWine();

        public string[] courseOptions;
        public string[] appOptions;
        public string[] entreeOptions;
        public string[] wineChoice;
        public string[] wineOptions;
        public string[] soupOptions;
        public string[] dessertOptions;

        public int courseBill;
        public bool wineArrangement;
        public string shopping_Cart;

       

        Wine1 wine1;
        Wine1 wine2;
        Wine1 wine3;
        Wine1 wine4;
        Wine1 wine5;

        public ArrowMenu beginningScreen;
        public ArrowMenu appScreen;
        public ArrowMenu entreeScreen;
        public ArrowMenu wineChoiceScreen;
        public ArrowMenu wineMenuScreen;
        public ArrowMenu dessertScreen;
        public ArrowMenu soupScreen;


        public arrangementMenu()
        {
            //wine
            this.wine1 = wineController.GetById(1);
            this.wine2 = wineController.GetById(2);
            this.wine3 = wineController.GetById(3);
            this.wine4 = wineController.GetById(4);
            this.wine5 = wineController.GetById(5);

            this.courseBill = 0;
            this.wineArrangement = false;
            this.shopping_Cart = "";
       

            //options
            this.soupOptions = new string[3]
            {
                "soup1","soup2","soup3"
            };
            this.dessertOptions = new string[4]
            {
                "dessert1","desser2","dessert3","dessert4"
            };
            this.wineOptions = new string[5]
            {
                $"{this.wine1.Wine_name}",$"{this.wine2.Wine_name}",$"{this.wine3.Wine_name}",$"{this.wine4.Wine_name}",$"{this.wine5.Wine_name}"
            };
            this.wineChoice = new string[2]
            {
                "Yes", "No"
            };
            this.appOptions = new string[3]
            {
                "app1","app2","app3"
            };
            this.courseOptions = new string[3]
            {
                "Two course dinner (30,-)", "Three course dinner (40,-)", "Four course dinner (55,-)"
            };
            this.entreeOptions = new string[4]
            {
                "entree1", "entree2", "entree3", "entree4"
            };
            //menus
            this.beginningScreen = new ArrowMenu("Please pick out of these options:", courseOptions, 0);
            this.appScreen = new ArrowMenu("Please pick your appetizer:", appOptions, 0);
            this.entreeScreen = new ArrowMenu("Please pick your entree:", entreeOptions, 0);
            this.wineChoiceScreen = new ArrowMenu("Would you like a wine arrangement for an extra 15,-?", wineChoice, 0);
            this.wineMenuScreen = new ArrowMenu("Please pick your wine:", wineOptions, 0);
            this.dessertScreen = new ArrowMenu("Please pick your dessert:", dessertOptions, 0);
            this.soupScreen = new ArrowMenu("Please pick your soup:", soupOptions, 0);
        }
        public void Start()
        {
            int selectedIndex = this.beginningScreen.Move();
            switch (selectedIndex)
            {
                case 0:
                    this.courseBill += 30;
                    this.OptionOne();
                    break;
                case 1:
                    this.courseBill += 40;
                    this.OptionTwo();
                    break;
                case 2:
                    this.courseBill += 55;
                    this.OptionThree();
                    break;
            }

            
        }
        public void OptionOne()
        {

            int app_selectedIndex = this.appScreen.Move();
            int entree_selectedIndex = this.entreeScreen.Move();
            int winechoice_selectedIndex = this.wineChoiceScreen.Move();
            int winemenu_selectedIndex = this.wineMenuScreen.Move();
            switch (app_selectedIndex)
            {
                case 0:
                    Cart("app1");

                    break;
                case 1:
                    Cart("app2");
                    break;
                case 2:
                    Cart("app3");
                    break;

            }
            switch (entree_selectedIndex)
            {
                case 0:
                    Cart("entree1");
                    break;
                case 1:
                    Cart("entree2");
                    break;
                case 2:
                    Cart("entree3");
                    break;
                case 3:
                    Cart("entree4");
                    break;
            }
            switch (winechoice_selectedIndex)
            {
                case 0:
                    this.courseBill += 15;
                    this.wineArrangement = true;
                    break;
                case 1:
                    this.wineArrangement = false;
                    break;

            }
            if (this.wineArrangement == false) {
                Console.Clear();
                Console.WriteLine("Your final bill is " + courseBill + ",-");
                Console.WriteLine("Your shopping cart contains: ");
                Console.WriteLine(this.shopping_Cart);
            }
            else
            {
                switch(winechoice_selectedIndex)
                {
                    case 0:
                        Cart(wine1.Wine_name);
                        break;
                    case 1:
                        Cart(wine2.Wine_name);
                        break;
                    case 2:
                        Cart(wine3.Wine_name);
                        break;
                    case 3:
                        Cart(wine4.Wine_name);
                        break;
                    case 4:
                        Cart(wine5.Wine_name);
                        break;
                }
                
            }
            Console.Clear();
            Console.WriteLine("Your final bill is " + courseBill + ",-");
            Console.WriteLine("Your shopping cart contains: ");
            Console.WriteLine(this.shopping_Cart);

        }
        public void OptionTwo()
        {
            int app_selectedIndex = this.appScreen.Move();
            int entree_selectedIndex = this.entreeScreen.Move();
            int dessertmenu_selectedIndex = this.dessertScreen.Move();
            int winechoice_selectedIndex = this.wineChoiceScreen.Move();
            int winemenu_selectedIndex = this.wineMenuScreen.Move();
            
            switch (app_selectedIndex)
            {
                case 0:
                    Cart("app1");

                    break;
                case 1:
                    Cart("app2");
                    break;
                case 2:
                    Cart("app3");
                    break;

            }
            switch (entree_selectedIndex)
            {
                case 0:
                    Cart("entree1");
                    break;
                case 1:
                    Cart("entree2");
                    break;
                case 2:
                    Cart("entree3");
                    break;
                case 3:
                    Cart("entree4");
                    break;
            }
            switch (dessertmenu_selectedIndex)
            {
                case 0:
                    Cart("dessert1");
                    break;
                case 1:
                    Cart("dessert2");
                    break;
                case 2:
                    Cart("dessert3");
                    break;
                case 3:
                    Cart("dessert4");
                    break;
            }
            switch (winechoice_selectedIndex)
            {
                case 0:
                    this.courseBill += 15;
                    this.wineArrangement = true;
                    break;
                case 1:
                    this.wineArrangement = false;
                    break;

            }
            if (this.wineArrangement == false)
            {
                Console.Clear();
                Console.WriteLine("Your final bill is " + courseBill + ",-");
                Console.WriteLine("Your shopping cart contains: ");
                Console.WriteLine(this.shopping_Cart);
            }
            else
            {
                switch (winechoice_selectedIndex)
                {
                    case 0:
                        Cart(wine1.Wine_name);
                        break;
                    case 1:
                        Cart(wine2.Wine_name);
                        break;
                    case 2:
                        Cart(wine3.Wine_name);
                        break;
                    case 3:
                        Cart(wine4.Wine_name);
                        break;
                    case 4:
                        Cart(wine5.Wine_name);
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Your final bill is " + courseBill + ",-");
            Console.WriteLine("Your shopping cart contains: ");
            Console.WriteLine(this.shopping_Cart);
        }

        public void OptionThree()
        {
            int soupmenu_selectedIndex = this.soupScreen.Move();
            int app_selectedIndex = this.appScreen.Move();
            int entree_selectedIndex = this.entreeScreen.Move();
            int dessertmenu_selectedIndex = this.dessertScreen.Move();
            int winechoice_selectedIndex = this.wineChoiceScreen.Move();
            int winemenu_selectedIndex = this.wineMenuScreen.Move();
           
         
            switch (soupmenu_selectedIndex)
            {
                case 0:
                    Cart("soup1");
                    break;
                case 1:
                    Cart("soup1");
                    break;
                case 2:
                    Cart("soup1");
                    break;
            }
            switch (app_selectedIndex)
            {
                case 0:
                    Cart("app1");

                    break;
                case 1:
                    Cart("app2");
                    break;
                case 2:
                    Cart("app3");
                    break;

            }
            switch (entree_selectedIndex)
            {
                case 0:
                    Cart("entree1");
                    break;
                case 1:
                    Cart("entree2");
                    break;
                case 2:
                    Cart("entree3");
                    break;
                case 3:
                    Cart("entree4");
                    break;
            }
            switch (dessertmenu_selectedIndex)
            {
                case 0:
                    Cart("dessert1");
                    break;
                case 1:
                    Cart("dessert2");
                    break;
                case 2:
                    Cart("dessert3");
                    break;
                case 3:
                    Cart("dessert4");
                    break;
            }
            switch (winechoice_selectedIndex)
            {
                case 0:
                    this.courseBill += 15;
                    this.wineArrangement = true;
                    break;
                case 1:
                    this.wineArrangement = false;
                    break;

            }
            if (this.wineArrangement == false)
            {
                Console.Clear();
                Console.WriteLine("Your final bill is " + courseBill + ",-");
                Console.WriteLine("Your shopping cart contains: ");
                Console.WriteLine(this.shopping_Cart);
            }
            else
            {
                switch (winechoice_selectedIndex)
                {
                    case 0:
                        Cart(wine1.Wine_name);
                        break;
                    case 1:
                        Cart(wine2.Wine_name);
                        break;
                    case 2:
                        Cart(wine3.Wine_name);
                        break;
                    case 3:
                        Cart(wine4.Wine_name);
                        break;
                    case 4:
                        Cart(wine5.Wine_name);
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("Your final bill is " + courseBill + ",-");
            Console.WriteLine("Your shopping cart contains: ");
            Console.WriteLine(this.shopping_Cart);
        } 
        public void Cart(string item)
        {
            this.shopping_Cart = $"{this.shopping_Cart}\n{item}";
        }

    }
}
