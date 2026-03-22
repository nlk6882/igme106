namespace PE1_List_of_Objects__Nolan_K_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = false;

            Roster allStuds = new Roster("All Students");
            Roster Fresh = new Roster("Freshmen");

            if (running = false)
            {
                Student student1 = new Student("Sam", "GDD", 5);
                Console.WriteLine(student1.ToString());

                allStuds.addStudent(student1);
                allStuds.addStudent();

                allStuds.displayRoster();
            }

            while (running = true)
            {
                Console.WriteLine("Chse one of the folowing options:\n" +
                    "1 - Add a student\n" +
                    "2 - Change major of a student\n" +
                    "3 - Print the roster\n" +
                    "4 - Quit");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        allStuds.addStudent();
                        break;

                    case "2":
                        break;

                    case "3":
                        allStuds.displayRoster();
                        break;

                    case "4":
                        Environment.Exit(0);
                        break;

                }


            }


        }
    }
}
