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
        MultiWriter writer;
        public NetCalculations(MultiWriter Writer)
        {
            writer = Writer;
        }
        public List<IPAddress> GenerujListeIP(IPAddress poczatkowy, IPAddress koncowy)
        {
            List<IPAddress> ListaIP = new List<IPAddress>();
            byte[] start = poczatkowy.GetAddressBytes();
            byte[] end = koncowy.GetAddressBytes();
            IPAddress maska_32;
            maska_32 = IPAddress.Parse("254.254.254.254");
            byte[] maska = maska_32.GetAddressBytes();
            byte[] adres_byte = start;

            if (start[0] > end[0] || start[1] > end[1] || start[2] > end[2] || (start[3] > end[3] && start[2] <= end[2]))
            {
                writer.Write("Niepoprawna kombinacja adresów początkowego i końcowego");
                return null;
            }
            else
            {
                for (byte oktet1 = start[0]; oktet1 <= end[0]; oktet1++)
                    for (byte oktet2 = start[1]; oktet2 <= end[1]; oktet2++)
                        for (byte oktet3 = start[2]; oktet3 <= end[2]; oktet3++)
                            for (byte oktet4 = start[3]; oktet4 <= maska[3]; oktet4++)
                            {
                                if (oktet4 > end[3] && oktet3 == end[2])
                                {
                                    continue;
                                }
                                else
                                {
                                    IPAddress adres = new IPAddress(new byte[] { oktet1, oktet2, oktet3, oktet4 });
                                    ListaIP.Add(adres);
                                }
                            }
                return ListaIP;
            }
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

        public static IPAddress ObliczMaskePodsieci(IPAddress adres, NetworkInterface adapter)
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

        public static IPAddress ObliczAdresSieci(IPAddress adres, IPAddress maskaPodsieci)
        {
            byte[] adresWlasny = adres.GetAddressBytes();
            byte[] maska = maskaPodsieci.GetAddressBytes();

            byte[] adresSieci = new byte[adresWlasny.Length];
            for (int i = 0; i < adresSieci.Length; i++)
            {
                adresSieci[i] = (byte)(adresWlasny[i] & (maska[i]));
            }
            return new IPAddress(adresSieci);
        }

    }
}
