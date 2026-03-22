using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_GameOfLife
{
    internal class Cell
    {
        //feilds
        bool isAlive;

        //constructor
        internal Cell(bool isAlive)
        {
            this.isAlive = isAlive; 
        }

        //proporties
        public bool alive { get { return isAlive; } }

        //methods
        public override string ToString()
        {
            if (isAlive)
            {
                return "X";
            }
            else
            {
                return "-";
            }
        }




    }
}
