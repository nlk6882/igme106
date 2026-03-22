using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace IGME_106_Exam2_2245
{
    /// <summary>
    /// DuckQueue manages a queue of duckies by storing them in what is essentially a singly linked list.
    /// The fields and methods for this class are defined for you. You will be completing the logic.
    /// </summary>
    internal class DuckQueue : DataDuckure
    {
        // Reference to the front of the queue
        DataDuckureNode front = null;

        // TODO 2: Consider how DuckQueue can be modified to make it easier to add to the back of the queue.
        // You'll need to modify this class in multiple places to make this work.

        /// <summary>
        /// Creates a new DuckQueue object with the given content manager
        /// </summary>
        public DuckQueue(ContentManager content)
            : base(content)
        {
        }

        /// <summary>
        /// Draws ALL of the duckies in the queue starting from the front.
        /// This method is done for you and will work assuming the other methods are implemented correctly.
        /// </summary>
        protected override void DrawAll(SpriteBatch sb)
        {
            DataDuckureNode current = front;
            while (current != null)
            {
                current.Draw(sb);
                current = current.Next;
            }
        }

        /// <summary>
        /// Given a new position, create a new ducky node and add it to the back of the queue.
        /// </summary>
        protected override void Add(Vector2 position)
        {
            DataDuckureNode current = front;
            if (current == null)
            {
                front = new DataDuckureNode(image, position);
            }
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new DataDuckureNode(image, position);
            }
            Count++;
        }

        /// <summary>
        /// Remove from the front of the queue. If the queue is empty, do nothing.
        /// </summary>
        protected override void RemoveNext()
        {
            if (front != null)
            {
                front = front.Next;
                Count--;
            }
        }

        /// <summary>
        /// Reset the queue to an empty state.
        /// </summary>
        protected override void RemoveAll()
        {
            front = null;
            Count = 0;
#if SOLUTION
            back = null;
#endif
        }
    }
}
