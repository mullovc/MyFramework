using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMath
{
    public class Vector2
    {
        public static Vector2 zero = new Vector2(0, 0);

        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void add(Vector2 v)
        {
            x += v.x;
            y += v.y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator /(Vector2 v, int s)
        {
            return new Vector2(v.x / s, v.y / s);
        }

        public static Vector2 operator *(Vector2 v, int s)
        {
            return new Vector2(v.x * s, v.y * s);
        }

        public static Vector2 add(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public override string ToString()
        {
            return (String.Format("({0}, {1})", x, y));
        }
    }
}
