# LanScanner
Simple LAN scaner in C#
Prosty skaner sieci lokalnej w C#

## Jak uruchomi�?

Wystarczy uruchomi� plik LanScanner.exe
Instalacja nie jest konieczna.

## Jak przetestowa�?

Nale�y uruchomi� plik LanScanner.exe oraz post�powa� zgodnie z instrukcjami.

![My image](/screen.JPG)

## Opis dzia�ania

Aplikacja posiada 2 tryby - automatyczny i r�czny.
W ramach r�cznego u�ytkownik podaje pocz�tkowy i ko�cowy adres IP. W ramach automatycznego, aplikacja wy�wietla list� interfejs�w i adres�w IP na interfejsach. 
Po dokonaniu wyboru, program obliczy mask� podsieci, adres sieci oraz adres broadcast i na ich podstawie rozpocznie ping asynchroniczny lub wielow�tkowy.

## ToDo

* pobieranie adres�w MAC z tablicy ARP Windows-a i dopasowywanie ich do znalezionych w sieci adres�w IP
* szersza obs�uga b��d�w

## Wykorzystane �r�d�a

* https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/
* Or�owski S. - C#, Tworzenie aplikacji sieciowych. Gotowe projekty.

