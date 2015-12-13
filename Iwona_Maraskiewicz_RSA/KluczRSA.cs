using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iwona_Maraskiewicz_RSA
{
    class KluczRSA
    {
        private int wykladnik;

        public int n { get; private set; }

        public int e
        {
            get { return wykladnik; }
        }

        public int d
        {
            get { return wykladnik; }
        }

        public KluczRSA(int wykladnik, int n)
        {
            this.wykladnik = wykladnik;
            this.n = n;
        }
    }
}
