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
    /// Interaktionslogik für Regisstrieren2.xaml
    /// </summary>
    public partial class Registrieren2 : Window
    {
        public Registrieren2()
        {
            InitializeComponent();
        }

        string UsernameBox;
        string EmailBox;
        string PasswordBox;

        string IchBin;
        string Suche;

        string Name;
        string Geburtsdatum;
        string Wohnort;
        string Telefon;
        

        public void SetValues(string Username, string Email, string Password)
        {
            UsernameBox = Username;
            EmailBox = Email;
            PasswordBox = Password;

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register1 reg1 = new Register1();

            reg1.Show();

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Register3 reg3 = new Register3();
            reg3.Show();

            Name = NameBox.Text;
            Geburtsdatum = GeburtsdatumBox.Text;
            Wohnort = WohnortBox.Text;
            Telefon = TelefonBox.Text;

            if (NameBox.Text == string.Empty)
            {
                MessageBox.Show("Name Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (GeburtsdatumBox.Text == string.Empty)
            {
                MessageBox.Show("Geburtsdatum Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (WohnortBox.Text == string.Empty)
            {
                MessageBox.Show("Name Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (TelefonBox.Text == string.Empty)
            {
                MessageBox.Show("Name Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (Ich_Man.IsChecked == true)
            {
                IchBin = "M";
            }
            else if(Ich_Frau.IsChecked == true)
            {
                IchBin = "F";
            }
            else if(Ich_Divers.IsChecked == true)
            {
                IchBin = "D";
            }
            else
            {
                MessageBox.Show("Wählen sie geschlecht aus", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (Suche_Man.IsChecked == true)
            {
                Suche = "M";
            }
            else if (Suche_Frau.IsChecked == true)
            {
                Suche = "F";
            }
            else if (Suche_Divers.IsChecked == true)
            {
                Suche = "D";
            }
            else
            {
                MessageBox.Show("Wählen sie Partner geschlecht aus", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }

      

            this.Close();
        }
    }
}
