using System;
using System.Collections.Generic;
using System.Drawing;

using System.IO;

namespace Game.Model
{
    public interface IPerson
    {
        double X { get; set; }
        double Y { get; set; }
        int Size { get; set; }
        Direction Dir { get; set; }
    }
    public class Person:IPerson
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Size { get; set; }
        public Direction Dir { get; set; }
        public int CurrentRoad { get; set; }
        public List<Vector> Path { get; set; }
        public int Step { get; set; }
        public double Speed { get; set; }
        public Person(double x, double y, Direction direction, int road = 1, double speed = 0.2)
        {
            X = x;
            Y = y;
            Dir = direction;
            CurrentRoad = road;
            Step = 0;
            Speed = speed;
            Size = 50;
        }
        
        public void CheckNextMove()
        {
            if (Step == Path.Count) return;
            if (Y - Path[Step].Y > 0.05)
            {
                MoveOnWay(Direction.Up);
                if (Math.Abs(Path[Step].Y - Y) < 0.0005) Step++;
            }
            else if (Path[Step].Y - Y > 0.05)
            {
                MoveOnWay(Direction.Down);
                if (Math.Abs(Path[Step].Y - Y) < 0.0005) Step++;
            }
            else if (Path[Step].X - X > 0.05)
            {
                MoveOnWay(Direction.Right);
                if (Math.Abs(Path[Step].X - X) < 0.0005) Step++;
            }
            else if (X - Path[Step].X > 0.05)
            {
                MoveOnWay(Direction.Left);
                if (Math.Abs(Path[Step].X - X) < 0.0005) Step++;
            }
        }

        public void Move(Direction dir, bool isSwapDir, double step)
        {
            if (isSwapDir) Dir = dir;
            switch (dir)
            {
                case Direction.Up:
                {
                    Y -= step;
                    break;
                }
                case Direction.Down:
                {
                    Y += step;
                    break;
                }
                case Direction.Right:
                {
                    X += step;
                    break;
                }
                case Direction.Left:
                {
                    X -= step;
                    break;
                }
                    
            }
        }
        
        private void MoveOnWay(Direction dir)
        {
            Move(dir, true, Speed);
        }
    }
    
}