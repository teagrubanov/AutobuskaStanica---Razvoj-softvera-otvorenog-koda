using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Interaction logic for IstekleRezervacije.xaml
    /// </summary>
    public partial class IstekleRezervacije : Window
    {

        private int idK, idV;

        public IstekleRezervacije()
        {
            InitializeComponent();
            UcitajDataGrid();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-6TRB9F1\SQLEXPRESS;Initial Catalog=AutobuskaStanica;Integrated Security=True");

        private void UcitajDataGrid()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT a.idVoznje as idVoznje, a.NazivPrevoznika, a.Datum, a.Vreme ,a.MestoPolaska, a.MestoDolaska, k.idKorisnika as idKorisnika, (k.Ime+' '+k.Prezime) as Korisnik, r.Status FROM Voznja a, Rezervacija r, Korisnik k WHERE k.idKorisnika = r.idKorisnika AND a.idVoznje = r.idVoznje and  (cast (Datum as Date) != cast (GETDATE() as Date) AND (Datum < GETDATE()))";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Rezervacija");
            dataAdapter.Fill(dataTable);
            RezervacijeDataGrid.ItemsSource = new DataView(dataTable);
            sqlCon.Close();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            Rezervacije dashboard = new Rezervacije();
            dashboard.Show();
            this.Close();

        }
    }
}
