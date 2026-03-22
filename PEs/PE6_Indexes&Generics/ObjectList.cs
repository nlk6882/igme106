using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE6_Indexes_Generics
{
    internal class ObjectList<T>
    {
        //feilds
        List<T> rawData = new List<T>();

        //properties
        public int Count {get{ return rawData.Count; } }

        public T this[string name] 
        {
            get
            { 
                foreach(T i in rawData)
                {
                    if(i.ToString() == name)
                    {
                        return i;
                    }
                    else
                    {
                        continue;
                    }
                }

                throw new KeyNotFoundException();

                return default(T);
                
            }
            set 
            {
                foreach (T i in rawData)
                {
                    if (i.ToString() == name)
                    {
                        int index = rawData.IndexOf(i);
                        rawData.Remove(i);
                        rawData.Insert(index, i);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a list of names of the objects in the list as an IEnumerable.
        /// Doing this instead of returning the list directly prevents the caller 
        /// from modifying the list.
        /// </summary>
        public IEnumerable<String> Names
        {
            get
            {
                List<String> names = new List<String>();
                foreach (T item in rawData)
                {
                    names.Add(item.ToString());
                }
                return names.AsEnumerable();
            }
        }

        //contructor (maybe)

        //methods
        public void Add(T item)
        {
            //if (rawData.Contains(item.ToString())) //check if item is already in our list
            if (rawData[item]) //check if item is already in our list
            {
                throw new ArgumentException();
            }
            else
            {
                //add item to list
            }
        }

    }
}
