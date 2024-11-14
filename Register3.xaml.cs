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

namespace OnlyFinder
{
    /// <summary>
    /// Interaktionslogik für Register3.xaml
    /// </summary>
    public partial class Register3 : Window
    {
        public Register3()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        string Username;
        string Email;
        string Password;

        string IchBin;
        string Suche;

        string Name;
        string Geburtsdatum;
        string Wohnort;
        string Telefon;

        string Hobbies;
        string WelcomeText;
        // image?

        public void GetData(string User, string email, string password, string ichbin, string suche, string name, string geburtsdatum, string wohnort, string telefon)
        {
            Username = User;
            Email = email;
            Password = password;
            IchBin = ichbin;
            Suche = suche;
            Name = name;
            Geburtsdatum = geburtsdatum;
            Wohnort = wohnort;
            Telefon = telefon;
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
            MainWindow MyMain = new MainWindow();
            MyMain.Show();
            this.Close();
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
