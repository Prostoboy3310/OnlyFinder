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
           
            this.Close();
        }
    }
}
