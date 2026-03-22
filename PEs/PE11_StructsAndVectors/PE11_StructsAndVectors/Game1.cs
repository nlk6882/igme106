using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;

namespace PE11_StructsAndVectors
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int windowXSize = 500;
        private int windowYSize = 500;

        private Texture2D imageNM;
        private int imageNMXPos;
        private int imageNMYPos;

        private int xPosInc = 1;
        private int yPosInc = 1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = windowXSize;  // set this value to the desired width
            _graphics.PreferredBackBufferHeight = windowYSize;   // set this value to the desired height
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            imageNM = Content.Load<Texture2D>("NoMotes");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //image height is 1001, width is 978

            //if image hits left wall
            if(imageNMXPos < 0)
            {
                xPosInc = -1 * xPosInc;
            }
            //if image hits right wall
            else if(windowXSize < (imageNMXPos + imageNM.Width/10))
            {
                xPosInc = -1 * xPosInc;
            }
            
            //if image hits ceiling
            if (imageNMYPos < 0)
            {
                yPosInc = -1 * yPosInc;
            }
            //if image hits floor
            else if (windowYSize < (imageNMYPos + imageNM.Height/10))
            {
                yPosInc = -1*yPosInc;
            }
            
            imageNMXPos += xPosInc;
            imageNMYPos += yPosInc;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //_spriteBatch.Draw(imageER, new Vector2(0,0), Color.White);
            //_spriteBatch.Draw(imageER, new Rectangle(0, 0, 800, 500), Color.White);
            _spriteBatch.Draw(imageNM, new Rectangle(imageNMXPos, imageNMYPos, imageNM.Width / 10, imageNM.Height / 10), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
