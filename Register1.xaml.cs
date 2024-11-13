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

namespace OnlyFinder
{
    /// <summary>
    /// Interaktionslogik für Register1.xaml
    /// </summary>
    public partial class Register1 : Window
    {
        public Register1()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        string UserBox;
        string Email;
        string Password;
        string Password2;
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainW = new MainWindow();

            MainW.Show();

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Registrieren2 Reg2 = new Registrieren2();

            UserBox = UsernameBox.Text;
            Email = EmailBox.Text;
            Password = PasswordBox.Text;
            Password2 = Password2Bpx.Text;

            if (UsernameBox.Text == string.Empty)
            {
                MessageBox.Show("Username Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (EmailBox.Text == string.Empty)
            {
                MessageBox.Show("Email Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (PasswordBox.Text == string.Empty)
            {
                MessageBox.Show("Passwort kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(Password.Length < 4)
            {
                MessageBox.Show("Passwort kann nicht kleiner als 4 sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Password == Password2)
            {
                Reg2.SetValues(UserBox, Email, Password);

                Reg2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Passwort nicht koreckt eingegeben!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }




        }
    }
}
