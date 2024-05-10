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
    /// Interaction logic for Voznje.xaml
    /// </summary>
    public partial class Voznje : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-6TRB9F1\SQLExpress;Initial Catalog=AutobuskaStanica;Integrated Security=True");

        public Voznje()
        {
            InitializeComponent();
            UcitajDataGrid();
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

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            PocetnaAdmin dashboard = new PocetnaAdmin();
            dashboard.Show();
            this.Close();
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava dashboard = new Prijava();
            dashboard.Show();
            this.Close();
        }

        private void btnIzmenaVoznji_Click(object sender, RoutedEventArgs e)
        {
            IzmenaVoznji dashboard = new IzmenaVoznji();
            dashboard.Show();
            this.Close();
        }

        private void btnIstekleVoznje_Click(object sender, RoutedEventArgs e)
        {
            IstekleVoznje dashboard = new IstekleVoznje();
            dashboard.Show();   
            this.Close();
        }
    }
}
