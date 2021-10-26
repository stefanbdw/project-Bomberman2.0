﻿using System;
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
        int rangeExplosion = 5;

        public Rectangle myRec;
        public ImageBrush brush = new ImageBrush();
        public Tile placedOn;
        public bool destroyed = false;
        public List<Tile> ExplodingTiles = new List<Tile>();
        private Timer bombTime = new Timer(3000);
        private List<Tile> GameTiles = new List<Tile>();
        public Canvas canvas;

        //create a bomb
        public void CreateBomb()
        {
            //brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bieb.png"));
            //nijntjeImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));
            StartBombCounter();
        }

        public void StartBombCounter()
        {
            //Timer bombTime = new Timer(5000);
            bombTime.Start();
            bombTime.Elapsed += BombTime_Elapsed;
            bombTime.AutoReset = false;
        }
        public void CreateExplosion()
        {
            placedOn.Exploding = true;


        }

        private void BombTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            placedOn.HasBomb = false;
            destroyed = true;
            //ImageBrush Explodingpic = new ImageBrush();
            //Explodingpic.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bieb.png"));
            bool up = true;
            bool down = true;
            bool left = true;
            bool right = true;
            //ga elke richting op

            for (int i = 0; i < rangeExplosion; i++)
            {
                Tile modTile;
                if (ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY).Type != "wall" && right)
                {
                    modTile = ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY);
                    //ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY));
                    if (modTile.Type != "Breakable")
                    {
                        ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY));
                    }
                    else
                    {

                        ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY));
                        right = false;
                    }
                }
                else
                {
                    right = false;
                }
                //ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX + (i * 80), placedOn.DebugPosY));
                if (ReturnClosestTile(GameTiles, placedOn.DebugPosX - (i * 80), placedOn.DebugPosY).Type != "wall" && left)
                {
                    modTile = ReturnClosestTile(GameTiles, placedOn.DebugPosX - (i * 80), placedOn.DebugPosY);
                    if (modTile.Type != "Breakable")
                    {
                        ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX - (i * 80), placedOn.DebugPosY));
                    }
                    else
                    {
                        ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX - (i * 80), placedOn.DebugPosY));
                        left = false;
                    }
                }
                else
                {
                    left = false;
                }
                //ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX - (i * 80), placedOn.DebugPosY));
                if (ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY + (i * 80)).Type != "wall" && up)
                {
                    modTile = ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY + (i * 80));
                    if (modTile.Type != "Breakable")
                    {
                        ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY + (i * 80)));
                    }
                    else
                    {
                        up = false;
                        ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY + (i * 80)));
                    }
                }
                else
                {
                    up = false;
                }
                if (ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY - (i * 80)).Type != "wall" && down)
                {
                    modTile = ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY - (i * 80));
                    if (modTile.Type != "Breakable")
                    {
                        ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY - (i * 80)));
                    }
                    else
                    {
                        down = false;
                        ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY - (i * 80)));
                    }
                }
                else
                {
                    down = false;
                }
                //ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY + (i * 80)));
                //ExplodingTiles.Add(ReturnClosestTile(GameTiles, placedOn.DebugPosX, placedOn.DebugPosY - (i * 80)));

            }
            foreach (Tile tile in ExplodingTiles)
            {
                tile.Exploding = true;
                //tile.myRec.Fill = Explodingpic;
            }

        }

        //create the object in rectangle form
        public Rectangle CreateRect(int height, int width, ImageBrush img, double strokethickness)
        {
            myRec = new Rectangle
            {
                Height = height,
                Width = width,
                Fill = img,
                Stroke = Brushes.Black,
                StrokeThickness = strokethickness
            };

            return myRec;
        }

        public bool GetClosestTile(List<Tile> tiles, double x, double y)
        {

            double closetsx = 1000;
            double closesty = 1000;
            Tile closestTile;
            double testVar = (x + y);
            //calutale the distance between these two points
            double distance = Math.Sqrt(Math.Pow((x - tiles[0].DebugPosX), 2) + Math.Pow((y - tiles[0].DebugPosY), 2));

            foreach (Tile tile in tiles)
            {
                //check if the distance between points is smaller then the already set distance
                if (Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2)) < distance)
                {
                    distance = Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2));
                    placedOn = tile;
                }

            }
            if (placedOn != null && !placedOn.HasBomb)
            {
                //CreateBomb();
                //brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bieb.png"));
                myRec = CreateRect(60, 60, brush, 1);
                Canvas.SetLeft(myRec, placedOn.posX);
                Canvas.SetTop(myRec, placedOn.posY);
                placedOn.HasBomb = true;
                placedOn.Hasplayer = true;
                StartBombCounter();
                return true;
            }
            return false;
        }
        public bool GetClosestTile(List<Tile> tiles, double x, double y, Tile playerTile)
        {
            GameTiles = tiles;
            double closetsx = 1000;
            double closesty = 1000;
            Tile closestTile;
            double testVar = (x + y);
            //calutale the distance between these two points
            double distance = Math.Sqrt(Math.Pow((x - tiles[0].DebugPosX), 2) + Math.Pow((y - tiles[0].DebugPosY), 2));

            foreach (Tile tile in tiles)
            {
                //check if the distance between points is smaller then the already set distance
                if (Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2)) < distance)
                {
                    distance = Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2));
                    placedOn = tile;
                }

            }

            if (placedOn != null && !placedOn.HasBomb)
            {
                if (playerTile != null && placedOn != playerTile)
                {
                    playerTile.Hasplayer = false;
                    placedOn.Hasplayer = true;
                }
                else
                {
                    placedOn.Hasplayer = true;
                }
                brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/BOM.png"));
                myRec = CreateRect(80, 80, brush, 1);
                Canvas.SetLeft(myRec, placedOn.posX);
                Canvas.SetTop(myRec, placedOn.posY);
                placedOn.HasBomb = true;
                StartBombCounter();
                return true;
            }
            return false;
        }
        public Tile ReturnClosestTile(List<Tile> tiles, double x, double y)
        {

            double closetsx = 1000;
            double closesty = 1000;
            Tile closestTile = tiles[0];
            double testVar = (x + y);
            //calutale the distance between these two points
            double distance = Math.Sqrt(Math.Pow((x - tiles[0].DebugPosX), 2) + Math.Pow((y - tiles[0].DebugPosY), 2));

            foreach (Tile tile in tiles)
            {
                //check if the distance between points is smaller then the already set distance
                if (Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2)) < distance)
                {
                    distance = Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2));
                    //placedOn = tile;
                    closestTile = tile;
                }

            }
            //if (placedOn != null && !placedOn.HasBomb)
            //{
            //    //CreateBomb();
            //    brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/oke1.png"));
            //    myRec = CreateRect(60, 60, brush, 1);
            //    Canvas.SetLeft(myRec, placedOn.posX);
            //    Canvas.SetTop(myRec, placedOn.posY);
            //    placedOn.HasBomb = true;
            //    StartBombCounter();
            //    return placedOn;
            //}
            return closestTile;
        }



    }
}
