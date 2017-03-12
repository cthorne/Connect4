using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Board
    {
        // Placeholder for each board position, before counter placed.
        public const string POSITION_PLACEHOLDER = "*";
        private const string POSITION_SEPARATOR = " ";

        // Number of board rows / cols
        public int numberRows { get; set; }
        public int numberCols { get; set; }

        /// <summary>
        /// 2d array of game board [Rows, Cols]
        /// </summary>
        public string[,] gameBoard { get; set; }

        /// <summary>
        /// Number of counters in a column - key is column, value count
        /// </summary>
        public Dictionary<int, int> columnCounters { get; set; }

        /// <summary>
        /// Number of moves made in the game.
        /// </summary>
        public int boardMovesMade { get; set; }

        /// <summary>
        /// Maximum number of moves possible for the board.
        /// </summary>
        public int maxBoardMoves {
            get {
                return numberRows * numberCols;
            }
        }

        // Status of game completion
        public bool gameWon { get; set; }

        /// <summary>
        /// Setup board.
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of columns</param>
        public Board(int rows, int cols)
        {
            numberRows = rows;
            numberCols = cols;
            gameBoard = new string[rows, cols];
            // Initialise board values to placeholder (empty)
            for (int c = 0; c < numberCols; c++)
            {
                for (int r = 0; r < numberRows; r++)
                {
                    gameBoard[r, c] = POSITION_PLACEHOLDER;
                }
            }
            gameWon = false;
            columnCounters = new Dictionary<int, int>();
            for (int i = 0; i < numberCols; i++)
            {
                columnCounters[i] = 0;
            }
        }
        /// <summary>
        /// Output visual representation of board to console.
        /// </summary>
        public void printBoard()
        {
            Console.WriteLine();
            StringBuilder outputString = new StringBuilder(); // Stringbuilder faster for large boards
            for (int r = numberRows - 1; r >= 0; r--)
            {
                for (int c = 0; c < numberCols; c++)
                {
                    outputString.Append(gameBoard[r, c] + POSITION_SEPARATOR);
                }
                outputString.AppendLine();
            }
            Console.WriteLine(outputString);
        }
    }
}
