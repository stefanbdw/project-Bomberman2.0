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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project_Bomberman
{
    /// <summary>
    /// Interaction logic for Hoofdmenu.xaml
    /// </summary>
    public partial class Hoofdmenu : Window
    {
        public Hoofdmenu()
        {
            InitializeComponent();
        }
        

        private void Start1v1Btn(object sender, RoutedEventArgs e)
        {
            NameInput g = new NameInput();
            g.Show();
            this.Close();

        }
        

        private void SettingsBtn(object sender, RoutedEventArgs e)
        {

        }

        private void HighScoreBtn(object sender, RoutedEventArgs e)
        {
            HighScoreWindow h = new HighScoreWindow();
            h.Show();
        }

        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}

