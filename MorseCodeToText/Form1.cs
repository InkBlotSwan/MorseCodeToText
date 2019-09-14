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
        globalKeyboardHook gkh = new globalKeyboardHook();
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

        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            lstLog.Items.Add("Up\t" + e.KeyCode.ToString());
            e.Handled = true;
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            lstLog.Items.Add("Down\t" + e.KeyCode.ToString());
            e.Handled = true;
        }
    }
}
