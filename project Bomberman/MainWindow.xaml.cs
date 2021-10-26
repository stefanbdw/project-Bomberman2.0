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
        bool goup; // this boolean will be used for the player to go up the screen halloooo
        bool godown; // this boolean will be used for the player to go down the screen
        bool goleft; // this boolean will be used for the player to go left to the screen
        bool goright; // this boolean will be used for the player to right to the screen

        bool goup1; // this boolean will be used for the player to go up the screen
        bool godown1; // this boolean will be used for the player to go down the screen
        bool goleft1; // this boolean will be used for the player to go left to the screen
        bool goright1; // this boolean will be used for the player to right to the screen

        bool placedBombPl1 = false;
        bool placedBombPl2 = false;

        bool placingBombPl1 = false;
        bool placingBombpl2 = false;

        double ScorePlayer1;
        double ScorePlayer2;


        Rectangle nijntje; // player rectangle
        Rectangle nijtje2; // opponent rectangle
        Rectangle block;


        int speed = 4;
        int speed1 = 4;// this integer is for the speed of the player
        int i = -1;
        int j = -1;

        Rect pacmanHitBox;
        Rect hitBoxNijntje2;

        Rectangle landingRec;
        Rectangle landingRec1;
        // position and current position integer for the player
        int position;
        int currentPosition;

        // position and current position for the opponent
        int opponentPosition;
        int opponentCurrentPosition;

        // this images integer will be used to show the board images when we create them
        int images = -1;

        int bombRefreshPl1 = 5000;          //in milliseconds
        int bombRefreshPl2 = 5000;         //in milliseconds
        // new random class instance called rand will be used to calculate the dice rolls in the game
        Random rand = new Random();

        // two Boolean which will detemrine whos turn it is in the game
        bool playerOneRound, playerTwoRound;

        // this integer will show the current position of the player and the opponent to the GUI
        int tempPos;

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





            MyCanvas.Focus(); // set my canvas as the main focus for the program
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(10); // set time to tick every 20 milliseconds
            gameTimer.Start(); // start the time


            ImageBrush nijntjeImage = new ImageBrush();

            nijntjeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));


            ImageBrush nijntjeImage2 = new ImageBrush();
            nijntjeImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));

            ImageBrush breakAbleImage = new ImageBrush();
            breakAbleImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/oke1.png"));



            List<Rectangle> blocktoRemove = new List<Rectangle>();
            //this is the set up game function.In this function we will set up the game board, the player and the opponent

            // in order to create the board we will need to make 3 local variables below
            int leftPos = 180; // left pos will help us position the boxes from right to left 
            int topPos = 900; // top pos will help us position the boxes from bottom to top
            int a = 0; // a integer will help us to lay 10 boxes in a row

            // the two lines below are importing the images for the player and the opponent and attaching them to the image brush we created earlier



            // this is the main for loop where we will make the game board
            // this loop will run a 100 times inside of this function
            // it will run like this because we need 100 tiles for this game to work
            for (int i = 0; i < 187; i++)
            {
                // first we increment the images integer we created in the program before

                //create a new image brush called tile images, this will attach an image to the rectangles for the board
                images++;
                ImageBrush tileImages = new ImageBrush();




                tileImages.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + images + ".jpg"));

                // below we are creating a new rectangle called box
                // this rectangle will have 60x60 height and width, fill is the tile images and a black border around it
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
                //create a tile with the class

                tiles.Add(tile);


                // we need to indentify the rectangle created in this loop, so we will give each box a unique name
                box.Name = "box" + i.ToString(); // name the boxes 
                this.RegisterName(box.Name, box); // register the name inside of the WPF app

                Moves.Add(box); // add the newly created box to the moves rectangles list

                // below we are making the algorithm we need to lay the boxes 10 in a row
                // we will make the boxes from left to right then move up and reverse that process
                // remember "a" integer is controlling how we position the boxes down so we need to keep in mind on it can be controlled inside of this loop

                // if a is equals to 10
                if (a == 17)
                {
                    // this will happen when we have positioned 10 boxes from left to right
                    topPos -= 80; // in that case reduce 60 from the top pos integer 
                    a = 37; // change the value of a to 30, we are doing this to move the boxes from right to left now
                }

                // if a is equals to 20
                if (a == 20)
                {

                    topPos -= 80; // again reduce 60 from the top pos integer
                    a = 0; // set a integer back to 0
                }

                // if a is greater than 20
                if (a > 20)
                {
                    // if the value of a is greater than 20 then we can
                    // this if statement will help us position the boxes from right to left
                    a--; // reduce 1 from a each loop
                    //Canvas.SetLeft(box, leftPos); // set the box inside the canvas by the value of the left pos integer
                    Canvas.SetLeft(tile.myRec, leftPos);
                    tile.posX = leftPos;            //set the tile x pos to the left value
                    leftPos -= 80; // reduce 60 from the left pos each loop

                }

                // if a is less than 10
                if (a < 17)
                {
                    // this will happen when we want to position the boxes from left to right
                    //if the value of a is less than 10 
                    a++; // add 1 to a integer each loop

                    leftPos += 80; // add 60 to the left pos integer 
                    Canvas.SetLeft(tile.myRec, leftPos);            //sets the tile
                    tile.posX = leftPos;            //set the tile x pos to the left value
                }
                tile.posY = topPos;
                //Canvas.SetTop(box, topPos); //set the box top position to the value of top pos integer each loop
                tile.setVars();                 //calculate middlepoints
                Canvas.SetTop(tile.myRec, topPos);              //sets the box top position to the given value
                MyCanvas.Children.Add(tile.myRec);


                //add the new rec to my scanvas

            }
            collider = new Collisions
            {
                Alltiles = tiles
            };
            // end the loop
            nijntje = new Rectangle
            {
                Height = 40,
                Width = 40,
                Fill = nijntjeImage,
                StrokeThickness = 2
            };

            // set up the opponent rectangle the same way as the player
            nijtje2 = new Rectangle
            {
                Height = 40,
                Width = 40,
                Fill = nijntjeImage2,

            };
            MyCanvas.Children.Add(nijntje);
            MyCanvas.Children.Add(nijtje2);

            MovePiece(nijntje, "box" + 154);             //was 34
            MovePiece1(nijtje2, "box" + 32);
            // end the loop
            ImageBrush blockHout = new ImageBrush();
            blockHout.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/Houtblock.png"));
            for (int i = 0; i < 187; i++)
            {

                Rectangle block = new Rectangle
                {
                    Height = 70,
                    Width = 70,
                    Fill = blockHout,
                    StrokeThickness = 2,
                    Stroke = Brushes.Black,

                };



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
            //for(int i < tile)
            

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
            // if the left key is pressed then do the following
            if (e.Key == Key.Left)
            {
                goleft = true; // change go left to true                
                nijntje.RenderTransform = new RotateTransform(0, nijntje.Width / 2, nijntje.Height / 2);
            }
            // end of left key selection
            // if the right key is pressed then do the following
            else if (e.Key == Key.Right)
            {
                goright = true;

                nijntje.RenderTransform = new RotateTransform(0, nijntje.Width / 2, nijntje.Height / 2);
            }
            // end of right key selection

            // if the up key is pressed then do the following
            else if (e.Key == Key.Up)
            {

                goup = true; // change go up to true

                nijntje.RenderTransform = new RotateTransform(-90, nijntje.Width / 2, nijntje.Height / 2);
            }
            // if the down key is pressed then do the following
            else if (e.Key == Key.Down)
            {

                godown = true; // change go down to true

                nijntje.RenderTransform = new RotateTransform(90, nijntje.Width / 2, nijntje.Height / 2);
            }
            // end of the down key selection
            if (placedBombPl1 == false && e.Key == Key.Enter && !placingBombPl1)
            {
                placingBombPl1 = true;

            }
            if (e.Key == Key.Space && !placedBombPl2 && !placingBombpl2)
            {
                placingBombpl2 = true;
            }



            // if the left key is pressed then do the following
            if (e.Key == Key.A)
            {
                goleft1 = true; // change go left to true

                nijtje2.RenderTransform = new RotateTransform(0, nijtje2.Width / 2, nijtje2.Height / 2);
            }

            // end of left key selection

            // if the right key is pressed then do the following
            if (e.Key == Key.D)
            {

                goright1 = true;

                nijtje2.RenderTransform = new RotateTransform(0, nijtje2.Width / 2, nijtje2.Height / 2);
            }
            // end of right key selection

            // if the up key is pressed then do the following
            if (e.Key == Key.W)
            {

                goup1 = true; // change go up to true

                nijtje2.RenderTransform = new RotateTransform(-90, nijtje2.Width / 2, nijtje2.Height / 2);
            }

            // end of up key selection

            // if the down key is pressed then do the following
            if (e.Key == Key.S)
            {

                godown1 = true; // change go down to true

                nijtje2.RenderTransform = new RotateTransform(90, nijtje2.Width / 2, nijtje2.Height / 2);
            }
            // end of the down key selection
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
            if (e.Key == Key.Down)
            {
                godown = false; // down is released go down will be false
            }
            if (e.Key == Key.Up)
            {
                goup = false; // up key is released go up will be false
            }
            if (e.Key == Key.Left)
            {
                goleft = false; // left key is released go left will be false
            }
            if (e.Key == Key.Right)
            {
                goright = false; // right key is released go right will be false
            }

            if (e.Key == Key.S)
            {
                godown1 = false; // down is released go down will be false
            }
            if (e.Key == Key.W)
            {
                goup1 = false; // up key is released go up will be false
            }
            if (e.Key == Key.A)
            {
                goleft1 = false; // left key is released go left will be false
            }
            if (e.Key == Key.D)
            {
                goright1 = false; // right key is released go right will be false
            }
            if (e.Key == Key.Space)
            {
                placingBombpl2 = false;
            }
            if (e.Key == Key.Enter)
            {
                placingBombPl1 = false;
            }
            if (e.Key == Key.P)
            {
                gameTimer.Stop();
                
            }

            else
            {
                if (e.Key == Key.Enter)

                    gameTimer.Start();
                
            }
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
                    canvas = MyCanvas
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
                    canvas = MyCanvas
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
                // if go left boolean is true
                //Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) - speed);
            }
            if (goup)
            {
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2), ((Canvas.GetTop(nijntje)) + nijntje.Height / 2) - colliderMargin - speed))
                {
                    Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) - speed);
                }
                // if go up boolean is true 
            }
            if (godown)
            {
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2), ((Canvas.GetTop(nijntje)) + nijntje.Height / 2) + colliderMargin + speed))
                {
                    Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) + speed);
                }
                // if go down boolean is true
                // end pijltjes cijfers 

                // begin w toets
            }

            if (goright1)
            {
                // if go right boolean is true 
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


                pacmanHitBox = new Rect(Canvas.GetLeft(nijntje), Canvas.GetTop(nijntje), nijntje.Width, nijntje.Height);
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
                    if (pacmanHitBox.IntersectsWith(platforms) | hitBoxNijntje2.IntersectsWith(platforms))
                    {
                        if (goleft == true && pacmanHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) + 10);

                            goleft = false;
                        }
                        // check if we are colliding with the wall while moving right if true then stop the pac man movement
                        if (goright == true && pacmanHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) - 10);

                            goright = false;
                        }
                        // check if we are colliding with the wall while moving down if true then stop the pac man movement
                        if (godown == true && pacmanHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) - 10);

                            godown = false;
                        }
                        // check if we are colliding with the wall while moving up if true then stop the pac man movement
                        if (goup == true && pacmanHitBox.IntersectsWith(platforms))
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
                    if (pacmanHitBox.IntersectsWith(tileHitbox) && tile.Exploding)
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













