using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Bomberman
{
    class Collisions
    {
        public List<Tile> Alltiles = new List<Tile>();

        //check if next tile has a collider


        public bool CollisionCheck(double nijntjeX, double nijntjeY)
        {
            double distance = Math.Sqrt(Math.Pow((nijntjeX - Alltiles[0].DebugPosX), 2) + Math.Pow((nijntjeY - Alltiles[0].DebugPosY), 2));
            Tile tileToCheck = Alltiles[0];
            foreach(Tile tile in Alltiles)
            {
                if(Math.Sqrt(Math.Pow((nijntjeX - tile.DebugPosX), 2) + Math.Pow((nijntjeY - tile.DebugPosY), 2)) < distance)
                {
                    distance = Math.Sqrt(Math.Pow((nijntjeX - tile.DebugPosX), 2) + Math.Pow((nijntjeY - tile.DebugPosY), 2));
                    tileToCheck = tile;
                    

                }
            }
            if (!tileToCheck.Passable && !tileToCheck.HasBomb || tileToCheck.Hasplayer)
            {
                return true;
            }
            return false;
        }

    }
}
