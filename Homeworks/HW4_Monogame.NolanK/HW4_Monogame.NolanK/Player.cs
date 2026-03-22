using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HW4_Monogame.NolanK
{
    //enum for states of player sprite
    enum playerStates
    {
        movingLeft,
        movingRight,
        movingUp,
        movingDown,
        movingLeftUp,
        movingLeftDown,
        movingRightUp,
        movingRightDown,
        stationary
    }

    internal class Player
    {
        Vector2 playerLoc;
        playerStates currentState;

        public int playerRectHeight;     // The height of a single frame
        public int playerRectWidth;      // The width of a single frame

        internal int XPos = 40;
        internal int YPos = 40;
        private const int xPosInc = 2;
        private const int yPosInc = 2;

        public playerStates CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public Player(Vector2 playerLoc, playerStates startingState, int width, int height)
        {
            this.playerLoc = playerLoc;
            this.currentState = startingState;
            this.playerRectWidth = width;
            this.playerRectHeight = height;

        }

        public void Update(GameTime gameTime)
        {
            //NOTE: currently no handeling for chicking when the keys are done being pressed
            KeyboardState kbState = Keyboard.GetState();

            switch (currentState)
            {
                case playerStates.stationary:
                    //if right key pressed, state becomes moving right
                    if (kbState.IsKeyDown(Keys.Right))
                    {
                        currentState = playerStates.movingRight;
                    }

                    //if left key pressed, state becomes moving left
                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        currentState = playerStates.movingLeft;
                    }

                    //if up key pressed, state becomes moving up
                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        currentState = playerStates.movingUp;
                    }

                    //if down key pressed, state becomes moving down
                    if (kbState.IsKeyDown(Keys.Down))
                    {
                        currentState = playerStates.movingDown;
                    }
                    break;
                //---------------------------------------------------------------------------------------------
                case playerStates.movingRight:
                    //if right and down keys are pressed, state becomes moving right down
                    if (kbState.IsKeyDown(Keys.Down))
                    {
                        currentState = playerStates.movingRightDown;
                    }
                    //if right and up keys are pressed, state becomes moving right up
                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        currentState = playerStates.movingRightUp;
                    }
                    //if right key is released, state becomes stationary
                    if (kbState.IsKeyUp(Keys.Right))
                    {
                        currentState = playerStates.stationary;
                    }
                    XPos += xPosInc;
                    break;
                case playerStates.movingLeft:
                    //if left and down keys are pressed, state becomes moving left down
                    if (kbState.IsKeyDown(Keys.Down))
                    {
                        currentState = playerStates.movingLeftDown;
                    }
                    //if left and up keys are pressed, state becomes moving left up
                    if (kbState.IsKeyDown(Keys.Up))
                    {
                        currentState = playerStates.movingLeftUp;
                    }
                    //if left key is released, state becomes stationary
                    if (kbState.IsKeyUp(Keys.Left))
                    {
                        currentState = playerStates.stationary;
                    }
                    XPos -= xPosInc;
                    break;
                case playerStates.movingUp:
                    //if left and up keys are pressed, state becomes moving left up
                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        currentState = playerStates.movingLeftUp;
                    }
                    //if right and up keys are pressed, state becomes moving right up
                    if (kbState.IsKeyDown(Keys.Right))
                    {
                        currentState = playerStates.movingRightUp;
                    }
                    //if up key is released, state becomes stationary
                    if (kbState.IsKeyUp(Keys.Up))
                    {
                        currentState = playerStates.stationary;
                    }
                    YPos -= yPosInc;
                    break;
                case playerStates.movingDown:
                    //if left and down keys are pressed, state becomes moving left down
                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        currentState = playerStates.movingLeftDown;
                    }
                    //if right and down keys are pressed, state becomes moving right down
                    if (kbState.IsKeyDown(Keys.Right))
                    {
                        currentState = playerStates.movingRightDown;
                    }
                    //if down key is released, state becomes stationary
                    if (kbState.IsKeyUp(Keys.Down))
                    {
                        currentState = playerStates.stationary;
                    }
                    YPos += yPosInc;
                    break;
                //---------------------------------------------------------------------------------------------
                case playerStates.movingRightUp:
                    //if right key is released, state becomes moving up
                    if (kbState.IsKeyUp(Keys.Right))
                    {
                        currentState = playerStates.movingUp;
                    }
                    //if up key is released, state becomes moving right
                    if (kbState.IsKeyUp(Keys.Up))
                    {
                        currentState = playerStates.movingRight;
                    }
                    XPos += xPosInc;
                    YPos -= yPosInc;
                    break;
                case playerStates.movingRightDown:
                    //if right key is released, state becomes moving down
                    if (kbState.IsKeyUp(Keys.Right))
                    {
                        currentState = playerStates.movingDown;
                    }
                    //if down key is released, state becomes moving right
                    if (kbState.IsKeyUp(Keys.Down))
                    {
                        currentState = playerStates.movingRight;
                    }
                    XPos += xPosInc;
                    YPos += yPosInc;
                    break;
                case playerStates.movingLeftUp:
                    //if left key is released, state becomes moving up
                    if (kbState.IsKeyUp(Keys.Up))
                    {
                        currentState = playerStates.movingUp;
                    }
                    //if up key is released, state becomes moving left
                    if (kbState.IsKeyUp(Keys.Up))
                    {
                        currentState = playerStates.movingLeft;
                    }
                    XPos -= xPosInc;
                    YPos -= yPosInc;
                    break;
                case playerStates.movingLeftDown:
                    //if left key is released, state becomes moving down
                    if (kbState.IsKeyUp(Keys.Left))
                    {
                        currentState = playerStates.movingDown;
                    }
                    //if down key is released, state becomes moving left
                    if (kbState.IsKeyUp(Keys.Down))
                    {
                        currentState = playerStates.movingLeft;
                    }
                    XPos -= xPosInc;
                    YPos += yPosInc;
                    break;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            //image wrapping code here
            //if image leaves right edge
            if(XPos > Game1.windowXSize)
            {
                XPos = 0 - playerRectWidth;
            }
            //if image leaves bottom edge
            if (YPos > Game1.windowYSize)
            {
                YPos = 0 - playerRectHeight;
            }
            //case if image leaves left edge
            if(XPos+playerRectWidth < 0)
            {
                XPos = Game1.windowXSize;
            }
            //case if image leaves top edge
            if (YPos + playerRectHeight < 0)
            {
                YPos = Game1.windowYSize;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }



    }
}
