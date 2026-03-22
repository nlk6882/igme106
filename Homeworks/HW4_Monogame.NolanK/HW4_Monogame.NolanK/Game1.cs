using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic;

//enum for game states
public enum gameStates
{
    mainMenu,
    inGame,
    pause,
    gameOver
}

namespace HW4_Monogame.NolanK
{
    public class Game1 : Game
    {
        // Fields created by the MG template
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //creation of a current game state
        gameStates currentGameState = gameStates.mainMenu;
        public string title = "My Fun Game!";
        public int timer;
        private const int timeLimit = 10;
        int level;
        int score;
        int totalScore;
        int numCows = 1;
        int timeElapsed = 0;

        // The list of buttons
        private SpriteFont font;
        private List<Button> menuButtons = new List<Button>();
        private List<Button> gameButtons = new List<Button>();
        private List<Button> pauseButtons = new List<Button>();
        private List<Button> gameOverButtons = new List<Button>();

        //random if needed
        private Random rng = new Random();

        //handeling for window
        public const int windowXSize = 500;
        public const int windowYSize = 500;

        private Texture2D imageNM;
        private Texture2D imageUFO;
        private Texture2D imageCow;

        private Player player;
        private Enemy enemy;

        private List<Cow> cowList = new List<Cow>();

        public gameStates CurrentGameState
        {
            get { return currentGameState; }
        }
        

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

            timer = 60 * timeLimit;

            level = 1;
            score = 0;
            totalScore = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //use this.Content to load game content here
            font = Content.Load<SpriteFont>("fontUsed");
            imageNM = Content.Load<Texture2D>("Chad");
            imageUFO = Content.Load<Texture2D>("ufo");
            imageCow = Content.Load<Texture2D>("Cow");

            //create cows
            for (int i = 0; i < numCows; i++)
            {
                cowList.Add(new Cow((int)rng.NextInt64(50, windowXSize - 50), (int)rng.NextInt64(50, windowYSize - 50), imageCow.Height, imageCow.Width));
            }

            //instantiate player object
            Vector2 playerLoc = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            player = new Player(playerLoc, playerStates.stationary, imageNM.Width, imageNM.Height);

            //instantiate enemy object
            Vector2 enemyLoc = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            enemy = new Enemy(enemyLoc);

