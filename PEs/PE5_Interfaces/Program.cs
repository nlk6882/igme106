using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE5_Interfaces
{
    internal class Program
    {
        enum ShapeType
        {
            Point,
            Circle
        }

        public static void Main()
        {

            // Setup a point and circle to use later for comparisons
            Point center = new Point(0, 0);
            Circle unit = new Circle(0, 0, 1);

            Console.WriteLine("Center: " + center);
            Console.WriteLine("Unit circle: " + unit);
            Console.WriteLine();

            // Get a list of shapes to play with
            List<IPosition> positions = new List<IPosition>();
            string choice = null;
            do
            {
                Console.WriteLine("What shape would you like to create? ");
                foreach (ShapeType sType in Enum.GetValues(typeof(ShapeType)))
                {
                    Console.WriteLine("\t{0} - {1}", (int)sType, sType);
                }
                Console.Write("(or are you 'done') > ");
                choice = Console.ReadLine().Trim().ToLower();

                if (choice != "done")
                {
                    ShapeType sType;
                    while (!Enum.TryParse<ShapeType>(choice, true, out sType)
                        // true as the middle param tells TryParse to ignore case

                        || !Enum.IsDefined(typeof(ShapeType), sType))
                    // TryParse accepts ints that aren't actually valid for this
                    // enum. Using IsDefined checks them before allowing the loop
                    // to proceed.
                    {
                        Console.WriteLine("Sorry, I don't know what that shape is.\n");
                        Console.Write("What is another shape you like? ");
                        choice = Console.ReadLine().Trim();
                    }

                    // TODO: ADD YOUR CODE HERE to use the shape type entered above to:
                    // - determine the correct shape to create
                    // - prompt for and get the x & y coordinates (for all shapes)
                    // - The radius for circles
                    // - Create the correct shape type
                    // - Add the new shape to the positions List
                }
                Console.WriteLine();
            }
            while (choice != "done");

            if (positions.Count >= 2)
            {
                // Move everything a bit
                Console.WriteLine("Moving everything by (.5, -.25)");
                foreach (IPosition position in positions)
                {
                    position.MoveBy(.5, -.25);
                }

                // Move just the last shape to 10, 10
                positions[positions.Count - 1].MoveTo(10, 10);

                // TODO: ADD YOUR CODE HERE to print every object in the positions List

                // Check the distance between the first and last shapes
                IPosition p1 = positions[0];
                IPosition p2 = positions[positions.Count - 1];
                Console.WriteLine("The distance between the first and last shapes is {0:F2}",
                    p1.DistanceTo(p2));
                Console.WriteLine();

                foreach (IPosition position in positions)
                {
                    // TODO: ADD YOUR CODE HERE to
                    // - Print the current position object
                    // - Find out if it's in the unit circle
                    // - If it’s a circle, is it larger than unit circle 
                    // - How far is it from the unit circle
                }
            }


        }


    }
}
