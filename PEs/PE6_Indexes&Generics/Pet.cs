using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE6_Indexes_Generics
{
    /// <summary>
    /// Simple Pet class for testing the ObjectList class.
    /// </summary>
    internal class Pet
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public float Weight { get; set; }
        public int Age
        {
            get
            {
                TimeSpan age = DateTime.Now - Birthday;
                return (int)(age.TotalDays / 365.25f);
            }
        }

        public Pet(string name, DateTime birthday, float weight)
        {
            Name = name;
            Birthday = birthday;
            Weight = weight;
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
