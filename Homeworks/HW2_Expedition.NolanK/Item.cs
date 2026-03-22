using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HW2_Expedition.NolanK
{
    internal class Item
    {
        //fields
        double itemValue;
        string name;
        string description;
        ItemTypes type;
        int tier;

        //properties
        public double Value
        {
            get { return itemValue; } set { itemValue = value; }
        }
        public string Name
        {
            get { return name; }
        }
        public string Description
        {
            get { return description; }
        }
        public string Type
        {
            get { return type.ToString(); }
        }
        public int Tier
        {
            get { return tier; }
        }


        //constructor
        internal Item(string name, ItemTypes type, int tier, double value, string description)
        {
            this.name = name;
            this.type = type;
            this.itemValue = value;
            this.tier = tier;
            this.description = description;
        }

        //methods


    }
}
