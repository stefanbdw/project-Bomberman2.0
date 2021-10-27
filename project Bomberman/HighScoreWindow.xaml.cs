using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tghee\\Source\\Repos\\project-Bomberman2.0\\project Bomberman\\Data\\db_highscores.mdf;Integrated Security=True";
        public HighScoreWindow()
        {
            InitializeComponent();
            GetHighScores();
            CreateLabels();
        }

        private void GetHighScores()
        {
            //todo get highscores
            highscores.Clear();

            string query = "SELECT Player1Name,Player1Score FROM [Table];";

            using (SqlConnection connection = new SqlConnection(connectionString))
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

        private void SetHighScores()
        {
            //todo insert into database

            string query1 = "INSERT INTO [Highscores] ([Player],[Score],[Date]) VALUES ('Jos','42','" + DateTime.Today.ToString("yyyy-MM-dd") + ")";
            string query2 = "INSERT INTO [Highscores] ([Player],[Score],[Date]) VALUES ('Allard','43','" + DateTime.Today.ToString("yyyy-MM-dd") + "')";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = query2;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Gelukt!");
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
