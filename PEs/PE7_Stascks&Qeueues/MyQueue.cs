using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE7_Stascks_Qeueues
{
    internal class MyQueue<T> : IQueue<T>
    {
        //fields
        List<T> items = new List<T> { };

        //properties
        public int Count
        {
            get { return items.Count(); }
        }
        public bool IsEmpty
        {
            get { if (items.Count() == 0) { return true; } else { return false; } }
        }

        //methods
        public T Peek()
        {
            return items[0];
        }

        public void Enqueue(T item) 
        {
            items.Add(item);
        }

        public T Dequeue()
        {
            T temp = Peek();
            items.Remove(temp);
            return temp;
        }

    }
}
