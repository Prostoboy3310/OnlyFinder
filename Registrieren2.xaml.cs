﻿using System;
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
            this.WindowState = WindowState.Maximized;
        }

        string UsernameBox;
        string EmailBox;
        string PasswordBox;

        string IchBin;
        string Suche;

        string Name;
        string Geburtsdatum;
        DateTime geburtsdatum;
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
            // Werte aus den Textboxen und Checkboxen holen
            Name = NameBox.Text;
            Geburtsdatum = GeburtsdatumBox.Text;
            Wohnort = WohnortBox.Text;
            Telefon = TelefonBox.Text;

            // Validierung
            if (NameBox.Text == string.Empty)
            {
                MessageBox.Show("Name Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (GeburtsdatumBox.Text == string.Empty)
            {
                MessageBox.Show("Geburtsdatum Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (WohnortBox.Text == string.Empty)
            {
                MessageBox.Show("Wohnort Kann nicht leer sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            if (DateTime.TryParse(Geburtsdatum, out geburtsdatum))
            {
                // Das Datum wurde erfolgreich konvertiert
            }
            else
            {
                // Fehler beim Konvertieren des Datums, handle den Fehler hier
                MessageBox.Show("Das eingegebene Datum ist ungültig.");
                return;
            }

            // Geschlecht prüfen
            if (Ich_Man.IsChecked == true)
            {
                IchBin = "M";
            }
            else if (Ich_Frau.IsChecked == true)
            {
                IchBin = "F";
            }
            else if (Ich_Divers.IsChecked == true)
            {
                IchBin = "D";
            }
            else
            {
                MessageBox.Show("Wählen Sie Geschlecht aus", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Geschlecht des Partners prüfen
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
                MessageBox.Show("Wählen Sie Partner-Geschlecht aus", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Wenn alle Überprüfungen erfolgreich waren, neues Fenster öffnen
            Register3 reg3 = new Register3();
            reg3.GetData(UsernameBox, EmailBox, PasswordBox, IchBin, Suche, Name, Geburtsdatum, geburtsdatum, Wohnort, Telefon);
            reg3.Show();

            // Aktuelles Fenster schließen
            this.Close();
        }
    }
}
