using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;

namespace OnlyFinder
{
    public partial class MyProfile : Window
    {
        private readonly string connectionString = "Server=localhost;Database=onlyfinder;User ID=root;Password=Justin0910;";
        private int UserId;

        public MyProfile(int userId)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            UserId = userId;
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT p.Name, p.Satz, p.Hobby, p.Bilder 
                        FROM Profil p 
                        JOIN Nutzer n ON n.NutzerID = p.PorfilID 
                        WHERE n.NutzerID = @UserId";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        NameBox.Text = $"Name: {reader.GetString("Name")}";
                        Satzblock.Text = $"Satz: {reader.GetString("Satz")}";
                        Hobbyblock.Text = $"Hobby: {reader.GetString("Hobby")}";

                        if (!reader.IsDBNull(reader.GetOrdinal("Bilder")))
                        {
                            byte[] imageBytes = (byte[])reader["Bilder"];
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                BitmapImage bitmap = new BitmapImage();
                                bitmap.BeginInit();
                                bitmap.StreamSource = ms;
                                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                bitmap.EndInit();
                                MyImage.Source = bitmap;
                            }
                        }
                        else
                        {
                            MyImage.Source = null; // Kein Bild
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kein Profil gefunden.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler: {ex.Message}");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
