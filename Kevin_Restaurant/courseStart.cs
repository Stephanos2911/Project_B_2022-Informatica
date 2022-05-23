using System;
using System.Collections.Generic;
using wijn_en_courses.Controllers;
using wijn_en_courses.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    //public class Coursedish
    //{
    //    public string dish_name;
    //    public int dish_id;
    //    public double dish_price;
    //    public string dish_description;
    //    public string dish_type;

    //    public Coursedish(string dish_name, int dish_id, double dish_price, string dish_description, string dish_type)
    //    {
    //        this.dish_name = dish_name;
    //        this.dish_id = dish_id;
    //        this.dish_price = dish_price;
    //        this.dish_description = dish_description;
    //        this.dish_type = dish_type;
    //    }
    //}

    //internal class ArrangementMenu
    //{
    //    List<String> coursecart = new List<string>();
    //    AllWine wineController = new AllWine();

  
    //    Wine1 wine1;
    //    Wine1 wine2;
    //    Wine1 wine3;
    //    Wine1 wine4;
    //    Wine1 wine5;


    //    public int arrangementBill;
    //    public int choice;
    //    public int appchoice;
    //    public int entreechoice;
    //    public int desertchoice;
    //    public bool winechoice;
    //    public int wineglasschoice;
    //    public string arrangementChoice;
    //    public string[] Options;


    //    public ArrowMenu beginningScreen;
    //    public ArrowMenu appMenu;
    //    public ArrowMenu entreeMenu;
    //    public ArrowMenu desertMenu;
    //    public ArrowMenu wineMenu;
    //    public ArrowMenu soupMenu;


    //    public string[] appOptions;
    //    public string[] entreeOptions;
    //    public string[] desertOptions;
    //    public string[] wineOptions;
    //    public string[] soupOptions;



    //    public ArrangementMenu()
    //    {
    //        this.wine1 = wineController.GetById(1);
    //        this.wine2 = wineController.GetById(2);
    //        this.wine3 = wineController.GetById(3);
    //        this.wine4 = wineController.GetById(4);
    //        this.wine5 = wineController.GetById(5);


    //        this.arrangementBill = 0;
    //        this.choice = 0;
    //        this.appchoice = 0;
    //        this.entreechoice = 0;
    //        this.desertchoice = 0;
    //        this.winechoice = false;
    //        this.wineglasschoice = 0;


    //        this.beginningScreen = new ArrowMenu("Make a choice out of the following options", this.Options, 0);
    //        this.Options = new string[]
    //        {
    //            "Two course dinner (30,-)", "Three course dinner (40,-)", "Four course dinner (55,-)"
    //        };
    //        this.appOptions = new string[]
    //        {

    //        };
    //        this.entreeOptions = new string[]
    //        {

    //        };
    //        this.desertOptions = new string[]
    //        {

    //        };
    //        this.wineOptions = new string[]
    //        {
    //            $"{this.wine1.Wine_name},{this.wine2.Wine_name},{this.wine3.Wine_name},{this.wine4.Wine_name},{this.wine5.Wine_name}"
    //        };
    //        this.soupOptions = new string[]
    //        {

    //        };

    //        this.appMenu = new ArrowMenu("Please pick your appetizer.", this.appOptions, 0);

    //    }
    //    public void showScreen()
    //    {

 
    //        int selectedIndex = this.beginningScreen.Move();
    //        switch (selectedIndex)
    //        {
    //            case 0:
    //                this.courseOneChoice();
    //                break;
    //            case 1:
    //                //this.CourseTwoChoice();
    //                break;

    //            case 2:
    //                //this.CourseThreeChoice();
    //                break;
 
                

    //        }
    //    }
    //    public void courseOneChoice()
    //    {
    //        int appMenu_Selectedindex = this.appMenu.Move();
    //        switch (appMenu_Selectedindex)
    //        {
    //            case 0:

    //                break;
    //        }
    //    }
    //}





    internal class Program
    {
        static void Main(string[] args)
        {
            
            arrangementMenu myMenu = new arrangementMenu();
            myMenu.Start();
            //        //AllWine wineController = new AllWine();

            //        //Wine1 currentWine = wineController.GetByName("Dessert wine");
            //        //currentWine.Wine_type = "Non Alcohlic";
            //        //Console.WriteLine(currentWine.Wine_type);




            //        Console.WriteLine("Please pick an option by typing in the corresponding number, if you would like to return type in back");
            //        Console.WriteLine("[1] Two course dinner (30,-)");
            //        Console.WriteLine("[2] Three course dinner (40,-)");
            //        Console.WriteLine("[3] Four course dinner (55,-)");
            //        List<String> coursecart = new List<string>();


            //        Coursedish app1 = new Coursedish("Carpaccio", 1, 10.0, ".", "Meat");

            //        Coursedish app2 = new Coursedish("Gambas", 2, 7.50, ".", "Fish");

            //        Coursedish app3 = new Coursedish("Salad", 3, 15.0, ".", "Vegan/Vegetarian");

            //        Coursedish entree1 = new Coursedish("Steak", 4, 15.0, ".", "Meat");

            //        Coursedish entree2 = new Coursedish("Salmon", 5, 17.50, ".", "Fish");

            //        Coursedish entree3 = new Coursedish("Vegetarian Schnitzel", 6, 20.0, ".", "Vegetarian");

            //        Coursedish entree4 = new Coursedish("Pasta", 7, 15.0, ".", "Vegan");

            //        Coursedish dessert1 = new Coursedish("Icecream", 8, 5.0, ".", "Dessert");

            //        Coursedish dessert2 = new Coursedish("Bananasplit", 9, 10.0, ".", "Dessert");

            //        Coursedish dessert3 = new Coursedish("Creme brulee", 10, 7.50, ".", "Dessert");

            //        Coursedish dessert4 = new Coursedish("Pudding", 11, 12.50, ".", "Dessert");

            //        Coursedish soup1 = new Coursedish("Tomato soup", 12, 5.0, ".", "Vegetarian");

            //        Coursedish soup2 = new Coursedish("Mushroom soup", 13, 7.50, ".", "Vegetarian");

            //        Coursedish soup3 = new Coursedish("Goulash soup", 14, 8.0, ".", "Meat");

            //        Wine1 wine1 = wineController.GetById(1);

            //        Wine1 wine2 = wineController.GetById(2);

            //        Wine1 wine3 = wineController.GetById(3);

            //        Wine1 wine4 = wineController.GetById(4);

            //        Wine1 wine5 = wineController.GetById(5);

            //        Console.WriteLine(wine1.Wine_name);
            //        Console.WriteLine(wine4.Wine_name);



            //        string choice = null;
            //        string soupchoice = null;
            //        string appchoice = null;
            //        string entreechoice = null;
            //        string dessertchoice = null;
            //        string winechoice = null;
            //        string wineglasschoice = null;
            //        int coursebill = 0;
            //        choice = Console.ReadLine();
            //        while (choice != "1" && choice != "2" && choice != "3" && choice != "back" && choice != "Back" && choice != "BACK")
            //        {
            //            Console.WriteLine("Please type in a valid command.");
            //            choice = Console.ReadLine();
            //        }
            //        Console.Clear();
            //        if (choice == "1")
            //        {
            //            coursebill = 30;
            //            Console.WriteLine("What would you like for your appetizer?");
            //            Console.WriteLine("[1] " + app1.dish_name);
            //            Console.WriteLine("[2] " + app2.dish_name);
            //            Console.WriteLine("[3] " + app3.dish_name);
            //            appchoice = Console.ReadLine();
            //            while (appchoice != "1" && appchoice != "2" && appchoice != "3")
            //            {
            //                Console.WriteLine("Please type in a valid command.");
            //                appchoice = Console.ReadLine();
            //            }
            //            Console.Clear();
            //            if (appchoice == "1")
            //            {

            //                coursecart.Add(app1.dish_name);
            //            }
            //            else if (appchoice == "2")
            //            {

            //                coursecart.Add(app2.dish_name);
            //            }
            //            else if (appchoice == "3")
            //            {

            //                coursecart.Add(app3.dish_name);
            //            }
            //            Console.WriteLine("What would you like for your main course?");
            //            Console.WriteLine("[1] " + entree1.dish_name);
            //            Console.WriteLine("[2] " + entree2.dish_name);
            //            Console.WriteLine("[3] " + entree3.dish_name);
            //            Console.WriteLine("[4] " + entree4.dish_name);
            //            entreechoice = Console.ReadLine();
            //            while (entreechoice != "1" && entreechoice != "2" && entreechoice != "3" && entreechoice != "4")
            //            {
            //                Console.WriteLine("Please type in a valid command.");
            //                entreechoice = Console.ReadLine();
            //            }
            //            Console.Clear();
            //            if (entreechoice == "1")
            //            {

            //                coursecart.Add(entree1.dish_name);
            //            }
            //            else if (entreechoice == "2")
            //            {

            //                coursecart.Add(entree2.dish_name);
            //            }
            //            else if (entreechoice == "3")
            //            {

            //                coursecart.Add(entree3.dish_name);
            //            }
            //            else if (entreechoice == "4")
            //            {

            //                coursecart.Add(entree4.dish_name);
            //            }
            //            Console.WriteLine("Would you like to add a wine arrangement onto your order for an extra 15,-?");
            //            Console.WriteLine("[Yes]");
            //            Console.WriteLine("[No]");
            //            winechoice = Console.ReadLine();
            //            while (winechoice != "yes" && winechoice != "no" && winechoice != "Yes" && winechoice != "No" && winechoice != "NO" && winechoice != "YES")
            //            {
            //                Console.WriteLine("Please type in a valid command.");
            //                winechoice = Console.ReadLine();
            //            }
            //            Console.Clear();
            //            if (winechoice == "no" || winechoice == "NO" || winechoice == "No")
            //            {
            //                Console.WriteLine("Your final bill is: " + coursebill + ",-");
            //                Console.WriteLine("You have ordered:");
            //                coursecart.ForEach(Console.WriteLine);
            //            }
            //            else if (winechoice == "YES" || winechoice == "Yes" || winechoice == "yes")
            //            {
            //                coursebill += 15;
            //                Console.WriteLine("Pick a wine:");
            //                Console.WriteLine("[1]" + wine1.Wine_name);
            //                Console.WriteLine("[2]" + wine2.Wine_name);
            //                Console.WriteLine("[3]" + wine3.Wine_name);
            //                Console.WriteLine("[4]" + wine4.Wine_name);
            //                Console.WriteLine("[5]" + wine5.Wine_name);
            //                wineglasschoice = Console.ReadLine();
            //                while (wineglasschoice != "1" && wineglasschoice != "2" && wineglasschoice != "3" && wineglasschoice != "4" && wineglasschoice != "5")
            //                {
            //                    Console.WriteLine("Play type in a valid command.");
            //                    wineglasschoice = Console.ReadLine();
            //                }
            //                Console.Clear();
            //                if (wineglasschoice == "1")
            //                {
            //                    coursecart.Add(wine1.Wine_name);
            //                }
            //                else if (wineglasschoice == "2")
            //                {
            //                    coursecart.Add(wine2.Wine_name);
            //                }
            //                else if (wineglasschoice == "3")
            //                {
            //                    coursecart.Add(wine3.Wine_name);
            //                }
            //                else if (wineglasschoice == "4")
            //                {
            //                    coursecart.Add(wine4.Wine_name);
            //                }
            //                else if (wineglasschoice == "5")
            //                {
            //                    coursecart.Add(wine5.Wine_name);
            //                }
            //                Console.WriteLine("Your final bill is: " + coursebill + ",-");
            //                Console.WriteLine("You have ordered:");
            //                coursecart.ForEach(Console.WriteLine);


            //            }

            //        }
            //        else if (choice == "2")
            //        {
            //            coursebill = 40;
            //            Console.WriteLine("What would you like for your appetizer?");
            //            Console.WriteLine("[1] " + app1.dish_name);
            //            Console.WriteLine("[2] " + app2.dish_name);
            //            Console.WriteLine("[3] " + app3.dish_name);
            //            appchoice = Console.ReadLine();
            //            while (appchoice != "1" && appchoice != "2" && appchoice != "3")
            //            {
            //                Console.WriteLine("Please type in a valid command.");
            //                appchoice = Console.ReadLine();
            //            }
            //            Console.Clear();
            //            if (appchoice == "1")
            //            {

            //                coursecart.Add(app1.dish_name);
            //            }
            //            else if (appchoice == "2")
            //            {

            //                coursecart.Add(app2.dish_name);
            //            }
            //            else if (appchoice == "3")
            //            {

            //                coursecart.Add(app3.dish_name);
            //            }
            //            Console.WriteLine("What would you like for your main course?");
            //            Console.WriteLine("[1] " + entree1.dish_name);
            //            Console.WriteLine("[2] " + entree2.dish_name);
            //            Console.WriteLine("[3] " + entree3.dish_name);
            //            Console.WriteLine("[4] " + entree4.dish_name);
            //            entreechoice = Console.ReadLine();
            //            while (entreechoice != "1" && entreechoice != "2" && entreechoice != "3" && entreechoice != "4")
            //            {
            //                Console.WriteLine("Please type in a valid command.");
            //                entreechoice = Console.ReadLine();
            //            }
            //            Console.Clear();
            //            if (entreechoice == "1")
            //            {

            //                coursecart.Add(entree1.dish_name);
            //            }
            //            else if (entreechoice == "2")
            //            {

            //                coursecart.Add(entree2.dish_name);
            //            }
            //            else if (entreechoice == "3")
            //            {

            //                coursecart.Add(entree3.dish_name);
            //            }
            //            else if (entreechoice == "4")
            //            {

            //                coursecart.Add(entree4.dish_name);
            //            }
            //            Console.WriteLine("What would you like for dessert?");
            //            Console.WriteLine("[1] " + dessert1.dish_name);
            //            Console.WriteLine("[2] " + dessert2.dish_name);
            //            Console.WriteLine("[3] " + dessert3.dish_name);
            //            Console.WriteLine("[4] " + dessert4.dish_name);
            //            dessertchoice = Console.ReadLine();
            //            while (dessertchoice != "1" && dessertchoice != "2" && dessertchoice != "3" && dessertchoice != "4")
            //            {
            //                Console.WriteLine("Please type in a valid command.");
            //                dessertchoice= Console.ReadLine();
            //            }
            //            Console.Clear();
            //            if (dessertchoice == "1")
            //            {
            //                coursecart.Add(dessert1.dish_name);
            //            }
            //            else if (dessertchoice == "2")
            //            {
            //                coursecart.Add(dessert2.dish_name);
            //            }
            //            else if (dessertchoice == "3")
            //            {
            //                coursecart.Add(dessert3.dish_name);
            //            }
            //            else if (dessertchoice == "4")
            //            {
            //                coursecart.Add(dessert4.dish_name);
            //            }
            //            Console.WriteLine("Would you like to add a wine arrangement onto your order for an extra 15,-?");
            //            Console.WriteLine("[Yes]");
            //            Console.WriteLine("[No]");
            //            winechoice = Console.ReadLine();
            //            while (winechoice != "yes" && winechoice != "no" && winechoice != "Yes" && winechoice != "No" && winechoice != "NO" && winechoice != "YES")
            //            {
            //                Console.WriteLine("Please type in a valid command.");
            //                winechoice = Console.ReadLine();
            //            }
            //            Console.Clear();
            //            if (winechoice == "no" || winechoice == "NO" || winechoice == "No")
            //            {
            //                Console.WriteLine("Your final bill is: " + coursebill + ",-");
            //                Console.WriteLine("You have ordered:");
            //                coursecart.ForEach(Console.WriteLine);
            //            }
            //            else if (winechoice == "YES" || winechoice == "Yes" || winechoice == "yes")
            //            {
            //                coursebill += 15;
            //                Console.WriteLine("Pick a wine:");
            //                Console.WriteLine("[1]" + wine1.Wine_name);
            //                Console.WriteLine("[2]" + wine2.Wine_name);
            //                Console.WriteLine("[3]" + wine3.Wine_name);
            //                Console.WriteLine("[4]" + wine4.Wine_name);
            //                Console.WriteLine("[5]" + wine5.Wine_name);
            //                wineglasschoice = Console.ReadLine();
            //                while (wineglasschoice != "1" && wineglasschoice != "2" && wineglasschoice != "3" && wineglasschoice != "4" && wineglasschoice != "5")
            //                {
            //                    Console.WriteLine("Play type in a valid command.");
            //                    wineglasschoice = Console.ReadLine();
            //                }
            //                Console.Clear();
            //                if (wineglasschoice == "1")
            //                {
            //                    coursecart.Add(wine1.Wine_name);
            //                }
            //                else if (wineglasschoice == "2")
            //                {
            //                    coursecart.Add(wine2.Wine_name);
            //                }
            //                else if (wineglasschoice == "3")
            //                {
            //                    coursecart.Add(wine3.Wine_name);
            //                }
            //                else if (wineglasschoice == "4")
            //                {
            //                    coursecart.Add(wine4.Wine_name);
            //                }
            //                else if (wineglasschoice == "5")
            //                {
            //                    coursecart.Add(wine5.Wine_name);
            //                }
            //                Console.WriteLine("Your final bill is: " + coursebill + ",-");
            //                Console.WriteLine("You have ordered:");
            //                coursecart.ForEach(Console.WriteLine);

            //            }

            //        }
            //        else if (choice == "3")
            //        {
            //            {
            //                coursebill = 55;
            //                Console.WriteLine("What kind of soup would you like?");
            //                Console.WriteLine("[1] " + soup1.dish_name);
            //                Console.WriteLine("[2] " + soup2.dish_name);
            //                Console.WriteLine("[3] " + soup3.dish_name);
            //                soupchoice = Console.ReadLine();
            //                while (soupchoice != "1" && soupchoice != "2" && soupchoice != "3")
            //                {
            //                    Console.WriteLine("Please type in a valid command.");
            //                    soupchoice = Console.ReadLine();
            //                }
            //                Console.Clear();
            //                if (soupchoice == "1")
            //                    {
            //                        coursecart.Add(soup1.dish_name);
            //                    }
            //                    else if (soupchoice == "2")
            //                    {
            //                        coursecart.Add(soup2.dish_name);
            //                    }
            //                    else if (soupchoice == "3")
            //                    {
            //                        coursecart.Add(soup3.dish_name);
            //                    }
            //                    Console.WriteLine("What would you like for your appetizer?");
            //                    Console.WriteLine("[1] " + app1.dish_name);
            //                    Console.WriteLine("[2] " + app2.dish_name);
            //                    Console.WriteLine("[3] " + app3.dish_name);
            //                    appchoice = Console.ReadLine();
            //                    while (appchoice != "1" && appchoice != "2" && appchoice != "3")
            //                    {
            //                        Console.WriteLine("Please type in a valid command.");
            //                        appchoice = Console.ReadLine();
            //                    }
            //                    Console.Clear();
            //                    if (appchoice == "1")
            //                    {

            //                        coursecart.Add(app1.dish_name);
            //                    }
            //                    else if (appchoice == "2")
            //                    {

            //                        coursecart.Add(app2.dish_name);
            //                    }
            //                    else if (appchoice == "3")
            //                    {

            //                        coursecart.Add(app3.dish_name);
            //                    }
            //                    Console.WriteLine("What would you like for your main course?");
            //                    Console.WriteLine("[1] " + entree1.dish_name);
            //                    Console.WriteLine("[2] " + entree2.dish_name);
            //                    Console.WriteLine("[3] " + entree3.dish_name);
            //                    Console.WriteLine("[4] " + entree4.dish_name);
            //                    entreechoice = Console.ReadLine();
            //                    while (entreechoice != "1" && entreechoice != "2" && entreechoice != "3" && entreechoice != "4")
            //                    {
            //                        Console.WriteLine("Please type in a valid command.");
            //                        entreechoice = Console.ReadLine();
            //                    }
            //                    Console.Clear();
            //                    if (entreechoice == "1")
            //                    {

            //                        coursecart.Add(entree1.dish_name);
            //                    }
            //                    else if (entreechoice == "2")
            //                    {

            //                        coursecart.Add(entree2.dish_name);
            //                    }
            //                    else if (entreechoice == "3")
            //                    {

            //                        coursecart.Add(entree3.dish_name);
            //                    }
            //                    else if (entreechoice == "4")
            //                    {

            //                        coursecart.Add(entree4.dish_name);
            //                    }
            //                    Console.WriteLine("What would you like for dessert?");
            //                    Console.WriteLine("[1] " + dessert1.dish_name);
            //                    Console.WriteLine("[2] " + dessert2.dish_name);
            //                    Console.WriteLine("[3] " + dessert3.dish_name);
            //                    Console.WriteLine("[4] " + dessert4.dish_name);
            //                    dessertchoice = Console.ReadLine();
            //                    while (dessertchoice != "1" && dessertchoice != "2" && dessertchoice != "3" && dessertchoice != "4")
            //                    {
            //                        Console.WriteLine("Please type in a valid command.");
            //                        dessertchoice = Console.ReadLine();
            //                    }
            //                    Console.Clear();
            //                    if (dessertchoice == "1")
            //                    {
            //                        coursecart.Add(dessert1.dish_name);
            //                    }
            //                    else if (dessertchoice == "2")
            //                    {
            //                        coursecart.Add(dessert2.dish_name);
            //                    }
            //                    else if (dessertchoice == "3")
            //                    {
            //                        coursecart.Add(dessert3.dish_name);
            //                    }
            //                    else if (dessertchoice == "4")
            //                    {
            //                        coursecart.Add(dessert4.dish_name);
            //                    }
            //                    Console.WriteLine("Would you like to add a wine arrangement onto your order for an extra 15,-?");
            //                    Console.WriteLine("[Yes]");
            //                    Console.WriteLine("[No]");
            //                    winechoice = Console.ReadLine();
            //                    while (winechoice != "yes" && winechoice != "no" && winechoice != "Yes" && winechoice != "No" && winechoice != "NO" && winechoice != "YES")
            //                    {
            //                        Console.WriteLine("Please type in a valid command.");
            //                        winechoice = Console.ReadLine();
            //                    }
            //                    Console.Clear();
            //                    if (winechoice == "no" || winechoice == "NO" || winechoice == "No")
            //                    {
            //                        Console.WriteLine("Your final bill is: " + coursebill + ",-");
            //                        Console.WriteLine("You have ordered:");
            //                        coursecart.ForEach(Console.WriteLine);
            //                    }
            //                    else if (winechoice == "YES" || winechoice == "Yes" || winechoice == "yes")
            //                    {
            //                        coursebill += 15;
            //                        Console.WriteLine("Pick a wine:");
            //                        Console.WriteLine("[1]" + wine1.Wine_name);
            //                        Console.WriteLine("[2]" + wine2.Wine_name);
            //                        Console.WriteLine("[3]" + wine3.Wine_name);
            //                        Console.WriteLine("[4]" + wine4.Wine_name);
            //                        Console.WriteLine("[5]" + wine5.Wine_name);
            //                        wineglasschoice = Console.ReadLine();
            //                        while (wineglasschoice != "1" && wineglasschoice != "2" && wineglasschoice != "3" && wineglasschoice != "4" && wineglasschoice != "5")
            //                        {
            //                            Console.WriteLine("Play type in a valid command.");
            //                            wineglasschoice = Console.ReadLine();
            //                        }
            //                        Console.Clear();
            //                        if (wineglasschoice == "1")
            //                        {
            //                            coursecart.Add(wine1.Wine_name);
            //                        }
            //                        else if (wineglasschoice == "2")
            //                        {
            //                            coursecart.Add(wine2.Wine_name);
            //                        }
            //                        else if (wineglasschoice == "3")
            //                        {
            //                            coursecart.Add(wine3.Wine_name);
            //                        }
            //                        else if (wineglasschoice == "4")
            //                        {
            //                            coursecart.Add(wine4.Wine_name);
            //                        }
            //                        else if (wineglasschoice == "5")
            //                        {
            //                            coursecart.Add(wine5.Wine_name);
            //                        }
            //                        Console.WriteLine("Your final bill is: " + coursebill + ",-");
            //                        Console.WriteLine("You have ordered:");
            //                        coursecart.ForEach(Console.WriteLine);

            //                    }

            //                }
            //            }
                   }
            }
        }
