using System.Collections.Generic;
using System.Drawing;
using System;

namespace Game.Model
{
    public class Player : Person
    {
        public Player(double x, double y, Direction direction): base(x, y, direction)
        {
            X = x;
            Y = y;
            Dir = direction;
            CurrentRoad = 1;
            Step = 0;
        }
        
        public void SwapPath(int x, int y, Direction dir, GameField gameField)
        {
            MoveOvertaking(dir);
            CurrentRoad = gameField.Field[y, x].NumberRoad;
            Path = gameField.BuildPath(new Vector(x, y), 24 + 1 - CurrentRoad, 0, CurrentRoad);
            Step = 0;
        }

        private void MoveOvertaking(Direction dir)
        {
            Move(dir, false, 1);
        }

    }
}