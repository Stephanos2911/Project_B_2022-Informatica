using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    public class Table_map
    {
        public Table[] Tables;
        public bool Show_map;
        public int Table_choice; //position in array van keuze
        public int temp_choice;
        public bool choice_picked;
        public int cancelled;
        public Table_map()
        {
            this.Show_map = true;
            this.Table_choice = 0;
            this.Tables = new Table[15];
            this.choice_picked = false;
            this.cancelled = 0;
            this.temp_choice = 1;
            //boven rij 
            Tables[0] = new Table(2, 0, 1, 2, 3);
            Tables[1] = new Table(2, 1, 2, 27, 3);
            Tables[2] = new Table(2, 2, 3, 51, 3);
            Tables[3] = new Table(2, 3, 4, 76, 3);
            //midden rij 
            Tables[4] = new Table(4, 4, 5, 2, 12);
            Tables[5] = new Table(4, 5, 6, 2, 21);
            Tables[6] = new Table(6, 6, 7, 19, 14);
            Tables[7] = new Table(4, 7, 8, 38, 16);
            Tables[8] = new Table(6, 8, 9, 55, 14);
            Tables[9] = new Table(4, 9, 10, 74, 12);
            Tables[10] = new Table(4, 10, 11, 74, 21);
            //onderij 
            Tables[11] = new Table(2, 11, 12, 2, 30);
            Tables[12] = new Table(2, 12, 13, 27, 30);
            Tables[13] = new Table(2, 13, 14, 51, 30);
            Tables[14] = new Table(2, 14, 15, 76, 30);
        }
        public void Show_Tables()// laat hele plattegrond zien
        {
            for (int i = 0; i < this.Tables.Length; i++)
            {
                Tables[i].table_print();
            }
            Console.SetCursorPosition(2, 40);
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
            this.Table_choice = table;
            this.choice_picked = true;
            this.Tables[table].available = false;
            Console.WriteLine(" Press any key to continue");
            ConsoleKeyInfo keypress = Console.ReadKey();
        }
        public void Auto_pick(int size)
        {
            Set_canSelect(size);
            Console.Clear();
            this.Show_Tables();
            bool isthere_even_a_table = false;
            if (size <= 2)
            {
                for (int i = 0; i < this.Tables.Length; i++) // check if there is atleast 1 table of type 2
                {
                    if (this.Tables[i].available == true && this.Tables[i].table_type == 2)
                    {
                        isthere_even_a_table = true;
                    }
                }
            }
            else if (size >= 5)
            {
                for (int i = 0; i < this.Tables.Length; i++) // check if there is atleast 1 table of type 6
                {
                    if (this.Tables[i].available == true && this.Tables[i].table_type == 6)
                    {
                        isthere_even_a_table = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.Tables.Length; i++) // check if there is atleast 1 table of type 4
                {
                    if (this.Tables[i].available == true && this.Tables[i].table_type == 4)
                    {
                        isthere_even_a_table = true;
                    }
                }
            }
            if (isthere_even_a_table)
            {
                bool found = false;
                while (found == false) // since there are only 2 table options for size 6, the picking is not randomized for optimization.
                {
                    if (size >= 5)
                    {
                        if (this.Tables[6].available == true)
                        {
                            AutoPicker(6);
                            found = true;
                        }
                        else
                        {
                            AutoPicker(8);
                            found = true;
                        }
                    }
                    else // random integer is created everytime until a valid random table is generated. 
                    {
                        bool innerfound = false;
                        while (innerfound == false)
                        {
                            Random rnd = new Random();
                            int num = rnd.Next(0, this.Tables.Length);
                            if (size <= 2)
                            {
                                if (this.Tables[num].available == true && this.Tables[num].table_type == 2)
                                {
                                    AutoPicker(num);
                                    innerfound = true;
                                }
                            }
                            else
                            {
                                if (this.Tables[num].available == true && this.Tables[num].table_type == 4)
                                {
                                    AutoPicker(num);
                                    innerfound = true;
                                }
                            }
                        }
                        found = true;
                    }

                }
            }
            else
            {
                Console.SetCursorPosition(0, 44);
                Console.WriteLine("  There is no available table for your group size.");
            }
        }

        public void Set_canSelect(int group_size) // sets can_select of every table of same type to true
        {
            if (group_size > 4)
            {
                for (int i = 0; i < this.Tables.Length; i++)
                {
                    if (this.Tables[i].table_type == 6)
                    {
                        this.Tables[i].Can_select = true;
                    }
                }
            }
            else if (group_size <= 2)
            {
                for (int i = 0; i < this.Tables.Length; i++)
                {
                    if (this.Tables[i].table_type == 2)
                    {
                        this.Tables[i].Can_select = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.Tables.Length; i++)
                {
                    if (this.Tables[i].table_type == 4)
                    {
                        this.Tables[i].Can_select = true;
                    }
                }
            }
        }

        public void Manual_pick(int groupsize)
        {
            Set_canSelect(groupsize);
            Console.Clear();
            this.Show_Tables();
            this.Color_Change(this.temp_choice, true);
            Tables[0].table_print(); // print table 1 because cancelling a table doesn't show table 1 correct.

            Console.SetCursorPosition(0, 42);
            Console.WriteLine("  use the arrow-keys to navigate, press enter to select a table.");
            while (this.choice_picked == false)
            {
                ConsoleKeyInfo newKeypressed = Console.ReadKey(); //reads key input from user
                switch (newKeypressed.Key) // navigates the map with key input from the arrows
                {
                    case ConsoleKey.RightArrow:
                        this.Color_Change(temp_choice, false);
                        switch (this.temp_choice)
                        {
                            case 15:
                            case 4:
                                this.temp_choice -= 3;
                                break;
                            case 10:
                            case 11:
                                this.temp_choice -= 5;
                                break;
                            case 5:
                                this.temp_choice += 2;
                                break;
                            default:
                                this.temp_choice += 1;
                                break;
                        }
                        this.Color_Change(this.temp_choice, true);
                        break;
                    case ConsoleKey.LeftArrow:
                        this.Color_Change(this.temp_choice, false);
                        switch (this.temp_choice)
                        {
                            case 5:
                            case 6:
                                this.temp_choice += 5;
                                break;
                            case 1:
                            case 12:
                                this.temp_choice += 3;
                                break;
                            case 11:
                                this.temp_choice -= 2;
                                break;
                            default:
                                this.temp_choice -= 1;
                                break;
                        }
                        this.Color_Change(this.temp_choice, true);
                        break;
                    case ConsoleKey.UpArrow:
                        this.Color_Change(this.temp_choice, false);
                        switch (this.temp_choice)
                        {
                            case 15:
                            case 5:
                                this.temp_choice -= 4;
                                break;
                            case 14:
                            case 7:
                                this.temp_choice -= 5;
                                break;
                            case 11:
                            case 6:
                                this.temp_choice -= 1;
                                break;
                            case int n when (n <= 4):
                                this.temp_choice += 11;
                                break;
                            default:
                                this.temp_choice -= 6;
                                break;
                        }
                        this.Color_Change(this.temp_choice, true);
                        break;
                    case ConsoleKey.DownArrow:
                        this.Color_Change(this.temp_choice, false);
                        switch (this.temp_choice)
                        {
                            case 1:
                            case 11:
                                this.temp_choice += 4;
                                break;
                            case 2:
                            case 8:
                            case 9:
                                this.temp_choice += 5;
                                break;
                            case 5:
                            case 10:
                                this.temp_choice += 1;
                                break;
                            case int n when (n > 10):
                                this.temp_choice -= 11;
                                break;
                            default:
                                this.temp_choice += 6;
                                break;
                        }
                        this.Color_Change(this.temp_choice, true);
                        break;
                    case ConsoleKey.Enter:
                        if (this.Tables[this.temp_choice - 1].available && this.Tables[this.temp_choice - 1].Can_select) // available and of same type.
                        {
                            Console.SetCursorPosition(0, 43);
                            Console.WriteLine($"  You picked table {this.temp_choice}. To confirm your choice press Enter, to cancel press Escape.");

                            bool check_confirm = false;
                            while (check_confirm == false)
                            {
                                ConsoleKeyInfo Keypressed_confirm = Console.ReadKey();
                                if (Keypressed_confirm.Key == ConsoleKey.Enter)
                                {
                                    this.Table_choice = this.temp_choice;
                                    this.choice_picked = true;
                                    check_confirm = true;
                                }
                                else if (Keypressed_confirm.Key == ConsoleKey.Escape)
                                {
                                    Manual_pick(groupsize);
                                    check_confirm = true;
                                }
                            }

                        }
                        else if (this.Tables[this.temp_choice - 1].available && !this.Tables[this.temp_choice - 1].Can_select) // the selected table is available but not of same type
                        {
                            if (groupsize < this.Tables[this.temp_choice - 1].table_type)
                            {
                                Console.SetCursorPosition(0, 42);
                                Console.WriteLine($"\n  table {this.temp_choice} is too big for the amount of people coming.                                   ");
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 42);
                                Console.WriteLine($"\n  table {this.temp_choice} is too small for the amount of people coming.                                   ");
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 42);
                            Console.WriteLine($"\n  table {this.temp_choice} is not available at the chosen time.                                   ");
                        }
                        break;
                }
            }
        }
        public int Choice(int groupsize)//functie voor uitvoeren keuze manual/auto-pick met pijltjes
        {
            Console.Clear(); //setup map
            string[] options = new string[] {
                    "let me pick",
                    "Choose for me"
                };
            ArrowMenu gek = new ArrowMenu("  You can pick a table of your choosing or let us decide the best table.", options, 0);
            int index = gek.Move();
            switch (index)
            {
                case 0:
                    Manual_pick(groupsize); // put in group size
                    break;
                case 1:
                    Auto_pick(groupsize);
                    break;
            }
            return this.Table_choice; 
        }
    }
}
