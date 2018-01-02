using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LanScanner
{
    class PingPong
    {
        MultiWriter writer;

        public PingPong (MultiWriter Writer)
        {
            writer = Writer;
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
                writer.Write("Błąd: Brak odpowiedzi z " + e.Reply.Address + ":" + odpowiedz.Status);
                ((IDisposable)(Ping)sender).Dispose();
            }

        }
    }
}
