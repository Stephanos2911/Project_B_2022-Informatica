using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Kevin_Restaurant
{
    public class ArrowMenu
    {
        // Variables for the menu of selected items.
        private int SelectedIndex;
        private string[] Options;
        private string Intro;
        private int startposY;

        public ArrowMenu(string intro, string[] options, int Startingposition)
        {
            // variables inside the class
            Intro = intro;
            Options = options;
            SelectedIndex = 0;
            this.startposY = Startingposition; 
        }

        public ArrowMenu(string intro, List<string> options, int Startingposition)
        {
            // variables inside the class
            Intro = intro;
            Options = options.ToArray();
            SelectedIndex = 0;
            this.startposY = Startingposition;
        }

        private void DisplayOptions() // displays the text.
        {
            for (int i = 0; i < Options.Length; i++) // iterates from line to line
            {
                string current_option = Options[i];
          
                if (i == SelectedIndex)
                {
            
                    ForegroundColor = ConsoleColor.Black; // makes sure the text that is selected is marked with a background color,
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($" {current_option}  "); // displays the text again.
            }
            ResetColor();
        }

        private void DisplaySingleOption(int index, bool selected) // Changes the selected option on or off
        {
            Console.SetCursorPosition(0,startposY+ (index+1));
            string current_option = Options[index];
            if (selected)
            {
                ForegroundColor = ConsoleColor.Black; 
                BackgroundColor = ConsoleColor.White;
                WriteLine($" {current_option}  ");
            }
            else
            {
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;
                WriteLine($" {current_option}  ");
            }
            ResetColor();
        }

        public int Move() // this class makes sure you can use the up and down arrow to iterate through strings.
        {
            Clear();
            WriteLine(Intro);
            DisplayOptions();
            ConsoleKey keyPressed;
            do 
            {
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow) // if you go an arrow up, the string above the other string will be selected.
                {
                    DisplaySingleOption(SelectedIndex, false);
                    SelectedIndex--;
                    if (SelectedIndex == -1) // if selected word is the first word in the arr, when pressing the up arrow the last word in the arr will be selected.
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                    DisplaySingleOption(SelectedIndex, true);
                }
                else if (keyPressed == ConsoleKey.DownArrow) // if you go an arrow down, the string underneath the other string will be selected.
                {
                    DisplaySingleOption(SelectedIndex, false);
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0; // if selected word is the last word in the arr, when pressing the down arrow the first word in the arr will be selected.
                    }
                    DisplaySingleOption(SelectedIndex, true);
                }
            } while (keyPressed != ConsoleKey.Enter); // This will be happening till enter is pressed.

            return SelectedIndex;
        }
    }
}
