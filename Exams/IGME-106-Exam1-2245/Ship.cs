namespace IGME_106_Exam1_2245
{
    /// <summary>
    /// enum to represent the result of a move.
    /// </summary>
    // ANALYZE: MoveResult enum
    // What is the benefit of assigning the individual values in the MoveResult
    // enum to be the result of characters cast to ints?
    public enum MoveResult
    {
        Hit = (int)'x',
        Miss = (int)'o',
        Sink = (int)'!'
    }

    /// <summary>
    /// Represents a ship on a player's board. Each ship has a single character ID, 
    /// a size (number of cells), a horizontal orientation, and a location on the board (row, col).
    /// </summary>
    public class Ship
    {
        // Auto-implemented properties with public gets and private sets
        // for most of the ship info
        public char ID { get; private set; }
        public int Size { get; private set; }
        public bool IsHorizontal { get; private set; }
        public int Row { get; private set; }
        public int Col { get; private set; }

        // An array of bools to track which cells of the ship have been hit
        private bool[] hitCells;

        /// <summary>
        /// Returns true if all cells of the ship have been hit.
        /// </summary>
        public bool IsSunk
        {
            get
            {
                int tracker = 0;
                for(int i = 0; i < hitCells.Length; i++) 
                {
                    if (hitCells[i] == true)
                    {
                        tracker++;
                    }
                }

                if(tracker == hitCells.Length)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                // TODO- DONE: Implement this property so that it returns true if all cells of the ship have been hit.
                // ANALYZE: What is the Big O for your implementation of the IsSunk property?  How did you determine this?


                
            }
        }

        /// <summary>
        /// Create a new Ship object with the given ID, size, orientation, and location.
        /// </summary>
        public Ship(char id, int size, bool isHorizontal, int row, int col)
        {
            ID = id;
            Size = size;
            IsHorizontal = isHorizontal;
            Row = row;
            Col = col;
            hitCells = new bool[size];
        }

        /// <summary>
        /// If the given location hits a cell of this ship, mark it as hit and return the result.
        /// </summary>
        public MoveResult TakeHit(int row, int col)
        {
            int offset = 0;
            MoveResult result = MoveResult.Miss;

            if (IsHorizontal && row == Row)
            {
                offset = col - Col;
            }
            else if (!IsHorizontal && col == Col)
            {
                offset = row - Row;
            }

            if (offset >= 0 && offset < Size)
            {
                hitCells[offset] = true;
                if(IsSunk)
                {
                    result = MoveResult.Sink;
                }
                else
                {
                    result = MoveResult.Hit;
                }
            }

            return result;
        }

        /// <summary>
        /// Print the ship's info in a CSV format.
        /// </summary>
        public override string ToString()
        {
            return $"{ID},{Size},{IsHorizontal},{Row},{Col}";
        }

        /// <summary>
        /// Equals is another method, like ToString, that is inherited from the Object class.
        /// By default, it checks if 2 objects have the same reference in memory.
        /// It's overridden here to check if 2 Ship objects have the same values for their
        /// id, size, orientation, row, and col.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Ship ship &&
                   ID == ship.ID &&
                   Size == ship.Size &&
                   IsHorizontal == ship.IsHorizontal &&
                   Row == ship.Row &&
                   Col == ship.Col;
        }

        /// <summary>
        /// Run tests for the Ship class.
        /// </summary>
        public static List<IUnitTest> RunTests()
        {
            List<IUnitTest> results = new List<IUnitTest>();

            // Test 1: Ship equality
            Ship s1 = new Ship('A', 3, true, 0, 0);
            Ship s2 = new Ship('A', 3, true, 0, 0);
            Ship s3 = new Ship('B', 5, true, 0, 0);
            Ship s4 = new Ship('A', 4, true, 0, 0);
            Ship s5 = new Ship('A', 3, false, 0, 0);
            Ship s6 = new Ship('A', 3, true, 1, 0);
            Ship s7 = new Ship('A', 3, true, 0, 1);
            results.Add(new UnitTest<bool>("Ship", "Equals",true, s1.Equals(s2)));
            results.Add(new UnitTest<bool>("Ship", "Not equals (ID)", false, s1.Equals(s3)));
            results.Add(new UnitTest<bool>("Ship", "Not equals (Size)", false, s1.Equals(s4)));
            results.Add(new UnitTest<bool>("Ship", "Not equals (IsHorizontal)", false, s1.Equals(s5)));
            results.Add(new UnitTest<bool>("Ship", "Not equals (Row)", false, s1.Equals(s6)));
            results.Add(new UnitTest<bool>("Ship", "Not equals (Col)", false, s1.Equals(s7)));

            // Make sure hitting a ship works
            results.Add(new UnitTest<MoveResult>("Ship", "Hit horz ship, 0", MoveResult.Hit, s1.TakeHit(0, 0)));
            results.Add(new UnitTest<MoveResult>("Ship", "Hit horz ship, 1", MoveResult.Hit, s1.TakeHit(0, 1)));
            results.Add(new UnitTest<MoveResult>("Ship", "Sink horz ship, 2", MoveResult.Sink, s1.TakeHit(0, 2)));
            results.Add(new UnitTest<bool>("Ship", "Sunk", true, s1.IsSunk));
            results.Add(new UnitTest<MoveResult>("Ship", "Hit vert ship, 0", MoveResult.Hit, s5.TakeHit(0, 0)));
            results.Add(new UnitTest<MoveResult>("Ship", "Hit vert ship, 1", MoveResult.Hit, s5.TakeHit(1, 0)));
            results.Add(new UnitTest<MoveResult>("Ship", "Sink vert ship, 2", MoveResult.Sink, s5.TakeHit(2, 0)));
            results.Add(new UnitTest<bool>("Ship", "Sunk", true, s5.IsSunk));

            // Make sure missing a ship works
            results.Add(new UnitTest<MoveResult>("Ship", "Miss horz ship", MoveResult.Miss, s1.TakeHit(0, 3)));

            // Make sure hitting a ship multiple times registers as a hit the first time but more doesn't
            // result in a sink
            results.Add(new UnitTest<MoveResult>("Ship", "Hit 1", MoveResult.Hit, s2.TakeHit(0, 0)));
            results.Add(new UnitTest<MoveResult>("Ship", "Hit 2", MoveResult.Hit, s2.TakeHit(0, 0)));
            results.Add(new UnitTest<MoveResult>("Ship", "Hit 3", MoveResult.Hit, s2.TakeHit(0, 0)));
            results.Add(new UnitTest<MoveResult>("Ship", "Hit 4", MoveResult.Hit, s2.TakeHit(0, 0)));
            results.Add(new UnitTest<bool>("Ship", "Sunk", false, s2.IsSunk));

            return results;
        }

    }
}
