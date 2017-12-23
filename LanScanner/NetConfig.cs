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
        static IPGlobalProperties wlasciowosciIP = IPGlobalProperties.GetIPGlobalProperties();
        static string NazwaHosta = wlasciowosciIP.HostName;
        static string NazwaDomeny = wlasciowosciIP.DomainName;

        static List<NetworkInterface> ListaInterfejsow() //Metoda zwraca listę obietów z interfejsami, do właściwości których można się potem odwoływać i pobierać dane
        {
            List<NetworkInterface> Interfejsy = new List<NetworkInterface>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                Interfejsy.Add(nic);
            }
            return Interfejsy;
        }
    }
}
