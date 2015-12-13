using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Iwona_Maraskiewicz_RSA
{
    class RSA
    {
        // wykonuje alogrytm sita erastotenesa na liczbach z przedzialu <0,n), zwraca liczby pierwsze z tego przedzialu
        public static List<int> sitoErastotenesa(int n)
        {
            return sitoErastotenesa(0, n);
        }

        // wykonuje alogrytm sita erastotenesa na liczbach z przedzialu <0,n), zwraca liczby pierwsze z przedzialu <min,n)
        public static List<int> sitoErastotenesa(int min, int n)
        {
            List<int> liczbyPierwsze = new List<int>();
            bool[] liczby = new bool[n];
            for (int i = 2; i < liczby.Length; ++i)
            {
                liczby[i] = true;
            }
            liczby[0] = false;
            liczby[1] = false;
            double pierwiastek = Math.Floor(Math.Sqrt(n));
            for (int i = 2; i <= pierwiastek; ++i)
            {
                if (liczby[i])
                {
                    for (int j = i * 2; j < liczby.Length; j += i)
                    {
                        liczby[j] = false;
                    }
                }
            }
            for (int i = min; i < liczby.Length; ++i)
            {
                if (liczby[i]) liczbyPierwsze.Add(i);
            }
            return liczbyPierwsze;
        }

        // generuje klucz publiczny i klucz prywatny dla pary liczb pierwszych p i q
        public static void WygenerujKluczePQ(int p, int q, out KluczRSA kluczPubliczny, out KluczRSA kluczPrywatny)
        {
            Random random = new Random();
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            int e = 1;
            int d;
            int nwd;
            do
            {
                e = random.Next(2, phi);
                nwd = NWD(phi, e, out d);
                if (d <= 1) d += phi;
            } while (nwd != 1);
            kluczPubliczny = new KluczRSA(e, n);
            kluczPrywatny = new KluczRSA(d, n);
        }

        //generuje klucz publiczny i prywatny dla dwoch losowych liczb pierwszych z przedzialu <min,max) roznych od siebie
        public static void WygenerujKlucze(int min, int max, out KluczRSA kluczPubliczny, out KluczRSA kluczPrywatny)
        {
            Random random = new Random();

            List<int> liczbyPierwsze = sitoErastotenesa(min, max);
            //wylosowanie p
            int p = liczbyPierwsze[random.Next(0, liczbyPierwsze.Count)];
            int q;
            //losowanie q tak dlugo az p bedzie rozne od q
            do
            {
                q = liczbyPierwsze[random.Next(0, liczbyPierwsze.Count)];
            } while (p == q);

            // wygenerowanie kluczy dla p i q
            WygenerujKluczePQ(p, q, out kluczPubliczny, out kluczPrywatny);
        }

        /*
         * Oblicza NWD Rozszerzonym Algorytmem Euklidesa
         *   http://www.algorytm.org/algorytmy-arytmetyczne/rozszerzony-algorytm-euklidesa.html
        */
        public static int NWD(int a, int b, out int y)
        {
            int q = a / b;
            int r = a % b;
            int nwd = b;
            int x2 = 1;
            int x1 = 0;
            int x = 1;
            int y2 = 0;
            int y1 = 1;
            y = q - 1;

            while (r != 0)
            {
                a = b;
                b = r;
                x = x2 - q * x1;
                y = y2 - q * y1;
                x2 = x1;
                y2 = y1;
                x1 = x;
                y1 = y;
                nwd = r;
                q = a / b;
                r = a % b;
            }

            return nwd;
        }

        public static string szyfruj(string wiadomosc, KluczRSA klucz)
        {
            string zaszyfrowanaWiadomosc = "";
            char[] znaki = wiadomosc.ToCharArray();
            byte[] bajty = Encoding.ASCII.GetBytes(znaki);
            for (int i = 0; i < znaki.Length; ++i)
            {
                BigInteger m = bajty[i];
                BigInteger c = BigInteger.ModPow(m, klucz.e, klucz.n);
                if (i > 0) zaszyfrowanaWiadomosc += ' ';
                zaszyfrowanaWiadomosc += c;
            }
            return zaszyfrowanaWiadomosc;
        }

        public static string deszyfruj(string wiadomosc, KluczRSA klucz)
        {
            string odszyfrowanaWiadomosc = "";
            string[] zaszyfrowaneWartosci = wiadomosc.Split(' ');
            for (int i = 0; i < zaszyfrowaneWartosci.Length; ++i)
            {
                try
                {
                    BigInteger c = BigInteger.Parse(zaszyfrowaneWartosci[i]);
                    BigInteger m = BigInteger.ModPow(c, klucz.d, klucz.n);
                    byte[] mBytes = m.ToByteArray();
                    odszyfrowanaWiadomosc += new string(Encoding.ASCII.GetChars(mBytes));
                }
                catch (FormatException e)
                {

                }
                catch (ArgumentNullException e)
                {

                }

            }
            return odszyfrowanaWiadomosc;
        }
    }
}
