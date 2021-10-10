using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;


namespace project_Bomberman
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();


        bool goup; // this boolean will be used for the player to go up the screen
        bool godown; // this boolean will be used for the player to go down the screen
        bool goleft; // this boolean will be used for the player to go left to the screen
        bool goright; // this boolean will be used for the player to right to the screen

        bool goup1; // this boolean will be used for the player to go up the screen
        bool godown1; // this boolean will be used for the player to go down the screen
        bool goleft1; // this boolean will be used for the player to go left to the screen
        bool goright1; // this boolean will be used for the player to right to the screen

        int speed = 10;
        int speed1 = 10;// this integer is for the speed of the player
        int steps = 10; // how many steps animation/timer will have
        

        Random rnd = new Random();


        public MainWindow()
        {
            InitializeComponent();
            GameSetUp();

        }


        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            

            // if the left key is pressed then do the following
            if (e.Key == Key.Left)
            {
                goleft = true; // change go left to true
                goright = goup = godown = false;
                nijntje.RenderTransform = new RotateTransform(0, nijntje.Width / 2, nijntje.Height / 2);
            }

            // end of left key selection

            // if the right key is pressed then do the following
            if (e.Key == Key.Right)
            {

                goright = true;
                goleft = goup = godown = false;
                nijntje.RenderTransform = new RotateTransform(0, nijntje.Width / 2, nijntje.Height / 2);
            }
            // end of right key selection

            // if the up key is pressed then do the following
            if (e.Key == Key.Up)
            {
                
                goup = true; // change go up to true
                goright = goleft = godown = false;
                nijntje.RenderTransform = new RotateTransform(-90, nijntje.Width / 2, nijntje.Height / 2);
            }

            // end of up key selection

            // if the down key is pressed then do the following
            if (e.Key == Key.Down)
            {
                
                godown = true; // change go down to true
                goright = goup = goleft = false;
                nijntje.RenderTransform = new RotateTransform(90, nijntje.Width / 2, nijntje.Height / 2);
            }
            // end of the down key selection
        


            // if the left key is pressed then do the following
            if (e.Key == Key.A)
            {
                goleft1 = true; // change go left to true
                goright1 = goup1 = godown1 = false;
                nijtje2.RenderTransform = new RotateTransform(0, nijtje2.Width / 2, nijtje2.Height / 2);
            }

            // end of left key selection

            // if the right key is pressed then do the following
            if (e.Key == Key.D)
            {

                goright1 = true;
                goleft1 = goup1 = godown1 = false;
                nijtje2.RenderTransform = new RotateTransform(0, nijtje2.Width / 2, nijtje2.Height / 2);
            }
            // end of right key selection

            // if the up key is pressed then do the following
            if (e.Key == Key.W)
            {

                goup1 = true; // change go up to true
                goright1 = goleft1 = godown1 = false;
                nijtje2.RenderTransform = new RotateTransform(-90, nijtje2.Width / 2, nijtje2.Height / 2);
            }

            // end of up key selection

            // if the down key is pressed then do the following
            if (e.Key == Key.S)
            {

                godown1 = true; // change go down to true
                goright1 = goup1 = goleft1 = false;
                nijtje2.RenderTransform = new RotateTransform(90, nijtje2.Width / 2, nijtje2.Height / 2);
            }
            // end of the down key selection
        }

        private void GameSetUp()
        {

            MyCanvas.Focus(); // set my canvas as the main focus for the program


            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); // set time to tick every 20 milliseconds
            gameTimer.Start(); // start the time

            ImageBrush nijntjeImage = new ImageBrush();

            nijntjeImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));
            nijntje.Fill = nijntjeImage;

            ImageBrush nijntjeImage2 = new ImageBrush();
            nijntjeImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/simpels.png"));
            nijtje2.Fill = nijntjeImage2;



        }

        

        private void GameLoop(object sender, EventArgs e)
        {

            if (goright)
            {
                // if go right boolean is true 
                Canvas.SetLeft(nijntje,    Canvas.GetLeft(nijntje) + speed);
            }
            if (goleft)
            {
                // if go left boolean is true
                Canvas.SetLeft(nijntje,  Canvas.GetLeft(nijntje) - speed);
            }
            if (goup)
            {
                // if go up boolean is true 
                Canvas.SetTop(nijntje,  Canvas.GetTop(nijntje) - speed);
            }
            if (godown)
            {
                // if go down boolean is true
                Canvas.SetTop(nijntje, Canvas.GetTop(nijntje) + speed);
                // end pijltjes cijfers 

                // begin w toets
            }
            
            if (goright1)
            {
                // if go right boolean is true 
                Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) + speed1);
            }
            if (goleft1)
            {
                // if go left boolean is true
                Canvas.SetLeft(nijtje2, Canvas.GetLeft(nijtje2) - speed1);
            }
            if (goup1)
            {
                // if go up boolean is true 
                Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) - speed1);
            }
            if (godown1)
            {
                // if go down boolean is true
                Canvas.SetTop(nijtje2, Canvas.GetTop(nijtje2) + speed1);
            }
        }
    }
}
         

                



     
    
            

    
    
