using System;

namespace SceneEditor.Core.General
{
    public struct Vector
    {
        public readonly float X;
        public readonly float Y;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Concat("(", X, ", ", Y, ")");
        }

        public static Vector operator +(Vector left, Vector right)
        {
            var x = left.X + right.X;
            var y = left.Y + right.Y;
            return new Vector(x, y);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            var x = left.X - right.X;
            var y = left.Y - right.Y;
            return new Vector(x, y);
        }

        public static Vector operator *(Vector left, Vector right)
        {
            var x = left.X * right.X;
            var y = left.Y * right.Y;
            return new Vector(x, y);
        }

        public static Vector operator /(Vector left, Vector right)
        {
            var x = left.X / right.X;
            var y = left.Y / right.Y;
            return new Vector(x, y);
        }

        // Equality overloads
        public override bool Equals(object obj)
        {
            return obj is Vector && this == (Vector)obj;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public static bool operator ==(Vector x, Vector y)
        {
            return Math.Abs(x.X - y.X) < float.Epsilon &&  
                   Math.Abs(x.Y - y.Y) < float.Epsilon;
        }

        public static bool operator !=(Vector x, Vector y)
        {
            return !(x == y);
        }
    }
}
