using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    public class Table_map
    {
        public List<Table> Tables;
        public List<Table> AvailableTablesLeft;

        public Table_map()
        {
            this.AvailableTablesLeft = new List<Table>();
            this.Tables = new List<Table>
            {
                new Table(2, 0, 1, 2, 3),
                new Table(2, 1, 2, 27, 3),
                new Table(2, 2, 3, 51, 3),
                new Table(2, 3, 4, 76, 3),
                new Table(4, 4, 5, 2, 12),
                new Table(4, 5, 6, 2, 21),
                new Table(6, 6, 7, 19, 14),
                new Table(4, 7, 8, 38, 16),
                new Table(6, 8, 9, 55, 14),
                new Table(4, 9, 10, 74, 12),
                new Table(4, 10, 11, 74, 21),
                new Table(2, 11, 12, 2, 30),
                new Table(2, 12, 13, 27, 30),
                new Table(2, 13, 14, 51, 30),
                new Table(2, 14, 15, 76, 30)
            };
        }

        public void Show_Tables()// laat hele plattegrond zien
        {
            foreach(Table tafel in Tables)
            {
                tafel.table_print();
            }
        }
        public void Color_Change(int table_number, bool available) // verandert kleur van opgegeven tafel met selected
        {
            if (available == true)
            {
                Tables[table_number - 1].selected = true;
                this.Tables[table_number - 1].table_print();
            }
            else
            {
                Tables[table_number - 1].selected = false;
                this.Tables[table_number - 1].table_print();
            }
        }

        public void AutoPicker(int table)
        {
            this.Color_Change(table + 1, true);
            Console.SetCursorPosition(0, 44);
            Console.WriteLine($"  We have picked table {this.Tables[table].table_number}");
            this.Tables[table].available = false;
            Console.WriteLine(" Press any key to continue");
            ConsoleKeyInfo keypress = Console.ReadKey();
        }

        public void SetAvailableTables()
        {
            foreach(Table tafel in Tables)
            {
                if (tafel.available)
                {
                    AvailableTablesLeft.Add(tafel);
                }
            }
        }
        public List<int> Auto_pick(int groupsize)
        {
            List<int> ChosenTables = new List<int>();
            int peopleleft = groupsize;
            while(peopleleft > 0)
            {
                if(peopleleft >= 5 && AvailableTablesLeft.Find(i => i.table_type == 6) != null)
                {
                    Table pickedtable = AvailableTablesLeft.Find(i => i.table_type == 6);
                    ChosenTables.Add(pickedtable.table_number);
                    AvailableTablesLeft.Remove(pickedtable);
                    Tables[pickedtable.table_number - 1].selected = true;
                    peopleleft -= 6;
                }
                else if(peopleleft >= 3 && AvailableTablesLeft.Find(i => i.table_type == 4) != null)
                {
                    Table pickedtable = AvailableTablesLeft.Find(i => i.table_type == 4);
                    ChosenTables.Add(pickedtable.table_number);
                    AvailableTablesLeft.Remove(pickedtable);
                    Tables[pickedtable.table_number - 1].selected = true;
                    peopleleft -= 4;
                }
                else
                {
                    Table pickedtable = AvailableTablesLeft.Find(i => i.table_type == 2);
                    ChosenTables.Add(pickedtable.table_number);
                    AvailableTablesLeft.Remove(pickedtable);
                    Tables[pickedtable.table_number - 1].selected = true;

                    peopleleft -= 2;
                }
            }

            ChosenTables.Sort(); // puts all integers from low to high
            this.Show_Tables();
            string alltables;
            if (ChosenTables.Count > 1)  // string creation for all the tables chosen
            {
                alltables = "We have chosen tables : ";
                for (int i = 0; i < ChosenTables.Count - 2; i++)
                {
                    alltables += $"{ChosenTables[i]}, ";
                }
                alltables += $"{ChosenTables[ChosenTables.Count - 2]} and {ChosenTables[ChosenTables.Count - 1]}";
            }
            else
            {
                alltables = $"We have chosen Table {ChosenTables[0]}";
            }
          
            Console.SetCursorPosition(2, 38);
            Console.WriteLine(alltables);
         
            Console.WriteLine("\n \n  Press any key to continue");
            ConsoleKeyInfo keypress = Console.ReadKey();
            return ChosenTables;
        }

        public List<Table> Set_canSelect(int group_size) // sets can_select of every table object in Tables of same type or smaller type to true
        {
            List<Table> ReturnList = new List<Table>();
            if (group_size <= 2)
            {
                foreach(Table X in Tables)
                {
                    if(X.table_type == 4 || X.table_type == 6)
                    {
                        ReturnList.Add(X);
                    }
                }
            }
            else if (group_size > 2 && group_size < 5)
            {
                foreach(Table X in Tables)
                {
                    if (X.table_type == 6)
                    {
                        ReturnList.Add(X);
                    }
                }
            }
            return ReturnList;
        }

        public List<int> Manual_pick(int groupsize)
        {
            List<int> ChosenTables = new List<int>();
            int temp_choice = 1;
            bool choice_picked;
            Console.SetCursorPosition(0, 40);
            Console.WriteLine("  use the arrow-keys to navigate, press enter to select a table.");

            //Tables[0].table_print(); // print table 1 because cancelling a table doesn't show table 1 correct.
            int PeopleLeft = groupsize;
            while (PeopleLeft > 0)
            {
                Console.SetCursorPosition(0, 37);
                Console.WriteLine($"  {PeopleLeft} people left     ");
                Console.SetCursorPosition(0, 43);
                Console.WriteLine("                                                                                       ");
                choice_picked = false;
                bool alreadyprintedonce = false;
                this.Show_Tables();
                while (choice_picked == false)
                {
                    if(alreadyprintedonce == false)
                    {
                        if (PeopleLeft <= 6)
                        {
                            List<Table> Canselect = Set_canSelect(PeopleLeft);
                            foreach(Table X in Canselect)
                            {
                                X.Can_select = false;
                                X.table_print();
                            }
                            this.Show_Tables();
                        }
                        alreadyprintedonce = true;
                    }

                    ConsoleKeyInfo newKeypressed = Console.ReadKey(); //reads key input from user
                    switch (newKeypressed.Key) // navigates the map with key input from the arrows
                    {
                        case ConsoleKey.RightArrow:
                            this.Color_Change(temp_choice, false);
                            switch (temp_choice)
                            {
                                case 15:
                                case 4:
                                    temp_choice -= 3;
                                    break;
                                case 10:
                                case 11:
                                    temp_choice -= 5;
                                    break;
                                case 5:
                                    temp_choice += 2;
                                    break;
                                default:
                                    temp_choice += 1;
                                    break;
                            }
                            this.Color_Change(temp_choice, true);
                            break;
                        case ConsoleKey.LeftArrow:
                            this.Color_Change(temp_choice, false);
                            switch (temp_choice)
                            {
                                case 5:
                                case 6:
                                    temp_choice += 5;
                                    break;
                                case 1:
                                case 12:
                                    temp_choice += 3;
                                    break;
                                case 11:
                                    temp_choice -= 2;
                                    break;
                                default:
                                    temp_choice -= 1;
                                    break;
                            }
                            this.Color_Change(temp_choice, true);
                            break;
                        case ConsoleKey.UpArrow:
                            this.Color_Change(temp_choice, false);
                            switch (temp_choice)
                            {
                                case 15:
                                case 5:
                                    temp_choice -= 4;
                                    break;
                                case 14:
                                case 7:
                                    temp_choice -= 5;
                                    break;
                                case 11:
                                case 6:
                                    temp_choice -= 1;
                                    break;
                                case int n when (n <= 4):
                                    temp_choice += 11;
                                    break;
                                default:
                                    temp_choice -= 6;
                                    break;
                            }
                            this.Color_Change(temp_choice, true);
                            break;
                        case ConsoleKey.DownArrow:
                            this.Color_Change(temp_choice, false);
                            switch (temp_choice)
                            {
                                case 1:
                                case 11:
                                    temp_choice += 4;
                                    break;
                                case 2:
                                case 8:
                                case 9:
                                    temp_choice += 5;
                                    break;
                                case 5:
                                case 10:
                                    temp_choice += 1;
                                    break;
                                case int n when (n > 10):
                                    temp_choice -= 11;
                                    break;
                                default:
                                    temp_choice += 6;
                                    break;
                            }
                            this.Color_Change(temp_choice, true);
                            break;
                        case ConsoleKey.Enter:
                            if (this.Tables[temp_choice - 1].available && this.Tables[temp_choice - 1].Can_select) // check if available
                            {
                                Console.SetCursorPosition(0, 43);
                                Console.WriteLine($"  You picked table {temp_choice}. To confirm your choice press Enter, to cancel press Escape.");

                                bool check_confirm = false;
                                while (check_confirm == false)
                                {
                                    ConsoleKeyInfo Keypressed_confirm = Console.ReadKey();
                                    if (Keypressed_confirm.Key == ConsoleKey.Enter)
                                    {
                                        Tables[temp_choice-1].available = false;
                                        Color_Change(temp_choice-1, false);
                                        ChosenTables.Add(temp_choice);
                                        PeopleLeft -= Tables[temp_choice-1].table_type;

                                        choice_picked = true;
                                        check_confirm = true;
                                    }
                                    else if (Keypressed_confirm.Key == ConsoleKey.Escape)
                                    {
                                        this.Tables[temp_choice - 1].selected = false;
                                        Show_Tables();
                                        Console.SetCursorPosition(0, 43);
                                        Console.WriteLine("                                                                                  ");
                                        check_confirm = true;
                                    }
                                }
                            }
                            else if (this.Tables[temp_choice - 1].available && !this.Tables[temp_choice - 1].Can_select) // the selected table is available but not of same type
                            {
                                if (groupsize < this.Tables[temp_choice - 1].table_type)
                                {
                                    Console.SetCursorPosition(0, 42);
                                    Console.WriteLine($"\n  table {temp_choice} is too big for the amount of people coming.                                   ");
                                }
                                else
                                {
                                    Console.SetCursorPosition(0, 42);
                                    Console.WriteLine($"\n  table {temp_choice} is too small for the amount of people coming.                                   ");
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 42);
                                Console.WriteLine($"\n  table {temp_choice} is not available at the chosen time.                                   ");
                            }
                            break;
                    }
                }
            }

            Console.SetCursorPosition(0, 42);
            Console.WriteLine("  All tables have been chosen. Press Any key to continue.                     \n                                                                                                  ");
            ConsoleKeyInfo pressanykeytocontinue = Console.ReadKey();
            return ChosenTables;
        }

        public List<int> Choice(int groupsize)//functie voor uitvoeren keuze manual/auto-pick met pijltjes
        {
            Console.Clear(); //setup map
            string[] options = new string[] {
                    "let me pick",
                    "Choose a table setup for me"
                };
            SetAvailableTables();
            ArrowMenu gek = new ArrowMenu("  You can pick a table of your choosing or let us decide the best table.", options, 0);
            int index = gek.Move();
            List<int> listofselectedtables = new List<int>(); 
            switch (index)
            {
                case 0:
                    Console.Clear();
                    listofselectedtables = Manual_pick(groupsize); // put in group size
                    break;
                case 1:
                    Console.Clear();
                    listofselectedtables = Auto_pick(groupsize);
                    break;
            }
            return listofselectedtables;
        }
    }
}
