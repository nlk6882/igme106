using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IGME_106_Exam2_2245
{
    /// <summary>
    /// Represents a node in a linked list of duckies.
    /// </summary>
    internal class DataDuckureNode
    {
        // The image to draw & position to draw it
        private Texture2D image;
        private Vector2 position;

        // The next node in the chain
        public DataDuckureNode Next { get; set; }

        /// <summary>
        /// Creates a new node with the given image and position.
        /// </summary>
        public DataDuckureNode(Texture2D image, Vector2 position)
        {
            this.image = image;
            this.position = position;
            Next = null;
        }

        /// <summary>
        /// Draws this node AND recursively tries to then draw all nodes following this one!
        /// Assumes SpriteBatch's Begin HAS already been called and that End will be called later!
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, position, Color.White);
        }
    }
}
