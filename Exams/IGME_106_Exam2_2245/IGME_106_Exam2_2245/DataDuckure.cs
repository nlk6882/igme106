using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IGME_106_Exam2_2245
{
    /// <summary>
    /// This base class provides common functionality for a demonstration of a data structure
    /// visualized with duckies.
    /// 
    /// How the ducky locations are stored/managed is up to the child classes.
    /// </summary>
    internal abstract class DataDuckure
    {
        /// <summary>
        /// The image to use for the duckies
        /// </summary>
        protected Texture2D image;

        /// <summary>
        /// The number of ducky locations currently stored. This count
        /// MUST be kept up to date by the child classes.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// The mouse state from the last update frame.
        /// </summary>
        private MouseState prevMouseState;

        /// <summary>
        /// Base constructor. Loads the ducky image.
        /// </summary>
        protected DataDuckure(ContentManager content)
        {
            this.image = content.Load<Texture2D>("ducky");
        }

        /// <summary>
        /// Updates the data structure based on mouse input by calling the appropriate methods implemented
        /// by the child classes.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            MouseState mState = Mouse.GetState();

            // To allow for testing of the child classes without single button clicks working:
            // - Add is called with the current mouse position on a left press (every frame!)
            // - RemoveNext is called on a right press (every frame!)

            // TODO 4: Update the left/right mouse button logic to detect a single button click (a press and then a release)
            if (mState.LeftButton == ButtonState.Pressed)
            {
                if (mState.LeftButton == ButtonState.Released)
                {
                    Add(mState.Position.ToVector2());
                }
            }
            if (mState.RightButton == ButtonState.Pressed)
            {
                if (mState.RightButton == ButtonState.Released)
                {
                    RemoveNext();
                }
            }

            prevMouseState = mState;
        }

        public void Draw(SpriteBatch sb)
        {
            DrawAll(sb);
        }

        public void Reset()
        {
            prevMouseState = new MouseState(); // usually a bad idea, but we want to reset to an empty one
            RemoveAll();
        }

        /// <summary>
        /// Implemented by the child classes to add a ducky at the given position 
        /// according to the rules and data structure of the child class.
        /// </summary>
        protected abstract void Add(Vector2 position);

        /// <summary>
        /// Implemented by the child classes to remove the next ducky according to the
        /// rules and data structure of the child class.
        /// </summary>
        protected abstract void RemoveNext();

        /// <summary>
        /// Implemented by the child classes to remove all duckies according to the
        /// rules and data structure of the child class.
        /// </summary>
        protected abstract void RemoveAll();

        /// <summary>
        /// Implemented by the child classes to draw all duckies
        /// Assumes SpriteBatch's Begin HAS already been called and that End will be called later!
        /// </summary>
        protected abstract void DrawAll(SpriteBatch sb);
    }
}
