using System.Collections.Generic;
using System.Drawing;
using System;

namespace Game.Model
{
    public class Bot: Person
    {
        public Bot(int x, int y, Direction direction, int road, double speed): base(x, y, direction, road, speed)
        {
            X = x;
            Y = y;
            Dir = direction;
            CurrentRoad = road;
            Step = 0;
            Speed = speed;
        }

        public bool CheckCollision(Player player)
        {
            return Math.Abs(player.X - X) < 0.01 && player.Y > Y && player.Y < Y + 1 && (Dir == Direction.Up || Dir == Direction.Down)||
                   Math.Abs(player.X - X) < 0.01 && player.Y < Y && player.Y + 1 > Y && (Dir == Direction.Up || Dir == Direction.Down)||
                   Math.Abs(player.Y - Y) < 0.01 && player.X > X && player.X < X + 1 && (Dir == Direction.Right || Dir == Direction.Left)||
                   Math.Abs(player.Y - Y) < 0.01 && player.X < X && player.X + 1 > X && (Dir == Direction.Right || Dir == Direction.Left);
        }
        
    }
    
    
}