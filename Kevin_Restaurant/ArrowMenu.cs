using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Kevin_Restaurant
{
    internal class ArrowMenu
    {
        // Variables for the menu of selected items.
        private int SelectedIndex;
        private string[] Options;
        private string Intro;

        public ArrowMenu(string intro, string[] options)
        {
            // variables inside the class
            Intro = intro;
            Options = options;
            SelectedIndex = 0;
        }

        public void DisplayOptions() // displays the text.
        {
            WriteLine(Intro);
            for (int i = 0; i < Options.Length; i++) // iterates from line to line
            {
                string current_option = Options[i];
                string temp;
                if (i == SelectedIndex)
                {
                    //temp = "=";
                    ForegroundColor = ConsoleColor.Black; // makes sure the text that is selected is marked with a background color,
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    //temp = "";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($" - {current_option} - "); // displays the text again.
            }
            ResetColor();
        }

        public int Move() // this class makes sure you can use the up and down arrow to iterate through strings.
        {
            ConsoleKey keyPressed;
            do // used in the guide video (dont know what this does :D )
            {
                Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow) // if you go an arrow up, the string above the other string will be selected.
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1) // if selected word is the first word in the arr, when pressing the up arrow the last word in the arr will be selected.
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow) // if you go an arrow down, the string underneath the other string will be selected.
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0; // if selected word is the last word in the arr, when pressing the down arrow the first word in the arr will be selected.
                    }
                }
            } while (keyPressed != ConsoleKey.Enter); // This will be happening till enter is pressed.

            return SelectedIndex;
        }
    }
}
