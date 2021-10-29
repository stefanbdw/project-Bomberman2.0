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

    //de class die het uploaden van scores regelt
    public class Score
    {

        public static string NamePlayer1 = "";              //naam van de eerste speler
        public static string NamePlayer2 = "";              //naam van de tweede speler

        public static int ScorePlayer1;                     //De score van speler 1
        public static int ScorePlayer2;                     //De score van speler 2
        //Dictionary<>
        //een string die vervolgens het pad naar de database gaat bevatten
        public static string path = "";
        //pak de locale pad naar database
        public static void SetDir()
        {
            //pak de actieve directory
            string workingDirectory = Environment.CurrentDirectory;
            //de terugegeven map geeft een te hoog pad terug dus we gaan 2 stappen terug
            string dire = System.IO.Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            //zet de source naar het pad
            path = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dire + "\\Data\\db_highscores.mdf;Integrated Security = True";

        }
        //zet de score van de gegeven naam met score
        public static void SetScore(string name, int score)
        {
            //als de naam gelijk is 
            if(name == NamePlayer1)
            {
                //zet de speler score
                ScorePlayer1 += score;
            }
            //als de naam gelijk is 
            else if (name == NamePlayer2)
            {
                //zet de speler score
                ScorePlayer2 += score;
            }
        }

        //stuur de highscores naar de database
        public static void SetHighScores(string p1, int score_p1, string p2, int score_p2)
        {
            //pak de locale database pad
            SetDir();
            //maak een array met de gegevens aan
            string[] queryarray = { $"INSERT INTO [Table] ([Name],[Score]) VALUES ('{p1}','{score_p1}')", $"INSERT INTO [Table] ([Name],[Score]) VALUES ('{p2}','{score_p2}')"};
            //als i < kleiner is dan 2
            for (int i = 0; i < 2; i++)
            {
                //maak een connection variable aan met een pad string
                SqlConnection connection = new SqlConnection(path);
                //maak een niewe sql command
                SqlCommand command = new SqlCommand();
                //elke command
                try
                {
                    //de text van het commando
                    command.CommandText = queryarray[i];
                    //het type text van de commando
                    command.CommandType = CommandType.Text;
                    //de connectie string
                    command.Connection = connection;
                    //open de database connection
                    connection.Open();
                    //voer het uit
                    command.ExecuteNonQuery();
                    //sluit de verbinding
                    connection.Close();
                    //MessageBox.Show("Gelukt!");
                }
                catch (Exception e)
                {
                    //sluit de verbinding
                    connection.Close();
                    //geef een waarschuwing
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
