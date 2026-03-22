using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace IGME_106_Exam2_2245
{
    /// <summary>
    /// DuckStack manages a stack of duckies by storing them in what is essentially a singly linked list.
    /// The fields and methods for this class are defined for you. You will be completing the logic.
    /// </summary>
    internal class DuckStack : DataDuckure
    {
        // A reference to the top of the stack
        private DataDuckureNode top = null;

        /// <summary>
        /// Creates a new DuckStack object with the given content manager
        /// </summary>
        public DuckStack(ContentManager content)
            : base(content)
        {
        }

        /// <summary>
        /// Draws ALL of the duckies in the stack starting from the top.
        /// The method is done for you and will work assuming the other methods are implemented correctly.
        /// </summary>
        protected override void DrawAll(SpriteBatch sb)
        {
            DataDuckureNode current = top;
            while (current != null)
            {
                current.Draw(sb);
                current = current.Next;
            }
        }

        /// <summary>
        /// Given a new position, create a new ducky node and add it to the top of the stack.
        /// </summary>
        protected override void Add(Vector2 position)
        {
            // TODO 3a: DONE Implement Add for a ducky stack.
            DataDuckureNode current = top;
            while (current != null)
            {
                current = current.Next;
            }
            current.Next = new DataDuckureNode(default, position);
        }

        /// <summary>
        /// Removes the top ducky from the stack. If the stack is empty, do nothing.
        /// </summary>
        protected override void RemoveNext()
        {
            // TODO 3b: Implement RemoveNext for a ducky stack
            if(top == null)
            {
                return;
            }

            DataDuckureNode newTop = top.Next;
            top = newTop;

        }

        /// <summary>
        /// Resets the stack to an empty state.
        /// </summary>
        protected override void RemoveAll()
        {
            top = null;
            Count = 0;
        }

    }
}
