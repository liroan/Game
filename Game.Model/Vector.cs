using System.Xml.Serialization;

using System;

namespace Game.Model
{
    public struct Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool operator ==(Vector left, Vector right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Vector left, Vector right) => !(left == right);
        public override bool Equals(object obj) => obj is Vector point && point.X == this.X && point.Y == this.Y;
        public override int GetHashCode() => X ^ Y;
    }
}