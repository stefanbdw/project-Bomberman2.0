using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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


namespace project_Bomberman
{
    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    /// 
    public partial class HighScoreWindow : Window 
    {
        Dictionary<string, int> highscores = new Dictionary<string, int>();
        const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Programmeren 1\\project Bomberman\\project Bomberman\\Data\\db_highscores.mdf;Integrated Security=True";
        string path = "";
        public void SetDir()
        {
            string workingDirectory = Environment.CurrentDirectory;

            string dire = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            path = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dire + "\\Data\\db_highscores.mdf;Integrated Security = True";

        }
        public HighScoreWindow()
        {
            InitializeComponent();
            SetDir();
            MessageBox.Show(System.AppDomain.CurrentDomain.BaseDirectory);
            GetHighScores();
            CreateLabels();
        }

        private void GetHighScores()
        {
            //todo get highscores
            highscores.Clear();

            string query = "SELECT Name,Score FROM [Table];";

            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    //MessageBox.Show("Data: " + reader[0] + " en: " + reader[1]);
                    highscores.Add((string)reader[0], (int)reader[1]);
                }

                // Call Close when done reading.
                reader.Close();
            }
        }

        public void SetHighScores(string SQL_Text)
        {

            //string query1 = "INSERT INTO [Table] ([Player],[Score]) VALUES ('Jos','500')";

            SqlConnection connection = new SqlConnection(path);

            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = SQL_Text;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                //MessageBox.Show("Gelukt!");
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message);
            }
        }

        private void CreateLabels()
        {
            HighScoresPanel.Children.Clear();
            var sortedHighscores = from score in highscores orderby score.Value descending select score;
            foreach (KeyValuePair<string, int> highscore in sortedHighscores)
            {
                Label label = new Label();
                label.Content = "Player " + highscore.Key + " scored " + highscore.Value;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                HighScoresPanel.Children.Add(label);
            }
        }

        private void backBtn(object sender, RoutedEventArgs e)
        {
            Hoofdmenu h = new Hoofdmenu();
            h.Visibility = Visibility.Visible;
            this.Hide();
        }
    }
}
