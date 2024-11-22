using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;

namespace OnlyFinder
{
    public partial class DatingWindow : Window
    {
        public DatingWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        public string Name; // Hier speichern wir die NutzerID bzw. den Namen des Benutzers

        string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=Justin0910;";

        // Methode zum Abrufen des Namens, Satzes, Hobbys und des Bildes aus der Datenbank basierend auf der NutzerID
        public void ConnectAndQueryDatabase(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Verbindung erfolgreich!");

                    // SQL-Join-Abfrage, um den Namen, Satz, Hobby und Bild des Benutzers aus der 'Profil'-Tabelle zu holen
                    string query = "SELECT p.Name, p.Satz, p.Hobby, p.Bilder FROM Profil p " +
                                   "JOIN Nutzer n ON n.NutzerID = p.PorfilID " + // Verknüpfen der Tabellen
                                   "WHERE n.NutzerID = @UserId";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserId", userId); // Verwende hier den korrekten Datentyp für NutzerID (INT)

                    // Ausführen der Abfrage und Abrufen des Namens, Satzes, Hobbys und Bildes
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string fetchedName = reader.GetString("Name");
                        string fetchedSatz = reader.GetString("Satz");
                        string fetchedHobby = reader.GetString("Hobby");

                        // Setzen der abgerufenen Werte in die Textfelder
                        NameBox.Text = $"Name: {fetchedName}";
                        Satzblock.Text = $"Satz: {fetchedSatz}";
                        Hobbyblock.Text = $"Hobby: {fetchedHobby}";

                        // Abrufen des Bildes als Byte-Array
                        if (!reader.IsDBNull(reader.GetOrdinal("Bilder")))
                        {
                            byte[] imageBytes = reader["Bilder"] as byte[];
                            if (imageBytes != null && imageBytes.Length > 0)
                            {
                                // Konvertieren der Byte-Daten in ein Bild
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    BitmapImage bitmap = new BitmapImage();
                                    bitmap.BeginInit();
                                    bitmap.StreamSource = ms;
                                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                    bitmap.EndInit();

                                    // Bild im Image-Steuerelement anzeigen
                                    MyImage.Source = bitmap;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kein Benutzer mit dieser ID gefunden.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler bei der Datenbankverbindung: " + ex.Message);
                }
            }
        }

        // Diese Methode ruft die Benutzerdaten aus der Datenbank ab, nachdem die Name-Variable gesetzt wurde
        public void GetDataFromDB()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                // Wenn der Name gesetzt wurde, holen wir den vollständigen Benutzernamen aus der Datenbank
                ConnectAndQueryDatabase(Convert.ToInt32(Name)); // Nutze Convert.ToInt32, um die ID zu int zu konvertieren
            }
            else
            {
                NameBox.Text = "Kein Benutzername gesetzt.";
                Satzblock.Text = "Kein Satz gesetzt.";
                Hobbyblock.Text = "Kein Hobby gesetzt.";
                MyImage.Source = null; // Keine Bildanzeige, falls kein Bild vorhanden ist
            }
        }

        // Setzt den Benutzernamen (Name bzw. NutzerID) und zeigt ihn im Textfeld an
        public void GetName(int userId)
        {
            Name = userId.ToString(); // Wandelt die int ID in einen String um
            NameBox.Text = $"User ID: {Name}"; // Beispiel für die Anzeige der ID

            // Jetzt rufen wir den Namen des Benutzers ab
            GetDataFromDB();
        }

        // Event-Handler für den "Zurück"-Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Close();
        }
    }
}
