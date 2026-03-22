namespace IGME_106_Exam1_2245
{
    /// <summary>
    /// Represents a player (human or AI).
    /// Stores the player’s board and ships. 
    /// Has methods for making a move.
    /// </summary>
    public class Player
    {
        // Game balance constants for ship sizes and counts
        private const int MaxShipSize = 5;
        private const int MinShipSize = 1;
        private const int MaxShips = 4;

        // Boards with the player's ships and to track their moves against the opponent
        private ShipManager myShipManager;
        private Board myMoves;

        // How many sonar buoys the player has left
        private int sonarBuoys = 2;
        private const int SonarRange = 2;

        /// <summary>
        /// The player's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Returns true if all of the player's ships have been sunk.
        /// </summary>
        public bool IsDefeated { get { return myShipManager.IsDefeated; } }

        /// <summary>
        /// Create a new player with the given name and default boards.
        /// </summary>
        public Player(string name)
        {
            Name = name;
            myShipManager = new ShipManager();
            myMoves = new Board();
        }

        /// <summary>
        /// Initialize the game for this player by asking if they want to load a saved 
        /// ship file or place new ships.
        /// </summary>
        public void Init()
        {
            // ask if they want to load a saved ship file
            if (SmartConsole.Prompt("Load a saved ship file?", true))
            {
                myShipManager.LoadShips(Name + "_ships.txt");
            }
            else
            {
                // Keep prompting for new ships until the player has placed MaxShips
                while (myShipManager.Count < MaxShips)
                {
                    myShipManager.DisplayBoard();
                    Console.WriteLine(MaxShips - myShipManager.Count + " ships left to add.\n");
                    Ship newShip = new Ship(
                        SmartConsole.Prompt("Ship ID:").ToUpper()[0],
                        SmartConsole.Prompt("Ship size:", MinShipSize, MaxShipSize),
                        SmartConsole.Prompt("Is the ship horizontal? (y/n):", true),
                        SmartConsole.Prompt("Row:", 0, myShipManager.Size),
                        SmartConsole.Prompt("Column:", 0, myShipManager.Size)
                    );

                    if (myShipManager.PlaceShip(newShip))
                    {
                        SmartConsole.PrintSuccess("Placed: " + newShip);
                    }
                    else
                    {
                        SmartConsole.PrintError("Ship doesn't fit on the board: " + newShip);
                    }
                }

                myShipManager.DisplayBoard();
                if (SmartConsole.Prompt("Save this ship data for later?", true))
                {
                    myShipManager.SaveShips(Name + "_ships.txt");
                }
            }
        }

        /// <summary>
        /// Display BOTH of the player's boards, side by side.
        /// </summary>
        public void DisplayBoards()
        {
            // Heading
            Console.WriteLine("Ships".PadRight(myShipManager.Size) + "\t\tMoves".PadRight(myMoves.Size));

            // Column labels
            Console.Write(" ");
            for (int col = 0; col < myShipManager.Size; col++)
            {
                Console.Write(col);
            }
            Console.Write("\t\t ");
            for (int col = 0; col < myShipManager.Size; col++)
            {
                Console.Write(col);
            }
            Console.WriteLine();

            // Rows
            for (int row = 0; row < myShipManager.Size; row++)
            {
                // Row label and ship board
                Console.Write(row);
                for (int shipsCol = 0; shipsCol < myShipManager.Size; shipsCol++)
                {
                    Console.Write(myShipManager[row, shipsCol]);
                }
                Console.Write("\t\t");

                // Row label and move board
                Console.Write(row);
                for (int movesCol = 0; movesCol < myMoves.Size; movesCol++)
                {
                    Console.Write(myMoves[row, movesCol]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Register a hit on the player's ship board at the given location.
        /// </summary>
        // ANALYZE: What is the Big O for the Player's TakeHit method? How did you determine this?
        public MoveResult TakeHit(int row, int col)
        {
            if (myShipManager[row, col] == '-')
            {
                return MoveResult.Miss;
            }
            else
            {
                Ship hitShip = myShipManager[myShipManager[row, col]];
                return hitShip.TakeHit(row, col);
            }
        }

        /// <summary>
        /// Prompts the player for a move and attacks the opponent's board at that location.
        /// </summary>
        public void Attack(Player opponent)
        {
            Console.WriteLine("\nWhere do you want to target your next strike?");
            int row = SmartConsole.Prompt("\tRow:", 0, myMoves.Size);
            int col = SmartConsole.Prompt("\tColumn:", 0, myMoves.Size);

            char choice = 'm';
            if (sonarBuoys > 0)
            {
                choice = SmartConsole.Prompt("Do you want to launch a missile or a sonar buoy? (m/s):", "ms".ToCharArray());
            }
            else
            {
                Console.WriteLine("No sonar buoys left. Launching missile...");
            }

            switch(choice)
            {
                case 'm':
                    MoveResult result = opponent.TakeHit(row, col);
                    if (result == MoveResult.Miss)
                    {
                        SmartConsole.PrintWarning($"{Name} missed at ({row},{col})!");
                    }
                    else if (result == MoveResult.Hit)
                    {
                        SmartConsole.PrintSuccess($"{Name} HIT at ({row},{col})!");
                    }
                    else
                    {
                        SmartConsole.PrintSuccess($"{Name} SUNK a ship at ({row},{col})!");
                    }
                    myMoves[row, col] = (char)result;
                    break;

                case 's':
                    Board sonarMap = new Board();
                    LaunchSonarBuoy(sonarMap, opponent, row, col, SonarRange);
                    sonarMap.DisplayBoard();
                    sonarBuoys--;
                    break;
            }
        }

        /// <summary>
        /// Give a starting location and a range, launch a sonar buoy to reveal the number
        /// of ships within that range of the starting location.
        /// </summary>
        // ANALYZE: Add comments to identify the 3 laws of recursion in the code below.
        // In your response in myCourses, explain how these 3 laws work together in
        // this case.
        // BUG: There is a problem in this method that causes the resulting sonar map to be lopsided.
        private void LaunchSonarBuoy(Board sonarMap, Player opponent, int row, int col, int range)
        {
            //check base case
            if (range == 0)
            {
                if (opponent.myShipManager.Ping(row, col))
                {
                    sonarMap[row, col] = '*';
                }
                else
                {
                    sonarMap[row, col] = ' ';
                }
            }
            //iteration
            else
            {
                for (int r = -1; r < 1; r++)
                {
                    for (int c = -1; c < 1; c++)
                    {
                        //recall method
                        LaunchSonarBuoy(sonarMap, opponent, row + r, col + c, range - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Run a series of tests on the Player class using a test board.
        /// </summary>
        public static List<IUnitTest> RunTests()
        {
            List<IUnitTest> results = new List<IUnitTest>();

            // Test the Player constructor
            Player p = new Player("TestPlayer");

            /*
                 0123456789
                0----------
                1-A--------
                2----------
                3---B------
                4----------
                5-----C----
                6-----C----
                7----------
                8----------
                9--------DD
            */

            // Load a test board
            p.myShipManager.LoadShips("TestPlayer_ships.txt");

            // Can't automate testing of Init & Attack because they require user input

            // Test TakeHit with a few misses 
            results.Add(new UnitTest<MoveResult>("Player", "Miss at 0,0", MoveResult.Miss, p.TakeHit(0, 0)));
            results.Add(new UnitTest<MoveResult>("Player", "Miss at 2,2", MoveResult.Miss, p.TakeHit(2, 2)));
            results.Add(new UnitTest<MoveResult>("Player", "Miss at 9,7", MoveResult.Miss, p.TakeHit(9, 7)));

            // Test TakeHit to hit and sink everything else
            results.Add(new UnitTest<MoveResult>("Player", "Sink A at 1,1", MoveResult.Sink, p.TakeHit(1, 1)));
            results.Add(new UnitTest<MoveResult>("Player", "Sink B at 3,4", MoveResult.Sink, p.TakeHit(3, 3)));
            results.Add(new UnitTest<MoveResult>("Player", "Hit C at 5,5", MoveResult.Hit, p.TakeHit(5, 5)));
            results.Add(new UnitTest<MoveResult>("Player", "Sink C at 6,5", MoveResult.Sink, p.TakeHit(6, 5)));
            results.Add(new UnitTest<MoveResult>("Player", "Hit D at 9,9", MoveResult.Hit, p.TakeHit(9, 9)));
            results.Add(new UnitTest<MoveResult>("Player", "Sink D at 9,8", MoveResult.Sink, p.TakeHit(9, 8)));

            /* sonar at range 2
             
                 0123456789
                0----------
                1-A--------
                2----------
                3---*    --
                4---     --
                5---  *  --
                6---  *  --
                7---     --
                8----------
                9--------DD
            */

            // Test launching a sonar buoy against ourself at different ranges
            Board sonarMap;
            int row = 5;
            int col = 5;

            int range = 0;
            sonarMap = new Board();
            p.LaunchSonarBuoy(sonarMap, p, row, col, range);
            sonarMap.DisplayBoard();
            results.Add(new UnitTest<char>("Player", "Sonar center (r=" + range + ")", '*', sonarMap[row, col]));
            results.Add(new UnitTest<char>("Player", "Sonar +1,+1 (r=" + range + ")", '-', sonarMap[row+1, col+1]));
            results.Add(new UnitTest<char>("Player", "Sonar -1,-1 (r=" + range + ")", '-', sonarMap[row - 1, col - 1]));
            results.Add(new UnitTest<char>("Player", "Sonar +2,+2 (r=" + range + ")", '-', sonarMap[row + 2, col + 2]));
            results.Add(new UnitTest<char>("Player", "Sonar -2,-2 (r=" + range + ")", '-', sonarMap[row - 2, col - 2]));

            range = 1;
            sonarMap = new Board();
            p.LaunchSonarBuoy(sonarMap, p, row, col, range);
            sonarMap.DisplayBoard();
            results.Add(new UnitTest<char>("Player", "Sonar center (r=" + range + ")", '*', sonarMap[row, col]));
            results.Add(new UnitTest<char>("Player", "Sonar +1,+1 (r=" + range + ")", ' ', sonarMap[row + 1, col + 1]));
            results.Add(new UnitTest<char>("Player", "Sonar -1,-1 (r=" + range + ")", ' ', sonarMap[row - 1, col - 1]));
            results.Add(new UnitTest<char>("Player", "Sonar +2,+2 (r=" + range + ")", '-', sonarMap[row + 2, col + 2]));
            results.Add(new UnitTest<char>("Player", "Sonar -2,-2 (r=" + range + ")", '-', sonarMap[row - 2, col - 2]));

            range = 2;
            sonarMap = new Board();
            p.LaunchSonarBuoy(sonarMap, p, row, col, range);
            sonarMap.DisplayBoard();
            results.Add(new UnitTest<char>("Player", "Sonar center (r=" + range + ")", '*', sonarMap[row, col]));
            results.Add(new UnitTest<char>("Player", "Sonar +1,+1 (r=" + range + ")", ' ', sonarMap[row + 1, col + 1]));
            results.Add(new UnitTest<char>("Player", "Sonar -1,-1 (r=" + range + ")", ' ', sonarMap[row - 1, col - 1]));
            results.Add(new UnitTest<char>("Player", "Sonar +2,+2 (r=" + range + ")", ' ', sonarMap[row + 2, col + 2]));
            results.Add(new UnitTest<char>("Player", "Sonar -2,-2 (r=" + range + ")", '*', sonarMap[row - 2, col - 2]));

            return results;
        }
    }
}
