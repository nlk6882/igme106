using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
namespace HW4_Monogame.NolanK
{
    internal class Cow : GameObject
    {
        //feilds
        public bool isCollected = false;
        public int imageHeight;
        public int imageWidth;

        public Cow(int xpos, int ypos, int imageHeight, int imageWidth)
        {
            this.imageXPos = xpos;
            this.imageYPos = ypos;
            this.imageHeight = 60;
            this.imageWidth = 80;
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch, Texture2D cowImage)
        {
            if (!isCollected)
            {
                spriteBatch.Draw(cowImage, new Rectangle(imageXPos, imageYPos, imageWidth, imageHeight), Color.White);
            }
        }

        public void collected()
        {
            isCollected = true;
        }


    }
}
