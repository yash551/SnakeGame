//namespace WinFormsApp1
//{
//    internal static class Program
//    {
//        /// <summary>
//        ///  The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            // To customize application configuration such as set high DPI settings or default font,
//            // see https://aka.ms/applicationconfiguration.
//            ApplicationConfiguration.Initialize();
//            Application.Run(new Form1());
//        }
//    }
//}

using System;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Windows.Forms.Timer;
using Application = System.Windows.Forms.Application;

public class SnakeGame : Form
{
    private List<Point> snake;
    private Point food;
    private int cellSize = 20;
    private int gridSize = 20;
    private Direction direction = Direction.Right;
    private Timer gameTimer;

    public SnakeGame()
    {
        snake = new List<Point>();
        snake.Add(new Point(5, 5)); // Initial position of the snake

        food = GenerateFoodLocation();

        gameTimer = new Timer();
        gameTimer.Interval = 200;
        gameTimer.Tick += Update;
        gameTimer.Start();

        this.Size = new Size(gridSize * cellSize, gridSize * cellSize);
        this.Text = "Snake Game";
        this.DoubleBuffered = true;
        this.KeyDown += OnKeyDown;
    }

    private Point GenerateFoodLocation()
    {
        Random rand = new Random();
        return new Point(rand.Next(gridSize), rand.Next(gridSize));
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                direction = Direction.Left;
                break;
            case Keys.Right:
                direction = Direction.Right;
                break;
            case Keys.Up:
                direction = Direction.Up;
                break;
            case Keys.Down:
                direction = Direction.Down;
                break;
        }
    }

    private void Update(object sender, EventArgs e)
    {
        Point newHead = snake.First();
        switch (direction)
        {
            case Direction.Left:
                newHead = new Point(newHead.X - 1, newHead.Y);
                break;
            case Direction.Right:
                newHead = new Point(newHead.X + 1, newHead.Y);
                break;
            case Direction.Up:
                newHead = new Point(newHead.X, newHead.Y - 1);
                break;
            case Direction.Down:
                newHead = new Point(newHead.X, newHead.Y + 1);
                break;
        }

        if (newHead.Equals(food))
        {
            snake.Insert(0, food);
            food = GenerateFoodLocation();
        }
        else
        {
            snake.Insert(0, newHead);
            snake.RemoveAt(snake.Count - 1);
        }

        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;

        foreach (Point segment in snake)
        {
            g.FillRectangle(Brushes.Green, segment.X * cellSize, segment.Y * cellSize, cellSize, cellSize);
        }

        g.FillRectangle(Brushes.Red, food.X * cellSize, food.Y * cellSize, cellSize, cellSize);
    }

    public static void Main()
    {
        Application.Run(new SnakeGame());
    }
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}