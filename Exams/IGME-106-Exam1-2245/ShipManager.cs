namespace IGME_106_Exam1_2245
{
    /// <summary>
    /// A "smart" Board that manages a set of ships.
    /// </summary>
    // BUG: There are multiple problems in this file related to File IO.
    public class ShipManager : Board
    {
        // The relative path to the ship data files
        private const string ShipDataPath = "shipData/";

        // The list of ships on the board
        protected List<Ship> ships = new List<Ship>();

        /// <summary>
        /// Indexer to get a ship by its ID
        /// </summary>
        public Ship this[char ID]
        {
            get
            {
                int size = ships.Count;
                char[] IDList = new char[size];
                int i = 0;

                
                    foreach (Ship ship in ships)
                    {
                        if (ship.ID == ID)
                        {
                            return ship;
                        }
                        else
                        {
                            return null;
                        }
                    }

                return null;

                // TODO- DONE: Implement this indexer so that it returns the ship with the given ID. If a ship with that ID is not found, return null.
                //return new Ship('?', 1, false, 0, 0); // Temp code so the project runs without crashing until the indexer is implemented
            }
        }

        /// <summary>
        /// The number of ships on the board
        /// </summary>
        public int Count { get { return ships.Count; } }

        /// <summary>
        /// Returns true if all ships on the board have been sunk.
        /// </summary>
        public bool IsDefeated
        {
            get
            {
                foreach (Ship s in ships)
                {
                    if (!s.IsSunk)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Create a new ShipManager with the given size (or the default size)
        /// </summary>
        public ShipManager(int size = BoardSize) : base(size)
        {
        }

        /// <summary>
        /// "Ping" a location to see if it contains something
        /// </summary>
        public bool Ping(int row, int col)
        {
            return this[row, col] != Empty;
        }

        /// <summary>
        /// Place a ship on the board if it fits and doesn't overlap with another ship.
        /// If placement was successful, the ship is added to the list of ships and the
        /// method returns true. Otherwise, the method returns false.
        /// </summary>
        public bool PlaceShip(Ship ship)
        {
            // Is the ship location within the board?
            if (ship.Row < 0 || ship.Row >= Size ||
                ship.Col < 0 || ship.Col >= Size)
            {
                return false;
            }

            // Does a ship with this ID already exist?
            else if (this[ship.ID] != null)
            {
                return false;
            }

            // Are any of the locations for this ship already occupied?
            else if (ship.IsHorizontal)
            {
                for (int col = ship.Col; col < ship.Col + ship.Size; col++)
                {
                    if (grid[ship.Row, col] != Empty)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int row = ship.Row; row < ship.Row + ship.Size; row++)
                {
                    if (grid[row, ship.Col] != Empty)
                    {
                        return false;
                    }
                }
            }

            // if we get here, the ship can be placed
            ships.Add(ship);
            if (ship.IsHorizontal)
            {
                for (int col = ship.Col; col < ship.Col + ship.Size; col++)
                {
                    grid[ship.Row, col] = ship.ID;
                }
            }
            else
            {
                for (int row = ship.Row; row < ship.Row + ship.Size; row++)
                {
                    grid[row, ship.Col] = ship.ID;
                }
            }
            return true;
        }

        /// <summary>
        /// Save the list of ships to a file in the shipData folder.
        /// </summary>
        /// <param name="filename"></param>
        public void SaveShips(string filename)
        {
            StreamWriter output = null;
            try
            {
                output = new StreamWriter(ShipDataPath + filename);
                foreach (Ship s in ships)
                {
                    output.WriteLine(s);
                }
                output.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving ships: " + e.Message);
            }
        }

        /// <summary>
        /// Load ships from a file in the shipData folder.
        /// </summary>
        public void LoadShips(string filename)
        {
            ships.Clear();
            StreamReader input = null;
            try
            {
                input = new StreamReader(ShipDataPath + filename);
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    Ship newShip = new Ship(
                        parts[0][0],
                        int.Parse(parts[1]),
                        bool.Parse(parts[2]),
                        int.Parse(parts[3]),
                        int.Parse(parts[4])
                    );
                    PlaceShip(newShip);
                }
                input.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading ships: " + e.Message);
            }

            Console.WriteLine("Loaded " + ships.Count + " ships.");
            DisplayBoard();
        }

        /// <summary>
        /// Sets up and runs a series of tests on the ShipManager class.
        /// </summary>
        public static List<IUnitTest> RunTests()
        {
            List<IUnitTest> results = new List<IUnitTest>();

            /*
                 01234
                0--A--
                1--ADD 
                2CCC--
                3---B-
                4---B-
             */

            ShipManager board = new ShipManager(5);

            // Some ships that all fit
            Ship a = new Ship('A', 2, false, 0, 2);
            Ship b = new Ship('B', 2, false, 3, 3);
            Ship c = new Ship('C', 3, true, 2, 0);
            Ship d = new Ship('D', 2, true, 1, 3);

            // Test that each is placed correctly and then access the [row,col] values to verify
            results.Add(new UnitTest<bool>("ShipManager", "Place A", true, board.PlaceShip(a)));
            results.Add(new UnitTest<char>("ShipManager", "A loc start", 'A', board[0, 2]));
            results.Add(new UnitTest<char>("ShipManager", "A loc end", 'A', board[1, 2]));

            results.Add(new UnitTest<bool>("ShipManager", "Place B", true, board.PlaceShip(b)));
            results.Add(new UnitTest<char>("ShipManager", "B loc start", 'B', board[3, 3]));
            results.Add(new UnitTest<char>("ShipManager", "B loc end", 'B', board[4, 3]));

            results.Add(new UnitTest<bool>("ShipManager", "Place C", true, board.PlaceShip(c)));
            results.Add(new UnitTest<char>("ShipManager", "C loc start", 'C', board[2, 0]));
            results.Add(new UnitTest<char>("ShipManager", "C loc mid", 'C', board[2, 2]));

            results.Add(new UnitTest<bool>("ShipManager", "Place D", true, board.PlaceShip(d)));
            results.Add(new UnitTest<char>("ShipManager", "D loc start", 'D', board[1, 3]));
            results.Add(new UnitTest<char>("ShipManager", "D loc end", 'D', board[1, 4]));

            // Test that a ship can't be placed on top of another
            Ship e = new Ship('E', 2, true, 1, 3);
            results.Add(new UnitTest<bool>("ShipManager", "Place overlapping E", false, board.PlaceShip(e)));
            results.Add(new UnitTest<char>("ShipManager", "E loc still D", 'D', board[1, 3]));

            // Test that a ship can't be placed off the board
            Ship f = new Ship('F', 2, true, 1, 4);
            results.Add(new UnitTest<bool>("ShipManager", "Place off-board F", false, board.PlaceShip(f)));

            // Test that a ship can't be placed with the same ID
            Ship g = new Ship('A', 2, true, 1, 4);
            results.Add(new UnitTest<bool>("ShipManager", "Place duplicate ID A", false, board.PlaceShip(g)));

            // Save all the ships that were successfully placed to test_ships.txt (making sure the file doesn't already exist)
            try
            {
                if (File.Exists(ShipDataPath + "test_ships.txt"))
                {
                    File.Delete(ShipDataPath + "test_ships.txt");
                }
                board.SaveShips("test_ships.txt");
                results.Add(new UnitTest<bool>("ShipManager", "File saved", true, File.Exists(ShipDataPath + "test_ships.txt")));

                // Reload the ships from test_ships.txt
                ShipManager board2 = new ShipManager(5);
                board2.LoadShips("test_ships.txt");

                // Test that the ships were loaded correctly
                // Use the this[char] indexer to get a loaded ship by ID and compare to the originals
                results.Add(new UnitTest<int>("ShipManager", "Loaded ship count", 4, board2.Count));
                results.Add(new UnitTest<Ship>("ShipManager", "Loaded A matches", a, board2['A']));
                results.Add(new UnitTest<Ship>("ShipManager", "Loaded B matches", b, board2['B']));
                results.Add(new UnitTest<Ship>("ShipManager", "Loaded C matches", c, board2['C']));
                results.Add(new UnitTest<Ship>("ShipManager", "Loaded D matches", d, board2['D']));

                // Test Ping
                results.Add(new UnitTest<bool>("ShipManager", "Ping A", true, board2.Ping(0, 2)));
                results.Add(new UnitTest<bool>("ShipManager", "Ping an empty cell", false, board2.Ping(0, 0)));

                // Try to save one more time
                if (File.Exists(ShipDataPath + "test_ships.txt"))
                {
                    File.Delete(ShipDataPath + "test_ships.txt");
                }
                board.SaveShips("test_ships.txt");
            }
            catch (Exception ex)
            {
                results.Add(new UnitTest<bool>("ShipManager", "File IO error: " + ex.Message, true, false));
            }

            return results;
        }

    }
}
