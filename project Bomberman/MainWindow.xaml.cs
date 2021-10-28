using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace project_Bomberman
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public besturing control = new besturing();

        DispatcherTimer gameTimer = new DispatcherTimer();

        List<Rectangle> Moves = new List<Rectangle>();

        int colliderMargin = 7;
        // besturing Nijntje1
        bool goup; // Omhoog
        bool godown; // naar beneden
        bool goleft; // naar links
        bool goright; // naar rechts
        // einde besturing nijtnje 1
        //Besturing Nijntje 2
        bool goup1; // Omhoog
        bool godown1;// naar beneden
        bool goleft1;// naar links
        bool goright1; // naar rechts
        // einde besturing Nijntje 2

        bool placedBombPl1 = false;
        bool placedBombPl2 = false;

        bool placingBombPl1 = false;
        bool placingBombpl2 = false;

        double ScorePlayer1;
        double ScorePlayer2;


        Rectangle nijntje; // speler rectangle
        Rectangle nijtje2; // tegenstander rectangle
        Rectangle block; // breakable block van Hout


        int speed = 4; // Speed nijntje1
        int speed1 = 4;// speed Nijtnje 2
        int i = -1;
        int j = -1;

        Rect nijntjeHitBox;
        Rect hitBoxNijntje2;

        Rectangle landingRec;
        Rectangle landingRec1;
        int images = -1;

        Random rand = new Random();

        Collisions collider;
        List<Tile> tiles = new List<Tile>();
        List<Bomb> bombs = new List<Bomb>();

        Score score = new Score();

        Tile OnTilePl1;
        Tile OntilePl2;

        Timer cooldownP1 = new Timer(1000);
        Timer cooldownP2 = new Timer(1000);


        Random rnd = new Random();
        

        public MainWindow()
        {
            InitializeComponent();
            GameSetUp();
        }

        private void GameSetUp()
        {

            cooldownP1.AutoReset = false;
            cooldownP2.AutoReset = false;





            MyCanvas.Focus(); // Set een Canvas als belangrijkste in hetProject.
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(10); // Hier staat de Gametick op 10 Milliseconds.
            gameTimer.Start(); // start Game

            // hier worden de speler image toegevoegd
            ImageBrush nijntjeImage = new ImageBrush();
            //image nijntje 1
            nijntjeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));

            //nijntje image 2
            ImageBrush nijntjeImage2 = new ImageBrush();
            nijntjeImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));

            // moet dit hier ?
            ImageBrush breakAbleImage = new ImageBrush();
            breakAbleImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/oke1.png"));
            // 



            List<Rectangle> blocktoRemove = new List<Rectangle>();
            //hier word de grid aangemaakt

            // om het bord te maken, moeten we hieronder 3 lokale variabelen maken
            int leftPos = 180; // helpt het Grid naar rechts naar links te plaatsen
            int topPos = 900; // Helpt het Grid naar onder naar boven te plaatsen
            int a = 0; // hiermee kan je verschillende lagen instellen

            // hier word het spel bord aangemaakt
            // Deze lus 187 keer opnieuw afspellen om 188 boxen aan te maken
            for (int i = 0; i < 187; i++)
            {

                //Hier word een image toegevoegd aan een box in het spellers veld
                images++;
                ImageBrush tileImages = new ImageBrush();
                tileImages.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + images + ".jpg"));


                // hier word de box aangemaakt waaruit het spellers veld bestaad
                Rectangle box = new Rectangle
                {
                    Height = 80,
                    Width = 80,
                    Fill = tileImages,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,

                };
                Tile tile = new Tile
                {
                    myRec = box
                };

                tiles.Add(tile);


                // hier word er een naam mee gegeven aan de box waardoor die later benader baar is
                box.Name = "box" + i.ToString(); // naam box
                this.RegisterName(box.Name, box);

                Moves.Add(box); // voeg het nieuw gemaakte vak toe aan de lijst met verplaatste Rectangles

                // hieronder word het algoritme dat nodig is om de dozen 17 op een rij te zetten
                // de boxen worden aangemaakt van links naar rechts
                // als de waarde 17 is
                if (a == 17)
                {
                    topPos -= 80; // verminder in dat geval 80 van het bovenste pos integer
                    a = 37; // hier word de int a veranderd in 37 waardoor het spellerbord van rechts naar links word aangemaakt
                }
                if (a == 20)
                {
                    topPos -= 80; 
                    a = 0; 
                }
                if (a > 20)
                {
                    // als de value a is groter dan 20 worden de boxen aangemaakt van rechts naar links
                    a--;
                    Canvas.SetLeft(tile.myRec, leftPos);
                    tile.posX = leftPos;
                    leftPos -= 80; 

                }

                if (a < 17)
                {
                    // als de value a is onder de 17 wort er boxen aangemaakt van links naar rechts
                    a++; // als deze code wordt uitgevoerd moet het spelbord wel 1 opschuiven

                    leftPos += 80;
                    Canvas.SetLeft(tile.myRec, leftPos);
                    tile.posX = leftPos;
                }
                tile.posY = topPos;
                tile.setVars();// hier word het middel punt berekend
                Canvas.SetTop(tile.myRec, topPos); // hier wordt de box geplaatst op de locatie die wordt toegekent 
                MyCanvas.Children.Add(tile.myRec);
            }


            collider = new Collisions
            {
                Alltiles = tiles
            };
            // einde loop

            // hier worden de spellers aangemaakt

            // hier word speller 1 aamgemaakt
            nijntje = new Rectangle
            {
                Height = 40,
                Width = 40,
                Fill = nijntjeImage,// hier word er een image toegevoegd 
                StrokeThickness = 2
            };

            // hier word speller 2 aangemaakt
            nijtje2 = new Rectangle
            {
                Height = 40,
                Width = 40,
                Fill = nijntjeImage2, // hier word er een image toegevoegd 

            };
            MyCanvas.Children.Add(nijntje); // toegevoegd aan het spel
            MyCanvas.Children.Add(nijtje2); // toegevoegd aan het spel

            MovePiece(nijntje, "box" + 154); // hier word de locatie gezet van player 1
            MovePiece1(nijtje2, "box" + 32); // hier word de locatie gezet van player 2
 

            // hier worden de houten blokken toegevoegd aan het spellers veld
            ImageBrush blockHout = new ImageBrush();
            blockHout.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Houtblock.png"));

            // deze lus loop door het spellersveld heen en zet op de juiste locaties een hout blok neer
            for (int i = 0; i < 187; i++)
            {
                // hout block rectangle
                Rectangle block = new Rectangle
                {
                    Height = 70,
                    Width = 70,
                    Fill = blockHout,
                    StrokeThickness = 2,
                    Stroke = Brushes.Black,

                };


                // locaties waar een block geplaatst wordt
                int[] numbers = {
                   18,  19,
                20,  21,  22,  23 , 24,   25,  26,  27,  28,  29,
                30,          37,    39,
                  41,    43,    45,    47,    49,
                 52,  53,  54,  55,  56,  57,  58,  59,
                60,  61,  62,  63,  64,  65,  66,   69,
                  71,    73,    75,    77,    79,
                  81,    83,      86,  87,  88,  89,
                90,  91,  92,  93,  94,  95,  96,  97,  98,  99,
                100,   103,  105,  107,  109,
                 111,  113,  115,  117,
                120, 121, 122, 123, 124, 125, 126, 127, 128, 129,
                130, 131, 132, 133, 134,  137,  139,
                 141,  143,  145,  147,  149,
                     156, 157, 158, 159,
                160, 161, 162, 163, 164, 165, 166, 167, 168 };
                
                if (i > numbers.Length - 1)
                {
                    
                    break;
                }

                else
                {
                    Moveblock(block, "box" + numbers[i]);
                }
                
                
                    
                
                
                blocktoRemove.Add(block);
                MyCanvas.Children.Add(block);

                Rect check = new Rect(Canvas.GetLeft(block), Canvas.GetTop(block), block.Width / 2, block.Height / 2);
                foreach (Tile tile in tiles)
                {
                    Rect checkTile = new Rect(tile.DebugPosX, tile.DebugPosY, tile.myRec.Width / 2, tile.myRec.Height / 2);
                    if (checkTile.IntersectsWith(check))
                    {
                        tile.BreakablePic = blockHout;
                        tile.Type = "Breakable";
                        tile.myRec.Fill = tile.BreakablePic;
                        tile.Passable = true;
                    }

                }

                //MyCanvas.Children.Add(block);
                MyCanvas.Children.Remove(block);
            }
            
            MyCanvas.Children.Remove(block);
 
            

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {

                if (x is Rectangle && (string)x.Tag == "wall")
                {
                    Rect staticWall = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    foreach (Tile tile in tiles)
                    {
                        Rect checkTile = new Rect(tile.DebugPosX, tile.DebugPosY, tile.myRec.Width / 3, tile.myRec.Height / 3);
                        if (staticWall.IntersectsWith(checkTile))
                        {
                            tile.Type = "wall";
                        }
                    }
                }
            }
        }
        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            // key binding player 1 
            // Wat hier gebeurt is als er een key word ingedrukt veranderd er een value waardoor nijntje bijvoorbeeld naar voren gaat bewegen
            if (e.Key == Key.Left)
            {
                goleft = true;               
                nijntje.RenderTransform = new RotateTransform(0, nijntje.Width / 2, nijntje.Height / 2); // 
            }
            else if (e.Key == Key.Right)
            {
                goright = true;
                nijntje.RenderTransform = new RotateTransform(0, nijntje.Width / 2, nijntje.Height / 2);
            }
            else if (e.Key == Key.Up)
            {
                goup = true; 
                nijntje.RenderTransform = new RotateTransform(-90, nijntje.Width / 2, nijntje.Height / 2);
            }
            else if (e.Key == Key.Down)
            {
                godown = true; 
                nijntje.RenderTransform = new RotateTransform(90, nijntje.Width / 2, nijntje.Height / 2);
            }
            // einde movement keys
            // hier worden de bommen geplaatst van player 1 en 2
            if (placedBombPl1 == false && e.Key == Key.Enter && !placingBombPl1)
            {
                placingBombPl1 = true;
            }
            if (e.Key == Key.Space && !placedBombPl2 && !placingBombpl2)
            {
                placingBombpl2 = true;
            }
            // einde bommen key


            // begin keys nijntje 2
            if (e.Key == Key.A)
            {
                goleft1 = true; // change go left to true
                nijtje2.RenderTransform = new RotateTransform(0, nijtje2.Width / 2, nijtje2.Height / 2);
            }
            if (e.Key == Key.D)
            {
                goright1 = true;
                nijtje2.RenderTransform = new RotateTransform(0, nijtje2.Width / 2, nijtje2.Height / 2);
            }
            if (e.Key == Key.W)
            {
                goup1 = true; // change go up to true
                nijtje2.RenderTransform = new RotateTransform(-90, nijtje2.Width / 2, nijtje2.Height / 2);
            }
            if (e.Key == Key.S)
            {
                godown1 = true; // change go down to true
                nijtje2.RenderTransform = new RotateTransform(90, nijtje2.Width / 2, nijtje2.Height / 2);
            }
            // einde key gedeeldte 
        }

        private void Timer_ElapsedP1(object sender, ElapsedEventArgs e)
        {
            placedBombPl1 = false;
        }
        private void Timer_ElapsedP2(object sender, ElapsedEventArgs e)
        {
            placedBombPl2 = false;
        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            // hier word aangegeven wanneer de speller de key loslaat stop nijntje met bewegen 
            // voor Nijntje 1
            if (e.Key == Key.Down)
            {
                godown = false; 
            }
            if (e.Key == Key.Up)
            {
                goup = false; 
            }
            if (e.Key == Key.Left)
            {
                goleft = false; 
            }
            if (e.Key == Key.Right)
            {
                goright = false; 
            }
            // einde keys nijntje 1

            // begin keys nijntje 2
            if (e.Key == Key.S)
            {
                godown1 = false; 
            }
            if (e.Key == Key.W)
            {
                goup1 = false; 
            }
            if (e.Key == Key.A)
            {
                goleft1 = false; 
            }
            if (e.Key == Key.D)
            {
                goright1 = false; 
            }
            // einde keys nijntje 2
            // Hier wordt er een bom geplaatst al key space word uitgevoerd
            if (e.Key == Key.Space)
            {
                placingBombpl2 = false;
            }
                // bom nijntje 2
            if (e.Key == Key.Enter)
            {
                placingBombPl1 = false;
            }
            // Pauze menu, wanneer er op P word gedrukt gaat het spel op pauze
            if (e.Key == Key.P)
            {
                int top = 1000;
                int left = 100;
                
                gameTimer.Stop();
                ImageBrush pauze = new ImageBrush();
                pauze.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pause-button-png-29672.png"));
                Rectangle pauzeimage = new Rectangle
                {
                    Height = 1000,
                    Width = 1000,
                    Fill = pauze,

            };
                Canvas.SetLeft(pauzeimage, top);
                Canvas.SetTop(pauzeimage, left);
                MyCanvas.Children.Add(pauzeimage);
            }
            // Wanneer een gebruiker op P heeft gedrukt kan de gebruikker het spel verder spellen als die op enter drukt
            else
            {
                if (e.Key == Key.Enter)

                    gameTimer.Start();
                
                
            }
            // met de key M word het menu geopent in het spel
            if (e.Key == Key.M)
            {
                Hoofdmenu g = new Hoofdmenu();
                g.Visibility = Visibility.Visible;
                this.Close();

            }
        }

        private void GameLoop(object sender, EventArgs e)
        {

            if (placingBombPl1 && !placedBombPl1)
            {
                placedBombPl1 = true;
                placingBombPl1 = false;
                cooldownP1.Elapsed += Timer_ElapsedP1;
                Bomb bom = new Bomb
                {
                };
                if (bom.GetClosestTile(tiles, (Canvas.GetLeft(nijntje) + nijntje.Width / 2), (Canvas.GetTop(nijntje) + nijntje.Height / 2), OnTilePl1, Score.NamePlayer1))
                {
                    MyCanvas.Children.Add(bom.myRec);
                }
                bombs.Add(bom);
                OnTilePl1 = bom.placedOn;
                OnTilePl1.Hasplayer = true;
                cooldownP1.Start();
            }
            if (placingBombpl2 && !placedBombPl2)
            {
                placedBombPl2 = true;
                cooldownP2.Elapsed += Timer_ElapsedP2;
                Bomb bom = new Bomb
                {
                };
                if (bom.GetClosestTile(tiles, (Canvas.GetLeft(nijtje2) + nijtje2.Width / 2), (Canvas.GetTop(nijtje2) + nijtje2.Height / 2), OntilePl2, Score.NamePlayer2))
                {
                    MyCanvas.Children.Add(bom.myRec);
                }
                bombs.Add(bom);
                OntilePl2 = bom.placedOn;
                OntilePl2.Hasplayer = true;
                cooldownP2.Start();
            }


            if (goright)
            {

                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2) + speed + colliderMargin, (Canvas.GetTop(nijntje)) + nijntje.Height / 2))
                {
                    Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) + speed);
                }
            }
            if (goleft)
            {
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2) - speed - colliderMargin, (Canvas.GetTop(nijntje)) + nijntje.Height / 2))
                {
                    Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) - speed);
                }
            }
            if (goup)
            {
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2), ((Canvas.GetTop(nijntje)) + nijntje.Height / 2) - colliderMargin - speed))
                {
                    Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) - speed);
                }
            }
            if (godown)
            {
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2), ((Canvas.GetTop(nijntje)) + nijntje.Height / 2) + colliderMargin + speed))
                {
                    Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) + speed);
                }
            }

            if (goright1)
            {
                if (collider.CollisionCheck((Canvas.GetLeft(nijtje2) + nijtje2.Width / 2) + speed1 + colliderMargin, (Canvas.GetTop(nijtje2)) + nijtje2.Height / 2))
                {
                    Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) + speed1);
                }
            }
            if (goleft1)
            {
                // if go left boolean is true
                if (collider.CollisionCheck((Canvas.GetLeft(nijtje2) + nijtje2.Width / 2) - speed1 - colliderMargin, (Canvas.GetTop(nijtje2)) + nijtje2.Height / 2))
                {
                    Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) - speed1);
                }
            }
            if (goup1)
            {
                // if go up boolean is true 
                if (collider.CollisionCheck((Canvas.GetLeft(nijtje2) + nijtje2.Width / 2), ((Canvas.GetTop(nijtje2)) + nijtje2.Height / 2) - colliderMargin - speed1))
                {
                    Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) - speed1);
                }
            }
            if (godown1)
            {
                // if go down boolean is true
                if (collider.CollisionCheck((Canvas.GetLeft(nijtje2) + nijtje2.Width / 2), ((Canvas.GetTop(nijtje2)) + nijtje2.Height / 2) + colliderMargin + speed1))
                {
                    Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) + speed1);
                }
            }


            foreach (Bomb bomb in bombs)
            {
                if (bomb.destroyed)
                {
                    MyCanvas.Children.Remove(bomb.myRec);
                    bombs.Remove(bomb);

                    break;

                }
            }
            foreach (Tile tile in tiles)
            {
                if (tile.Exploding && tile.ResetDone == false && tile.ResetStarted == false)
                {
                    ImageBrush Explodingpic = new ImageBrush();
                    Explodingpic.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bieb.png"));
                    tile.myRec.Fill = Explodingpic;
                    tile.ResetStarted = true;
                }
                else if (tile.ResetDone)
                {
                    tile.myRec.Fill = tile.OgPic;
                    tile.ResetDone = false;
                    tile.Type = "normal";
                    tile.Passable = false;
                }
                if (tile.Type == "Breakable" && !tile.TileSetup)
                {
                    tile.TileSetup = true;
                    tile.myRec.Fill = tile.BreakablePic;
                }
            }






            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                // loop through all of the rectangles inside of the game and identify them using the x variable

                //Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height); // create a new rect called hit box for all of the available rectangles inside of the game


                nijntjeHitBox = new Rect(Canvas.GetLeft(nijntje), Canvas.GetTop(nijntje), nijntje.Width, nijntje.Height);
                hitBoxNijntje2 = new Rect(Canvas.GetLeft(nijtje2), Canvas.GetTop(nijtje2), nijtje2.Width, nijtje2.Height);
                //if (x.Tag != null)
                if (x is Rectangle && (string)x.Tag == "wall")
                {
                    // create a platform id variable and save the tags for the platform in it

                    // if the tag is a platform then we can do the following

                    // in order to do the hit test we need to define the elements for this app
                    // first define who the player is as in what object it is and how to calculate its height and width

                    //pacmanHitBox = new Rect(Canvas.GetLeft(nijntje), Canvas.GetTop(nijntje), nijntje.Width, nijntje.Height);
                    //hitBoxNijntje2 = new Rect(Canvas.GetLeft(nijtje2), Canvas.GetTop(nijtje2), nijtje2.Width, nijtje2.Height);

                    // second we will need to do the same for the platforms
                    // since they are running inside a loop we can use the X keyword to identify them and save their values
                    Rect platforms = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // now we can check if the player intersets or HITs the platforms if so do we can do the following
                    if (nijntjeHitBox.IntersectsWith(platforms) | hitBoxNijntje2.IntersectsWith(platforms))
                    {
                        if (goleft == true && nijntjeHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) + 10);

                            goleft = false;
                        }
                        // check if we are colliding with the wall while moving right if true then stop the pac man movement
                        if (goright == true && nijntjeHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) - 10);

                            goright = false;
                        }
                        // check if we are colliding with the wall while moving down if true then stop the pac man movement
                        if (godown == true && nijntjeHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) - 10);

                            godown = false;
                        }
                        // check if we are colliding with the wall while moving up if true then stop the pac man movement
                        if (goup == true && nijntjeHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) + 10);

                            goup = false;
                        }


                        if (goleft1 == true && hitBoxNijntje2.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) + 10);

                            goleft1 = false;
                        }
                        // check if we are colliding with the wall while moving right if true then stop the pac man movement
                        if (goright1 == true && hitBoxNijntje2.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) - 10);

                            goright1 = false;
                        }
                        // check if we are colliding with the wall while moving down if true then stop the pac man movement
                        if (godown1 == true && hitBoxNijntje2.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) - 10);

                            godown1 = false;
                        }
                        // check if we are colliding with the wall while moving up if true then stop the pac man movement
                        if (goup1 == true && hitBoxNijntje2.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) + 10);

                            goup1 = false;
                        }

                    }



                }

                foreach (Tile tile in tiles)
                {
                    Rect tileHitbox = new Rect(tile.posX, tile.posY, tile.myRec.Width, tile.myRec.Height);
                    if (nijntjeHitBox.IntersectsWith(tileHitbox) && tile.Exploding)
                    {
                        nijntje.Fill = null;
                    }
                    if (hitBoxNijntje2.IntersectsWith(tileHitbox) && tile.Exploding)
                    {
                        nijtje2.Fill = null;
                    }
                }






            }
        }


        private void MovePiece(Rectangle nijntje, string posName)
        {

            //this function will move the player and the opponent across the board
            //the way it does it is very simply, we have added of the board rectangles to the moves list
            //from the for each loop below we can loop through all of the rectangles from that list

            //we are also checking if any of the rectangle has the posName, if they do then we will link the landing rect to that rectangle found inside of the for each loop

            //this way we can move the rectangle that is being passed inside of this function and run in the timer event to animate it when it starts



            foreach (Rectangle rectangle in Moves)
            {
                if (rectangle.Name == posName)
                {
                    landingRec = rectangle;
                }
            }


            // the two lines here will place the "player" object that is being passed in this function to the landingRec location
            Canvas.SetLeft(nijntje, Canvas.GetLeft(landingRec) + nijntje.Width / 2);

            Canvas.SetTop(nijntje, Canvas.GetTop(landingRec) + nijntje.Height / 2);


        }
        private void MovePiece1(Rectangle nijtje2, string posName)
        {

            //this function will move the player and the opponent across the board
            //the way it does it is very simply, we have added of the board rectangles to the moves list
            //from the for each loop below we can loop through all of the rectangles from that list

            //we are also checking if any of the rectangle has the posName, if they do then we will link the landing rect to that rectangle found inside of the for each loop

            //this way we can move the rectangle that is being passed inside of this function and run in the timer event to animate it when it starts



            foreach (Rectangle rectangle in Moves)
            {
                if (rectangle.Name == posName)
                {
                    landingRec1 = rectangle;
                }
            }


            // the two lines here will place the "player" object that is being passed in this function to the landingRec location
            Canvas.SetLeft(nijtje2, Canvas.GetLeft(landingRec1) + nijtje2.Width / 2);
            Canvas.SetTop(nijtje2, Canvas.GetTop(landingRec1) + nijtje2.Height / 2);

        }
        private void Moveblock(Rectangle block, string posName)
        {

            //this function will move the player and the opponent across the board
            //the way it does it is very simply, we have added of the board rectangles to the moves list
            //from the for each loop below we can loop through all of the rectangles from that list

            //we are also checking if any of the rectangle has the posName, if they do then we will link the landing rect to that rectangle found inside of the for each loop

            //this way we can move the rectangle that is being passed inside of this function and run in the timer event to animate it when it starts



            foreach (Rectangle rectangle in Moves)
            {
                if (rectangle.Name == posName)
                {
                    landingRec1 = rectangle;
                }
            }


            // the two lines here will place the "player" object that is being passed in this function to the landingRec location
            Canvas.SetLeft(block, Canvas.GetLeft(landingRec1) + 5);
            Canvas.SetTop(block, Canvas.GetTop(landingRec1) + 5);

        }
    }
}













