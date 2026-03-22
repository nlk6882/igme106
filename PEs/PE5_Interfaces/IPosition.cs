using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE5_Interfaces
{
    interface IPosition
    {
        // Properties
        double X { get; set; }
        double Y { get; set; }

        // Methods
        double DistanceTo(IPosition position);
        void MoveTo(double x, double y);  // Set the point’s X and Y
        void MoveBy(double xOffset, double yOffset); // Add to the point’s X and Y
    }

}
