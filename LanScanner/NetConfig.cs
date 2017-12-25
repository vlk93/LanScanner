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

        static List<IPAddress> ListaIPwSieci(NetworkInterface nic) //metoda zwraca listę obiektów klasy IPAddress, bo jak się okazuje może być więcej niż jeden IP per NIC
        {
            List<IPAddress> adresyWlasne = new List<IPAddress>();
            foreach (IPAddress ipek in nic.GetIPProperties().WinsServersAddresses)
            {
                adresyWlasne.Add(ipek);
            }
            return adresyWlasne;
        }


    }
}
