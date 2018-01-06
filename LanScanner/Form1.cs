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
        NetworkInterface wybranyInterfejs;
        List<IPAddress> adresyWlasne = new List<IPAddress>();
        IPAddress wybranyIpek;
        int licznik = 0; //wprowadzony by uniknąć zbędnych wywyołań eventu odpowiedzialnego za wybór wartości w lisbox1
        

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
            NetCalculations obliczenia = new NetCalculations(writer); //inicjalizacja obiektu niezbędna do przeprowadzenia obliczeń
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

            listaIP = obliczenia.GenerujListeIP(poczatkowy, koncowy);
            writer.Write("Wygenerowano następującą listę IP:");
            progressBar1.Maximum = listaIP.Count;

            foreach (IPAddress adres in listaIP)
            {
                writer.Write(adres.ToString());
            }

            writer.Write("Rozpoczynam ping");

            PingPong pingowanie = new PingPong(writer, progressBar1);

            foreach (IPAddress adres in listaIP)
            {
                pingowanie.Ping_Asynch(adres, timeout, bufor, opcje);
            }
            if (progressBar1.Value >= progressBar1.Maximum)
                writer.Write("Zakończono");
        }

        public void button2_Click(object sender, EventArgs e)
        {
            List<NetworkInterface> interfejsy = new List<NetworkInterface>();

            MultiWriter writer = new MultiWriter(textBox1, sciezka_log);

            interfejsy = NetConfig.ListaInterfejsow();
            listBox1.DataSource = interfejsy;
            listBox1.DisplayMember = "name";
            
        }


        private void listBox1_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (licznik <= 1) //warunek konieczny by uniknąć zbędnych wywołań eventu. Zrobiony w związku z tmy, że nie znalazłe właściwości odpowiedzialnej za domyślną wartość listbox-a.
            {
                licznik++; 
            }
            else
            {
                MultiWriter writer = new MultiWriter(textBox1, sciezka_log);
                wybranyInterfejs = (NetworkInterface)listBox1.SelectedItem;
                writer.Write("Wybrano Interfejs: " + wybranyInterfejs.Name);
                if (wybranyInterfejs == null)
                {
                    writer.Write("Ne wybrano interfejsu");
                }
                else
                {
                    adresyWlasne = NetConfig.ListaIPwlasne(wybranyInterfejs);
                }

                listBox2.DataSource = adresyWlasne;
                listBox1.DisplayMember = "ToString";
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiWriter writer = new MultiWriter(textBox1, sciezka_log);
            wybranyIpek = (IPAddress)listBox2.SelectedItem;
            writer.Write("Wybrany adres IP to: " + wybranyIpek.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MultiWriter writer = new MultiWriter(textBox1, sciezka_log);
            NetCalculations obliczenia = new NetCalculations(writer);
            writer.Write("Rozpoczynam obliczenia");
            IPAddress maskaPodsieci;
            IPAddress adresSieci;
            IPAddress adresBroadcast;
            string dane = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] bufor = Encoding.ASCII.GetBytes(dane);
            int timeout = 120;
            PingOptions opcje = new PingOptions();
            List<IPAddress> adresyLAN = new List<IPAddress>();

            maskaPodsieci = NetCalculations.ObliczMaskePodsieci(wybranyIpek, wybranyInterfejs).MapToIPv4();
            writer.Write("Maska podsieci to: " + maskaPodsieci);

            adresSieci = NetCalculations.ObliczAdresSieci(wybranyIpek, maskaPodsieci);
            writer.Write("Adres sieci to: " + adresSieci);

            adresBroadcast = NetCalculations.ObliczAdresBroadcast(wybranyIpek, maskaPodsieci);
            writer.Write("Maska Podsieci to" + adresBroadcast);

            adresyLAN = obliczenia.GenerujListeIP(adresSieci, adresBroadcast);
            progressBar1.Maximum = adresyLAN.Count;

            PingPong pingowanie = new PingPong(writer, progressBar1);
            writer.Write("Rozpoczynam pingowanie");

            foreach (IPAddress ipek in adresyLAN)
            {
                pingowanie.Ping_Asynch(ipek, timeout, bufor, opcje);
            }

            writer.Write("Koniec pingu");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            MultiWriter writer = new MultiWriter(textBox1, sciezka_log);
            NetCalculations obliczenia = new NetCalculations(writer);
            writer.Write("Rozpoczynam obliczenia");
            IPAddress maskaPodsieci;
            IPAddress adresSieci;
            IPAddress adresBroadcast;
            string dane = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] bufor = Encoding.ASCII.GetBytes(dane);
            int timeout = 120;
            PingOptions opcje = new PingOptions();
            List<IPAddress> adresyLAN = new List<IPAddress>();

            maskaPodsieci = NetCalculations.ObliczMaskePodsieci(wybranyIpek, wybranyInterfejs).MapToIPv4();
            writer.Write("Maska podsieci to: " + maskaPodsieci);

            adresSieci = NetCalculations.ObliczAdresSieci(wybranyIpek, maskaPodsieci);
            writer.Write("Adres sieci to: " + adresSieci);

            adresBroadcast = NetCalculations.ObliczAdresBroadcast(wybranyIpek, maskaPodsieci);
            writer.Write("Maska Podsieci to" + adresBroadcast);

            adresyLAN = obliczenia.GenerujListeIP(adresSieci, adresBroadcast);
            progressBar1.Maximum = adresyLAN.Count;

            PingPong pingowanie = new PingPong(writer, progressBar1);
            writer.Write("Rozpoczynam pingowanie");

            pingowanie.Ping_Wielowatkowy(adresyLAN, timeout, bufor, opcje);


        }
    }
}
