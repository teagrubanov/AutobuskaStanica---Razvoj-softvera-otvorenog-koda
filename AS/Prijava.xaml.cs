using AS.Admin;
using AS.Korisnik;
using Microsoft.Win32;
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
    /// Interaction logic for Prijava.xaml
    /// </summary>
    public partial class Prijava : Window
    {

        private string status = "Admin";
        private int KorId;

        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-6TRB9F1\SQLEXPRESS;Initial Catalog=AutobuskaStanica;Integrated Security=True");

        public Prijava()
        {
            InitializeComponent();
        }

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string queryAdmin = "SELECT COUNT(*) FROM Korisnik WHERE Korisnickoime = @KORISNICKOIME AND Lozinka = @LOZINKA AND Status = '" + status + "'";
                string queryKor = "SELECT COUNT(*),idKorisnika FROM Korisnik WHERE Korisnickoime = @KORISNICKOIME AND Lozinka = @LOZINKA GROUP BY idKorisnika";
                string queryID = "SELECT idKorisnika FROM Korisnik WHERE Korisnickoime = @KORISNICKOIME AND Lozinka = @LOZINKA ";

                SqlCommand sqlCmdAdmin = new SqlCommand(queryAdmin, sqlCon);
                sqlCmdAdmin.CommandType = CommandType.Text;
                sqlCmdAdmin.Parameters.AddWithValue("@KORISNICKOIME", KorisnickoimeTxtbox.Text);
                sqlCmdAdmin.Parameters.AddWithValue("@LOZINKA", SifraPasswordBox.Password);
                int count = Convert.ToInt32(sqlCmdAdmin.ExecuteScalar());
                if (count == 1)
                {
                    PocetnaAdmin dashboard = new PocetnaAdmin();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    SqlCommand sqlCmdKor = new SqlCommand(queryKor, sqlCon);
                    sqlCmdKor.CommandType = CommandType.Text;
                    sqlCmdKor.Parameters.AddWithValue("@KORISNICKOIME", KorisnickoimeTxtbox.Text);
                    sqlCmdKor.Parameters.AddWithValue("@LOZINKA", SifraPasswordBox.Password);
                    int countUsr = Convert.ToInt32(sqlCmdKor.ExecuteScalar());
                    if (countUsr == 1)
                    {
                        SqlCommand sqlCmdID = new SqlCommand(queryID, sqlCon);
                        sqlCmdID.CommandType = CommandType.Text;
                        sqlCmdID.Parameters.AddWithValue("@KORISNICKOIME", KorisnickoimeTxtbox.Text);
                        sqlCmdID.Parameters.AddWithValue("@LOZINKA", SifraPasswordBox.Password);
                        KorId = Convert.ToInt32(sqlCmdID.ExecuteScalar());

                        KorisnickiPrikaz dashboard = new KorisnickiPrikaz(KorId);
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Korisničko ime ili šifra nisu dobro uneti. Pokušajte ponovo.");
                    }
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

        private void btnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            Registracija dashboard = new Registracija();
            dashboard.Show();
            this.Close();
        }
    }
}
