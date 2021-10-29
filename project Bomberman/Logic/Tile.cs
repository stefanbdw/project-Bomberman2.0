using System;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace project_Bomberman
{
    //this class contains everything tile related
    class Tile
    {
        public double posX = 0.0;                   //x positie van de tile 
        public double posY = 0.0;                   //y positie van de tile 

        public double DebugPosX = 0;                //middenpunt van tile x
        public double DebugPosY = 0;                //middenpunt van tile y
        public Brush OgPic = new ImageBrush();      //orginele plaatje van de tile
        public Brush BreakablePic = new ImageBrush();   //plaatje als de tile breekbaar is
        public Timer ResetTimer = new Timer(500);       //De resettimer regelt tijd voordat de tile terug gaat naar de orginele staat
        public string Type = "";                        //Het type dat de tile is bijvoorbeeld "breakable"

        public bool Exploding = false;                  //ontploft deze tile
        public bool TileSetup = false;                  //word de tile veranderd
        public bool HasBomb = false;                    //bezit de tile een bom
        public bool Passable = false;                   //Kan deze tile gepaseerd worden
        public bool Hasplayer = false;                  //heeft de spler zijn bom als laatste op dezr tile geplaatst
        public bool ResetDone = false;                  //is de tile klaar met zijn reset
        private bool resetStarted = false;              //is de reset gestart
        public bool ResetStarted                        //get set om reset te starten zodra de variabel is veranderd
        {
            //lever de info van de private resetStarted bool
            get
            {
                return resetStarted;
            }
            set
            {
                //als de nieuwe waarde true is
                if (value)
                {
                    //start de cooldown
                    CooldownTile();
                }
                //zet de nieuwe waarde in de private bool
                resetStarted = value;
            }
        }
 

        public ImageBrush imageBrush = new ImageBrush();        //een image brush om plaatjes aan tiles toe te voegen
        public Rectangle myRec = new Rectangle();               //de rectangle die deze tile vertegnwoordigt

        //start de timer voor de reset van de tile
        public void CooldownTile()
        {
            //roep functie aan als de resetimer interval is geweest
            ResetTimer.Elapsed += ResetTimer_Elapsed;
            //start de resettimer
            ResetTimer.Start();
        }
        //wordt aangeroepen als de timer een interval heeft gehad. hier wordt de tile gereset
        private void ResetTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //de tile explodeert niet meer
            Exploding = false;
            //de tile is klaar en wordt gereset
            ResetDone = true;
            //Reset is klaar en geef dit aan
            resetStarted = false;
            //stop de timer
            ResetTimer.Stop();
        }
        //bereken de middenpunt van de tile
        public void setVars()
        {
            //pak het middelste punt van de y en zet hem naar debugPosY
            DebugPosY = posY + (myRec.Height / 2);
            //pak het middelste punt van de x en zet hem naar debugPosX
            DebugPosX = posX + (myRec.Width / 2);
            //Plaats het orginele plaatje in de tile
            OgPic = myRec.Fill;
        }
    }
}
