using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace project_Bomberman
{
    //this class contains everything tile related
    class Tile
    {
        public float posX = 0.0f;                   //x positie van de tile middenpunt
        public float posY = 0.0f;                   //y positie van de tile middenpunt

        public double DebugPosX = 0;




        public float TileSize = 16;                 //hoe groot is de tile

        public ImageBrush imageBrush = new ImageBrush();
        public Rectangle myRec = new Rectangle();

        public void CreateRect(int height, int width, ImageBrush img, double strokethickness)
        {

            myRec.Height = height;
            myRec.Width = width;
            myRec.Fill = img;
            myRec.Stroke = Brushes.Black;
            myRec.StrokeThickness = strokethickness;
            
            
            //myRec.
        }


        public bool CreateTile()
        {
            return false;
        }

        //public void CreateRect(qdwuwqdwq)


        public void SetMyImage(BitmapImage img)
        {
            imageBrush.ImageSource = img;
        }


    }
}
