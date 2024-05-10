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
using System.Data.Common;
using System.Reflection;

namespace AS.Korisnik
{
    /// <summary>
    /// Interaction logic for KorisnickiPrikaz.xaml
    /// </summary>
    public partial class KorisnickiPrikaz : Window
    {
        private int idK;
        private int idV;
        private string Prevoznik;
        private string Od;
        private string Do;
        private string Pregled;

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-6TRB9F1\SQLExpress;Initial Catalog=AutobuskaStanica;Integrated Security=True");
        public KorisnickiPrikaz(int Id)
        {
            InitializeComponent();
            UcitajDataGrid();
            this.idK = Id;
           
        }

        private void UcitajDataGrid()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Voznja WHERE cast (Datum as Date)= cast (GETDATE() as Date) OR (Datum > GETDATE())";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Voznja");
            dataAdapter.Fill(dataTable);
            VoznjeDataGrid.ItemsSource = new DataView(dataTable);
            VoznjeDataGrid.Columns[0].Visibility = Visibility.Hidden;

            sqlCon.Close();
        }


        private void btnRezervacijaKarte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "INSERT INTO Rezervacija (idKorisnika, idVoznje) VALUES(@idK, @idV)";
                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@idK", idK);
                sqlCmd.Parameters.AddWithValue("@idV", idV);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            PotvrdaRezervacije dashboard = new PotvrdaRezervacije(Prevoznik, Do, Od, idK);
            dashboard.Show();
            this.Close();
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava dashboard = new Prijava();
            dashboard.Show();
            this.Close();
        }

        private void VoznjeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                idV = int.Parse(dr["idVoznje"].ToString());
                Prevoznik = dr["NazivPrevoznika"].ToString();
                Od = dr["MestoPolaska"].ToString();
                Do = dr["MestoDolaska"].ToString();

            }

            Pregled = "PREVOZNIK -> " + Prevoznik + " \n OD ->  " + Od + " \n DO -> " + Do;

            PregledTxtBox.Text = Pregled.ToString();
        }
    }
}
