using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE16_Trees
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TalentTreeNode root = new TalentTreeNode("magic", true);

            root.Left = new TalentTreeNode("Fireball", true);
            root.Right = new TalentTreeNode("Magic Arrow", true);

            root.Left.Left = new TalentTreeNode("Crazy Big Fireball", false);
            root.Left.Right = new TalentTreeNode("1000 Tiny Fireballs", true);
            root.Right.Left = new TalentTreeNode("Ice Arrow", false);
            root.Right.Right = new TalentTreeNode("Exploding Arrow", false);

        }
    }
}
