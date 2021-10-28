using System;
using System.Collections.Generic;

namespace project_Bomberman
{
    class Collisions
    {
        //een lijst van alle tiles in de game
        public List<Tile> Alltiles = new List<Tile>();

        //check if next tile has a collider

        //check of de gegeven variabellen in de buurt koemen van een van de tiles 
        public bool CollisionCheck(double nijntjeX, double nijntjeY)
        {
            //bereken de aftand tussen de 2 locaties
            double distance = Math.Sqrt(Math.Pow((nijntjeX - Alltiles[0].DebugPosX), 2) + Math.Pow((nijntjeY - Alltiles[0].DebugPosY), 2));
            //pak de eerste tile uit de alltiles lijst en zet deze als een locale variable
            Tile tileToCheck = Alltiles[0];
            //voor elke tile in de alltiles lijst
            foreach (Tile tile in Alltiles)
            {
                if (Math.Sqrt(Math.Pow((nijntjeX - tile.DebugPosX), 2) + Math.Pow((nijntjeY - tile.DebugPosY), 2)) < distance)
                {
                    //pak de kleinere afstand en zet het als de nieuwe distance variable
                    distance = Math.Sqrt(Math.Pow((nijntjeX - tile.DebugPosX), 2) + Math.Pow((nijntjeY - tile.DebugPosY), 2));
                    //zet de PlacedOm als de kleiner tile
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
