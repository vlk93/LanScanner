using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanScanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void konsola1_TextChanged(object sender, EventArgs e)
        {
            konsola1.Text += NetConfig.PokazInterfejsy();

        }
    }
}
