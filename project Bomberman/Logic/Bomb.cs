using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace project_Bomberman
{
    class Bomb
    {

        //zet de afstand en berijk van de explosie
        int rangeExplosion = 5;
        //De rectangle van de tile class hiermee wordt her plaatje weergeveb
        public Rectangle myRec;
        //Maakt een image brush aan zodat we met gemak plaatjes kunnen vastzetten
        public ImageBrush brush = new ImageBrush();
        //De tile class waar de bom op geplaatst wordt
        public Tile placedOn;
        // als de tile vernietigd moet worden word deze bool naar true gezet
        public bool destroyed = false;
        //een lijst waar alle tiles in komen die door een explosie berijkt worden
        public List<Tile> ExplodingTiles = new List<Tile>();
        //een timer class die regelt dat de bom na (milliseconden) ontploft
        private Timer bombTime = new Timer(3000);
        //een lijst van alle tiles die aanwezig zijn ingame
        private List<Tile> GameTiles = new List<Tile>();
        //de naam van de eigenaar die de bom heeft geplaatst
        public string bombOwner;

        
        //activeer de timer van de bom 
        public void StartBombCounter()
        {
            //start de timer
            bombTime.Start();
            //na aantal seconden als er een interval is geweest roep aangegeven funtie aan
            bombTime.Elapsed += BombTime_Elapsed;
            //zet de auto reset uit zodat de code niet gaat loopen
            bombTime.AutoReset = false;
        }

        //functie die bom ontploft nadat hij is aangeroepen door een timer
        private void BombTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            // registreerd dat de bom weg is van de tile
            placedOn.HasBomb = false;
            //registreerd dat de bom destroyed is. zodat de engine hem in de volgende update correct weghaald
            destroyed = true;

            //onderstaande bools regelen de richting van de explosie
            bool up = true;         //explosie gaat omhoog = true
            bool down = true;       //explosie gaat omlaag = true
            bool left = true;       //explosie gaat links = true
            bool right = true;      //explosie gaat rechts = true

            //voor elke stap in de range van de de explosie 
            for (int i = 0; i < rangeExplosion; i++)
            {
                //een locale tile variable om te verkomen dat de <ReturnClosestTile> te vaak gebruikt wordt
                Tile modTile;
                //pak de dichtsbijstaande tile en check of die tile een muur type is en dat de richt bool aan staat
                if (ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY).Type != "wall" && right)
                {
                    //modtile wordt de tile van ReturnClosestTile
                    modTile = ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY);
                    //check of de type van de tile geen breakable is
                    if (modTile.Type != "Breakable")
                    {
                        //voeg de tile toe aan de explosie lijst
                        ExplodingTiles.Add(modTile);

                        //ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY));
                    }
                    //als de modtile wel een breakable type is. zorg ervoor dat de explosie ophoud maar de tile nogwel kapot gaat
                    else
                    {
                        //voeg de tile toe aan de explosie lijst
                        ExplodingTiles.Add(modTile);
                        //zet de right bool uit zodat de explosie stopt
                        right = false;
                        //geef de speler punten via de static score class
                        Score.SetScore(bombOwner, 100);
                    }
                }
                else
                {
                    //zet de right bool uit zodat de explosie stopt
                    right = false;
                }
                //pak de dichtsbijstaande tile en check of die tile een muur type is en dat de left bool aan staat
                if (ReturnClosestTile(GameTiles, placedOn.DebugPosX - (i * 80), placedOn.DebugPosY).Type != "wall" && left)
                {
                    //een locale tile variable om te verkomen dat de <ReturnClosestTile> te vaak gebruikt wordt
                    modTile = ReturnClosestTile(GameTiles, placedOn.DebugPosX - (i * 80), placedOn.DebugPosY);
                    //check of de type van de tile geen breakable is
                    if (modTile.Type != "Breakable")
                    {
                        //voeg de tile toe aan de explosie lijst
                        ExplodingTiles.Add(modTile);
                    }
                    //als de modtile wel een breakable type is. zorg ervoor dat de explosie ophoud maar de tile nogwel kapot gaat
                    else
                    {
                        //voeg de tile toe aan de explosie lijst
                        ExplodingTiles.Add(modTile);
                        //zet de left bool uit zodat de explosie stopt
                        left = false;
                        //geef de speler punten via de static score class
                        Score.SetScore(bombOwner, 100);
                    }
                }
                else
                {
                    //zet de left bool uit zodat de explosie stopt
                    left = false;
                }
                //pak de dichtsbijstaande tile en check of die tile een muur type is en dat de up bool aan staat
                if (ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY + (i * 80)).Type != "wall" && up)
                {
                    //een locale tile variable om te verkomen dat de <ReturnClosestTile> te vaak gebruikt wordt
                    modTile = ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY + (i * 80));
                    //check of de type van de tile geen breakable is
                    if (modTile.Type != "Breakable")
                    {
                        //voeg de tile toe aan de explosie lijst
                        ExplodingTiles.Add(modTile);
                    }
                    else
                    {
                        //zet de up bool uit zodat de explosie stopt
                        up = false;
                        //voeg de tile toe aan de explosie lijst
                        ExplodingTiles.Add(modTile);
                        //geef de speler punten via de static score class
                        Score.SetScore(bombOwner, 100);
                    }
                }
                else
                {
                    //zet de up bool uit zodat de explosie stopt
                    up = false;
                }
                if (ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY - (i * 80)).Type != "wall" && down)
                {
                    //een locale tile variable om te verkomen dat de <ReturnClosestTile> te vaak gebruikt wordt
                    modTile = ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY - (i * 80));
                    //check of de type van de tile geen breakable is
                    if (modTile.Type != "Breakable")
                    {
                        //voeg de tile toe aan de explosie lijst
                        ExplodingTiles.Add(modTile);
                    }
                    else
                    {
                        //zet de down bool uit zodat de explosie stopt
                        down = false;
                        //voeg de tile toe aan de explosie lijst
                        ExplodingTiles.Add(modTile);
                        //geef de speler punten via de static score class
                        Score.SetScore(bombOwner, 100);
                    }
                }
                else
                {
                    //zet de up bool uit zodat de explosie stopt
                    down = false;
                }

            }
            //loop door de exploding tile lijst en zet de tiles op exploding
            foreach (Tile tile in ExplodingTiles)
            {
                //zet exploding op true
                tile.Exploding = true;
            }

        }

        //maak een rectangle aan met de gegeven variabellen 
        public Rectangle CreateRect(int height, int width, ImageBrush img, double strokethickness)
        {
            //my rec wordt een nieuwe rectangle
            myRec = new Rectangle
            {
                Height = height,
                Width = width,
                Fill = img,
                Stroke = Brushes.Black,
                StrokeThickness = strokethickness
            };
            //geef de gemaakte rectangle terug
            return myRec;
        }

        //pack de dichtsbezijnde tile 
        public bool GetClosestTile(List<Tile> tiles, double x, double y, Tile playerTile, string playerName)
        {
            //voeg de tiles toe aan de Gametiles lijst
            GameTiles = tiles;
            //zet de naam van de speler als de eigenaar
            bombOwner = playerName;

            //bereken de aftand tussen de 2 locaties
            double distance = Math.Sqrt(Math.Pow((x - tiles[0].DebugPosX), 2) + Math.Pow((y - tiles[0].DebugPosY), 2));
            //voor elke tile in de tiles lijst
            foreach (Tile tile in tiles)
            {
                //check of de aftand tussen de volgende tile in lijst kleiner is
                if (Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2)) < distance)
                {
                    //pak de kleinere afstand en zet het als de nieuwe distance variable
                    distance = Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2));
                    //zet de PlacedOm als de kleiner tile
                    placedOn = tile;
                }

            }
            //als placedOn niet leef is en als PlacedOn tile geen bom heeft
            if (placedOn != null && !placedOn.HasBomb)
            {
                //als de playertile niet leef is en als placedOn niet gelijk is aan de playertile
                if (playerTile != null && placedOn != playerTile)
                {
                    //playerTile heeft geen player
                    playerTile.Hasplayer = false;
                    //placedOn heeft nu de speler en gaat op true
                    placedOn.Hasplayer = true;
                }
                else
                {
                    //zet de speler op placedon
                    placedOn.Hasplayer = true;
                }
                //maak een bom plaatje aan en zet hem in de brush als een imagesource
                brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/BOM.png"));
                //roep de createRectclass aan en zet de resultaat als myRec
                myRec = CreateRect(80, 80, brush, 1);
                //plaat de myRec op de gegeven positie op de x
                Canvas.SetLeft(myRec, placedOn.posX);
                //plaat de myRec op de gegeven positie op de y
                Canvas.SetTop(myRec, placedOn.posY);
                //placedOn heeft nu de bom
                placedOn.HasBomb = true;
                //activeer de timer van de bom
                StartBombCounter();
                //geef aan dat de functie gelukt is
                return true;
            }
            //geef aan dat de functie niet correct is uitgevoerd
            return false;
        }
        //geef de dichtbijzijnde tile terug
        public Tile ReturnClosestTile(List<Tile> tiles, double x, double y)
        {
            //zet de locale tile als de eerste tile uit de meegegeven lijst
            Tile closestTile = tiles[0];
            //bereken de aftand tussen de 2 locaties
            double distance = Math.Sqrt(Math.Pow((x - tiles[0].DebugPosX), 2) + Math.Pow((y - tiles[0].DebugPosY), 2));
            //voor elke tile in de tiles lijst
            foreach (Tile tile in tiles)
            {
                //check of de aftand tussen de volgende tile in lijst kleiner is
                if (Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2)) < distance)
                {
                    //pak de kleinere afstand en zet het als de nieuwe distance variable
                    distance = Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2));
                    //zet de dichtsbijzijnde tile als de niewe closest tile
                    closestTile = tile;
                }
            }
            //geef de dichtsbijzijnde tile terug
            return closestTile;
        }

    }
}
