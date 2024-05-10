using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Prevoznici.xaml
    /// </summary>
    public partial class Prevoznici : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-6TRB9F1\SQLEXPRESS;Initial Catalog=AutobuskaStanica;Integrated Security=True");


        public Prevoznici()
        {
            InitializeComponent();
            UcitajDataGrid();
        }

        private void UcitajDataGrid()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Prevoznik";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Prevoznik");
            dataAdapter.Fill(dataTable);
            PrevoznikDataGrid.ItemsSource = new DataView(dataTable);
            sqlCon.Close();
        }

        private void OcistiPodatke()
        {

            NazivPrevoznikaTxtBox.Text = "";
            AdresaPrevoznikaTxtBox.Text = "";

        }

        private void PrevoznikDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                NazivPrevoznikaTxtBox.Text = dr["NazivPrevoznika"].ToString();
                AdresaPrevoznikaTxtBox.Text = dr["AdresaPrevoznika"].ToString();
            }

        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open(); string query = "INSERT INTO Prevoznik (NazivPrevoznika,AdresaPrevoznika)VALUES(@Naziv, @Adresa)"
                 ;
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Naziv", NazivPrevoznikaTxtBox.Text);
                sqlCmd.Parameters.AddWithValue("@Adresa", AdresaPrevoznikaTxtBox.Text);
                MessageBox.Show("Uspešno ste dodali prevoznika");
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                UcitajDataGrid();
            }
            OcistiPodatke();
        }

       
        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            PocetnaAdmin dashboard = new PocetnaAdmin();
            dashboard.Show();
            this.Close();
        }

        private void btnObrisiSelekciju_Click(object sender, RoutedEventArgs e)
        {
            OcistiPodatke();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "DELETE FROM Prevoznik WHERE NazivPrevoznika = '" + this.NazivPrevoznikaTxtBox.Text + "'";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
              
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Nije moguće obrisati zapis zbog povezanosti sa tabelom Vožnje");
            }
            finally
            {
                sqlCon.Close();
                UcitajDataGrid();
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "UPDATE Prevoznik SET NazivPrevoznika = @NazivPrevoznika, AdresaPrevoznika = @Adresa WHERE AdresaPrevoznika = '" + this.AdresaPrevoznikaTxtBox.Text + "' OR  NazivPrevoznika = '" + this.NazivPrevoznikaTxtBox.Text + "'";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@NazivPrevoznika", NazivPrevoznikaTxtBox.Text);
                sqlCmd.Parameters.AddWithValue("@Adresa", AdresaPrevoznikaTxtBox.Text);
                sqlCmd.CommandType = CommandType.Text;
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());


                if (count == 1)
                {
                    UcitajDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije moguće promeniti naziv zbog povezanosti zapisa sa tabelom Vožnje");
            }
            finally
            {
                sqlCon.Close();
                UcitajDataGrid();
            }
            OcistiPodatke();
        }
    }
}
