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

namespace project_Bomberman
{
    /// <summary>
    /// Interaction logic for NameInput.xaml
    /// </summary>
    public partial class NameInput : Window
    {
        public NameInput()
        {
            InitializeComponent();
        }

        private void input(object sender, RoutedEventArgs e)
        {
            string userInput1 = NameP1.Text.ToString();
            Score.NamePlayer1 = NameP1.Text.ToString();     //zet de eerste naam als de naam van speler 1
            string userInput2 = NameP2.Text.ToString();
            Score.NamePlayer2 = NameP2.Text.ToString();     //zet de 2de naam als de naam van speler 2
            MainWindow g = new MainWindow();                //open de nieuwe gamemode
            g.Show();                                       //laat de window zien
            this.Close();                                   //sluit dit
        }
    }
}
