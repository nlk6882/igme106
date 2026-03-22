using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE1_List_of_Objects__Nolan_K_
{
    internal class Student
    {

        //feilds
        private string name;
        private string major;
        private int year;

        //proporties for feilds
        public string nameProp { get { return name; } }
        public string majorProp { get { return major; } }
        public int yearProp { get { return year; } }

        //constructor
        internal Student(string name, string major, int year)
        {
            this.name = name;
            this.major = major;
            this.year = year;
        }

        //overide of ToString to 
        public override string ToString()
        {
            return $"{name} - {major} - {year}";
        }





    }
}
