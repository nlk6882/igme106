using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE6_Indexes_Generics
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Test the ObjectList class with some Pet objects that all have unique names.
            ObjectList<Pet> pets = new ObjectList<Pet>();
            pets.Add(new Pet("Aiden", new DateTime(2000, 1, 1), 10));
            pets.Add(new Pet("Boo", new DateTime(2001, 1, 1), 20));
            pets.Add(new Pet("Candy", new DateTime(2002, 1, 1), 30));
            pets.Add(new Pet("Daisy", new DateTime(2003, 1, 1), 40));
            pets.Add(new Pet("Ella", new DateTime(2004, 1, 1), 50));

            foreach (string name in pets.Names)
            {
                Console.WriteLine($"{name} is {pets[name].Age} years old.");
            }

            // Show that the ObjectList works with ANY type, but it assumes that the type's
            // ToString method returns a unique string for each object.
            ObjectList<string> strings = new ObjectList<string>();
            strings.Add("Aiden");
            strings.Add("Boo");
            strings.Add("Candy");
            Console.WriteLine($"string ObjectList has {strings.Count} items.");

            ObjectList<List<int>> listOfLists = new ObjectList<List<int>>();
            listOfLists.Add(new List<int> { 1, 2, 3 });
            try
            {
                listOfLists.Add(new List<int> { 4, 5, 6 });
            }
            catch (Exception)
            {
                Console.WriteLine("Couldn't add a 2nd List");
            }
            Console.WriteLine($"listOfLists ObjectList has {listOfLists.Count} items.");



        }

    }
}
