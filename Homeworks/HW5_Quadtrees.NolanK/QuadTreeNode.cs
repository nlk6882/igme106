using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

/**
 * 
 * DO NOT MODIFY *ANYTHING* IN THIS FILE EXCEPT WHERE MARKED WITH TODO COMMENTS
 * and to change the namespace to match your project
 * 
 */
namespace HW5_QuadTrees
{
	class QuadTreeNode
	{
		// The maximum number of objects in a quad
		// BEFORE a subdivision occurs.  In other words,
		// once this node has MORE than this many objects,
		// it should subdivide.
		private const int MaxObjectsBeforeSubdivide = 3;

		/// <summary>
		/// The divisions of this quad
		/// </summary>
		public QuadTreeNode[] Divisions { get; private set; }

		/// <summary>
		/// This quad's rectangle area
		/// </summary>
		public Rectangle Bounds { get; private set; }

		/// <summary>
		/// The game objects inside this quad
		/// </summary>
		public List<GameObject> GameObjects { get; private set; }

		/// <summary>
		/// Creates a new Quad Tree
		/// </summary>
		/// <param name="x">This quad's x position</param>
		/// <param name="y">This quad's y position</param>
		/// <param name="width">This quad's width</param>
		/// <param name="height">This quad's height</param>
		public QuadTreeNode(int x, int y, int width, int height)
		{
			// Save the rectangle
			Bounds = new Rectangle(x, y, width, height);

			// Create the object list
			GameObjects = new List<GameObject>();

			// No divisions yet
			Divisions = null;
		}


		/// <summary>
		/// Tries to insert a given game object into this quad's subtree.
		/// - If the object is not contained in this quad, returns false.
		/// - If the object is contained in this quad, it's added to the smallest possible
		///		subquad that contains it.
		///	- If no sub-divisions exist, the object is simply added to this quad and then, if
		///		the numbers of objects in this quad is greater than MaxObjectsBeforeSubdivide,
		///		Divide() is called.
		/// </summary>
		/// <param name="gameObj">The object to insert</param>
		/// <returns>False if the gameObj didn't fit in this quad at all</returns>
		public bool Insert(GameObject gameObj)
		{
			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if (!this.GameObjects.Contains(gameObj)) return false;
			else
			{
				if (this.GameObjects.Count < MaxObjectsBeforeSubdivide)
				{
                    this.GameObjects.Add(gameObj);
					return true;
                }
				else
				{
					Divide();
					if (GetSmallestContainingQuad(this.Bounds).GameObjects.Count < MaxObjectsBeforeSubdivide)
					{
						GetSmallestContainingQuad(this.Bounds).Insert(gameObj);
					}
					else
					{
						return false;
					}
				}
			}

			//should never have to run but VS got angry
			return false;
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }

        /// <summary>
        /// Divides this quad into 4 smaller quads.  Moves any game objects
        /// that are completely contained within the new smaller quads into
        /// those quads and removes them from this one.
        /// </summary>
        public void Divide()
		{
			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			//create new nodes and add to divisions array
			QuadTreeNode topLeft = new QuadTreeNode(Bounds.X, Bounds.Y, Bounds.Width/2, Bounds.Height/2);
			Divisions[0] = topLeft;
            QuadTreeNode topRight = new QuadTreeNode(Bounds.X + Bounds.Width/2, Bounds.Y, Bounds.Width / 2, Bounds.Height / 2);
			Divisions[1] = topRight;
            QuadTreeNode botLeft = new QuadTreeNode(Bounds.X, Bounds.Y + Bounds.Height/2, Bounds.Width / 2, Bounds.Height / 2);
			Divisions[2] = botLeft;
            QuadTreeNode botRight = new QuadTreeNode(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2, Bounds.Width / 2, Bounds.Height / 2);
			Divisions[3] = botRight;

			//move game objects to the correct division
			foreach(GameObject obj in this.GameObjects)
			{
				botRight.Insert(obj);
			}
			this.GameObjects.Clear();
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        }

        /// <summary>
        /// Recursively populates a list with all of the bounding rectangles
        /// in the tree.  This include the bounding rectangle for this quad 
        /// all child quads (if they exist).
        /// </summary>
        /// <returns>A list of rectangles</returns>
        public List<Rectangle> GetAllQuadBounds()
		{
            List<Rectangle> rects = new List<Rectangle>();

			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

			//edge case handeling for when divisions is first null
			if (this.Divisions != null)
			{
                //iterate through all nodes existing in divisions, if a division exists, call method again
                foreach (QuadTreeNode qtn in this.Divisions)
				{
					rects.Add(qtn.Bounds);
					if (qtn.Divisions != null)
					{
						GetAllQuadBounds();
					}
				}
			}
			else
			{
				rects.Add(Bounds);
			}
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            return rects;
		}

		/// <summary>
		/// Recursively searches within this quad's subtree for the smallest possible quad whose bounds
		/// contain the provided rectangle.
		/// </summary>
		/// <param name="rect">The rectangle to check</param>
		/// <returns>The smallest quad that contains the rectangle</returns>
		public QuadTreeNode GetSmallestContainingQuad(Rectangle rect)
		{
			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if (this.Divisions != null)
			{
				//loop through all nodes in current nodes divisions
				foreach (QuadTreeNode qtn in this.Divisions)
				{
					//check that another division exists before attempting to search
					if (qtn.Divisions != null)
					{
						if (qtn.Divisions[0].Bounds == rect)
						{
							//if we found the rectangle, return the object
							return qtn;
						}
						//else, recursively call method again
						GetSmallestContainingQuad(rect);
					}
				}
			}
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            // Return null if this quad doesn't completely contain
            // the rectangle that was passed in
            return null;
		}
	}
}
