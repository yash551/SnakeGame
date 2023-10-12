

using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SnakeGame
{
    //internal static class Program
    //{
    //    /// <summary>
    //    ///  The main entry point for the application.
    //    /// </summary>
    //    [STAThread]
    //    static void Main()
    //    {
    //        // To customize application configuration such as set high DPI settings or default font,
    //        // see https://aka.ms/applicationconfiguration.
    //        ApplicationConfiguration.Initialize();
    //        Application.Run(new BoardForm());
    //    }
    //}


    public class SnakeGame : Form
    {
        private List<Point> snake;
        private Point target;
        private Directions direction = Directions.Right;

        private int cellSize = 20;
        private int gridSize = 20;

        private int formXCoordinate = 50;
        private int formYCoordinate = 50;

        private Timer timer;
        
        
        public SnakeGame()
        {
            snake = new List<Point>();
            snake.Add(new Point(formYCoordinate/2, formYCoordinate/2));
            GenerateTarget();

            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += Update;//deligate function
            timer.Start();

            this.KeyDown += OnKeyDown;

            //this.MinimizeBox = true;
            //this.MaximizeBox = true;
            //this.Width = formXCoordinate;
            //this.Height = formYCoordinate;
        }

        //generate random targets
        public void GenerateTarget()
        {
            Random random = new Random();
            target = new Point(random.Next(gridSize), random.Next(gridSize));
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    direction = Directions.Up;
                    break;
                case Keys.Down:
                    direction = Directions.Down;
                    break;
                case Keys.Left:
                    direction = Directions.Left;
                    break;
                case Keys.Right:
                    direction = Directions.Right;
                    break;
            }
        }
        
        private void Update(object sender, EventArgs e)
        {
            Point newHead = snake.First();
            switch(direction) 
            { 
                case Directions.Up:
                    newHead = new Point(newHead.X, newHead.Y-1); 
                    break;
                case Directions.Down:
                    newHead = new Point(newHead.X, newHead.Y+1);
                    break;
                case Directions.Left:
                    newHead = new Point(newHead.X-1, newHead.Y);
                    break;
                case Directions.Right:
                    newHead = new Point(newHead.X+1, newHead.Y);
                    break;
            }

            if (newHead.Equals(target)) 
            {
                snake.Insert(0, target);
                GenerateTarget();
            }
            else
            {
                snake.Insert(0, newHead);
                snake.RemoveAt(snake.Count-1);
            }

            //make the control invalid, so the updated form can be redrawn
            this.Invalidate();
        }



        //method to paint the target and snake blocks
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;

            foreach (Point p in snake) 
            {
                graphics.FillRectangle(Brushes.Blue, p.X * cellSize, p.Y * cellSize, cellSize, cellSize);
            }
            graphics.FillRectangle(Brushes.Red, target.X*cellSize, target.Y*cellSize, cellSize, cellSize);
        }

        enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }


        public static void Main()
        {
            Application.Run(new SnakeGame());
        }
    }

}