            // Create a 100x200 button down the left side
            menuButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 80, 200, 100),
                    "Start Game",
                    font,
                    Color.DarkBlue));
            //this is where we subscribe the methods to the event
            menuButtons[0].OnButtonLeftClick += this.transitionGameplay;

            // Create a 100x200 button for the pause button
            gameButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize-110, 20, 100, 50),
                    "Pause",
                    font,
                    Color.DarkBlue));
            //this is where we subscribe the methods to the event
            gameButtons[0].OnButtonLeftClick += this.transitionPause;

            // Create a 100x200 button for the pause button
            pauseButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize/2-75, windowYSize/2, 150, 50),
                    "Return To Menu",
                    font,
                    Color.DarkBlue));
            //this is where we subscribe the methods to the event
            pauseButtons[0].OnButtonLeftClick += this.transitionMenu;
            pauseButtons[0].OnButtonLeftClick += this.resetGame;

            pauseButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize / 2 - 75, windowYSize/2-60, 150, 50),
                    "Return To Game",
                    font,
                    Color.DarkBlue));
            //this is where we subscribe the methods to the event
            pauseButtons[1].OnButtonLeftClick += this.transitionGameplay;

            gameOverButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize / 2 - 75, windowYSize / 2 - 60, 150, 50),
                    "Exit",
                    font,
                    Color.DarkBlue));
            //this is where we subscribe the methods to the event
            gameOverButtons[0].OnButtonLeftClick += this.envExit;

            gameOverButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize / 2 - 75, windowYSize / 2 + 10, 150, 50),
                    "Main Menu",
                    font,
                    Color.DarkBlue));
            //this is where we subscribe the methods to the event
            gameOverButtons[1].OnButtonLeftClick += this.transitionMenu;
            gameOverButtons[1].OnButtonLeftClick += this.resetGame;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState kbState = Keyboard.GetState();

            //update for the menu screen
            if (currentGameState == gameStates.mainMenu)
            {
                foreach (Button b in menuButtons)
                {
                    b.Update(gameTime);
                }

            }
            //update for the gameplay screen
            if (currentGameState == gameStates.inGame)
            {
                timeElapsed++;
                timer--;
                if(timer <= 0)
                {
                    transitionGameOver();
                }

                foreach (Button b in gameButtons)
                {
                    b.Update(gameTime);
                }

                #region collisions
                Rectangle enemyRec = new Rectangle(enemy.imageXPos, enemy.imageXPos, enemy.enemyRectWidth-10, enemy.enemyRectHeight-10);
                Rectangle playerRec = new Rectangle(player.XPos, player.YPos, player.playerRectWidth-10, player.playerRectHeight-10);

                //enemy hitting player
                if (playerRec.Intersects(enemyRec))
                {
                    transitionGameOver();
                }

                //collecting cows
                foreach (Cow c in cowList)
                {
                    Rectangle cowRec = new Rectangle(c.imageXPos, c.imageYPos, c.imageWidth, c.imageHeight);
                    if (playerRec.Intersects(cowRec))
                    {
                        if (!c.isCollected)
                        {
                            score++;
                            totalScore++;
                        }
                        c.collected();
                    }
                }
                #endregion

                //check to see if player advaces level
                if(score >= numCows)
                {
                    nextLevel();
                }


                player.Update(gameTime);
                enemy.Update(gameTime);

            }
            //update for the pause menu
            if (currentGameState == gameStates.pause)
            {
                foreach (Button b in pauseButtons)
                {
                    b.Update(gameTime);
                }
            }

            if(currentGameState == gameStates.gameOver)
            {
                foreach (Button b in gameOverButtons)
                {
                    b.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            // Draw the menu
            if (currentGameState == gameStates.mainMenu)
            {
                //buttons
                foreach (Button b in menuButtons)
                {
                    b.Draw(_spriteBatch);
                }
                //title
                _spriteBatch.DrawString(font, title, new System.Numerics.Vector2(0, 0), Color.Black);

                //instructions
                _spriteBatch.DrawString(font, "Use WASD to collect the cows and avoid the humans! \nCollect all the cowws before time runs out!", new System.Numerics.Vector2(0, 20), Color.Black);
            }
            //draw the gameplay screen
            if(currentGameState == gameStates.inGame)
            {
                _spriteBatch.Draw(imageNM, new Rectangle(enemy.imageXPos, enemy.imageYPos, enemy.enemyRectWidth, enemy.enemyRectHeight), Color.White);

                _spriteBatch.Draw(imageUFO, new Rectangle(player.XPos, player.YPos, imageNM.Width, imageNM.Height), Color.White);

                //TESTING:
                /*
                Texture2D pixel;
                pixel = new Texture2D(GraphicsDevice, 1, 1);
                pixel.SetData<Color>(new Color[] { Color.White });
                _spriteBatch.Draw(pixel, new Rectangle(player.XPos, player.YPos, imageNM.Width, imageNM.Height), Color.Blue);

                pixel = new Texture2D(GraphicsDevice, 1, 1);
                pixel.SetData<Color>(new Color[] { Color.White });
                _spriteBatch.Draw(pixel, new Rectangle(enemy.imageXPos, enemy.imageYPos, enemy.enemyRectWidth, enemy.enemyRectHeight), Color.Red);
                */

                //UI
                _spriteBatch.DrawString(font, "Time Left   " + String.Format("{0:0.00}", timer / 60) + "\n" +
                    "Total Score: " + totalScore + "\n" +
                    "Level Score: " + score + "\n" +
                    "Level: " + level +
                    "", new System.Numerics.Vector2(0, 0), Color.Black);

                //buttons
                foreach (Button b in gameButtons)
                {
                    b.Draw(_spriteBatch);
                }

                //cows
                foreach (Cow c in cowList)
                {
                    c.Draw(_spriteBatch, imageCow);
                }

            }
            //draw the pause menu
            if(currentGameState == gameStates.pause)
            {
                //buttons
                foreach (Button b in pauseButtons)
                {
                    b.Draw(_spriteBatch);
                }
            }

            if(currentGameState == gameStates.gameOver)
            {
                //buttons
                foreach (Button b in gameOverButtons)
                {
                    b.Draw(_spriteBatch);
                }

                //game over text
                _spriteBatch.DrawString(font, "Game Over!\nTotal Score: "+totalScore+"\nLevel Reached: " + level+"\nTime Elapsed: " + String.Format("{0:0.00}", timeElapsed / 60) + "\n", new System.Numerics.Vector2(0, 0), Color.Black);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //methods

        public void transitionGameplay()
        {
            currentGameState = gameStates.inGame;
        }
        public void transitionPause()
        {
            currentGameState = gameStates.pause;
        }
        public void transitionMenu()
        {
            currentGameState = gameStates.mainMenu;
        }
        public void transitionGameOver()
        {
            currentGameState = gameStates.gameOver;
        }
        public void envExit()
        {
            Environment.Exit(1);
        }
        public void resetGame()
        {
            //reset for player
            Vector2 playerLoc = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            player = new Player(playerLoc, playerStates.stationary, imageNM.Width, imageNM.Height);
            
            //reset for enemy
            Vector2 enemyLoc = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            enemy = new Enemy(enemyLoc);

            //general game reset
            timer = 60 * timeLimit;
            totalScore = 0;
            score = 0;
            level = 1;

            for(int i = 0; i < numCows; i++)
            {
                cowList.Add(new Cow((int)rng.NextInt64(50, windowXSize-50), (int)rng.NextInt64(50, windowYSize-50), imageCow.Height, imageCow.Width));
            }
        }
        public void nextLevel()
        {
            //general game reset
            timer = 60 * timeLimit;
            score = 0;
            level++;
            numCows++;

            //another bonus feature that enemy moves faster as levels progress - makes collision buggy so commented out for now
            /*
            if (enemy.xPosInc < 0) { enemy.xPosInc = enemy.xPosInc - 1; }
            if (enemy.xPosInc > 0) { enemy.xPosInc = enemy.xPosInc + 1; }
            if (enemy.xPosInc < 0) { enemy.yPosInc = enemy.yPosInc - 1; }
            if (enemy.xPosInc > 0) { enemy.yPosInc = enemy.yPosInc + 1; }
            */

            for (int i = 0; i < numCows; i++)
            {
                cowList.Add(new Cow((int)rng.NextInt64(50, windowXSize - 50), (int)rng.NextInt64(50, windowYSize - 50), imageCow.Height, imageCow.Width));
            }
        }

    }
}
