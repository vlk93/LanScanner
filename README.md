# LanScanner
Simple LAN scaner in C#
Prosty skaner sieci lokalnej w C#

## Jak uruchomiæ?

Wystarczy uruchomiæ plik LanScanner.exe
Instalacja nie jest konieczna.

## Jak przetestowaæ?

Nale¿y uruchomiæ plik LanScanner.exe oraz postêpowaæ zgodnie z instrukcjami.

![My image](/screen.JPG)

## Opis dzia³ania

Aplikacja posiada 2 tryby - automatyczny i rêczny.
W ramach rêcznego u¿ytkownik podaje pocz¹tkowy i koñcowy adres IP. W ramach automatycznego, aplikacja wyœwietla listê interfejsów i adresów IP na interfejsach. 
Po dokonaniu wyboru, program obliczy maskê podsieci, adres sieci oraz adres broadcast i na ich podstawie rozpocznie ping asynchroniczny lub wielow¹tkowy.

## ToDo

* pobieranie adresów MAC z tablicy ARP Windows-a i dopasowywanie ich do znalezionych w sieci adresów IP
* szersza obs³uga b³êdów

## Wykorzystane Ÿród³a

* https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/
* Or³owski S. - C#, Tworzenie aplikacji sieciowych. Gotowe projekty.

