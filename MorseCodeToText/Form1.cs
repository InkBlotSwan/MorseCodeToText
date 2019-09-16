using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        // String message variables.
        string word;
        string letter;
        public Form1()
        {
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
            bool addLetter = true;
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
                    addLetter = false;
                    break;
            }
            if (addLetter)
            {
                letter = "";
                lstLog.Items.Add(word);
            }
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
            lstLog.Items.Add("Up\t" + e.KeyCode.ToString());
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
                lstLog.Items.Add("Down\t" + e.KeyCode.ToString());
                keyReleased = false;
                e.Handled = true;
            }
        }
    }
}
