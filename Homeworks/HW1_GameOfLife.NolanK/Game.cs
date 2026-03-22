using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW1_GameOfLife
{
    internal class Game
    {
        //create random instance
        Random myRand = new Random();

        //fields
        private Cell[,] myBoard;
        private Cell[,] futureBoard;

        //constructor
        internal Game() 
        {
            this.myBoard = null;
            this.futureBoard = null;
        }

        //properties
        public bool HasBoard{ get { 
                if (this.myBoard == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            } 
        }

        //methods
        internal void advance()
        {
            SmartConsole.PrintWarning("\nLoading...\n");

            //iterate through 2d array
            //loop for rows
            for (int i = 0; i < myBoard.GetLength(0); i++)
            {
                //loop for collumns
                for (int j = 0; j < myBoard.GetLength(1); j++)
                {
                    //by default set futureboard to what currentboard equals
                    futureBoard[i,j] = myBoard[i,j];

                    //Any live cell with fewer than two live neighbors dies, as if caused by under population.
                    if ((myBoard[i, j].alive == true) && (liveNeighbors(true, i, j) < 2))
                    {
                        futureBoard[i,j] = new Cell(false);
                    }

                    //Any live cell with two or three living neighbors lives on to the next generation.
                    if ((myBoard[i, j].alive == true) && ((liveNeighbors(true, i, j) == 2) || (liveNeighbors(true, i, j) == 3)))
                    {
                        futureBoard[i, j] = new Cell(true);
                    }

                    //Any live cell with more than three live neighbors dies, as if by overpopulation.
                    if ((myBoard[i, j].alive == true) && (liveNeighbors(true, i, j) > 3))
                    {
                        futureBoard[i, j] = new Cell(false);
                    }

                    //Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
                    if ((myBoard[i, j].alive == false) && (liveNeighbors(true, i, j) == 3))
                    {
                        futureBoard[i, j] = new Cell(true);
                    }
                }

                //new row

            }

            SmartConsole.PrintSuccess("Simulation Advanced");
            copyBoard();

        }

        internal void copyBoard()
        {

            //iterate through 2d array
            //loop for rows
            for (int i = 0; i < myBoard.GetLength(0); i++)
            {
                //loop for collumns
                for (int j = 0; j < myBoard.GetLength(1); j++)
                {
                    //set myboard slots equal to the futureBoard, set futureboard back to all null
                    myBoard[i, j] = futureBoard[i, j];
                    futureBoard[i, j] = null;

                }

                //new row

            }

        }

        internal void diaplayBoard()
        {
            Console.WriteLine();

            if(myBoard == null)
            {
                SmartConsole.PrintWarning("No active board currently. Generate or load a board first.");
                return;
            }

            //iterate through 2d array
            for (int i = 0; i < myBoard.GetLength(0); i++)
            {
                for (int j = 0; j < myBoard.GetLength(1); j++)
                {
                    //print each cell
                    Console.Write(myBoard[i, j]);
                }
                //indent
                Console.Write("\n");
            }

            //confirmation
            SmartConsole.PrintSuccess("\nBoard displayed");
        }

        internal void generateBoard()
        {
            //randomly generate a height and legnth
            int height = (int)myRand.NextInt64(10) + 10;
            int length = (int)myRand.NextInt64(30) + 20;

            //instantiate boards
            myBoard = new Cell[height, length];
            futureBoard = new Cell[height, length];

            Console.WriteLine();
            //iterate through 2d array
            for (int i = 0; i < myBoard.GetLength(0); i++)
            {
                for (int j = 0; j < myBoard.GetLength(1); j++)
                {
                    //calculation to see if cell will be dead or alive
                    int chance = (int)myRand.NextInt64(100);
                    bool isAlive = false;
                    if (chance <= 30)
                    {
                        //if chance < 30, the cell will be alive, otherwise it will be dead
                        isAlive = true;
                    }

                    //set each space equal to new cell object
                    myBoard[i, j] = new Cell(isAlive);
                }
            }

            //confirmation
            SmartConsole.PrintSuccess($"New {height}*{length} board generated");

        }

        //take in an input coordinate, output how many living neigbors the cell has
        internal int liveNeighbors(bool checkingAlive, int row, int collumn)
        {

            //note: might not actually be checking alive variables depending on what checking alive equals
            int aliveCounter = 0;

            //check top left
            try
            {
                if (myBoard[row - 1, collumn - 1].alive == checkingAlive)
                {
                    aliveCounter++;
                }
            }
            catch { }
            //check top middle
            try { 
                if (myBoard[row - 1, collumn].alive == checkingAlive)
                {
                    aliveCounter++;
                }
            }
            catch { }
            //check top right
            try
            {
                if (myBoard[row - 1, collumn + 1].alive == checkingAlive)
                {
                    aliveCounter++;
                }
            }
            catch { }
            //check middle left
            try
            {
                if (myBoard[row, collumn - 1].alive == checkingAlive)
                {
                    aliveCounter++;
                }
            }
            catch {}
            //check middle right
            try
            {
                if (myBoard[row, collumn + 1].alive == checkingAlive)
                {
                    aliveCounter++;
                }
            }
            catch { }
            //check bottom left
            try
            {
                if (myBoard[row + 1, collumn - 1].alive == checkingAlive)
                {
                    aliveCounter++;
                }
            }
            catch { }
            //check bottom middle
            try
            {
                if (myBoard[row + 1, collumn].alive == checkingAlive)
                {
                    aliveCounter++;
                }
            }
            catch { }
            //check bottow right
            try
            {
                if (myBoard[row + 1, collumn + 1].alive == checkingAlive)
                {
                    aliveCounter++;
                }
            }
            catch { }


            return aliveCounter;

        }

        internal void save()
        {

            string response = SmartConsole.Prompt("What would you like to name your save file?");
            response = "../../../" + response + ".txt";

            StreamWriter writer = new StreamWriter(response);

            //write board size
            writer.WriteLine(myBoard.GetLength(1)+","+myBoard.GetLength(0));

            //get cell symbols, sloppy way of doing this but it works for now
            Cell testCell = new Cell(true);
            string aliveSym = testCell.ToString();

            Cell testCell2 = new Cell(false);
            string deadSym = testCell2.ToString();

            //write dead alive symbols
            writer.WriteLine(aliveSym+","+deadSym);

            //wirte full board
            for (int i = 0; i < myBoard.GetLength(0); i++)
            {
                for (int j = 0; j < myBoard.GetLength(1); j++)
                {
                    //print each cell
                    writer.Write(myBoard[i, j]);
                }

                //indent, but check it isnt the last line so theres not an index out of bounds err
                if (i < myBoard.GetLength(0)-1)
                {
                    writer.WriteLine();
                }

            }

            writer.Close();

            SmartConsole.PrintSuccess("\nGame saved sucessfully\n");

        }

        internal void load() 
        {
            //prompt for and define file name
            string file = SmartConsole.Prompt("What is the name of the file you wish to load? (Do NOT include .txt)");
            file = "../../../" + file + ".txt";

            //check if file exists firsts
            try
            {
            

                StreamReader reader = new StreamReader(file);

                //read in width and height, split to obtain height and legnth
                string lengthHeight = reader.ReadLine();
                string[] sizeArr = lengthHeight.Split(",");
                int length = Convert.ToInt32(sizeArr[0]);
                int height = Convert.ToInt32(sizeArr[1]);

                //read in alive and dead characters
                string aliveDead = reader.ReadLine();
                string[] cellStatusArr = aliveDead.Split(",");
                char alive = Convert.ToChar(cellStatusArr[0]);
                char dead = Convert.ToChar(cellStatusArr[1]);

                //instantiate board
                myBoard = new Cell[height, length];
                futureBoard = new Cell[height, length];

                for (int i = 0; i < height; i++)
                {
                    string boardLine = reader.ReadLine();

                    for (int j = 0; j < boardLine.Length; j++)
                    {
                        //check to see if cell is dead or alive
                        bool isAlive = false;
                        if (Convert.ToChar(boardLine[j]) == alive)
                        {
                            //if the cell matches the alive symbol, the cell will be alive, otherwise it will be dead
                            isAlive = true;
                        }

                        //set each space equal to new cell object
                        myBoard[i, j] = new Cell(isAlive);

                    }

                }

                //confirmation
                SmartConsole.PrintSuccess($"\nBoard of {height}*{length} board loaded");

                reader.Close();

            }
            catch (Exception e)
            {
                SmartConsole.PrintError("\n"+e.Message);
            }




        }


    }
}
