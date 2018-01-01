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
        private List<IPAddress> listaIP;

        public PingPong (List<IPAddress> ListaIP)
        {
            ListaIP = listaIP;
        }

        public void Ping_Asynch(IPAddress adres, int timeout, byte[] bufor, PingOptions konfig)
        {
            Ping PingPong = new Ping();
            PingPong.PingCompleted += new PingCompletedEventHandler(KoniecPing);

            try
            {
                PingPong.SendAsync(adres.ToString(), timeout, bufor, konfig, null);
            }
            catch (Exception ex) //tu do zrobienia obsługa błędów
            {

            }
        }

        public void KoniecPing(object sender, PingCompletedEventArgs e)
        {
            //tu do zrboienia informacja z wynikiem pingu
        }
    }
}
