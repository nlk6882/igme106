using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Expedition.NolanK
{
    internal class Fighter : PartyMember
    {
        //fields
        string name;
        Roles charType;

        //properties

        //constructor
        public Fighter(string name, int health, Roles charType) : base(health, name, charType)
        {

        }

        //methods

        //method that overrides from parent to handle the special attack
        internal override void specialAbility()
        {

        }

    }
}
