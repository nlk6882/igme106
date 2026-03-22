using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGME_106_Team_C_Project
{
    public delegate void OnButtonClickDelegate();

    internal class Button
    {
        // fields
        protected SpriteFont font;
        protected MouseState prevMState;
        protected bool enabled = true;
        private string text;
        protected Rectangle position; // Button position and size
        private Vector2 textLoc;
        Texture2D buttonImg;
        private Color textColor;

        //button event
        public event OnButtonClickDelegate LeftButtonClick;

        /// <summary>
        /// custom button to click
        /// </summary>
        /// <param name="device"></param>
        /// <param name="position"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        public Button(GraphicsDevice device, Rectangle position, String text, SpriteFont font, Color color)
        {
            // copies info we will use late
            this.font = font;
            this.position = position;
            this.text = text;

            // Figure out where on the button to draw the text
            Vector2 textSize = font.MeasureString(text);
            textLoc = new Vector2(
                (position.X + position.Width / 2) - textSize.X / 2,
                (position.Y + position.Height / 2) - textSize.Y / 2
            );

            // Invert the button color for text color
            this.textColor = new Color(255 - color.R, 255 - color.G, 255 - color.B);

            // custom 2D texture for button
            buttonImg = new Texture2D(device, position.Width, position.Height, false, SurfaceFormat.Color);
            int[] colorData = new int[buttonImg.Width * buttonImg.Height];
            Array.Fill<int>(colorData, (int)color.PackedValue);
            buttonImg.SetData<Int32>(colorData, 0, colorData.Length);
        }

        public Button(Rectangle position, Texture2D texture)
        {
            this.position = position;
            buttonImg = texture;
        }

        /// <summary>
        /// updates game each frame to check button state
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Check/capture the mouse state regardless of whether this button is pressed or not
            MouseState mState = Mouse.GetState();
            if (mState.LeftButton == ButtonState.Released &&
                prevMState.LeftButton == ButtonState.Pressed &&
                this.position.Contains(mState.Position))
            {
                if (LeftButtonClick != null)
                {
                    LeftButtonClick();
                }
            }

            // previous state is update to the last mouse state
            prevMState = mState;
        }

        /// <summary>
        /// Draws the button to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the button itself
            spriteBatch.Draw(buttonImg, position, Color.White);

            // Draw button text over the button
            spriteBatch.DrawString(font, text, textLoc, textColor);
        }
    }
}
