using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace LanScanner
{
    static class NetConfig
    {
        static string nazwaHosta = Dns.GetHostName();
        static IPHostEntry adresyHosta = Dns.GetHostEntry(nazwaHosta); //trzeba przeszukać po wybranym interfejsie
        static string ipHosta;
        

        static void pokazInterfejsy() //pobierze listę interfejsów
        {
            NetworkInterface[] interfejsy = NetworkInterface.GetAllNetworkInterfaces();
            string listaInterfejsy = interfejsy.ToString();
            Console.WriteLine(listaInterfejsy);
            listaInterfejsy.
        }



    }
}
