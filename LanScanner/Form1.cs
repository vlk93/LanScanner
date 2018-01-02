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
        /*Wszystkie pola statyczne niezbędne do ustalenie lokalizacji pliku z logiem
         * zmienna random jest ustawiana jednorazowo, wraz ze startem programu
         * zmienna sciezka_log przechowuje zawsze pełną ścieżkę do pliku z logiem
         */

        static DateTime data = DateTime.Today;
        static Random randomizer = new Random();
        static int random = randomizer.Next(300000, 999999);
        static string sciezka_log = @"log_" + data.ToString("dd-MM-yyyy_") + random.ToString() + ".txt";

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

            MultiWriter Writer = new MultiWriter(textBox1, sciezka_log); //Powitalny wpis w logu
            Writer.Write("Witamy w programie LAN Scanner!");
            Writer.Write("Wybierz tryb ręczny lub automatyczny, by rozpocząć.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MultiWriter writer = new MultiWriter(textBox1, sciezka_log);
            NetCalculations Obliczenia = new NetCalculations(writer); //inicjalizacja obiektu niezbędna do przeprowadzenia obliczeń
            IPAddress poczatkowy = null;
            IPAddress koncowy = null;
            List<IPAddress> listaIP;
            string dane = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] bufor = Encoding.ASCII.GetBytes(dane);
            int timeout = 120;
            PingOptions opcje = new PingOptions();

            writer.Write("Wybrano tryb ręczny");

            try
            {
                poczatkowy = IPAddress.Parse(textBox2.Text);     
            }
            catch (Exception)
            {
                MessageBox.Show("Adres początkowy: wprowadzono nieprawidłową wartość.");
                writer.Write("Adres początkowy: wprowadzono nieprawidłową wartość."); 
            }
            writer.Write("Wpisano IP początkowe: " + poczatkowy.ToString());

            try
            {
                koncowy = IPAddress.Parse(textBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Adres końcowy: wprowadzono nieprawidłową wartość.");
                writer.Write("Adres końcowy: wprowadzono nieprawidłową wartość.");
            }
            writer.Write("Wpisano IP końcowe: " + koncowy.ToString());

            listaIP = Obliczenia.GenerujListeIP(poczatkowy, koncowy);
            writer.Write("Wygenerowano następującą listę IP:");

            foreach (IPAddress adres in listaIP)
            {
                writer.Write(adres.ToString());
            }

            PingPong pingowanie = new PingPong(writer);

            foreach (IPAddress adres in listaIP)
            {
                pingowanie.Ping_Asynch(adres, timeout, bufor, opcje);
            }
        }
    }
}
