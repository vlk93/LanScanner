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
    class NetCalculations
    {
        public List<IPAddress> ListaIPwSieci(NetworkInterface nic) //metoda zwraca listę obiektów klasy IPAddress, bo jak się okazuje może być więcej niż jeden IP per NIC
        {
            List<IPAddress> adresyWlasne = new List<IPAddress>();
            foreach (IPAddress ipek in nic.GetIPProperties().WinsServersAddresses)
            {
                adresyWlasne.Add(ipek);
            }
            return adresyWlasne;
        }

        public IPAddress PodajMaske(NetworkInterface nic)
        {
            IPAddress adresMaski;
            foreach (UnicastIPAddressInformation unicastAddress in nic.GetIPProperties().UnicastAddresses)
            {
                if (unicastAddress != null && unicastAddress.Address != null && unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    adresMaski = unicastAddress.IPv4Mask;
                    return unicastAddress.Address;
                }
            }
            return null;
        }
    }
}
