using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Numerics;

namespace PE_MG_Buttons
{
    public class Game1 : Game
    {
        // Fields created by the MG template
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // The list of buttons and setup for random bg color
        private SpriteFont font;
        private List<Button> buttons = new List<Button>();
        private Color bgColor = Color.White;
        private Random rng = new Random();

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // TODO: ADD Your new fields here!
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        private int numLeftClicks;
        private int numRightClicks;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //use this.Content to load game content here
            font = Content.Load<SpriteFont>("buttonFont");

            // Create a few 100x200 buttons down the left side
            buttons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 40, 200, 100),
                    "Random BG",
                    font,
                    Color.Purple));

            //this is where we subscribe the methods to the event
            buttons[0].OnButtonLeftClick += this.RandomizeBackground;
            buttons[0].OnButtonLeftClick += this.ButtonLeftClicked;

            buttons[0].OnButtonRightClick += this.RandomizeBackground;
            buttons[0].OnButtonRightClick += this.ButtonRightClicked;

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // TODO: Add your additional button setup code here!
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            buttons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 160, 200, 100),
                    "Set to white",
                    font,
                    Color.Red));

            buttons[1].OnButtonLeftClick += this.BackgroundWhite;
            buttons[1].OnButtonLeftClick += this.ButtonLeftClicked;

            buttons[1].OnButtonRightClick += this.RandomizeBackground;
            buttons[1].OnButtonRightClick += this.ButtonRightClicked;

            buttons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 280, 200, 100),
                    "Set to black",
                    font,
                    Color.Green));

            buttons[2].OnButtonLeftClick += this.BackgroundBlack;
            buttons[2].OnButtonLeftClick += this.ButtonLeftClicked;

            buttons[2].OnButtonRightClick += this.RandomizeBackground;
            buttons[2].OnButtonRightClick += this.ButtonRightClicked;


        }

        // There is no need to add anything to Game1's Update method!
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Button b in buttons)
            {
                b.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);

            _spriteBatch.Begin();

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // TODO: Add your additional drawing code here!
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            _spriteBatch.DrawString(font, "Number of times left clicked: "+numLeftClicks+" Number of times right clicked: "+numRightClicks, new System.Numerics.Vector2(0, 0), Color.Black);


            // Draw the buttons last.
            foreach (Button b in buttons)
            {
                b.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // #################################################################################
        // Button click handlers!
        // #################################################################################

        /// <summary>
        /// LEAVE THIS ONE ALONE
        /// Randomizes the saved background color
        /// </summary>
        public void RandomizeBackground()
        {
            bgColor = new Color(rng.Next(0, 256), rng.Next(0, 256), rng.Next(0, 256));
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // TODO: Add your new button click handlers here!
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public void ButtonLeftClicked()
        {
            numLeftClicks++;
        }
        public void ButtonRightClicked()
        {
            numRightClicks++;
        }
        public void BackgroundWhite()
        {
            bgColor = Color.White;
        }
        public void BackgroundBlack()
        {
            bgColor = Color.Black;
        }

    }
}
