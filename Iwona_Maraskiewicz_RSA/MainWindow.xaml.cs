using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Iwona_Maraskiewicz_RSA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KluczRSA publiczny;
        KluczRSA prywatny;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Klucz_Click(object sender, RoutedEventArgs e)
        {
            if (TextP.Text != "" && TextQ.Text != "") // to sie wykona tylko i wylacznie wtedy, kiedy pola P i Q maja jakies wartosci
            {
                int numberP = Int32.Parse(TextP.Text);
                int numberQ = Int32.Parse(TextQ.Text);

                RSA.WygenerujKluczePQ(numberP, numberQ, out publiczny, out prywatny);

                string PublicznyE = publiczny.e.ToString();
                string PublicznyN = publiczny.n.ToString();
                string PrywatnyD = prywatny.d.ToString();
                string PrywatnyN = prywatny.n.ToString();
                TextEPublicznyPQ.Text = PublicznyE;
                TextNPublicznyPQ.Text = PublicznyN;
                TextDPrywatnyPQ.Text = PrywatnyD;
                TextNPrywatnyPQ.Text = PrywatnyN;
            }

            if (TextMin.Text != "" && TextMax.Text != "") // to sie wykona tylko i wylacznie wtedy, kiedy pola Min i Max maja jakies wartosci
            {
                int numberMin = Int32.Parse(TextMin.Text);
                int numberMax = Int32.Parse(TextMax.Text);

                RSA.WygenerujKlucze(numberMin, numberMax, out publiczny, out prywatny);

                string PublicznyD = publiczny.d.ToString();
                string PublicznyN = publiczny.n.ToString();
                string PrywatnyD = prywatny.d.ToString();
                string PrywatnyN = prywatny.n.ToString();
                TextDPubliczny.Text = PublicznyD;
                TextNPubliczny.Text = PublicznyN;
                TextDPrywatny.Text = PrywatnyD;
                TextNPrywatny.Text = PrywatnyN;
            }
        }

        private void Szyfruj_Click(object sender, RoutedEventArgs e)
        {
            if (publiczny != null) TextZaszyfrowany.Text = RSA.szyfruj(TextOdszyfrowany.Text, publiczny); 
        }

        private void Deszyfruj_Click(object sender, RoutedEventArgs e)
        {
            if (prywatny != null) TextOdszyfrowany.Text = RSA.deszyfruj(TextZaszyfrowany.Text, prywatny); 
        }

    }
}
