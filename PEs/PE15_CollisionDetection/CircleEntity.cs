using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ****************************************************************************
// *** Do not modify anywhere except where marked with TODO comments! ***
// ****************************************************************************
namespace PE_CollisionDetection
{
    /// <summary>
    /// CircleEntity represents any circular object in the game.
    /// Defined by a center and a radius.
    /// </summary>
    public class CircleEntity
    {
        // Fields
        private Texture2D texture;
        private Vector2 center;
        private int radius;

        /// <summary>
        /// Center point of this Circle
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
        }

        /// <summary>
        /// Radius of this Circle
        /// </summary>
        public int Radius 
        { 
            get { return radius; }
        }

        /// <summary>
        /// Retrieve and/or change the X component of this circle's center
        /// </summary>
        public float X
        {
            get { return center.X; }
            set { center.X = value; }
        }

        /// <summary>
        /// Retrieve and/or change the Y component of this circle's center
        /// </summary>
        public float Y
        {
            get { return center.Y; }
            set { center.Y = value; }
        }

        /// <summary>
        /// Instantiates a CircleEntity
        /// </summary>
        /// <param name="texture">Image to use when rendering</param>
        /// <param name="x">X position of this circle's center</param>
        /// <param name="y">Y position of this circle's center</param>
        /// <param name="radius">Radius of this circle</param>
        public CircleEntity(Texture2D texture, int x, int y, int radius)
        {
            this.texture = texture;
            this.radius = radius;
            this.center = new Vector2(x, y); 
        }


        // ********************************************************************
        // TODO: Write the Intersects method to implement circle-circle collision detection
        // ********************************************************************

        /// <summary>
        /// Draws this CircleEntity to the game window.
        /// </summary>
        /// <param name="sb">Reference to the SpriteBatch object in Game1.</param>
        /// <param name="tint">The color of this game object.</param>
        public void Draw(SpriteBatch sb, Color tint)
        {
            // ********************************************************************
            // TODO: Use the correct (X, Y) and width and height to represent this circle's bounds
            // (0, 0, 0, 0) is a placeholder here and are NOT the correct values.
            Rectangle circleRect = new Rectangle(0, 0, 0, 0);
            // ********************************************************************


            // Leave this line of code alone!
            sb.Draw(texture, circleRect, tint);
        }
    }
}
