using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace project_Bomberman
{
    //deze class bezit een constructor voor de score


    public class Score
    {
        public static Dictionary<string, int> Scores = new Dictionary<string, int>();
        const string connectionString = "";

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


    }
}
