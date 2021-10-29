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

        DispatcherTimer gameTimer = new DispatcherTimer();          //maak een timer class aan 

        List<Rectangle> Moves = new List<Rectangle>();              //een lijst waar alle rectangles in komen nodig voor beweging

        int colliderMargin = 7;                                     //een kleine offset die collisions wat vloeiender maken
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

        bool placedBombPl1 = false;                             //Speler 1 heeft een bom geplaatst
        bool placedBombPl2 = false;                             //Speler 2 heeft een bom geplaatst

        bool placingBombPl1 = false;                            //Speler 1 is een bom aan het plaatsen 
        bool placingBombpl2 = false;                            //Speler 2 is een bom aan het plaatsen


        Rectangle nijntje; // speler rectangle
        Rectangle nijtje2; // tegenstander rectangle


        int speed = 4; // Speed nijntje1
        int speed1 = 4;// speed Nijtnje 2


        Rect nijntjeHitBox;                     //hibox voor speler 1
        Rect hitBoxNijntje2;                    //hitbox voor speler 2

        Rectangle landingRec;                   //collider voor positie speler 1
        Rectangle landingRec1;                  //collider voor positie speler 2
        int images = -1;                        //aantal plaatjes


        Collisions collider;                        //een class voor de collider functies
        List<Tile> tiles = new List<Tile>();        //een lijst met alle tiles op het speelveld
        List<Bomb> bombs = new List<Bomb>();        //een lijst met geplaatste bommen

        Tile OnTilePl1;                             //tile waar speler 1 op staat
        Tile OntilePl2;                             //tile waar speler 2 op staat

        Timer cooldownP1 = new Timer(1000);         //aantal milliseconden voordat de speler opnieuw een bom kan plaatsen
        Timer cooldownP2 = new Timer(1000);         //aantal milliseconden voordat de speler 2 opnieuw een bom kan plaatsen

        
        //Maak alles klaar
        public MainWindow()
        {
            //maak class aan
            InitializeComponent();
            //laad de game in
            GameSetUp();
        }
        //zet de game klaar zodat het gespeeld kan worden
        private void GameSetUp()
        {
            
            cooldownP1.AutoReset = false;           //zet de autoreset van de timer uit zodat de cooldown functie niet nonstop wordt aangeroepen 
            cooldownP2.AutoReset = false;           //zet de autoreset van de timer uit zodat de cooldown functie niet nonstop wordt aangeroepen 



            MyCanvas.Focus(); // Set een Canvas als belangrijkste in hetProject.
            gameTimer.Tick += GameLoop;     //koppel de timer aan de gameloop elke interval wordt de loop aangeroepen
            gameTimer.Interval = TimeSpan.FromMilliseconds(10); // Hier staat de Gametick op 10 Milliseconds.
            gameTimer.Start(); // start Game

            // hier worden de speler image toegevoegd
            ImageBrush nijntjeImage = new ImageBrush();
            //image nijntje 1
            nijntjeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));

            //nijntje image 2
            ImageBrush nijntjeImage2 = new ImageBrush();
            nijntjeImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));


            //een lijst waar tiles in komen die verwijdert moeten worden op het einde van de setup
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
                //maak een nieuwe tile class aan
                Tile tile = new Tile
                {
                    myRec = box         //zet de info van box over naar myrec
                };
                //voeg de tile aan de tile lijst toe
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
                    Canvas.SetLeft(tile.myRec, leftPos);        //zet de locatie van de tile naar de gegeven locatie
                    tile.posX = leftPos;                        //zet de xpos van de tile naar doe van de locatie
                    leftPos -= 80; 

                }

                if (a < 17)
                {
                    // als de value a is onder de 17 wort er boxen aangemaakt van links naar rechts
                    a++; // als deze code wordt uitgevoerd moet het spelbord wel 1 opschuiven

                    leftPos += 80;
                    Canvas.SetLeft(tile.myRec, leftPos);        //zet de locatie van de tile naar de gegeven locatie
                    tile.posX = leftPos;                        //zet de xpos van de tile naar doe van de locatie
                }
                tile.posY = topPos;                             //zet de xpos van de tile naar de y locatie op het canvas
                tile.setVars();                                 //Bereken de middenpunten van de tile
                Canvas.SetTop(tile.myRec, topPos);              // hier wordt de box geplaatst op de locatie die wordt toegekent 
                MyCanvas.Children.Add(tile.myRec);              //voeg de tile toe aan mycanvas zodat hij zichtbaar wordt
            }

            //maak een niew collisons class aan
            collider = new Collisions
            {
                Alltiles = tiles                                //voeg de lijst met alle tiles toe aan de collisions class
            };
            // einde loop

            // hier worden de spellers aangemaakt

            // hier word speler 1 aamgemaakt
            nijntje = new Rectangle
            {
                Height = 40,
                Width = 40,
                Fill = nijntjeImage,                        // hier word er een image toegevoegd 
                StrokeThickness = 2
            };

            // hier word speler 2 aangemaakt
            nijtje2 = new Rectangle
            {
                Height = 40,
                Width = 40,
                Fill = nijntjeImage2,                       // hier word er een image toegevoegd 

            };
            MyCanvas.Children.Add(nijntje);                 // toegevoegd aan het spel
            MyCanvas.Children.Add(nijtje2);                 // toegevoegd aan het spel

            MovePiece(nijntje, "box" + 154);                // hier word de locatie gezet van player 1
            MovePiece1(nijtje2, "box" + 32);                // hier word de locatie gezet van player 2
 

            // hier worden de houten blokken toegevoegd aan het speelveld
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
                int[] possiblenumbers = {
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

                List<int> numbers = new List<int>();                //nieuwe lijst met nummers van breekbare tiles
                
                foreach (var number in possiblenumbers)             //voor elk nummer in de possiblenumbers array
                {
                    Random r = new Random();                        //randomizer class zodat het veld elke keer anders is
                    if (r.Next(100) < 80)
                    {
                        numbers.Add(number);                        //voeg nummer toe aan de numbers lijst
                        
                    }

                }
                
                
                if (i > numbers.Count - 1)                          //voorkom een overflow
                {
                    
                    break;
                }

                else
                {
                    Moveblock(block, "box" + numbers[i]);           //plaats de tile op de gennummerde positie
                }
                
                
                blocktoRemove.Add(block);                           //voeg het block toe aan de blocktoremove lijst zodat het later correct verwijdert wordt
                MyCanvas.Children.Add(block);                       //voeg het block toe aan de canvas
                Rect check = new Rect(Canvas.GetLeft(block), Canvas.GetTop(block), block.Width / 2, block.Height / 2);      //maak een rect aan die als collider gebruikt wordt 
                //voor alle tiles in de tiles lijst
                foreach (Tile tile in tiles)
                {
                    Rect checkTile = new Rect(tile.DebugPosX, tile.DebugPosY, tile.myRec.Width / 2, tile.myRec.Height / 2); //maak een rect aan die als collider gebruikt wordt 
                    if (checkTile.IntersectsWith(check))        //raken de tiles elkaar aan
                    {
                        tile.BreakablePic = blockHout;          //voeg een breakable plaatje toe aan de tile
                        tile.Type = "Breakable";                //zet de tile type naar breakable
                        tile.myRec.Fill = tile.BreakablePic;    //zet het correcte plaatje in de tile
                        tile.Passable = true;                   //de speler kan niet langs deze tile
                    }
                }
                MyCanvas.Children.Remove(block);                //haal het block van het veld af
            }
 
            

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())    //voor alle rectangles in mycanvas
            {

                if (x is Rectangle && (string)x.Tag == "wall")  //als x een rectangle is en de muur tag heeft
                {
                    //maak een rect aan voor de info van x en noem hem static wall
                    Rect staticWall = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    //voor elke tile in de tiles lijst
                    foreach (Tile tile in tiles)
                    {
                        //maak een rect aan voor de middenpunten van de tile
                        Rect checkTile = new Rect(tile.DebugPosX, tile.DebugPosY, tile.myRec.Width / 3, tile.myRec.Height / 3);
                        if (staticWall.IntersectsWith(checkTile))       //komt de tile in aanraking met een muur
                        {
                            tile.Type = "wall";                         //zet de tile type naar dat van een muur
                        }
                    }
                }
            }
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)       //er is een toets ingedrukt
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
        //wordt aangeroepen als  timer1 een interval heeft gemaakt reset de placedbomb variabel
        private void Timer_ElapsedP1(object sender, ElapsedEventArgs e)
        {
            placedBombPl1 = false;
        }
        //wordt aangeroepen als timer2 een interval heeft gemaakt reset de placedbomb variabel
        private void Timer_ElapsedP2(object sender, ElapsedEventArgs e)
        {
            placedBombPl2 = false;
        }
        //wordt aangeroepen als een speler zijn toets loslaat
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
            //if (pauze1 == true)
            //{
            //    //int top = 450;
            //    //int left = 20;

            //    //ImageBrush pauze = new ImageBrush();
            //    //pauze.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pause-button.png"));

            //    //Rectangle pauzeimage = new Rectangle
            //    //{
            //    //    Height = 1000,
            //    //    Width = 1000,
            //    //    Fill = pauze,

            //    //};
            //    //Canvas.SetLeft(pauzeimage, top);
            //    //Canvas.SetTop(pauzeimage, left);
            //    //MyCanvas.Children.Add(pauzeimage);
            //}
            if (e.Key == Key.P)
            {
                gameTimer.Stop();           //stopt de engine en zet alles dus stil
                //pauze1 = true;
                //int top = 450;
                //int left = 20;
                //Canvas.SetLeft(pauzeimage, top);
                //Canvas.SetTop(pauzeimage, left);
                //MyCanvas.Children.Add(pauzeimage);



            }
            // Wanneer een gebruiker op P heeft gedrukt kan de gebruikker het spel verder spellen als die op enter drukt
            else
            {
                if (e.Key == Key.Enter)
                { 
                    gameTimer.Start();      //start de game weer

                //MyCanvas.Children.Remove(pauzeimage);
                }
                
                    

            }
            // met de key escape word het menu geopent in het spel
            if (e.Key == Key.Escape)
            {
                Hoofdmenu g = new Hoofdmenu();
                g.Visibility = Visibility.Visible;
                this.Close();

            }
        }
        //dit is de loop van de game
        private void GameLoop(object sender, EventArgs e)
        {
            //als speler 1 een bom plaatst mar nog niet heeft geplaatst. "voorkomt dubbelle bommen"
            if (placingBombPl1 && !placedBombPl1)
            {
                placedBombPl1 = true;                       //heeft bom geplaatst
                placingBombPl1 = false;                     //is niet meer bom aan het plaatsen
                cooldownP1.Elapsed += Timer_ElapsedP1;      //voeg de elepsed functie toe 

                Bomb bom = new Bomb                         //maak een nieuwe bom aan 
                {
                };
                //check of de closest tile beschikbaar is
                if (bom.GetClosestTile(tiles, (Canvas.GetLeft(nijntje) + nijntje.Width / 2), (Canvas.GetTop(nijntje) + nijntje.Height / 2), OnTilePl1, Score.NamePlayer1))
                {
                    MyCanvas.Children.Add(bom.myRec);       //voeg bom toe aan mycanvas
                }
                bombs.Add(bom);                             //voeg bom toe aan lijst
                OnTilePl1 = bom.placedOn;                   //Bom staat op ontilep1
                OnTilePl1.Hasplayer = true;                 //ontile1 heeft speler
                cooldownP1.Start();                         //start cooldwown
            }
            //als speler 2 een bom plaatst mar nog niet heeft geplaatst. "voorkomt dubbelle bommen"
            if (placingBombpl2 && !placedBombPl2)
            {
                placedBombPl2 = true;                       //heeft bom geplaatst
                cooldownP2.Elapsed += Timer_ElapsedP2;      //voeg de elepsed functie toe 
                Bomb bom = new Bomb                         //maak een nieuwe bom aan 
                {
                };
                //check of de closest tile beschikbaar is
                if (bom.GetClosestTile(tiles, (Canvas.GetLeft(nijtje2) + nijtje2.Width / 2), (Canvas.GetTop(nijtje2) + nijtje2.Height / 2), OntilePl2, Score.NamePlayer2))
                {
                    MyCanvas.Children.Add(bom.myRec);       //voeg bom toe aan mycanvas
                }
                bombs.Add(bom);                             //voeg bom toe aan lijst
                OntilePl2 = bom.placedOn;                   //Bom staat op ontilep1
                OntilePl2.Hasplayer = true;                 //ontile1 heeft speler
                cooldownP2.Start();                         //start cooldwown
            }

            // Wat hier gebeurd is wanneer nijntje een wall raakt de snellheid naar 0 gaat.
            // nijntje 1
            if (goright)
            {
                //check of de speler niet tegen een collider aanloopt 
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2) + speed + colliderMargin, (Canvas.GetTop(nijntje)) + nijntje.Height / 2))
                {
                    Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) + speed);
                }
            }
            if (goleft)
            {
                //check of de speler niet tegen een collider aanloopt 
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2) - speed - colliderMargin, (Canvas.GetTop(nijntje)) + nijntje.Height / 2))
                {
                    Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) - speed);
                }
            }
            if (goup)
            {
                //check of de speler niet tegen een collider aanloopt 
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2), ((Canvas.GetTop(nijntje)) + nijntje.Height / 2) - colliderMargin - speed))
                {
                    Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) - speed);
                }
            }
            if (godown)
            {
                //check of de speler niet tegen een collider aanloopt 
                if (collider.CollisionCheck((Canvas.GetLeft(nijntje) + nijntje.Width / 2), ((Canvas.GetTop(nijntje)) + nijntje.Height / 2) + colliderMargin + speed))
                {
                    Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) + speed);
                }
            }

            if (goright1)
            {
                //check of de speler niet tegen een collider aanloopt 
                if (collider.CollisionCheck((Canvas.GetLeft(nijtje2) + nijtje2.Width / 2) + speed1 + colliderMargin, (Canvas.GetTop(nijtje2)) + nijtje2.Height / 2))
                {
                    Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) + speed1);
                }
            }
            if (goleft1)
            {
                //check of de speler niet tegen een collider aanloopt 
                if (collider.CollisionCheck((Canvas.GetLeft(nijtje2) + nijtje2.Width / 2) - speed1 - colliderMargin, (Canvas.GetTop(nijtje2)) + nijtje2.Height / 2))
                {
                    Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) - speed1);
                }
            }
            if (goup1)
            {
                //check of de speler niet tegen een collider aanloopt 
                if (collider.CollisionCheck((Canvas.GetLeft(nijtje2) + nijtje2.Width / 2), ((Canvas.GetTop(nijtje2)) + nijtje2.Height / 2) - colliderMargin - speed1))
                {
                    Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) - speed1);
                }
            }
            if (godown1)
            {
                //check of de speler niet tegen een collider aanloopt 
                if (collider.CollisionCheck((Canvas.GetLeft(nijtje2) + nijtje2.Width / 2), ((Canvas.GetTop(nijtje2)) + nijtje2.Height / 2) + colliderMargin + speed1))
                {
                    Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) + speed1);
                }
            }
            //einde movement

            //voor elke bom in de bombs lijst
            foreach (Bomb bomb in bombs)
            {
                if (bomb.destroyed)                         //als de bom kapot is
                {
                    MyCanvas.Children.Remove(bomb.myRec);   //verwijder de bom van het veld
                    bombs.Remove(bomb);                     //verwijder de bom van de bombs lijst
                    break;                                  //breek de loop
                }
            }
            //voor elke tile in tiles
            foreach (Tile tile in tiles)
            {
                //als een tile ontploft en als de reset niet klaar is en de  niet is gestart
                if (tile.Exploding && tile.ResetDone == false && tile.ResetStarted == false)
                {
                    ImageBrush Explodingpic = new ImageBrush();
                    Explodingpic.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bieb.png"));
                    tile.myRec.Fill = Explodingpic;         //verander het plaatje van de tile met dat van een ontploffing
                    tile.ResetStarted = true;               //start de reset
                }
                //als de reset wel klaar is
                else if (tile.ResetDone)
                {
                    tile.myRec.Fill = tile.OgPic;           //pak orginele plaatje tile en set dat als het huidige plaatje
                    tile.ResetDone = false;                 //reset is niet klaar
                    tile.Type = "normal";                   //zet type naar normaal
                    tile.Passable = false;                  //speler kan nu over de tile heenlopen
                }
                //als de tile breekbaar is en niet een setup heeft gedaan
                if (tile.Type == "Breakable" && !tile.TileSetup)
                {
                    tile.TileSetup = true;                  //zet de setup als klaar
                    tile.myRec.Fill = tile.BreakablePic;    //zer het plaatje van de tile als breekbraar
                }
            }





            //voor elke rectangle in mycanvas
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {            
                // Hier wordt een rect aangemaakt voor de hitbox van Nijntje.


                nijntjeHitBox = new Rect(Canvas.GetLeft(nijntje), Canvas.GetTop(nijntje), nijntje.Width, nijntje.Height); // Hier wordt de hitbox van nijntje 1 aangemaakt
                hitBoxNijntje2 = new Rect(Canvas.GetLeft(nijtje2), Canvas.GetTop(nijtje2), nijtje2.Width, nijtje2.Height);// Hier wordt de hitbox van nijntje 2 aangemaakt

                if (x is Rectangle && (string)x.Tag == "wall")
                {

                    Rect platforms = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    // Wat hier gebeurd is dat die elke movement controleerd wat die geen wall raakt.
                    if (nijntjeHitBox.IntersectsWith(platforms) | hitBoxNijntje2.IntersectsWith(platforms))
                    {
                        // nijntje 1 hitbox
                        if (goleft == true && nijntjeHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) + 10);
                            goleft = false;
                        }
                        if (goright == true && nijntjeHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijntje, Canvas.GetLeft(nijntje) - 10);
                            goright = false;
                        }
                        if (godown == true && nijntjeHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) - 10);
                            godown = false;
                        }
                        if (goup == true && nijntjeHitBox.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) + 10);
                            goup = false;
                        }
                        // einde hitbox 
                        // begin hitbox nijntje 2
                        if (goleft1 == true && hitBoxNijntje2.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) + 10);

                            goleft1 = false;
                        }
                        if (goright1 == true && hitBoxNijntje2.IntersectsWith(platforms))
                        {
                            Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) - 10);

                            goright1 = false;
                        }
                        if (godown1 == true && hitBoxNijntje2.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) - 10);

                            godown1 = false;
                        }

                        if (goup1 == true && hitBoxNijntje2.IntersectsWith(platforms))
                        {
                            Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) + 10);

                            goup1 = false;
                        }

                    }



                }
                //voor elke tile in tiles
                foreach (Tile tile in tiles)
                {
                    //maak een niew hitbox aan
                    Rect tileHitbox = new Rect(tile.posX, tile.posY, tile.myRec.Width, tile.myRec.Height);
                    //check of de hotbox tile nijntje aanraakt en ontploft
                    if (nijntjeHitBox.IntersectsWith(tileHitbox) && tile.Exploding)
                    {
                        nijntje.Fill = null;                        //zet het nijntje plaatje uit
                        Score.SetScore(Score.NamePlayer2, 1000);    //geef de andere speler 1000 punten
                        //zet de highscores in de database 
                        Score.SetHighScores(Score.NamePlayer1, Score.ScorePlayer1, Score.NamePlayer2, Score.ScorePlayer2);
                        //maak een niew highscore window aan
                        HighScoreWindow h = new HighScoreWindow();
                        //laat de nieuew window zien
                        h.Show();
                        //sluit dit
                        this.Close();
                    }
                    //check of de hotbox tile nijntje aanraakt en ontploft
                    if (hitBoxNijntje2.IntersectsWith(tileHitbox) && tile.Exploding)
                    {
                        nijtje2.Fill = null;                        //zet het nijntje plaatje uit
                        Score.SetScore(Score.NamePlayer1, 1000);    //geef de andere speler 1000 punten
                        //zet de highscores in de database 
                        Score.SetHighScores(Score.NamePlayer1, Score.ScorePlayer1, Score.NamePlayer2, Score.ScorePlayer2);
                        //maak een niew highscore window aan
                        HighScoreWindow h = new HighScoreWindow();
                        //laat de nieuew window zien
                        h.Show();
                        //sluit dit
                        this.Close();
                    }
                }
            }
        }
        //zet de postietie van het gegeven plaatje
        private void MovePiece(Rectangle nijntje, string posName)
        {
            foreach (Rectangle rectangle in Moves)
            {
                if (rectangle.Name == posName)
                {
                    landingRec = rectangle;
                }
            }
            Canvas.SetLeft(nijntje, Canvas.GetLeft(landingRec) + nijntje.Width / 2);
            Canvas.SetTop(nijntje, Canvas.GetTop(landingRec) + nijntje.Height / 2);
        }
        //zet de start positie van speler 2
        private void MovePiece1(Rectangle nijtje2, string posName)
        {
            foreach (Rectangle rectangle in Moves)
            {
                if (rectangle.Name == posName)
                {
                    landingRec1 = rectangle;
                }
            }
            Canvas.SetLeft(nijtje2, Canvas.GetLeft(landingRec1) + nijtje2.Width / 2);
            Canvas.SetTop(nijtje2, Canvas.GetTop(landingRec1) + nijtje2.Height / 2);

        }
        //zet de blokken goed 
        private void Moveblock(Rectangle block, string posName)
        {
            foreach (Rectangle rectangle in Moves)
            {
                if (rectangle.Name == posName)
                {
                    landingRec1 = rectangle;
                }
            }

            Canvas.SetLeft(block, Canvas.GetLeft(landingRec1) + 5);
            Canvas.SetTop(block, Canvas.GetTop(landingRec1) + 5);

        }
    }
}













