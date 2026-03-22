using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE5_Interfaces
{
    internal class Circle : IPosition, IArea
    {
        //feilds
        double xPos;
        double yPos;
        double radius;

        //properties
        public double X { get { return xPos; } set { xPos = value; } }
        public double Y { get { return yPos; } set { yPos = value; } }
        public double Area { get { return radius * radius * double.Pi; } }    // PI * radius * radius
        public double Perimeter { get { return radius * 2 * double.Pi; } }   // PI * 2 * radius

        //constructor
        public Circle(double xPos, double yPos, double radius)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.radius = radius;
        }

        //methods
        public double DistanceTo(IPosition position)
        {
            return Math.Sqrt(Math.Pow(position.X - this.X, 2) + Math.Pow(position.Y - this.Y, 2));
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

        public bool ContainsPosition(IPosition position)
        {
            //check if the point input is less then the curent point + or - the radius
            if (position.X <= this.X + this.radius &&  position.Y <= this.Y + this.radius)
            {
                return true; 
            }
            else
            {
                return false;
            }

        }

        public bool IsLargerThan(IArea areaToCheck)
        {

            if(this.Area > areaToCheck.Area)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    

    }
}
