using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace IGME_106_Team_C_Project
{
    internal class CharacterEntity : ObjectEntity
    {
        // TODO: Put in Nao's implementation of the orders.

        private const string ROOTDIR = "..\\..\\..\\character-files\\";

        private string[] _dialogue;
        private Button _characterButton;
        private float _timeCounter;
        private Order _order;
        private SpriteFont _dialogueFont;
        private string _currentDialogue;
        private int _dialogueOffset;
        //private string _name;
        /// <summary>
        /// Subscribes to the LeftButtonClick event in the character's button. Subscribe only.
        /// </summary>
        public OnButtonClickDelegate LeftButtonClick
        {
            set { _characterButton.LeftButtonClick += value; }
        }

        public float TimeCounter
        {
            get { return _timeCounter; }
            set { _timeCounter = value; } 
        }
        

        public Order Order
        {
            get { return _order; }
        }

        /// <summary>
        /// Returns the appriopriate dialogue string based on the index given.
        /// </summary>
        /// <param name="i">The integer corresponding to a dialogue string's index within the dialogue list.</param>
        /// <returns>The requested dialogue from the indexer.</returns>
        public string this[int i]
        {
            get { return _dialogue[i]; }
        }

        public int Count
        {
            get { return _dialogue.Length; }
        }

        /// <summary>
        /// Instantiates a new CharacterEntity, which loads the dialogue and order for the entity, 
        /// creates a button for the entity, and keeps track of the remaining time the player has to complete the order.
        /// </summary>
        /// <param name="texture">The texture to be displayed.</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="level">The level that the entity is in, which is used to load the appriopriate data.</param>
        /// <param name="name">The internal file name of the entity, used to load the appriopriate data.</param>
        /// <param name="timeCounter">The time the player has at the start of their order, in seconds.</param>
        public CharacterEntity(Texture2D texture, int x, int y, int width, int height, int level, string name, SpriteFont font, float timeCounter=15.0f, int dialogueOffset=50) : base(texture, x, y, width, height)
        {
            _characterButton = new Button(EntityRectangle, texture);
            _timeCounter = timeCounter;
            _dialogueFont = font;
            _dialogueOffset = dialogueOffset;
            //_name = name;
            Load(level, /*_*/name);
        }

        /// <summary>
        /// Updates the CharacterEntity by counting down the time the player has to complete its order
        /// and checking if it has been clicked.
        /// </summary>
        /// <param name="gameTime">The GameTime object from Game1's Update method.</param>
        /// <returns>The truth-value corresponding to if there is no time left for the player to complete the order.</returns>
        public override bool Update(GameTime gameTime)
        {
            // Subtracts the the time remaining from the time that has passed.
            _timeCounter -= (float) gameTime.ElapsedGameTime.TotalSeconds;

            // Checks to see if the button has been clicked, then runs any methods subscribed to the left click event.
            _characterButton.Update(gameTime);

            // If the time is less than or equal to 0, return true, so we can handle what happens in Game1's update method.
            return _timeCounter <= 0;
        }

        /// <summary>
        /// Draws both the texture of the entity and the current dialogue for the entity with appriopriate offsets.
        /// </summary>
        /// <param name="sb">The SpriteBatch object from a Game1 instance.</param>
        /// <param name="tint">The tint of the dialogue for the character.</param>
        public override void Draw(SpriteBatch sb, Color entityTint, Color textTint)
        {
            sb.Draw(base.Texture, base.EntityRectangle, entityTint);

            Vector2 textSize = _dialogueFont.MeasureString(_currentDialogue);

            sb.DrawString(_dialogueFont, _currentDialogue, new Vector2(X-textSize.X, Y), textTint);

            if (_currentDialogue == _dialogue[0])
            {
                for (int i = 1; i < _dialogue.Length-1; i++)
                {
                    textSize = _dialogueFont.MeasureString(_dialogue[i]);
                    sb.DrawString(_dialogueFont, _dialogue[i], new Vector2(X - textSize.X, Y + textSize.Y + (i * 3)), textTint);
                }
            }
        }

        /// <summary>
        /// The current dialogue to be drawn to the screen.
        /// </summary>
        /// <param name="i">The index within the dialogue array in this object.</param>
        public void SetDialogue(int i)
        {
            _currentDialogue = _dialogue[i];
        }

        private void Load(int level, string name)
        {
            string dir = ROOTDIR + $"level-{level}\\{name}.txt";

            // If the file with the current directory different above does not exist, throw an exception.
            if (!File.Exists(dir))
            {
                throw new ArgumentException($"File name {name} does not exist within level {level}.");
            }
            
            // Limits these two variables to only exist within these scopes, which limits some
            // memory leaks and other problems associated reading and writing files.
            using (FileStream file = File.OpenRead(dir))
            {
               using (StreamReader sr = new StreamReader(file, Encoding.UTF8, false))
               {
                    // Reads from file into a string array
                    _dialogue = sr.ReadLine().Split("/");

                    // Creates a local array for food items, represented as strings for now.
                    string[] foodItems = sr.ReadLine().Split(",");

                    // Creates the temp order for FoodItems to be while they are each loaded.
                    List<FoodItems> tempOrder = new List<FoodItems>();

                    // For every string within the local string array.
                    foreach (string food in foodItems)
                    {
                        // Creates a temp FoodItems enum variable so the TryParse method can send its output to something.
                        FoodItems tempFood = new FoodItems();

                        // Try to match the current string from the local array to the FoodItems enum, and send the result to tempFood.
                        // If it can't, continue the for loop without adding to the temp FoodItems array.
                        if (!Enum.TryParse<FoodItems>(food, out tempFood))
                        {
                            continue;
                        }
                        tempOrder.Add(tempFood);
                    }

                    // Instantiate the private order object field with the food items array loaded from the character's file.
                    _order = new Order(tempOrder);

                    // Close the stream after loading orders.
                    sr.Close();
                }
                _currentDialogue = _dialogue[0];
            }
        }

       /*public override string ToString()
        {
            return $"{_name}";
        }*/
    }
}
