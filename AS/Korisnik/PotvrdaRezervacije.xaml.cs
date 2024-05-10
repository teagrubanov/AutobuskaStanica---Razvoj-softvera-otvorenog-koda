using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AS.Korisnik
{
    /// <summary>
    /// Interaction logic for PotvrdaRezervacije.xaml
    /// </summary>
    public partial class PotvrdaRezervacije : Window
    {
        public string Prevoznik, Od, Do;
        public int idK;
        public string Voznja;

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            KorisnickiPrikaz dashboard = new KorisnickiPrikaz(idK);
            dashboard.Show();
            this.Close();
        }

        public PotvrdaRezervacije(string Prevoznik, string Od, string Do, int idK)
        {
            InitializeComponent();
            this.Prevoznik = Prevoznik;
            this.Od = Od;
            this.Do = Do; ;
            this.idK = idK;

            Pregled();
        }

        public void Pregled()
        {
            Voznja = Do + " -> " + Od;

            PrevoznikTxtBox.Text = Prevoznik.ToString();
            VoznjaTxtBox.Text = Voznja.ToString();

        }
    }
}
