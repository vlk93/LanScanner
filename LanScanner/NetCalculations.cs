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
        public List<IPAddress> GenerujListeIP(IPAddress poczatkowy, IPAddress koncowy)
        {
            List<IPAddress> ListaIP = new List<IPAddress>();
            byte[] start = poczatkowy.GetAddressBytes();
            byte[] end = koncowy.GetAddressBytes();

            for (byte oktet1 = start[0]; oktet1 <= end[0]; oktet1++)
                for (byte oktet2 = start[1]; oktet2 <= end[1]; oktet2++)
                    for (byte oktet3 = start[2]; oktet3 <= end[2]; oktet3++)
                        for (byte oktet4 = start[3]; oktet4 <= end[3]; oktet4++)
                        {
                            IPAddress adres = new IPAddress(new byte[] { oktet1, oktet2, oktet3, oktet4 });
                            ListaIP.Add(adres);
                        }

            return ListaIP;
        }

        public static IPAddress ObliczAdresBroadcast (IPAddress adresWlasny, IPAddress maskaPodsieci)
        {
            //if (adresWlasny.GetAddressBytes().Length != maskaPodsieci.GetAddressBytes().Length)
                //throw new ArgumentException("Lengths of IP address and subnet mask do not match.");
                // Tu też obsługa błędów do zrobienia

            byte[] adresBroadcast = new byte[adresWlasny.GetAddressBytes().Length];
            for (int i = 0; i < adresBroadcast.Length; i++)
            {
                adresBroadcast[i] = (byte)(adresWlasny.GetAddressBytes()[i] | (maskaPodsieci.GetAddressBytes()[i] ^ 255));
            }
            return new IPAddress(adresBroadcast);
        }

        static IPAddress ObliczMaskePodsieci(IPAddress adres, NetworkInterface adapter)
        {
            foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
            {
                if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (adres.Equals(unicastIPAddressInformation.Address))
                    {
                        return unicastIPAddressInformation.IPv4Mask;
                    }
                }
            }
            return null; //do zrobienia obsługa błędów
        }
    }
}
