using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PE7_Stascks_Qeueues
{
    internal class MyStack<T> : IStack<T>
    {
        //fields
        List<T> items = new List<T> { };

        //propeties
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
            return items[items.Count()-1];
        }

        public T Pop()
        {
            T temp = Peek();
            items.Remove(temp);
            return Peek();
        }

        public void Push(T item) 
        {
            items.Add(item);
        }

    }
}
