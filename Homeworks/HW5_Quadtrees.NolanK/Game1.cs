using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ShapeUtils;

/**
 * 
 * DO NOT MODIFY *ANYTHING* IN THIS FILE
 * except to change the namespace to match your project
 * and to potentially turn off the swirling background or
 * switch to begin with a static set of 30 objects.
 * 
 */
namespace HW5_QuadTrees
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		/// <summary>
		/// Should swirling colors be disabled?
		/// TODO: Students, you can change to true if you want.
		/// </summary>
		private const bool DisableSwirling = false;

		/// <summary>
		/// Should the program always begin with the same set of objects?
		/// TODO: You can change this to turn on/off the static set of objects.
		/// </summary>
		private const bool StaticStartingObjects = false;
        private const int StaticStartingCount = 30; 

		// Color "constants" (readonly since Colors cannot be const)
		private readonly Color SwirlColorA = Color.Indigo;
		private readonly Color SwirlColorB = Color.Black;
		private readonly Color ObjectOutlineColor = Color.GreenYellow;

		// Animation constants
		private const float SwirlSpeed = 3.0f;
		private const float FlashSpeed = 8.0f;

		// Sizing constants
		private const int WindowWidth = 1440;
		private const int WindowHeight = 900;
		private const int MinGameObjectSize = 15;
		private const int MaxGameObjectSize = 30;
		private const int MouseCursorSize = 24;
		private const int MouseCursorInner = 4;

		// Constant for title bar text
		private const string TitleBar = "<LEFT CLICK> Place Object  |  <SPACE> Random Object  |  <SHIFT> + <SPACE> Continually Add Objects  |  Highlighted Depth: ";

		// Input related data
		private Rectangle mouseRect;
		private MouseState prevMS;
		private KeyboardState prevKB;

		// The quad tree
		private QuadTreeNode quadTreeRoot;

		// A list of game objects for drawing purposes
		private List<GameObject> gameObjects;

		// Random number generator
		private Random random;
		
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = false;

			_graphics.PreferredBackBufferWidth = WindowWidth;
			_graphics.PreferredBackBufferHeight = WindowHeight;
			_graphics.ApplyChanges();
		}

		protected override void Initialize()
		{
			// Set up window title with instructions
			Window.Title = TitleBar + "None";

			// Other initialization
			gameObjects = new List<GameObject>();
			random = StaticStartingObjects ? new Random(1) : new Random();
			mouseRect = new Rectangle(0, 0, MouseCursorSize, MouseCursorSize);

			// Create the root node of the quad tree
			quadTreeRoot = new QuadTreeNode(0, 0, WindowWidth, WindowHeight);

			// If necessary, create 30 objects to start
			for (int i = 0; StaticStartingObjects && i < StaticStartingCount; i++)
				AddGameObject();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
			// Grab input and check for escape to exit
			KeyboardState kb = Keyboard.GetState();
			MouseState mouse = Mouse.GetState();
			if (kb.IsKeyDown(Keys.Escape))
				Exit();

			// Update the mouse rectangle
			mouseRect.X = mouse.X;
			mouseRect.Y = mouse.Y;

			// Check for adding a random object, which occurs when:
			// - A single press/release of the space bar
			// - Left Shift + Space are held down
			if (SingleKeyPress(Keys.Space) || (kb.IsKeyDown(Keys.LeftShift) && kb.IsKeyDown(Keys.Space)))
				AddGameObject();

			// Check for mouse click for adding a new object under the mouse
			if (SingleMouseClick())
				AddGameObject(new Rectangle(
					mouseRect.X + MouseCursorInner,
					mouseRect.Y + MouseCursorInner,
					mouseRect.Width - MouseCursorInner * 2,
					mouseRect.Height - MouseCursorInner * 2));

			// Save keyboard state for next frame
			prevKB = kb;
			prevMS = mouse;
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			// Clear the screen and start a shape batch
			GraphicsDevice.Clear(Color.Black);
			ShapeBatch.Begin(GraphicsDevice);

			// Is the mouse inside of a quad?
			QuadTreeNode mouseQuad = quadTreeRoot.GetSmallestContainingQuad(mouseRect);
			if (mouseQuad != null)
			{
				// Highlight this quad with swirling colors
				Color[] corners = GetSwirlColors(SwirlColorA, SwirlColorB, gameTime);
				ShapeBatch.Box(mouseQuad.Bounds, corners[0], corners[1], corners[2], corners[3]);
			}

			// Update title bar with quad info
			Window.Title = TitleBar + (mouseQuad == null ? "None" : CalculateQuadDepth(mouseQuad.Bounds));

			// Get all the quad tree bounding rectangles and draw them
			foreach (Rectangle rect in quadTreeRoot.GetAllQuadBounds())
				ShapeBatch.BoxOutline(rect, Color.White);

			// Draw all of the objects next
			foreach (GameObject gameObj in gameObjects)
				ShapeBatch.Box(gameObj.Rectangle, gameObj.Color);

			// Flash any objects in the current quad AFTER
			// drawing them all, so they're on top
			if (mouseQuad != null)
			{
				// Flash the quad's objects
				foreach (GameObject gameObj in mouseQuad.GameObjects)
				{
					ShapeBatch.Box(gameObj.Rectangle, GetFlashColor(gameObj.Color, gameTime));
					ShapeBatch.BoxOutline(gameObj.Rectangle, ObjectOutlineColor);
				}
			}

			// Draw the mouse rectangle on top of everything
			ShapeBatch.BoxOutline(mouseRect, Color.White);
			ShapeBatch.BoxOutline(
				mouseRect.X + MouseCursorInner,
				mouseRect.Y + MouseCursorInner,
				mouseRect.Width - MouseCursorInner * 2,
				mouseRect.Height - MouseCursorInner * 2,
				Color.White);

			// End the batch
			ShapeBatch.End();
			base.Draw(gameTime);
		}

		/// <summary>
		/// Calculates the depth of a quad in the overall tree using
		/// the width of that quad's rectangle and the overall screen width
		/// </summary>
		/// <param name="quad">The quad to calculate a depth</param>
		/// <returns>The 0-based depth of the quad</returns>
		private int CalculateQuadDepth(Rectangle rect)
		{
			int screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
			int quadWidth = rect.Width;

			return (int)Math.Log2(screenWidth / quadWidth);
		}

		/// <summary>
		/// Determines if a single press/release of a key occurred
		/// </summary>
		/// <param name="key">The key to check</param>
		/// <returns>True if mouse was just released, false otherwise</returns>
		private bool SingleKeyPress(Keys key)
		{
			return Keyboard.GetState().IsKeyDown(key) && prevKB.IsKeyUp(key);
		}

		/// <summary>
		/// Determines if a single left mouse click occurred
		/// </summary>
		/// <returns>True if the left button was just released, false otherwise</returns>
		private bool SingleMouseClick()
		{
			return Mouse.GetState().LeftButton == ButtonState.Pressed &&
				prevMS.LeftButton == ButtonState.Released;
		}

		/// <summary>
		/// Adds a single game object to the tree.  If no rectangle is provided, the object's
		/// rectangle will be randomly generated within the bounds of the window.
		/// Also adds a reference to the same object to Game1's list of objects (for drawing).
		/// </summary>
		/// <param name="rect">Optional rectangle for the new game object</param>
		private void AddGameObject(Rectangle? rect = null)
		{
			// Choose a random size
			int size = random.Next(MinGameObjectSize, MaxGameObjectSize + 1);

			// Choose x and y values (with a buffer around the border of the window)
			int x = random.Next(size, GraphicsDevice.Viewport.Width - size);
			int y = random.Next(size, GraphicsDevice.Viewport.Height - size);
			Color color = new Color(
				(float)Math.Max(random.NextDouble(), 0.35f),
				(float)Math.Max(random.NextDouble(), 0.35f),
				(float)Math.Max(random.NextDouble(), 0.35f),
				1.0f);

			// Make the game object and add to both the list and tree
			GameObject gameObj = new GameObject(rect.HasValue ? rect.Value : new Rectangle(x, y, size, size), color);
			gameObjects.Add(gameObj);
			quadTreeRoot.Insert(gameObj);
		}

		/// <summary>
		/// Calculates a flashing version of a color
		/// </summary>
		/// <param name="c">The color to flash</param>
		/// <param name="gt">Game time info</param>
		/// <returns>The altered color for this frame</returns>
		private Color GetFlashColor(Color c, GameTime gt)
		{
			float seconds = (float)gt.TotalGameTime.TotalSeconds;
			float flash = MathF.Sin(seconds * FlashSpeed) * 0.5f + 0.5f;

			return new Color(
				(int)(c.R * flash),
				(int)(c.G * flash),
				(int)(c.B * flash),
				255);
		}

		/// <summary>
		/// Calculates a swirling pattern of two colors in the 4 corners of a rectangle
		/// </summary>
		/// <param name="a">The first color</param>
		/// <param name="b">The second color</param>
		/// <param name="gt">Game time info</param>
		/// <returns>An array of four colors</returns>
		private Color[] GetSwirlColors(Color a, Color b, GameTime gt)
		{
			if (DisableSwirling)
			{
				return new Color[4] { a, a, a, a };
			}

			// Make a swirling gradient pattern
			float seconds = (float)gt.TotalGameTime.TotalSeconds;
			float s0 = MathF.Sin(seconds * SwirlSpeed + MathHelper.PiOver2 * 0) * 0.5f + 0.5f;
			float s1 = MathF.Sin(seconds * SwirlSpeed + MathHelper.PiOver2 * 1) * 0.5f + 0.5f;
			float s2 = MathF.Sin(seconds * SwirlSpeed + MathHelper.PiOver2 * 2) * 0.5f + 0.5f;
			float s3 = MathF.Sin(seconds * SwirlSpeed + MathHelper.PiOver2 * 3) * 0.5f + 0.5f;

			return new Color[4]{
				Color.Lerp(a, b, s0),
				Color.Lerp(a, b, s1),
				Color.Lerp(a, b, s2),
				Color.Lerp(a, b, s3)
			};

		}

	}
}