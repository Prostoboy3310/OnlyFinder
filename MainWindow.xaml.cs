using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace OnlyFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

                    // SQL-Abfrage, um zu überprüfen, ob der Benutzer existiert
                    string query = "SELECT COUNT(1) FROM Nutzer WHERE email = @Email AND Passworde = @Password";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    // Wenn 1 zurückgegeben wird, bedeutet es, dass die Kombination existiert
                    return result == 1;
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