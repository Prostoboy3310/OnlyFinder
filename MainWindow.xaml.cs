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
        int User_ID = 4; // Variable zur Speicherung der UserID

        string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=1234;";

        private bool AuthenticateUser(string email, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-Abfrage, um zu überprüfen, ob der Benutzer existiert und das Passwort stimmt
                    string query = "SELECT Passworde, NutzerID FROM Nutzer WHERE email = @Email";

                    //string query2 = "SELECT NutzerID FROM Nutzer WHERE email = @Email";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);

                    // Holen des gespeicherten Passworts und der UserID aus der Datenbank
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        var storedPassword = reader.GetString("Passworde");
                        //UserID = reader.GetInt32("NutzerID"); // Benutzer-ID speichern

                        // Überprüfen, ob das eingegebene Passwort mit dem gespeicherten Passwort übereinstimmt
                        if (storedPassword == password)
                        {
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
                // Wenn Benutzer existiert und Passwort korrekt ist
                // Die UserID ist jetzt in der Variablen UserID gespeichert
                DatingWindow DW = new DatingWindow();
                //DW.GetID(UserID); // Die UserID an das DatingWindow übergeben

                DW.UserID = User_ID;
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