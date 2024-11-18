using System;
using System.Collections.Generic;
using System.IO;
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
using Org.BouncyCastle.Utilities.Encoders;

namespace OnlyFinder
{
    /// <summary>
    /// Interaktionslogik für Register3.xaml
    /// </summary>
    public partial class Register3 : Window
    {
        string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=1234;";

        public void ConnectAndQueryDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Verbindung erfolgreich!");

                    // Beispiel: Daten abfragen
                    string query = "SELECT * FROM Nutzer";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public Register3()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            ConnectAndQueryDatabase();
        }

        string Username;
        string Email;
        string Password;

        string IchBin;
        string Suche;

        string Name;
        string Geburtsdatum;
        DateTime gebdatum;
        string Wohnort;
        string Telefon;

        private string imageFilePath;

        string Hobbies;
        string WelcomeText;
        // image?

        public void GetData(string User, string email, string password, string ichbin, string suche, string name, string geburtsdatum, DateTime geburtsdatum2, string wohnort, string telefon)
        {
            Username = User;
            Email = email;
            Password = password;
            IchBin = ichbin;
            Suche = suche;
            Name = name;
            Geburtsdatum = geburtsdatum;
            gebdatum = geburtsdatum2;
            Wohnort = wohnort;
            Telefon = telefon;
        }
        private byte[] ConvertImageToByteArray(string filePath)
        {
            try
            {
                return File.ReadAllBytes(filePath); // Read image file as byte array
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Fehler beim Lesen des Bildes: {ex.Message}");
                return null;
            }
        }
        private void SaveUserData(string username, string email, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // SQL-INSERT-Befehl zum Speichern der Daten in der Nutzer-Tabelle
                    string query1 = "INSERT INTO Nutzer (Nutzername, Email, Passworde) VALUES (@username, @email, @password)";
                    MySqlCommand cmd1 = new MySqlCommand(query1, connection);

                    // Verwende Parameter, um SQL-Injection zu verhindern
                    cmd1.Parameters.AddWithValue("@username", username);
                    cmd1.Parameters.AddWithValue("@email", email);
                    cmd1.Parameters.AddWithValue("@password", password); // Passwort sollte gehasht sein

                    // Ausführen der ersten Abfrage
                    int rowsAffected1 = cmd1.ExecuteNonQuery();

                    if (rowsAffected1 > 0)
                    {
                        // Wenn der Benutzer erfolgreich gespeichert wurde, speichere die Profildaten
                        string query2 = "INSERT INTO Profil (Vorname, Gebutrsdatum, Wohnort, Telefon, Geschlecht, Sex, Hobby, Satz, Bilder) " +
                                        "VALUES (@vorname, @geburtsdatum, @wohnort, @telefon, @geschlecht, @sex, @hobby, @satz, @bild)";

                        MySqlCommand cmd2 = new MySqlCommand(query2, connection);

                        // Parameter für die Profildaten setzen
                        cmd2.Parameters.AddWithValue("@vorname", Name);
                        //cmd2.Parameters.AddWithValue("@nachname", nachname);
                        cmd2.Parameters.AddWithValue("@geburtsdatum", Geburtsdatum);
                        cmd2.Parameters.AddWithValue("@wohnort", Wohnort);
                        cmd2.Parameters.AddWithValue("@telefon", Telefon);
                        cmd2.Parameters.AddWithValue("@geschlecht", IchBin);
                        cmd2.Parameters.AddWithValue("@sex", Suche);
                        cmd2.Parameters.AddWithValue("@hobby", Hobbies);
                        cmd2.Parameters.AddWithValue("@satz", WelcomeText);
                        //cmd2.Parameters.AddWithValue("@bild", bild); // 'bild' als Byte-Array für Bilddaten
                        byte[] imageBytes = ConvertImageToByteArray(imageFilePath);
                        if (imageBytes != null)
                        {
                            cmd2.Parameters.AddWithValue("@bild", imageBytes);
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@bild", DBNull.Value); // Falls kein Bild vorhanden ist
                        }


                        // Ausführen der zweiten Abfrage
                        int rowsAffected2 = cmd2.ExecuteNonQuery();

                        if (rowsAffected2 > 0)
                        {
                            MessageBox.Show("Benutzer und Profil erfolgreich gespeichert!");
                        }
                        else
                        {
                            MessageBox.Show("Fehler beim Speichern des Profils.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Speichern des Benutzers.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler: {ex.Message}");
                }
            }
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Hole die Datei(en) vom Drag-and-Drop-Vorgang
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    // Lade das erste Bild im Array
                    string filePath = files[0];
                    imageFilePath = files[0];

                    // Erstelle ein BitmapImage und lade das Bild
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
                    bitmap.EndInit();

                    // Setze das Image Control's Source auf das BitmapImage
                    MyImage.Source = bitmap;
                }
            }
        }

        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Registrieren2 R2 = new Registrieren2();
            R2.Show();

            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Extrahiere den Text aus der RichTextBox für WelcomeText
            TextRange welcomeTextRange = new TextRange(SatzBox.Document.ContentStart, SatzBox.Document.ContentEnd);
            WelcomeText = welcomeTextRange.Text.Trim(); // Trim entfernt unnötige Leerzeichen oder Zeilenumbrüche

            // Extrahiere den Text aus der RichTextBox für Hobbies
            TextRange hobbiesTextRange = new TextRange(HobbiesBox.Document.ContentStart, HobbiesBox.Document.ContentEnd);
            Hobbies = hobbiesTextRange.Text.Trim(); // Trim entfernt unnötige Leerzeichen oder Zeilenumbrüche

            MainWindow MyMain = new MainWindow();
            MyMain.Show();
            SaveUserData(Username, Email, Password);
            this.Close();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
