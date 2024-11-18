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
        }


        string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=1234;";
        public void ConnectAndQueryDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Verbindung erfolgreich!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void GetDataFromDB()
        {

        }
    }
}
