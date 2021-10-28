using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Bomberman
{
    //deze class bezit een constructor voor de score


    public class Score
    {
        public static Dictionary<string, int> Scores = new Dictionary<string, int>();
        const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tghee\\Source\\Repos\\project-Bomberman2.0\\project Bomberman\\Data\\db_highscores.mdf;Integrated Security=True";

        public static string NamePlayer1 = "";              //naam van de eerste speler
        public static string NamePlayer2 = "";              //naam van de tweede speler

        public static int ScorePlayer1;
        public static int ScorePlayer2;
        //Dictionary<>

        public static void SetScore(string name, int score)
        {
            if(name == NamePlayer1)
            {
                ScorePlayer1 += score;
            }
            else if(name == NamePlayer2)
            {
                ScorePlayer2 += score;
            }
        }


        public void SetHighScores(string p1, int score_p1, string p2, int score_p2)
        {
            string query = $"INSERT INTO [Table] ([Name],[Score]) VALUES ('{p1}','{score_p1}')";
            string query2 = $"INSERT INTO [Table] ([Name],[Score]) VALUES ('{p2}','{score_p2}')";

            for (int i = 0; i < 2; i++)
            {
                SqlConnection connection = new SqlConnection(connectionString);

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
        }
    }
}
