
/**
 * Name: Nolan Kovalik
 * 
 * IGME-106 - Spring 2025 - Exam #1
 * 
 * ONLY the following resources are allowed:
 * - Visual Studio with only this project on an IGM lab computer. No laptops.
 * - myCourses with the exam quiz only
 * - The GitHub website only for making the release
 * - The following official C# references:
 *      - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/index
 *      - https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/index
 */
namespace IGME_106_Exam1_2245
{
    public class Program
    {
        /// <summary>
        /// Kickoff either running tests or the game (based on whichever is uncommented).
        /// </summary>
        public static void Main()
        {
            RunTests();
            RunGame();
        }

        /// <summary>
        /// Collect all of the unit test results from the various classes, print them, and determine if all tests passed.
        /// </summary>
        private static void RunTests()
        {
            List<IUnitTest> results = new List<IUnitTest>();
            results.AddRange(Ship.RunTests());
            results.AddRange(Board.RunTests());
            results.AddRange(ShipManager.RunTests());
            results.AddRange(Player.RunTests());

            int total = results.Count;
            double passed = 0;
            foreach (IUnitTest result in results)
            {
                result.Print();
                if (result.Passed)
                {
                    passed++;
                }
            }
            double percent = passed / total;
            if (percent >= 1)
            {
                SmartConsole.PrintSuccess($"All {total} tests passed!");
            }
            else
            {
                SmartConsole.PrintError($"{passed} of {total} tests passed ({percent:P0})");
            }
        }


        /// <summary>
        /// Run a simple game of Battleship where a human player plays against themself.
        /// </summary>
        private static void RunGame()
        {
            // You always play against yourself right now.
            Player p1 = new Player(SmartConsole.Prompt("What is player 1's name?"));

            // Initialize players.
            p1.Init();

            // While I haven't defeated myself :)
            while (!p1.IsDefeated)
            {
                Console.WriteLine($"\n\n{p1.Name}'s turn!");
                p1.DisplayBoards();

                p1.Attack(p1);
            }

            // Announce the winner.
            // Option to restart or quit.
            if (p1.IsDefeated)
            {
                SmartConsole.PrintSuccess($"\n\nCongrats {p1.Name}! You beat yourself at your own game!");
            }
        }
    }
}
