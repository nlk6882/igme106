using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PE16_Trees
{
    internal class TalentTreeNode
    {

        //fields
        string skillName;
        bool isLearned;

        //properties
        public TalentTreeNode Left { get; set; }
        public TalentTreeNode Right { get; set; }

        //constructor
        public TalentTreeNode(string name, bool learned)
        {
            Left = null;
            Right = null;
            this.skillName = name;
            this.isLearned = learned;
        }

        //methods

        //should print all of the abilities in the tree using “in order” traversal starting at the node on which the method was called.
        public void ListAllAbilities()
        {

        }

        //should print out which abilities the player knows starting at the node on which the method was called.
        public void ListKnownAbilities()
        {

        }

        //should only print the abilities the player could learn next starting at the node on which the method was called.
        //These are all the abilities that are not yet known but have a parent that is known.
        public void ListPossibleAbilities()
        {

        }

    }
}
