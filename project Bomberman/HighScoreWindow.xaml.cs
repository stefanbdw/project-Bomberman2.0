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
        //maak een dictonary voor de highscore aan
        Dictionary<string, int> highscores = new Dictionary<string, int>();
        //een string die vervolgens het pad naar de database gaat bevatten
        string path = "";
        //pak de locale pad naar database
        public void SetDir()
        {
            //pak de actieve directory
            string workingDirectory = Environment.CurrentDirectory;
            //de terugegeven map geeft een te hoog pad terug dus we gaan 2 stappen terug
            string dire = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //zet de source naar het pad
            path = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dire + "\\Data\\db_highscores.mdf;Integrated Security = True";
        }
        //Zet de highscore window klaar
        public HighScoreWindow()
        {
            //zet alles klaar
            InitializeComponent();
            //pak de locale pad naar database
            SetDir();
            //Pak de highscores
            GetHighScores();
            //maak tabellen aan voor de highscores
            CreateLabels();
        }
        //pak alle opgeslagen scores uit de database
        private void GetHighScores()
        {
            //maak de highscore disctonary leeg
            highscores.Clear();
            //de nodige query voor de database
            string query = "SELECT Name,Score FROM [Table];";
            //gebruik een connection met het gegeven pad
            using (SqlConnection connection = new SqlConnection(path))
            {
                //niew commdando met de gegeven info
                SqlCommand command = new SqlCommand(query, connection);
                //open verbinding met de database
                connection.Open();
                //maak een lokale reader class aan zodat we data uit kunnen lezen
                SqlDataReader reader = command.ExecuteReader();

                // Call lees voordat we de data gerbuiken
                while (reader.Read())
                {
                    //als de gebruikersnaam al bestaad of de score nul of lager is
                    if (highscores.ContainsKey((string)reader[0]) || (int)reader[1] <= 0)
                    {
                        //sla een stap over
                        continue;
                    }
                    else
                    {
                        //voeg de data toe aan de databse
                        highscores.Add((string)reader[0], (int)reader[1]);
                    }
                }

                // sluit de reader
                reader.Close();
            }
        }

        //Maak niewe labels aan voor de score
        private void CreateLabels()
        {
            //leeg het highscore paneel
            HighScoresPanel.Children.Clear();
            //sorteer alle scores van hoog naar laag
            var sortedHighscores = from score in highscores orderby score.Value descending select score;
            //voor elke key met een string en int in de sortedHighscores
            foreach (KeyValuePair<string, int> highscore in sortedHighscores)
            {
                //maak een nieuwe label
                Label label = new Label();
                //voeg data toe aan de label
                label.Content = "Player " + highscore.Key + " scored " + highscore.Value;
                //verplaats de label
                label.HorizontalAlignment = HorizontalAlignment.Center;
                //voeg de nieuwe label toe aan het highscorePanel
                HighScoresPanel.Children.Add(label);
            }
        }
        //terugknop 
        private void backBtn(object sender, RoutedEventArgs e)
        {
            //maak hoofdmenu aan
            Hoofdmenu h = new Hoofdmenu();
            //maak hoofdmenu zichtbaar
            h.Visibility = Visibility.Visible;
            //verberg dit
            this.Hide();
        }
    }
}
