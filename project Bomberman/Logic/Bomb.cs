using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;


namespace project_Bomberman
{
    class Bomb
    {
        int rangeExplosion = 2;

        public Rectangle myRec;
        public ImageBrush brush = new ImageBrush();
        public Tile placedOn;

        //create a bomb
        public void CreateBomb()
        {
            brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/oke1.png"));
            //nijntjeImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));
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
            //myRec.Height = height;
            //myRec.Width = width;
            //myRec.Fill = img;
            //myRec.Stroke = Brushes.Black;
            //myRec.StrokeThickness = strokethickness;
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
                if(Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2)) < distance)
                {
                    distance = Math.Sqrt(Math.Pow((x - tile.DebugPosX), 2) + Math.Pow((y - tile.DebugPosY), 2));
                    placedOn = tile;
                }
  
            }
            if (!placedOn.HasBomb)
            {
                //CreateBomb();
                brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/oke1.png"));
                myRec = CreateRect(60, 60, brush, 1);
                Canvas.SetLeft(myRec, placedOn.posX);
                Canvas.SetTop(myRec, placedOn.posY);
                return true;
            }
            return false;
        }



    }
}
