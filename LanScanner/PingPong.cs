using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace LanScanner
{
    class PingPong
    {
        MultiWriter writer;
        ProgressBar progres;

        public PingPong (MultiWriter Writer, ProgressBar Progres)
        {
            writer = Writer;
            progres = Progres;
        }

        public void Ping_Asynch(IPAddress adres, int timeout, byte[] bufor, PingOptions konfig)
        {
            Ping PingPong = new Ping();
            PingPong.PingCompleted += new PingCompletedEventHandler(KoniecPing);

            try
            {
                PingPong.SendAsync(adres.ToString(), timeout= 120, bufor, konfig, null);
            }
            catch (Exception ex) //tu do zrobienia obsługa błędów
            {
                writer.Write("Błąd podczas wysyłania pingu. Adres docelowy:" + adres.ToString() + " Kod błędu: " + ex.Message);
            }
        }

        public void KoniecPing(object sender, PingCompletedEventArgs e)
        {
            progres.Value++;
            progres.Refresh();
            if (e.Cancelled || e.Error != null)
            {
                writer.Write("Operacja anulowana bądź nieprawidłowy adres.");
                ((IDisposable)(Ping)sender).Dispose();
                return;
            }
            PingReply odpowiedz = e.Reply;
            if (odpowiedz.Status == IPStatus.Success)
            {
                writer.Write("Odpowiedź z " + odpowiedz.Address.ToString() + " bajtów = " + odpowiedz.Buffer.Length + " czas = " + odpowiedz.RoundtripTime + "ms TTL=" + odpowiedz.Options.Ttl);
            }
            else
            {
                writer.Write("Błąd: Brak odpowiedzi z " + odpowiedz.Address.ToString() + ":" + odpowiedz.Status);
                ((IDisposable)(Ping)sender).Dispose();
            }
        }

        private string Ping_Synch(string adres, int timeout, byte[] bufor, PingOptions opcje)
        {
            Ping ping = new Ping();
            try
            {
                PingReply odpowiedz = ping.Send(adres, timeout, bufor, opcje);
                if (odpowiedz.Status == IPStatus.Success)
                {
                    return "Odpowiedź z " + adres + " bajtów=" + odpowiedz.Buffer.Length + " czas=" + odpowiedz.RoundtripTime + "ms TTL=" + odpowiedz.Options.Ttl;
                }
                else
                {
                    return "Brak odpowiedzi z: " + adres + " " + odpowiedz.Status.ToString();
                }
            }
            catch (Exception ex)
            {
                return "Błąd podczas wysyłania pingu: " + adres + " " + ex.Message;
            }
        }

        public void Ping_Wielowatkowy(List<IPAddress> adresy, int timeout, byte[] bufor, PingOptions konfig)
        {
            List<string> odpowiedzi = new List<string>();
            List<Thread> watki = new List<Thread>();

            foreach (IPAddress adres in adresy)
            {
                watki.Add(new Thread(() => {lock(odpowiedzi) odpowiedzi.Add(Ping_Synch(adres.ToString(), timeout, bufor, konfig)); } ));
                progres.Value += (progres.Maximum - progres.Value) / watki.Count;
                progres.Refresh();
            }

            foreach (Thread watek in watki)
            {
                watek.Start();
            }
            
            //Thread.Sleep(timeout * (watki.Count + 3)); //czekam z wyświetlaniem wyników do momentu upłynięcia timeout-ów na pingach

            writer.Write("Koniec wątków");

            foreach (string odpowiedz in odpowiedzi)
            {
                writer.Write(odpowiedz);
            }
        }
    }
}
