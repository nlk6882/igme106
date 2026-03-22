using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HW4_Monogame.NolanK
{
    class Enemy : GameObject
    {

        public int enemyRectHeight = 72;     // The height of a single frame
        public int enemyRectWidth = 44;      // The width of a single frame

        //enemy characteristics
        public int xPosInc = 1;
        public int yPosInc = 1;
        Vector2 enemyLoc;

        public Enemy(Vector2 EnemyLoc)
        {
            this.enemyLoc = EnemyLoc;
            imageXPos = Game1.windowXSize - enemyRectWidth - 10;
            imageYPos = Game1.windowYSize - enemyRectHeight - 20;
        }

        public void Update(GameTime gameTime)
        {
            #region imagebouncehandeling 
            //Section for handeling bouncing image
            //if image hits left wall
            if (imageXPos < 0)
            {
                xPosInc = -1 * xPosInc;
            }
            //if image hits right wall
            else if (Game1.windowXSize < (imageXPos + enemyRectWidth))
            {
                xPosInc = -1 * xPosInc;
            }

            //if image hits ceiling
            if (imageYPos < 0)
            {
                yPosInc = -1 * yPosInc;
            }
            //if image hits floor
            else if (Game1.windowYSize < (imageYPos + enemyRectHeight))
            {
                yPosInc = -1 * yPosInc;
            }

            imageXPos += xPosInc;
            imageYPos += yPosInc;
            #endregion
        }

        public void Draw() 
        {
        
        }

    }
}
