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

namespace AS.Admin
{
    /// <summary>
    /// Interaction logic for PocetnaAdmin.xaml
    /// </summary>
    public partial class PocetnaAdmin : Window
    {
        public PocetnaAdmin()
        {
            InitializeComponent();
        }

        private void btnVoznje_Click(object sender, RoutedEventArgs e)
        {

            Voznje dashboard = new Voznje();
            dashboard.Show();
            this.Close();
        }

        private void btnPrevoznici_Click(object sender, RoutedEventArgs e)
        {
            Prevoznici dashboard = new Prevoznici();
            dashboard.Show();
            this.Close();  
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava dashboard = new Prijava();
            dashboard.Show();
            this.Close();
        }

        private void btnRezervacije_Click(object sender, RoutedEventArgs e)
        {
            Rezervacije dashboard = new Rezervacije();
            dashboard.Show();
            this.Close();

        }
    }
}
