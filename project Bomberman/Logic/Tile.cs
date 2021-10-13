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
    //this class contains everything tile related
    class Tile
    {
        public float posX = 0.0f;                   //x positie van de tile middenpunt
        public float posY = 0.0f;                   //y positie van de tile middenpunt

        public double DebugPosX = 0;                //middenpunt van tile x
        public double DebugPosY = 0;                //middenpunt van tile y

        public TextBox textBox;                     //een textbox

        public float TileSize = 16;                 //hoe groot is de tile

        public ImageBrush imageBrush = new ImageBrush();
        public Rectangle myRec = new Rectangle();           //the rectangle in the class

        //create a rectangle in the tileclass
        public void CreateRect(int height, int width, ImageBrush img, double strokethickness)
        {
            myRec.Height = height;
            myRec.Width = width;
            myRec.Fill = img;
            myRec.Stroke = Brushes.Black;
            myRec.StrokeThickness = strokethickness;
            
            
            //myRec.
        }
        
        //calculate the middlepoint of the tile
        public void setVars()
        {
            //calculate the middle y point
            DebugPosY = posY + (myRec.Height / 2);
            //calculate the middle x point
            DebugPosX = posX + (myRec.Width / 2);
            SetTextBlock();
        }
        //create a new textblock and enter the middle point in it
        public void SetTextBlock()
        {
            textBox = new TextBox
            {
                Height = 50,
                Width = 50,
                Text = DebugPosX.ToString() + DebugPosY.ToString()
            };
        }


        
        public void SetMyImage(BitmapImage img)
        {
            imageBrush.ImageSource = img;
        }


    }
}
