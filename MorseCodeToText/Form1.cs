using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Utilities;

namespace MorseCodeToText
{
    public partial class Form1 : Form
    {
        // Global keyboard hook variable.
        globalKeyboardHook gkh = new globalKeyboardHook();

        // Tracking length of keypress.
        DateTime startPress;
        DateTime endPress;

        // Preventing spamming of keydowns.
        bool keyReleased = true;
        // Tracks when to declare a letter finished.
        bool letterComplete = false;

        // String message variables.
        string word;
        string letter;

        // Timer to track space between letters and words.
        System.Timers.Timer spacingTimer = new System.Timers.Timer(500);
        public Form1()
        {
            spacingTimer.Elapsed += OnTimedEvent;

            gkh.HookedKeys.Add(Keys.Space);
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Interpret the user's input as either a dit or dah.
        /// </summary>
        /// <param name="difference"></param>
        void ditOrDah(TimeSpan difference)
        {
            // Compare whether difference is greater than 50 milliseconds.
            if (Convert.ToInt32(TimeSpan.Compare(difference, new TimeSpan(0, 0, 0, 0, 500))) == 1)
            {
                letter += "-";
            }
            else
            {
                letter += ".";
            }
            lstLog.Items.Add(letter);
            interpretLetter();
        }

        void interpretLetter()
        {
            // Checks whever to reset the morseCode letter.
            bool addedLetter = true;

            if (spacingTimer.Enabled == false)
            {
                spacingTimer.Start();
            }

            // Check whever it's time to check the letter.
            if (letterComplete)
            {
                switch (letter)
                {
                    case ".-":
                        word += "a";
                        break;
                    case "-...":
                        word += "b";
                        break;
                    case "-.-.":
                        word += "c";
                        break;
                    case "-..":
                        word += "d";
                        break;
                    case ".":
                        word += "e";
                        break;
                    case "..-.":
                        word += "f";
                        break;
                    case "--.":
                        word += "g";
                        break;
                    case "....":
                        word += "h";
                        break;
                    case "..":
                        word += "i";
                        break;
                    case ".---":
                        word += "j";
                        break;
                    case "-.-":
                        word += "k";
                        break;
                    case ".-..":
                        word += "l";
                        break;
                    case "--":
                        word += "m";
                        break;
                    case "-.":
                        word += "n";
                        break;
                    case "---":
                        word += "o";
                        break;
                    case ".--.":
                        word += "p";
                        break;
                    case "--.-":
                        word += "q";
                        break;
                    case ".-.":
                        word += "r";
                        break;
                    case "...":
                        word += "s";
                        break;
                    case "-":
                        word += "t";
                        break;
                    case "..-":
                        word += "u";
                        break;
                    case "...-":
                        word += "v";
                        break;
                    case ".--":
                        word += "w";
                        break;
                    case "-..-":
                        word += "x";
                        break;
                    case "-.--":
                        word += "y";
                        break;
                    case "--..":
                        word += "z";
                        break;
                    default:
                        addedLetter = false;
                        break;
                }
                if (addedLetter)
                {
                    letter = "";
                    lstLog.Items.Add(word);
                }
            }
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            letterComplete = true;
            spacingTimer.Stop();
        }

        /// <summary>
        /// Called when releasing a key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            endPress = DateTime.Now;
            TimeSpan difference = endPress - startPress;
            keyReleased = true;
            ditOrDah(difference);
            e.Handled = true;
        }

        /// <summary>
        /// Called when pressing a key. If the key is already held down this doesn't do anything.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyReleased)
            {
                startPress = DateTime.Now;
                keyReleased = false;
                e.Handled = true;
            }
        }
    }
}
