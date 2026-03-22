using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE7_Stascks_Qeueues
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MyStack<string> stringStack = new MyStack<string>();

            stringStack.Push("a");
            stringStack.Push("b");
            stringStack.Push("c");

            Console.WriteLine(stringStack.Peek());
            Console.WriteLine(stringStack.Pop());
            Console.WriteLine(stringStack.Peek());

            Console.WriteLine();

            //////////////////////////////////////////////
            
            MyStack<int> intStack = new MyStack<int>();

            intStack.Push(1);
            intStack.Push(2);
            intStack.Push(3);

            Console.WriteLine(intStack.Peek());
            Console.WriteLine(intStack.Pop());

            Console.WriteLine();

            //////////////////////////////////////////////
            
            MyQueue<string> queue = new MyQueue<string>();

            queue.Enqueue("a");
            queue.Enqueue("b");
            queue.Enqueue("c");

            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());

        }

            
        
            

    }
}
