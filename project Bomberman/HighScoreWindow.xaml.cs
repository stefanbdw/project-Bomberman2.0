using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    /// 
    public partial class HighScoreWindow : Window
    {
        static String connectionString = @"Data Source=vibes\sqlexpress;Initial Catalog=ParallelCodes;User ID=sa;Password=789;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataSet ds;
        SqlDataReader reader;

        public HighScoreWindow()
        {
            InitializeComponent();
        }
    }
}
