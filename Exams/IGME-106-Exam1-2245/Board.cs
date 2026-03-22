using System.Runtime.CompilerServices;

namespace IGME_106_Exam1_2245
{
    /// <summary>
    /// Manages a 2d grid of character values to represent a game board.
    /// </summary>
    public class Board
    {
        // Default board size & empty cell character
        protected const int BoardSize = 10;
        protected const char Empty = '-';

        // The grid itself
        protected char[,] grid;

        /// <summary>
        /// Auto-property for the actual size of this board
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Indexer to get/set values in the grid
        /// </summary>
        public char this[int row, int col]
        {
            get { return grid[row, col]; }
            set { grid[row, col] = value; }
        }

        /// <summary>
        /// Constructor to create a new board of a given size
        /// filled with empty cells.
        /// </summary>
        public Board(int boardSize = BoardSize)
        {
            grid = new char[boardSize, boardSize];
            this.Size = boardSize;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    grid[i, j] = Empty;
                }
            }
        }

        /// <summary>
        /// Display the current state of the board to the console with
        /// row and column numbers for reference. Non-empty cells are
        /// shown in blue, empty cells in gray.
        /// </summary>
        public void DisplayBoard()
        {
            Console.Write("\t ");
            for (int c = 0; c < Size; c++)
            {
                Console.Write(c);
            }
            Console.WriteLine();
            for (int r = 0; r < Size; r++)
            {
                Console.Write("\t" + r);
                for (int c = 0; c < Size; c++)
                {
                    if (grid[r, c] == '-')
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(grid[r, c]);
                    Console.ResetColor();
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
        }

        /// <summary>
        /// Create a small sample board and run some basic tests on it.
        /// </summary>
        /// <returns></returns>
        public static List<IUnitTest> RunTests()
        {
            List<IUnitTest> results = new List<IUnitTest>();

            // Test by creating a small 5x5 board, checking its size and
            // getting/setting values from some row/col combos

            Board b = new Board(5);
            results.Add(new UnitTest<int>("Board", "Size", 5, b.Size));
            results.Add(new UnitTest<char>("Board", "Empty cell - top left", Empty, b[0, 0]));
            results.Add(new UnitTest<char>("Board", "Empty cell - bottom right", Empty, b[4, 4]));
            results.Add(new UnitTest<char>("Board", "Empty cell - middle", Empty, b[2, 1]));

            // Set some values and check them
            b[0, 0] = 'X';
            b[4, 4] = 'O';
            b[2, 1] = 'Z';
            results.Add(new UnitTest<char>("Board", "Set cell - top left", 'X', b[0, 0]));
            results.Add(new UnitTest<char>("Board", "Set cell - bottom right", 'O', b[4, 4]));
            results.Add(new UnitTest<char>("Board", "Set cell - middle", 'Z', b[2, 1]));

            // Test the percentage occupied
            // TODO- DONE: Add a PercentOccupied method to the Board class that returns the percentage
            // of the board that isn't empty. Once done, uncomment the following line to test it and
            // comment out the placeholder line below that's registering a failed test.

            results.Add(new UnitTest<bool>("Board", "Percent occupied unimplemented", true, true));

            results.Add(new UnitTest<double>("Board", "Percent occupied", 0.12, b.PercentOccupied(b.Size*b.Size)));

            return results;
        }


        internal double PercentOccupied(double size)
        {
            
            double totalSpaces = size;
            double emptySpaces = 0.0;
            double occupiedSpaces = 0.0;

            for (int r = 0; r < Size; r++)
            {

                for (int c = 0; c < Size; c++)
                {
                    if (grid[r, c] == '-')
                    {
                        emptySpaces++;
                    }
                    else
                    {
                        occupiedSpaces++;
                    }

                }

            }
        

            return occupiedSpaces / totalSpaces;


        }


    }
}