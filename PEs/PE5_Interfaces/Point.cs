using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE5_Interfaces
{
    internal class Point : IPosition
    {
        //feilds
        double xPos;
        double yPos;

        //properties
        public double X { get { return xPos; } set { xPos = value; } }
        public double Y { get { return yPos; } set { yPos = value; } }

        //contructor
        public Point(double xPos, double yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos; 
        }

        //methods
        public double DistanceTo(IPosition position)
        {
            return Math.Sqrt(Math.Pow(position.X - this.X,2) + Math.Pow(position.Y - this.Y, 2));
        }
        public void MoveTo(double x, double y)
        {
            // Set the point’s X and Y
            this.X = x;
            this.Y = y;

        }

        public void MoveBy(double xOffset, double yOffset)
        {
            // Add to the point’s X and Y
            this.X += xOffset;
            this.Y += yOffset;


        }


    }
}
