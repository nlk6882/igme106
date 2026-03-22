using HW1_GameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Expedition.NolanK
{
    internal abstract class PartyMember
    {
        //feilds
        string name;
        int health;
        bool alive;
        Roles charType;

        //properties
        public string Name
        {
            get { return name; }
        }
        public int Health
        {
            get { return health; } set { health = value; }
        }
        public bool Alive
        {
            get { return alive; }
        }
        public string CharType
        {
            get { return charType.ToString(); }
        }
        /*
        public abstract string Test
        {
            get;
        }
        */
        //constructor
        internal PartyMember(int health, string name, Roles charType)
        {
            this.health = health;
            this.name = name;
            this.charType = charType;
            this.alive = true;
         
        }

        //methods

        //party member's health is lowered, then checks if they are alive or not
        internal void takeDamage(int damageTaken)
        {
            SmartConsole.PrintError($"{Name} has taken a hit of {damageTaken} health!");
            this.Health = this.health - damageTaken;

            //check if this kills the party member
            if (this.Health <= 0)
            {
                this.alive = false;
                SmartConsole.PrintError($"{Name} has died!");
                this.health = 0;

            }

            Console.WriteLine();

        }

        //abstract method for using special ability
        internal abstract void specialAbility();


    }
}
