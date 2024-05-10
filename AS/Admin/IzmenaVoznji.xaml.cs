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
    /// Interaction logic for IzmenaVoznji.xaml
    /// </summary>
    public partial class IzmenaVoznji : Window
    {
        private int ID;

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-6TRB9F1\SQLEXPRESS;Initial Catalog=AutobuskaStanica;Integrated Security=True");

        private List<string> getPrevoznik()
        {
            List<string> results = new List<string>();
            DataSet Prevoznici = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("SELECT NazivPrevoznika FROM Prevoznik", sqlCon);

                adapter.Fill(Prevoznici);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            foreach (DataRow row in Prevoznici.Tables[0].Rows)
            {
                results.Add((string)row["NazivPrevoznika"]);
            }
            return results;
        }

        public IzmenaVoznji()
        {
            InitializeComponent();
            UcitajDataGrid();
            PrevoznikComboBox.ItemsSource = getPrevoznik();
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
            sqlCon.Close();
        }

        private void OcistiPodatke()
        {
            PrevoznikComboBox.SelectedIndex = -1;
            DatumDatePicker.Text = "";
            VremepolTxtBox.Text = "";
            MestoPolaskaTxtBox.Text = "";
            MestoDolaskaTxtBox.Text = "";

        }

        private void VoznjeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                ID = int.Parse(dr["idVoznje"].ToString());
                PrevoznikComboBox.SelectedValue = dr["NazivPrevoznika"].ToString();
                DatumDatePicker.Text = dr["Datum"].ToString();
                VremepolTxtBox.Text = dr["Vreme"].ToString();
                MestoPolaskaTxtBox.Text = dr["MestoPolaska"].ToString();
                MestoDolaskaTxtBox.Text = dr["MestoDolaska"].ToString();

            }

        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "INSERT INTO Voznja(NazivPrevoznika, Datum, Vreme, MestoPolaska, MestoDolaska) VALUES(@NazivPrevoznika, @Datum, @Vreme, @MestoPolaska, @MestoDolaska)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@NazivPrevoznika", PrevoznikComboBox.SelectedItem);
                sqlCmd.Parameters.AddWithValue("@Datum", DatumDatePicker.SelectedDate);
                sqlCmd.Parameters.AddWithValue("@Vreme", VremepolTxtBox.Text);
                sqlCmd.Parameters.AddWithValue("@MestoPolaska", MestoPolaskaTxtBox.Text);
                sqlCmd.Parameters.AddWithValue("@MestoDolaska", MestoDolaskaTxtBox.Text);

                MessageBox.Show("Podaci su uspešno snimljeni");

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

        private void btnIzmena_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "UPDATE Voznja SET NazivPrevoznika = @NazivPrevoznika, Datum = @Datum, Vreme = @Vreme ,MestoPolaska = @MestoPolaska, MestoDolaska = @MestoDolaska WHERE idVoznje = " + ID + "";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@NazivPrevoznika", PrevoznikComboBox.SelectedItem);
                sqlCmd.Parameters.AddWithValue("@Datum", DatumDatePicker.SelectedDate);
                sqlCmd.Parameters.AddWithValue("@Vreme", VremepolTxtBox.Text);
                sqlCmd.Parameters.AddWithValue("@MestoPolaska", MestoPolaskaTxtBox.Text);
                sqlCmd.Parameters.AddWithValue("@MestoDolaska", MestoDolaskaTxtBox.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                MessageBox.Show("Podaci su uspešno promenjeni");
                if (count == 1)
                {
                    UcitajDataGrid();
                }
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

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "DELETE FROM Voznja WHERE idVoznje = " + ID + "";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                MessageBox.Show("Podaci su uspešno obrisani");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije moguće obrisati podatke o vožnji zbog postojanja jedne ili više rezervacija za ovu vožnju.");
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
            Voznje dashboadrd = new Voznje();
            dashboadrd.Show();
            this.Close();
        }

        private void btnObrisiSelekciju_Click(object sender, RoutedEventArgs e)
        {
            OcistiPodatke();
        }
    }
}
