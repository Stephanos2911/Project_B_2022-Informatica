using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kevin_Restaurant
{
    public class Table
    {
        public int table_type;
        public int table_id;
        public int table_number;
        public bool available;
        public int Start_posX;
        public int Start_posY;
        public bool selected;
        public bool Can_select;

        public Table(int table_type, int table_id, int table_number, int Start_posX, int Start_posY)
        {
            this.table_type = table_type;
            this.table_id = table_id;
            this.table_number = table_number;
            this.available = true;
            this.Start_posX = Start_posX;
            this.Start_posY = Start_posY;
            this.selected = false;
            this.Can_select = true;

        }

        public void TextAt(int left, int top, string text) //plaatst een string op een aangewezen plek, checkt availability voor kleur.
        {
            if (this.selected == true && this.available == true) // blue if selected
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(left, top);
                Console.Write(text);
            }
            else if (this.available && this.Can_select) // green if available for selection
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(left, top);
                Console.Write(text);
            }
            else if (this.available && !this.Can_select) // grey for tables available but not in correct size
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(left, top);
                Console.Write(text);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; // not available
                Console.SetCursorPosition(left, top);
                Console.Write(text);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void table_print()
        {
            if (this.table_type == 2) // checkt hoeveel personen
            {
                //bovenste laag
                for (int i = 0; i < 7; i++)
                {
                    TextAt(this.Start_posX + i + 2, this.Start_posY, "*");
                }
                //tafel nummer
                TextAt(this.Start_posX + 5, this.Start_posY + 2, this.table_number.ToString());

                //stoel links 
                TextAt(this.Start_posX, this.Start_posY + 2, "(");

                //stoel rechts
                TextAt(this.Start_posX + 10, this.Start_posY + 2, ")");

                //linker border
                for (int u = 1; u <= 4; u++)
                {
                    TextAt(this.Start_posX + 2, this.Start_posY + u, "|");
                }

                //rechter border
                for (int k = 1; k <= 4; k++)
                {
                    TextAt(this.Start_posX + 8, this.Start_posY + k, "|");
                }

                //onder laag
                for (int L = 0; L < 7; L++)
                {
                    TextAt(this.Start_posX + L + 2, this.Start_posY + 4, "*");
                }
            }

            else if (this.table_type == 4)
            {
                //bovenste laag
                for (int i = 0; i < 13; i++)
                {
                    TextAt(this.Start_posX + i, this.Start_posY, "*");
                }
                //tafel nummer
                TextAt(this.Start_posX + 6, this.Start_posY + 2, this.table_number.ToString());

                //stoel boven
                TextAt(this.Start_posX + 3, this.Start_posY - 1, "/");
                TextAt(this.Start_posX + 4, this.Start_posY - 1, @"\");

                TextAt(this.Start_posX + 8, this.Start_posY - 1, "/");
                TextAt(this.Start_posX + 9, this.Start_posY - 1, @"\");

                //stoel onder
                TextAt(this.Start_posX + 3, this.Start_posY + 5, @"\");
                TextAt(this.Start_posX + 4, this.Start_posY + 5, "/");

                TextAt(this.Start_posX + 8, this.Start_posY + 5, @"\");
                TextAt(this.Start_posX + 9, this.Start_posY + 5, "/");

                //linker border
                for (int u = 1; u <= 4; u++)
                {
                    TextAt(this.Start_posX, this.Start_posY + u, "|");
                }

                //rechter border
                for (int k = 1; k <= 4; k++)
                {
                    TextAt(this.Start_posX + 12, this.Start_posY + k, "|");
                }

                //onder laag
                for (int L = 0; L < 13; L++)
                {
                    TextAt(this.Start_posX + L, this.Start_posY + 4, "*");
                }
            }

            else
            {
                //bovenste laag
                for (int i = 0; i < 11; i++)
                {
                    TextAt(this.Start_posX + i + 2, this.Start_posY, "*");
                }
                //tafel nummer
                TextAt(this.Start_posX + 7, this.Start_posY + 4, this.table_number.ToString());

                //stoelen links 
                TextAt(this.Start_posX, this.Start_posY + 2, "(");
                TextAt(this.Start_posX, this.Start_posY + 4, "(");
                TextAt(this.Start_posX, this.Start_posY + 6, "(");

                //stoelen rechts
                TextAt(this.Start_posX + 14, this.Start_posY + 2, ")");
                TextAt(this.Start_posX + 14, this.Start_posY + 4, ")");
                TextAt(this.Start_posX + 14, this.Start_posY + 6, ")");



                //linker border
                for (int u = 1; u <= 8; u++)
                {
                    TextAt(this.Start_posX + 2, this.Start_posY + u, "|");
                }

                //rechter border
                for (int k = 1; k <= 8; k++)
                {
                    TextAt(this.Start_posX + 12, this.Start_posY + k, "|");
                }

                //onder laag
                for (int L = 0; L < 11; L++)
                {
                    TextAt(this.Start_posX + L + 2, this.Start_posY + 8, "*");
                }
            }

        }
    }
}
