using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


// TODO 0: Update this header to include your name and anything else we should know when grading
/**
 * Name: Nolan Kovalik
 * 
 * Release Notes:
 * 
 * IGME-106 - Spring 2025 - Exam #2
 * 
 * ONLY the following resources are allowed:
 * - Visual Studio with only this project on an IGM lab computer. No laptops.
 * - myCourses with the exam quiz only
 * - The GitHub website only for making the release
 * - The following online references:
 *      - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/index
 *      - https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/index
 *      - http://tinyurl.com/igme106-monogame-issues
 *              You may NOT reference online MonoGame documentation!
 */
namespace IGME_106_Exam2_2245
{
    public enum CanvasState
    {
        Empty,
        DuckStack,
        DuckQueue,
        CatGame
    }

    public class Game1 : Game
    {
        // Fields you may need to use, but should not change or add to
        private List<Button> buttons = new List<Button>();
        private CanvasState currentState = CanvasState.Empty;
        private DuckQueue duckQueue;
        private DuckStack duckStack;
        private CatGame catGame;

        // MonoGame basics + layout constants & some other fields you won't
        // need to use yourself.
        #region Other fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int Width = 800;
        private const int Height = 600;
        private const int ButtonWidth = 100;
        private const int ButtonHeight = 50;
        private const int ButtonSpacing = 10;
        private Rectangle bounds;
        private SpriteFont titleFont;
        private SpriteFont textFont;
        #endregion

        // Properties to determine the title, subtitle, and background color
        // based on the current state to keep the rest of the code simpler.
        // You won't need to call them yourself. Do NOT change these!
        #region State based properties
        private string Title
        {
            get
            {
                switch (currentState)
                {
                    case CanvasState.DuckStack:
                        return "Duck Stack: " + duckStack.Count;
                    case CanvasState.DuckQueue:
                        return "Duck Queue: " + duckQueue.Count;
                    case CanvasState.CatGame:
                        return "Cat Game";
                    default:
                        return "IGME-106 Exam 2";
                }
            }
        }
        private string Subtitle
        {
            get
            {
                switch (currentState)
                {
                    case CanvasState.DuckStack:
                    case CanvasState.DuckQueue:
                        return "Left click to add; right click to remove";
                    case CanvasState.CatGame:
                        return "Use WASD to evade the cat!";
                    default:
                        return "Select a button to switch modes";
                }
            }
        }
        private Color BGColor
        {
            get
            {
                switch (currentState)
                {
                    case CanvasState.DuckStack:
                        return Color.AliceBlue;
                    case CanvasState.DuckQueue:
                        return Color.SeaShell;
                    case CanvasState.CatGame:
                        return Color.LightPink;
                    default:
                        return Color.LightSlateGray;
                }
            }
        }
        #endregion

        // Game1, Initialize, & LoadContent are done for you. Do NOT modify these!
        #region Game1(), Initialize(), LoadContent()
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            duckQueue = new DuckQueue(Content);
            duckStack = new DuckStack(Content);
            bounds = new Rectangle(0, 0, Width, Height);
            catGame = new CatGame(bounds, Content);
            titleFont = Content.Load<SpriteFont>("titleFont");
            textFont = Content.Load<SpriteFont>("defaultFont");
            SetupButtons();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = Width;
            _graphics.PreferredBackBufferHeight = Height;
            _graphics.ApplyChanges();
        }
        #endregion

        // TODO 1a: DONE: Create three methods to handle the button clicks.
        public OnButtonClickDelegate GameClicked()
        {
            currentState = CanvasState.CatGame;
            catGame.Reset();
            return null;
        }

        public OnButtonClickDelegate StackClicked()
        {
            currentState = CanvasState.DuckStack;
            duckStack.Reset();
            return null;
        }
        
        public OnButtonClickDelegate QueueClicked()
        {
            currentState = CanvasState.DuckQueue;
            duckQueue.Reset();
            return null;
        }
        // Make sure the signatures are appropriate to allow these methods to be
        // subscribed to the buttons' OnLeftButtonClick event. In each, set currentState
        // appropriately and call reset on the appropriate object.
        // For example, when the Stack button is clicked, currentState should be set to DuckStack
        // and duckStack.Reset() should be called.

        // This method is called by LoadContent to set up the buttons.
        private void SetupButtons()
        {
            // Buttons
            buttons = new List<Button>();
            buttons.Add(
                new Button(
                    GraphicsDevice,
                    new Rectangle(ButtonSpacing, ButtonSpacing, ButtonWidth, ButtonHeight),
                    "Stack",
                    titleFont,
                    Color.AliceBlue));

            buttons.Add(
                new Button(
                    GraphicsDevice,
                    new Rectangle(ButtonSpacing, 2 * ButtonSpacing + ButtonHeight, ButtonWidth, ButtonHeight),
                    "Queue",
                    titleFont,
                    Color.SeaShell));

            buttons.Add(
                new Button(
                    GraphicsDevice,
                    new Rectangle(ButtonSpacing, 3 * ButtonSpacing + 2 * ButtonHeight, ButtonWidth, ButtonHeight),
                    "Game",
                    titleFont,
                    Color.LightPink));

            // TODO 1b: DONE Complete SetupButtons() by subscribing each button to the appropriate method to handle the click
            // The buttons are in the order they were created, so the first button is the stack, the second is the queue,
            // and the third is the game.
            KeyboardState kbs = Keyboard.GetState();
            //on buttonleftclick event, call the method assosiated with the button clicked
            buttons[0].OnLeftButtonClick += StackClicked();
            buttons[1].OnLeftButtonClick += QueueClicked();
            buttons[2].OnLeftButtonClick += GameClicked();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Button b in buttons)
            {
                b.Update();
            }

            // TODO 1c: DONE: Use a switch statement to call the appropriate update method for the current state
            switch (currentState) 
            {
                case CanvasState.CatGame:
                    GameClicked();
                    break;
                case CanvasState.DuckQueue:
                    QueueClicked();
                    break;
                case CanvasState.DuckStack:
                    StackClicked();
                    break;
                default:
                    break;

            }


            // TODO 0: To test without having the buttons working, uncomment the mode you want to test
            duckQueue.Update(gameTime);
            //duckStack.Update(gameTime);
            //catGame.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BGColor);

            _spriteBatch.Begin();
            foreach (Button b in buttons)
            {
                b.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(titleFont, Title, new Vector2(200, 10), Color.Black);
            _spriteBatch.DrawString(textFont, Subtitle, new Vector2(200, 40), Color.Black);

            // TODO 1d: Use a switch statement to call the appropriate draw method for the current state

            // TODO 0: To test without having the buttons working, uncomment the mode you want to test
            duckQueue.Draw(_spriteBatch);
            //duckStack.Draw(_spriteBatch);
            //catGame.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
