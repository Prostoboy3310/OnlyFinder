using System;
using System.Windows;

namespace OnlyFinder
{
    public partial class DatingWindow : Window
    {
        private int UserId;

        public DatingWindow(int userId)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            UserId = userId; // Nutzer-ID speichern
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyProfile my = new MyProfile(UserId); // Nutzer-ID übergeben
            my.Show();
            this.Close();
        }
    }
}
