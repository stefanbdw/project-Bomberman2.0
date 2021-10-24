using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Timers;
namespace project_Bomberman
{
    //this class contains everything tile related
    class Tile
    {
        public double posX = 0.0;                   //x positie van de tile middenpunt
        public double posY = 0.0;                   //y positie van de tile middenpunt

        public double DebugPosX = 0;                //middenpunt van tile x
        public double DebugPosY = 0;                //middenpunt van tile y
        public Brush OgPic = new ImageBrush();

        public Timer ResetTimer = new Timer(500);
        public string Type = "";

        private bool exploding = false;
        public bool Exploding
        {
            get
            {
                return exploding;
            }
            set
            {
                if (value)
                {
                    //Explode();
                    exploding = value;
                    
                }
                exploding = value;
            }
        }
        public bool HasBomb = false;                //does the tile have a bomb
        public bool Passable = false;
        public bool Hasplayer = false;
        public bool ResetDone = false;
        private bool resetStarted = false;
        public bool ResetStarted
        {
            get
            {
                return resetStarted;
            }
            set
            {
                if (value)
                {
                    CooldownTile();
                }
                resetStarted = value;
            }
        }
        public TextBox textBox;                     //een textbox

        public float TileSize = 16;                 //hoe groot is de tile

        public ImageBrush imageBrush = new ImageBrush();
        public Rectangle myRec = new Rectangle();           //the rectangle in the class

        //public Bomb Bomb;
        public void CooldownTile()
        {
            ResetTimer.Elapsed += ResetTimer_Elapsed;
            ResetTimer.Start();
        }

        private void ResetTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Exploding = false;
            ResetDone = true;
            resetStarted = false;
            ResetTimer.Stop();
        }

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
            OgPic = myRec.Fill;
            //SetTextBlock();
        }

        public void Explode()
        {
            ImageBrush Explodingpic = new ImageBrush();
            Explodingpic.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/oke.png"));

            //CreateRect(80, 80, Explodingpic, 2);
            //Dispatcher.CurrentDispatcher.Invoke(()=>
            //    {
            //        myRec.Fill = Explodingpic;
            //    });
            //System.Windows.Application.Current.Dispatcher.Invoke(
            //System.Windows.Threading.DispatcherPriority.Render, (Action)delegate
            //{
            //    myRec.Fill = Explodingpic;
            //});
            //myRec.Fill = Explodingpic;
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
