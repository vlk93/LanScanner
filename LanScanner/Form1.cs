using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LanScanner
{
    public partial class Form1 : Form
    {
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Form1()
        {
            InitializeComponent();
            DateTime data = DateTime.Today;
            Random randomizer = new Random();
            int random = randomizer.Next(300000, 999999);
            MultiWriter Writer = new MultiWriter(textBox1, @"log_" + data.ToString("dd-MM-yyyy_") + random.ToString() + ".txt");
            Writer.Write("Witamy w programie LAN Scanner!");
            Writer.Write("Wybierz tryb ręczny lub automatyczny, by rozpocząć.");
            Writer.Write("test test test");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetCalculations Obliczenia = new NetCalculations();
            IPAddress poczatkowy;
            IPAddress koncowy;

            try
            {
                poczatkowy = IPAddress.Parse(textBox2.Text);
            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                koncowy = IPAddress.Parse(textBox3.Text);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
