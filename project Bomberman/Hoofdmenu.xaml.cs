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

            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/1.jpg"));
            

        }
        private void Start1v1Btn(object sender, RoutedEventArgs e)
        {
            MainWindow g = new MainWindow();
            g.Visibility = Visibility.Visible;
            this.Hide();





        }
        

        private void SettingsBtn(object sender, RoutedEventArgs e)
        {

        }

        private void HighScoreBtn(object sender, RoutedEventArgs e)
        {

        }

        private void StartSoloBtn(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}

