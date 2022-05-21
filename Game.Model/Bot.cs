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
    }
    
    
}