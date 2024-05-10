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

namespace AS
{
    /// <summary>
    /// Interaction logic for Registracija.xaml
    /// </summary>
    public partial class Registracija : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-6TRB9F1\SQLEXPRESS;Initial Catalog=AutobuskaStanica;Integrated Security=True");

        public void OcistiPodatke()
        {
            ImeTxtbox.Text = "";
            PrezimeTxtbox.Text = "";
            KorisnickoimeTxtbox.Text = "";
            SifraPasswordBox.Password = "";
            
        }

        public Registracija()
        {
            InitializeComponent();
        }

        private void btnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "INSERT INTO Korisnik (Ime, Prezime, Korisnickoime, Lozinka) VALUES(@Ime, @Prezime, @Korisnickoime, @Lozinka)";
                string query1 = "SELECT COUNT(*) FROM Korisnik WHERE Korisnickoime = @KorIme";

                SqlCommand sqlCmdKor = new SqlCommand(query1, sqlCon);
                sqlCmdKor.CommandType = CommandType.Text;
                sqlCmdKor.Parameters.AddWithValue("@KorIme", KorisnickoimeTxtbox.Text);
                int count = Convert.ToInt32(sqlCmdKor.ExecuteScalar());
                if (count == 1)
                {
                    MessageBox.Show("Nalog sa istim korisničkim imenom već postoji. \n Da bi ste se registrovali promenite korisničko ime");
                    OcistiPodatke();
                }
                else
                {
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Korisnickoime", KorisnickoimeTxtbox.Text);
                    sqlCmd.Parameters.AddWithValue("@Lozinka", SifraPasswordBox.Password);
                    sqlCmd.Parameters.AddWithValue("@Ime", ImeTxtbox.Text);
                    sqlCmd.Parameters.AddWithValue("@Prezime", PrezimeTxtbox.Text);
                  
                    int countNew = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    MessageBox.Show("Uspesno ste se registrovali");

                    Prijava dashboard = new Prijava();
                    dashboard.Show();
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            Prijava dashboard = new Prijava();
            dashboard.Show();
            this.Close();
        }
    }
}
