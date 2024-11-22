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

        string Name; // Variable zur Speicherung der NutzerID

        string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=Justin0910;";

        private bool AuthenticateUser(string email, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-Abfrage, um zu überprüfen, ob der Benutzer existiert und das Passwort stimmt
                    string query = "SELECT Passworde, NutzerID FROM Nutzer WHERE email = @Email";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);

                    // Holen des gespeicherten Passworts und der NutzerID aus der Datenbank
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        var storedPassword = reader.GetString("Passworde");
                        int userId = reader.GetInt32("NutzerID"); // Holen der NutzerID als int

                        // Überprüfen, ob das eingegebene Passwort mit dem gespeicherten Passwort übereinstimmt
                        if (storedPassword == password)
                        {
                            // Speichern der NutzerID
                            Name = userId.ToString();
                            return true; // Authentifizierung erfolgreich
                        }
                    }

                    // Kein Benutzer gefunden oder Passwort stimmt nicht überein
                    return false;
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
                DatingWindow DW = new DatingWindow();

                // Passiere die NutzerID als int
                int userId = Convert.ToInt32(Name);  // Konvertiere Name, falls nötig, in int
                DW.GetName(userId);  // Übergabe der NutzerID an DatingWindow

                DW.Show();
                this.Close(); // Schließe das Hauptfenster
            }
            else
            {
                MessageBox.Show("E-Mail oder Passwort ist falsch.");
            }
        }
    }
}
