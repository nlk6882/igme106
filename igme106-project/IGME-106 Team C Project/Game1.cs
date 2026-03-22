using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace IGME_106_Team_C_Project
{
    /* NAMESPACE:
     * 
     * Project Created By: Nao, Nolan, Marcus, Madison
     * 
     * Created On: 4/13/25
     * 
     * Additional Notes:
     * 
     * 
    */ 
    internal enum possibleStates
    {
        mainMenu,
        pauseMenu,
        gameOver,
        gamePlay,
        shootOut
    }

    public enum FoodItems
    {
        bun,
        meat,
        lettuce,
        ketchup,
        onion
    }
    
    public class Game1 : Game
    {
        //project creation default feilds
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //random if needed
        private Random rng = new Random();

        //handeling for window
        internal const int windowXSize = 800;
        internal const int windowYSize = 800;

        // assets
        private Texture2D backGround;
        private Texture2D placeholderCustomer;
        private SpriteFont arialFont;
        Texture2D _texture;
        private SpriteFont arial20;

        //fields
        possibleStates state;
        private double timeBeforePlayerInput;
        private int level;
        private bool isRight; //decides if order was correct or not
        private bool isDueling;
        private Order playerPlate;
        private int initialTimeToComplete;
        private string orderStatus;
        private Button fireButton;
        private CharacterEntity currentCharacter;
        private Rectangle reticle; // the center reticle in the shooting state
        private Rectangle alignPoint; // the moving point on screen you have to align with the center reticle
        private Rectangle screenRect;
        private int moveSpeed;
        private int orderIllustrationX = 250;
        private int orderIllustrationY = windowYSize - 250;

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8
        //bulk object storage
        
        private List<Button> menuButtons;
        private List<Button> pauseButtons;
        private List<Button> gameplayButtons;
        private List<Button> level2Buttons;
        private List<Button> gameOverButtons;
        private List<CharacterEntity> characterSequence;
        

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8

        //properties
        internal int WindowXSize
        {
            get { return windowXSize; }
        }
        internal int WindowYSize
        {
            get { return windowYSize; }
        }

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8

        protected override void Initialize()
        {
            //initialization logic here
            state = possibleStates.mainMenu;
            _graphics.PreferredBackBufferWidth = windowXSize;  // set this value to the desired width
            _graphics.PreferredBackBufferHeight = windowYSize;   // set this value to the desired height
            _graphics.ApplyChanges();

            menuButtons       = new List<Button>();
            pauseButtons      = new List<Button>();
            gameplayButtons   = new List<Button>();
            level2Buttons     = new List<Button>();
            gameOverButtons   = new List<Button>();
            characterSequence = new List<CharacterEntity>();
            playerPlate       = new Order(new List<FoodItems>());

            reticle = new Rectangle(WindowXSize/2, WindowYSize/2, 100, 75);
            alignPoint = new Rectangle(100, WindowYSize / 2, 100, 75);
            screenRect = new Rectangle(0, 0, WindowXSize, WindowYSize);

            level = 1;
            timeBeforePlayerInput = 5;
            isDueling = false;
            initialTimeToComplete = 15;
            orderStatus = "Determine what the customer would like!";
            moveSpeed = 10;

            //texture instantiation
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.White });

            base.Initialize();
        }

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load content here
            arialFont = Content.Load<SpriteFont>("arialFont");
            arial20 = Content.Load<SpriteFont>("arial20");
            backGround = Content.Load<Texture2D>("CyberpunkBar");
            placeholderCustomer = Content.Load<Texture2D>("circle");
            int buttonWidth = 200;

            fireButton = (new Button(
                _graphics.GraphicsDevice,
                    new Rectangle(100,
                    160,
                    buttonWidth,
                    100),
                    "FIRE!",
                    arialFont,
                    Color.Red
                ));
            fireButton.LeftButtonClick += this.winOrLose;
            /////////////////////////////////////////////////////////// --- Main Menu Buttons
            
            // Create a 100x200 button down the left side
            menuButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize/2-105, 80, buttonWidth, 100),
                    "Start Game",
                    arialFont,
                    Color.DarkBlue));
            //this is where we subscribe the methods to the event
            menuButtons[0].LeftButtonClick += this.transitionGameplay;

            /////////////////////////////////////////////////////////// --- Gameplay Buttons

            gameplayButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 80, buttonWidth, 100),
                    "Add Bun",
                    arialFont,
                    Color.DarkBlue));
            gameplayButtons[0].LeftButtonClick += this.addBun;

            gameplayButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 200, buttonWidth, 100),
                    "Add Lettuce",
                    arialFont,
                    Color.DarkBlue));
            gameplayButtons[1].LeftButtonClick += this.addLettuce;

            gameplayButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 320, buttonWidth, 100),
                    "Add Meat",
                    arialFont,
                    Color.DarkBlue));
            gameplayButtons[2].LeftButtonClick += this.addMeat;

            gameplayButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 440, buttonWidth, 100),
                    "Add Ketchup",
                    arialFont,
                    Color.DarkBlue));
            gameplayButtons[3].LeftButtonClick += this.addKetchup;

            level2Buttons.Add(new Button(
                _graphics.GraphicsDevice,
                new Rectangle(10,560, buttonWidth, 100),
                "Add Onion",
                arialFont,
                Color.DarkBlue));
            level2Buttons[0].LeftButtonClick += this.addOnion;


            gameplayButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize-210, 80, buttonWidth, 100),
                    "Pause",
                    arialFont,
                    Color.DarkBlue));
            gameplayButtons[4].LeftButtonClick += this.transitionPause;

            gameplayButtons.Add(new Button(
                     _graphics.GraphicsDevice,
                     new Rectangle(windowXSize - 210, 200, buttonWidth, 100),
                     "Remove top item",
                     arialFont,
                     Color.DarkBlue));
            gameplayButtons[5].LeftButtonClick += this.RemoveTopItem;

            /////////////////////////////////////////////////////////// --- Pause Menu Buttons
            
            pauseButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize/2 - 105, windowXSize-150, buttonWidth, 100),
                    "Main Menu",
                    arialFont,
                    Color.DarkBlue));
            pauseButtons[0].LeftButtonClick += this.transitionMenu;

            pauseButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize / 2 - 105, windowXSize - 300, buttonWidth, 100),
                    "Resume",
                    arialFont,
                    Color.DarkBlue));
            pauseButtons[1].LeftButtonClick += this.transitionGameplay;

            /////////////////////////////////////////////////////////// --- Game Over Buttons

            gameOverButtons.Add(new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(windowXSize / 2 - 105, windowYSize / 2 - buttonWidth / 2, buttonWidth, 100),
                    "Main Menu",
                    arialFont,
                    Color.DarkBlue));
            gameOverButtons[0].LeftButtonClick += this.transitionMenu;
            
            currentCharacter = new CharacterEntity(
                placeholderCustomer, 
                (2 * windowXSize)/3 , 
                windowYSize-150, 
                100, 
                100, 
                level, 
                "rando", 
                arialFont, 
                initialTimeToComplete);
            currentCharacter.LeftButtonClick = CheckOrderCompletion;

            characterSequence.Add(currentCharacter);

            characterSequence.Add(new CharacterEntity(
                placeholderCustomer,
                (2 * windowXSize) / 3,
                windowYSize - 150,
                100,
                100,
                level,
                "next",
                arialFont,
                initialTimeToComplete + 2));
            characterSequence[1].LeftButtonClick = CheckOrderCompletionLevel;

            characterSequence.Add(new CharacterEntity(
                placeholderCustomer,
                windowXSize - 110,
                windowYSize - 150,
                100,
                100,
                level + 1,
                "first",
                arialFont,
                initialTimeToComplete + 4));
            characterSequence[2].LeftButtonClick = CheckOrderCompletion;

            characterSequence.Add(new CharacterEntity(
                placeholderCustomer,
                windowXSize - 110,
                windowYSize - 150,
                100,
                100,
                level + 1,
                "onion",
                arialFont,
                initialTimeToComplete + 6));
            characterSequence[3].LeftButtonClick = CheckOrderCompletion;

            characterSequence.Add(new CharacterEntity(
                placeholderCustomer,
                windowXSize - 110,
                windowYSize - 150,
                100,
                100,
                level + 1,
                "deluxe",
                arialFont,
                initialTimeToComplete + 8));
            characterSequence[4].LeftButtonClick = CheckOrderCompletionGame;

        }

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //update logic here
            KeyboardState kbState = Keyboard.GetState();

            switch (state)
            {
                case possibleStates.mainMenu:
                    //main menu update logic

                    //update buttons
                    foreach(Button button in menuButtons)
                    {
                        button.Update(gameTime);
                    }
                    currentCharacter.Update(gameTime);
                    break;

                case possibleStates.pauseMenu:
                    //pause menu update logic
                    foreach (Button button in pauseButtons)
                    {
                        button.Update(gameTime);
                    }

                    break;
                case possibleStates.gameOver:
                    //game over update logic
                    foreach (Button button in gameOverButtons)
                    {
                        button.Update(gameTime);
                    }

                    break;
                case possibleStates.gamePlay:
                    //method call for character to prompt for order
                    if (timeBeforePlayerInput > 0)
                    {
                        timeBeforePlayerInput -= gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    }

                    if (currentCharacter.Update(gameTime))
                    {
                        transitionGameOver();
                    }

                    foreach (Button button in gameplayButtons)
                    {
                        button.Update(gameTime);
                    }

                    //method call for possible duel if fail
                    
                    break;
                case possibleStates.shootOut:
                    fireButton.Update(gameTime);
                    // have the rectangle for alignPoint keep moving on the x-axis back and forth
                    alignPoint.X += moveSpeed;
                    if (alignPoint.Right >= screenRect.Right)
                    {
                        moveSpeed = -moveSpeed;
                    }
                    if (alignPoint.Left <= screenRect.Left)
                    {
                        moveSpeed = -moveSpeed;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8

        public void RemoveTopItem()
        {
            if (playerPlate.Count == 0)
            {
                orderStatus = "Could not remove item as there are no items on the plate.";
                return;
            }
            int count = playerPlate.Count - 1;
            orderStatus = $"Removed {playerPlate.Plate[count].ToString()} from the plate.";
            playerPlate.Plate.RemoveAt(count);
        }
        
        public void CheckOrderCompletion()
        {
            // Add Nao's order implementation, then check the character's order against the player's order
            // on a left click, assuming the player has a non-empty order.
            // If same, go on to next character and add points as appriopriate
            // Else, duel!
            if (!currentCharacter.Order.Check(playerPlate.Plate))
            {
                currentCharacter.SetDialogue(currentCharacter.Count - 1);
                // goes to duel
                orderStatus = "You didn't complete the order. :(";
                transitionShooting();
                return;
            }
            orderStatus = "You completed the order! Hooray!";
            timeBeforePlayerInput = 5;
            initialTimeToComplete += 2;
            int index = characterSequence.IndexOf(currentCharacter);
            currentCharacter = characterSequence[index + 1];
            playerPlate.Plate.Clear();
           
        }

        public void CheckOrderCompletionLevel()
        {
            if (!currentCharacter.Order.Check(playerPlate.Plate))
            {
                currentCharacter.SetDialogue(currentCharacter.Count-1);
                // goes to duel
                orderStatus = "You didn't complete the order. :(";
                transitionShooting();
                return;
            }
            orderStatus = "Onto the next level!";
            level += 1;
            timeBeforePlayerInput = 5;
            initialTimeToComplete += 2;
            int index = characterSequence.IndexOf(currentCharacter);
            currentCharacter = characterSequence[index + 1];
            playerPlate.Plate.Clear();

            if (level == 2)
            {
                gameplayButtons.AddRange(level2Buttons);
            }
        }

        public void CheckOrderCompletionGame()
        {
            if (!currentCharacter.Order.Check(playerPlate.Plate))
            {
                currentCharacter.SetDialogue(currentCharacter.Count - 1);
                // goes to duel
                orderStatus = "You didn't complete the order. :(";
                transitionShooting();
                return;
            }
            orderStatus = "Congratulations! You served all the bandits!";
            transitionGameOver();
        }

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Add your drawing code here
            _spriteBatch.Begin();

            switch (state)
            {
                case possibleStates.mainMenu:
                    //main menu draw

                    //draw buttons
                    foreach (Button button in menuButtons)
                    {
                        button.Draw(_spriteBatch);
                    }

                    //instructions
                    _spriteBatch.DrawString(arialFont, "Figure out what the raider is trying to order, then select the ingredients needed.\n" +
                        "Prepare the order right, and satisfy the bandit and procede! However, should you fail, it'll be a duel to the death!\n" +
                        "Dont't run out of time either! Good luck!", new Vector2(0, 20), Color.Black);

                    break;
                case possibleStates.pauseMenu:
                    //pause menu draw

                    //draw buttons
                    foreach (Button button in pauseButtons)
                    {
                        button.Draw(_spriteBatch);
                    }

                    _spriteBatch.DrawString(arialFont, "Game Paused", new Vector2(windowXSize / 2 - 50, 50), Color.Black);

                    break;
                case possibleStates.gameOver:
                    //game over draw

                    //draw buttons
                    foreach (Button button in gameOverButtons)
                    {
                        button.Draw(_spriteBatch);
                    }

                    string gameOver = "Game Over! Restart?";
                    int count = characterSequence.Count - 1;

                    if (currentCharacter == characterSequence[count])
                    {
                        gameOver = "You won! Restart?";
                        Vector2 textSize = arial20.MeasureString(gameOver);
                        _spriteBatch.DrawString(arial20, gameOver, new Vector2(windowXSize / 2 - textSize.X / 2, 50), Color.Black);
                    }
                    else
                    {
                        _spriteBatch.DrawString(arialFont, gameOver, new Vector2(windowXSize / 2 - 50, 50), Color.Black);
                    }

                    break;
                case possibleStates.gamePlay:

                    if (timeBeforePlayerInput < 4)
                    {
                        currentCharacter.Draw(_spriteBatch, Color.White, Color.DarkRed);
                    }

                    //method call for character to prompt for order
                    if (timeBeforePlayerInput > 0)
                    {
                        Vector2 textSize = arial20.MeasureString(orderStatus);
                        _spriteBatch.DrawString(arial20, orderStatus, new Vector2(windowXSize / 2, windowYSize / 2) - (textSize / 2), Color.White);
                        break;
                    }

                    //method draw gameplay buttons
                    


                    //method call for player to make order
                    foreach (Button button in gameplayButtons)
                    {
                        button.Draw(_spriteBatch);
                    }

                    //draw current order
                    _spriteBatch.DrawString(arialFont, orderStatus, new Vector2(0, 20), Color.Black);

                    //draw time remianing
                    _spriteBatch.DrawString(arialFont, "Time Remaining: " + (int)currentCharacter.TimeCounter, new Vector2(0, 50), Color.Black);

                    //for each ingrediant, draw the rectangle as added
                    int height = 50;
                    Rectangle rect = new Rectangle(orderIllustrationX, orderIllustrationY, 200, height);

                    //iterate through items on plate currently and draw
                    for (int i = 0; i < playerPlate.Count; i++)
                    {
                        FoodItems item = playerPlate.Plate[i];
                        switch (item) 
                        {
                            case FoodItems.bun:
                                height = 50;
                                rect = new Rectangle(orderIllustrationX, rect.Y - height, 200, height);
                                _spriteBatch.Draw(_texture, rect, Color.Tan);
                                break;
                            case FoodItems.meat:
                                height = 50;
                                rect = new Rectangle(orderIllustrationX,rect.Y - height, 200, height);
                                _spriteBatch.Draw(_texture, rect, Color.SaddleBrown);
                                break;
                            case FoodItems.lettuce:
                                height = 25;
                                rect = new Rectangle(orderIllustrationX, rect.Y - height, 200, height);
                                _spriteBatch.Draw(_texture, rect, Color.Green);
                                break;
                            case FoodItems.ketchup:
                                height = 10;
                                rect = new Rectangle(orderIllustrationX, rect.Y - height, 200, height);
                                _spriteBatch.Draw(_texture, rect, Color.DarkRed);
                                break;
                            case FoodItems.onion:
                                height = 25;
                                rect = new Rectangle(orderIllustrationX, rect.Y - height, 200, height);
                                _spriteBatch.Draw(_texture, rect, Color.MediumPurple);
                                break;
                        }
                    }

                    break;

                case possibleStates.shootOut:
                    fireButton.Draw(_spriteBatch);
                    // the program will draw the retical and alignPoint to the screen, and the align point will be the one moving back and forth
                    _spriteBatch.Draw(placeholderCustomer, reticle, Color.Green);
                    _spriteBatch.Draw(placeholderCustomer, alignPoint, Color.Cyan);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        //--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8--8

        //additional methods
        internal void transitionGameplay()
        {
            state = possibleStates.gamePlay;
        }
        internal void transitionPause()
        {
            state = possibleStates.pauseMenu;
        }
        internal void transitionMenu()
        {
            state = possibleStates.mainMenu;

            //reset handeling
            playerPlate.Plate.Clear();
            timeBeforePlayerInput = 5;
            currentCharacter = characterSequence[0];
            for (int i = 0; i < characterSequence.Count; i++)
            {
                CharacterEntity character = characterSequence[i];
                character.TimeCounter = 15 + i * 2;
                character.SetDialogue(0);
            }
        }

        internal void transitionGameOver()
        {

            state = possibleStates.gameOver;
            playerPlate.Plate.Clear();
            level = 1;
            orderStatus = "Determine what the customer would like!";

            for (int i = 0; i < gameplayButtons.Count; i++) 
            {
                Button button = gameplayButtons[i];
                if (level2Buttons.Contains(button))
                {
                    gameplayButtons.RemoveAt(i);
                }
            }
        }
        internal void transitionShooting()
        {
            state = possibleStates.shootOut;
        }
        internal void envExit()
        {
            Environment.Exit(1);
        }
        internal void addLettuce()
        {
            playerPlate.Plate.Add(FoodItems.lettuce);
            orderStatus = "Added some lettuce to the order!";
        }
        internal void addBun()
        {
            playerPlate.Plate.Add(FoodItems.bun);
            orderStatus = "Added a bun to the order!";
        }
        internal void addKetchup()
        {
            playerPlate.Plate.Add(FoodItems.ketchup);
            orderStatus = "Added some ketchup to the order!";
        }
        internal void addMeat()
        {
            playerPlate.Plate.Add(FoodItems.meat);
            orderStatus = "Added some meat to the order!";
        }

        internal void addOnion()
        {
            playerPlate.Plate.Add(FoodItems.onion);
            orderStatus = "Added an onion to the order!";
        }

        internal void winOrLose()
        {
            if (alignPoint.Intersects(reticle)) // if the aligned point is inside or colliding with the retical
            {
                isRight = true;
                moveSpeed += 1;
                transitionGameplay();
            }
            else
            {
                currentCharacter = null;
                transitionGameOver();
            }
            
        }
        internal void resetOrderIllustrationY()
        {
            orderIllustrationY = windowXSize - 250;
        }

    }
}
