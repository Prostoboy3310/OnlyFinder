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

        string email;
        string password;

        string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=1234;";

        private bool AuthenticateUser(string email, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-Abfrage, um zu überprüfen, ob der Benutzer existiert und das Passwort stimmt
                    string query = "SELECT Passworde FROM Nutzer WHERE email = @Email";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);

                    // Holen des gespeicherten Passworts aus der Datenbank
                    var storedPassword = cmd.ExecuteScalar() as string;

                    if (storedPassword == null)
                    {
                        // Kein Benutzer gefunden
                        return false;
                    }

                    // Überprüfen, ob das eingegebene Passwort mit dem gespeicherten Passwort übereinstimmt
                    return storedPassword == password;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler bei der Datenbankverbindung: " + ex.Message);
                    return false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register1 Reg1 = new Register1();
            Reg1.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            email = EmailBox.Text; // TextBox für E-Mail
            password = PassBox.Text; // PasswordBox für Passwort

            if (AuthenticateUser(email, password))
            {
                // Wenn Benutzer existiert, öffne das DatingWindow
                DatingWindow DW = new DatingWindow();
                DW.Show();
                this.Close(); // Schließe das Hauptfenster
            }
            else
            {
                // Zeige eine Fehlermeldung, wenn die Eingaben nicht gefunden werden
                MessageBox.Show("E-Mail oder Passwort ist falsch.");
            }
        }
    }
}