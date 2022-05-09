using System;

namespace Game.Model
{
    public class Cell
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Size = 50;
        public readonly bool IsRoad;
        public readonly int NumberRoad;
        public bool IsVisited { get; set; }
        public Cell(int x, int y, bool isRoad, int numberRoad = -1)
        {
            X = x;
            Y = y;
            IsRoad = isRoad;
            NumberRoad = numberRoad;
            IsVisited = false;
        }
    }
}