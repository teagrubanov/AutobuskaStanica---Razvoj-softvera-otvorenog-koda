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
    /// Interaction logic for Rezervacije.xaml
    /// </summary>
    public partial class Rezervacije : Window
    {

        private int idK, idV;

        public Rezervacije()
        {
            InitializeComponent();
            UcitajDataGrid();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-6TRB9F1\SQLEXPRESS;Initial Catalog=AutobuskaStanica;Integrated Security=True");

        private void UcitajDataGrid()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT a.idVoznje as idVoznje, a.NazivPrevoznika, a.Datum, a.Vreme ,a.MestoPolaska, a.MestoDolaska, k.idKorisnika as idKorisnika, (k.Ime+' '+k.Prezime) as Korisnik, r.Status     FROM Voznja a, Rezervacija r, Korisnik k WHERE k.idKorisnika = r.idKorisnika AND a.idVoznje = r.idVoznje and (cast (Datum as Date)= cast (GETDATE() as Date) OR (Datum > GETDATE())) and r.Status is null";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Rezervacija");
            dataAdapter.Fill(dataTable);
            RezervacijeDataGrid.ItemsSource = new DataView(dataTable);
            sqlCon.Close();
        }

        private void btnIstekleRezervacije_Click(object sender, RoutedEventArgs e)
        {
            IstekleRezervacije dashboard = new IstekleRezervacije();
            dashboard.Show();
            this.Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "UPDATE Rezervacija SET Status = 'Otkazano' where IDKorisnika = " + idK + " AND IDVoznje = " + idV + "";
                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Uspešno otkazana Rezervacija");
                sqlCon.Close();
                UcitajDataGrid();
            }

        }

        private void RezervacijeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                idV = int.Parse(dr["idVoznje"].ToString());
                idK = int.Parse(dr["idKorisnika"].ToString());
            }
        }

        private void btnOtkazaneRezervacije_Click(object sender, RoutedEventArgs e)
        {
            OtkazaneRezervacije dashboard = new OtkazaneRezervacije();
            dashboard.Show();
            this.Close();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            PocetnaAdmin dashboard = new PocetnaAdmin();
            dashboard.Show();
            this.Close();

        }
    }
}
