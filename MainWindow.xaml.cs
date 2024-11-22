using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace OnlyFinder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private string email;
        private string password;
        private int userId; // Nutzer-ID speichern

        private readonly string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=Justin0910;";

        private bool AuthenticateUser(string email, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Passworde, NutzerID FROM Nutzer WHERE email = @Email";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        var storedPassword = reader.GetString("Passworde");
                        userId = reader.GetInt32("NutzerID");

                        return storedPassword == password;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler bei der Datenbankverbindung: {ex.Message}");
                    return false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register1 reg1 = new Register1();
            reg1.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            email = EmailBox.Text;
            password = PassBox.Text; // PasswordBox verwenden

            if (AuthenticateUser(email, password))
            {
                // Öffne DatingWindow und übergebe die Nutzer-ID
                DatingWindow dw = new DatingWindow(userId);
                dw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("E-Mail oder Passwort ist falsch.");
            }
        }
    }
}
