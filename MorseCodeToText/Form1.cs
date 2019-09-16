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
        string morseString;
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
