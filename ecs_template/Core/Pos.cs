using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alchemist_mono
{
    public struct Pos
    {
        public static int TileSize = 48;

        public int X { get; set; }
        public int Y { get; set; }
        
        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int WorldX
        {
            get { return X * TileSize; }
        }

        public int WorldY
        {
            get { return Y * TileSize; }
        }
        
        public override bool Equals(object obj)
        {
            return X == ((Pos)obj).X && Y == ((Pos)obj).Y;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + X;
            hash = (hash * 7) + Y;
            return hash;
        }

        public static bool operator ==(Pos obj1, Pos obj2)
        {
            if (obj1 == null && obj2 == null)
                return true;
            if (obj1 == null || obj2 == null)
                return false;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Pos obj1, Pos obj2)
        {
            if (obj1 == null && obj2 == null)
                return false;
            if (obj1 == null || obj2 == null)
                return true;

            return !obj1.Equals(obj2);
        }

        public static Pos operator +(Pos obj1, Pos obj2)
        {
            return new Pos(obj1.X + obj2.X, obj1.Y + obj2.Y);
        }

        public static Pos operator -(Pos obj1, Pos obj2)
        {
            return new Pos(obj1.X - obj2.X, obj1.Y - obj2.Y);
        }

        public static implicit operator Vector2(Pos obj)
        {
            return new Vector2(obj.WorldX, obj.WorldY);
        }

        public static implicit operator Point(Pos obj)
        {
            return new Point(obj.WorldX, obj.WorldY);
        }
    }
}
