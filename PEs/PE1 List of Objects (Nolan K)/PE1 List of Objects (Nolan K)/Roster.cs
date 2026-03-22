using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PE1_List_of_Objects__Nolan_K_
{
    internal class Roster
    {

        //feilds
        private string rosterName;
        private List<Student> students;
        private string fileName;

        //constructor for roster
        internal Roster(string rosterName)
        {
            this.rosterName = rosterName;
            this.students = new List<Student>();
            fileName = "../../../" + rosterName + ".txt";

            //check for file and see if it exists

            //if file exists, load in data and populate student list

            //if file does not exist, no more work need be done in constructor

        }

        internal void Save()
        {
            //create streamwriter variable
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(fileName);

                //write out information for eah stdent in student list
                foreach (Student s in students)
                {
                    writer.WriteLine($"{s.nameProp},{s.majorProp},{s.yearProp}");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //close the file if file is open
            if(writer != null)
            {
                writer.Close();
            }

        }

        //overloaded method that prompts a user ot enter student information and adds a student to the students list and writes a confirmation message. returns student object,
        //or null if initialization failed
        internal Student addStudent()
        {
            Console.WriteLine("Enter student name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter student major");
            string major = Console.ReadLine();

            Console.WriteLine("Enter student year");
            int year = Int32.Parse(Console.ReadLine());


            Student testStudent = new Student(name, major, year);

            addStudent(testStudent);

            if (searchByName(testStudent.nameProp) == null)
            {
                return testStudent;
            }
            else
            {
                return null;
            }
            

            

        }

        //overloaded method that adds a student to the students list and writes a confirmation message. no return.
        internal void addStudent(Student student)
        {
            if (searchByName(student.nameProp) == null)
            {
                //case if no student with that name is found
                students.Add(student);
                Console.WriteLine($"Student {student.nameProp} has been added to the {rosterName} roster.");
            }
            else
            {
                //case if student already exists already
                Console.WriteLine($"Student {student.nameProp} could not be added to {rosterName} roster. (DUPLICATE NAME DETECTED)");

            }
        }

        //method that searches through list of students and sees if student exists. if YES IS FOUND returns student, if NO IS NOT FOUND returns null
        internal Student searchByName(string studentName)
        {

            foreach (Student student in this.students)
            {
                if (studentName == student.nameProp)
                {
                    return student;
                }
            }

            return null;

        }

        //just formats roster for display
        internal void displayRoster()
        {
            foreach(Student student in this.students)
            {
                Console.WriteLine(student.ToString());
            }
        }



    }
}
