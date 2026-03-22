using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// ****************************************************************************
// *** Do not modify anywhere except where marked with TODO comments! ***
// ****************************************************************************
namespace PE_CollisionDetection
{
    /// <summary>
    /// SquareEntity represents any square object in the game.
    /// Defined by an (X,Y) and a side length.
    /// </summary>
    public class SquareEntity
    {
        // Fields
        private Texture2D texture;
        private Rectangle squareRect;

        /// <summary>
        /// Reference to the bounds of this object. Cannot be changed.
        /// </summary>
        public Rectangle SquareRect 
        { 
            get { return squareRect; } 
        }

        /// <summary>
        /// X position of the upper-left corner of the SquareEntity
        /// </summary>
        public int X 
        { 
            get { return squareRect.X; } 
            set { squareRect.X = value; } 
        }

        /// <summary>
        /// Y position of the upper-left corner of the SquareEntity
        /// </summary>
        public int Y 
        { 
            get { return squareRect.Y; } 
            set { squareRect.Y = value; } 
        }

        /// <summary>
        /// Instantiates a new SquareEntity.
        /// </summary>
        /// <param name="texture">Image to use when rendered</param>
        /// <param name="x">X position of the upper-left corner</param>
        /// <param name="y">Y position of the upper-left corner</param>
        /// <param name="size">Size of the Square</param>
        public SquareEntity(Texture2D texture, int x, int y, int size)
        {
            this.texture = texture;
            this.squareRect = new Rectangle(x, y, size, size);
        }


        // ********************************************************************
        // TODO: Write the Intersects method of this SquareEntity class
        // ********************************************************************
        

        /// <summary>
        /// Draws this SquareEntity to the game window.
        /// </summary>
        /// <param name="sb">Reference to the SpriteBatch object in Game1.</param>
        /// <param name="tint">The color of this game object.</param>
        public void Draw(SpriteBatch sb, Color tint)
        {
            sb.Draw(texture, squareRect, tint);
        }
    }
}
