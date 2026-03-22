using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace IGME_106_Team_C_Project
{
    public abstract class ObjectEntity
    {
        private Texture2D _entityTexture;
        private Rectangle _entityRect;

        /// <summary>
        /// The texture of this ObjectEntity. Cannot be changed.
        /// </summary>
        public Texture2D Texture
        {
            get { return _entityTexture; }
        }

        /// <summary>
        /// The Vector2 position of this ObjectEntity. The object cannot be changed.
        /// The relative position changes depending on the specific entity.
        /// </summary>
        public Rectangle EntityRectangle
        {
            get { return _entityRect; }
        }

        /// <summary>
        /// The X position of this ObjectEntity.
        /// </summary>
        public int X
        {
            get { return _entityRect.X; }
            set { _entityRect.X = value; }
        }
        
        /// <summary>
        /// The Y position of this ObjectEntity.
        /// </summary>
        public int Y
        {
            get { return _entityRect.Y; }
            set { _entityRect.Y = value; }
        }

        /// <summary>
        /// The width of this ObjectEntity.
        /// </summary>
        public int Width
        {
            get { return _entityRect.Width; }
        }
        
        /// <summary>
        /// The height of this ObjectEntity.
        /// </summary>
        public int Height
        {
            get { return _entityRect.Height; }
        }

        /// <summary>
        /// Used by inheritors to instantiate a new entity, represents a drawable entity within the game.
        /// </summary>
        /// <param name="texture">The texture of the entity</param>
        /// <param name="x">The X position of the top-leftmost point of an object. May be a different point depending on the entity.</param>
        /// <param name="y">The Y position of the top-leftmost point of an object. May be a different point depending on the entity.</param>
        /// <param name="width">The width of the object. Can be used for separate purposes, like radius.</param>
        /// <param name="height">The height of the object. Can be used for separate purposes, like a focus for an ellipse.</param>
        protected ObjectEntity (Texture2D texture, int x, int y, int width, int height)
        {
            _entityTexture = texture;
            _entityRect = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Draws this ObjectEntity to the window.
        /// </summary>
        /// <param name="sb">The SpriteBatch object from Game1.</param>
        /// <param name="tint">The color of this ObjectEntity.</param>
        public abstract void Draw(SpriteBatch sb, Color entityTint, Color textTint);

        /// <summary>
        /// Runs the appropriate events based on the inheritor's interpretation.
        /// </summary>
        /// <param name="gameTime">The GameTime object from Game1's Update method.</param>
        /// <returns>The truth-value corresponding to if the event is triggered.</returns>
        public abstract bool Update(GameTime gameTime);
    }
}
