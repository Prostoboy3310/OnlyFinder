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
using MySql.Data.MySqlClient;

namespace OnlyFinder
{
    /// <summary>
    /// Interaktionslogik für DatingWindow.xaml
    /// </summary>
    public partial class DatingWindow : Window
    {
        public DatingWindow()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;

            ConnectAndQueryDatabase();
            GetDataFromDB();

        }
        public int UserID;




        public void GetID(int ID)
        {


        }

        string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=1234;";

        // Methode zum Verbinden und Abrufen von Benutzerdaten
        public void ConnectAndQueryDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Verbindung erfolgreich!");

                    // SQL-Abfrage, um den Namen des Benutzers anhand der UserID zu holen
                    string query = "SELECT NutzerID FROM Nutzer WHERE NutzerID = @UserID";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //cmd.Parameters.AddWithValue("@UserID", UserID); // UserID als Parameter hinzufügen

                    // Abrufen des Benutzernamens



                    // Den Namen in der NameBox anzeigen

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void GetDataFromDB()
        {
            NameBox.Text = UserID.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Close();
        }
    }
}
    
