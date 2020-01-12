using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockyAPI.BLOCKY
{
    public static class BlockyDrawingHelpers
    {
        public static bool IsPointInsideRectangle(Point point, Rectangle rect)
        {
            return point.X >= rect.X && point.X <= rect.X + rect.Width &&
               point.Y >= rect.Y && point.Y <= rect.Y + rect.Height;
        }
        public static int DistanceBetweenTwoPoints(Point point1,Point point2)
        {
            return (int)Math.Sqrt((point1.X-point2.X)*(point1.X - point2.X)+
                   (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }
    }
}
