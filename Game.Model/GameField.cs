using System;
using System.Collections;
using System.Collections.Generic;
namespace Game.Model
{
    public class GameField
    {
        private readonly char[,] startFieldChar = 
        new char[15, 25] {
            {'2', '2', '2', '2', '2', '2', '2', 'X', '2', '2', '2', '2', '2', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '2', '1'},
            {'2', '1', '1', '1', '1', '1', '2', 'X', '2', '1', '1', '1', '2', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', '1', '2', 'X', '2', '2', '2', '2', '2', '2', '2', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', '1', '2', 'X', '2', '1', '1', '1', '1', '1', '2', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', '1', '2', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', '1', '2', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', '1', '2', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', '1', '2', '2', '2', '1', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', '1', '2', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', '1', '1', '1', '1', '1', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', '1', '2', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', '1', '2', 'X', 'X', '2', '1'},
            {'2', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', '1', '2', '2', '2', '2', '1'},
            {'2', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', '1', '1', '1', '1', '1', '1'},
            {'2', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '1', '2', 'X', '2', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X'},
            {'2', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '1', '2', '2', '2', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X'},
            {'2', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', '1', '1', '1', '1', '1', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X'},
        };
        public Cell[,] Field = new Cell[15, 25];
        public GameField()
        {
            for (var i = 0; i < startFieldChar.GetLength(0); i++)
            {
                for (var j = 0; j < startFieldChar.GetLength(1); j++)
                {
                    if (startFieldChar[i, j] == 'X')
                        Field[i, j] = new Cell(j, i, false);
                    else
                    {
                        Field[i, j] = new Cell(j, i, true, Int32.Parse(startFieldChar[i, j].ToString()));
                    }
                }
            }
        }

        public List<Vector> BuildPath(Vector start, int endX, int endY, int currentRoad)
        {
            var defaultPoint = new Vector(-1, -1);
            var queue = new Queue<Vector>() ;
            var paths = new Dictionary<Vector, Vector>();
            var visited = new HashSet<Vector>() {start};
            queue.Enqueue(start);
            paths[start] = defaultPoint;
            var endPoint = new Vector(endX, endY);
            var activePoint = defaultPoint;
            if (start == endPoint) return new List<Vector>();
            while (queue.Count != 0)
            {
                var curPos = queue.Dequeue();
                for (var i = -1; i < 2; i++)
                {
                    for (var j = -1; j < 2; j++)
                    {
                        if (Math.Abs(i) + Math.Abs(j) != 1) continue;
                        var newPoint = new Vector(curPos.X + i, curPos.Y + j);
                        if (InField(newPoint.X, newPoint.Y) && !visited.Contains(newPoint) && Field[newPoint.Y, newPoint.X].NumberRoad ==
                            currentRoad)
                        {
                            paths[newPoint] = curPos;
                            visited.Add(newPoint);
                            queue.Enqueue(newPoint);
                            if (newPoint == endPoint)
                            {
                                activePoint = newPoint;
                                break;
                            }
                        }
                    }
                    if (activePoint != defaultPoint) break;
                }
                if (activePoint != defaultPoint) break;
            }

            var res = new List<Vector>();
            while (paths[activePoint] != defaultPoint)
            {
                res.Add(activePoint);
                activePoint = paths[activePoint];
            }
            res.Reverse();
            return res;
        }
        public bool IsCanRearrange(int x, int y, int road)
        {
            return InField(x, y) && Field[y, x].IsRoad && !IsCurrentRoad(x, y, road);
        }
        private bool InField(int x, int y)
        {
            return x >= 0 && x < Field.GetLength(1) && y >= 0 && y < Field.GetLength(0);
        }
        
        private bool IsCurrentRoad(int x, int y, int currentRoad)
        {
            return Field[y, x].NumberRoad == currentRoad;
        }
    }
}