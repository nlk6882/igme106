using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace IGME_106_Exam2_2245
{
    class CatGame
    {
        // ** Fields you may need to use, but should not change or add to **
        private Rectangle canvasBounds;

        // Cat data
        private Texture2D catImage;
        private Rectangle catPosition;
        private Vector2 catVelocity = new Vector2(0, 0);
        private const int CatSpeed = 3;

        // Pawprint data
        private Texture2D pawprintImage;
        private List<Vector2> pawprints = new List<Vector2>();
        private const int PawprintTimingMS = 250;
        private int pawprintTimer = 0;

        // Ducky data
        private Texture2D duckyImage;
        private Rectangle duckyPosition;
        private const int DuckySpeed = 5;

        /// <summary>
        /// Creates a new CatGame object with the given bounds and content manager
        /// </summary>
        public CatGame(Rectangle bounds, ContentManager content)
        {
            this.canvasBounds = bounds;
            catImage = content.Load<Texture2D>("cat");
            pawprintImage = content.Load<Texture2D>("pawprint");
            duckyImage = content.Load<Texture2D>("ducky");
            Reset();
        }

        /// <summary>
        /// Reset the game to its initial state
        /// </summary>
        public void Reset()
        {
            pawprints.Clear();

            duckyPosition = new Rectangle(
                canvasBounds.X + canvasBounds.Width / 2 - (int)duckyImage.Width / 2,
                canvasBounds.Y + canvasBounds.Height / 2 - (int)duckyImage.Height / 2,
                duckyImage.Width,
                duckyImage.Height);

            catPosition = new Rectangle(
                canvasBounds.X + canvasBounds.Width - (int)catImage.Width,
                canvasBounds.Y,
                (int)catImage.Width,
                (int)catImage.Height);
        }

        /// <summary>
        /// Checks for WASD input to move the duck around and updates the cat's position
        /// to move towards the duck. If the cat catches the duck, the game resets.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            MouseState mState = Mouse.GetState();
            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.W))
            {
                duckyPosition.Y -= DuckySpeed;
            }
            if (kState.IsKeyDown(Keys.A))
            {
                duckyPosition.X -= DuckySpeed;
            }
            if (kState.IsKeyDown(Keys.S))
            {
                duckyPosition.Y += DuckySpeed;
            }
            if (kState.IsKeyDown(Keys.D))
            {
                duckyPosition.X += DuckySpeed;
            }

            duckyPosition = StayInBounds(duckyPosition);

            // If the cat's center is within the duck's rectangle, the cat has caught the duck & it stops moving
            if (duckyPosition.Intersects(catPosition))
            {
                Reset();
            }
            else
            {
                // Make the cat move towards the duck at a constant speed
                Vector2 duckCenter = new Vector2(duckyPosition.X + duckyPosition.Width / 2, duckyPosition.Y + duckyPosition.Height / 2);
                Vector2 catCenter = new Vector2(catPosition.X + catPosition.Width / 2, catPosition.Y + catPosition.Height / 2);
                catVelocity = duckCenter - catCenter;
                catVelocity.Normalize();
                catVelocity *= CatSpeed;
                catPosition.X += (int)catVelocity.X;
                catPosition.Y += (int)catVelocity.Y;
                catPosition = StayInBounds(catPosition);

                // If the cat is moving, add a pawprint every PawprintTimingMS milliseconds
                pawprintTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (pawprintTimer >= PawprintTimingMS)
                {
                    pawprints.Add(new Vector2(catPosition.X, catPosition.Y));
                    pawprintTimer -= PawprintTimingMS;
                }
            }
        }

        /// <summary>
        /// Draw everything for the game
        /// </summary>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(duckyImage, duckyPosition, Color.Yellow);
            foreach (Vector2 pawprint in pawprints)
            {
                sb.Draw(pawprintImage, pawprint, Color.Black);
            }
            sb.Draw(catImage, catPosition, Color.White);
        }

        /// <summary>
        /// If the given rectangle is outside the bounds, move it back inside taking
        /// into account its width and height. Returns the adjusted rectangle.
        /// </summary>
        private Rectangle StayInBounds(Rectangle posRect)
        {
            Rectangle result = posRect;

            // TODO 5: Write the logic to check the posRect and adjust as needed to stay within the canvas bounds.
            // rectangle to shift it back inside whatever edge it left. For example, if
            // the posRect is outside the left edge of the bounds, set the result's X to the bounds' X.
            // When outside the right or bottom edge, you'll need to consider the width and height of the
            // posRect as well.

            /*
             * dont have time to write this out, but what I would od here is define the play area rectangle in terms of the current window's size,
             * so the cat and mouse cannot go off screen. For the top and right side of the screen, I just have to make sure the assosiated x and y coordinates
             * don't go below 0, but for the right and bottom, I need to make sure the x and y coords plus the width or height of the image are less than the window size
            */

            return result;
        }

    }
}
