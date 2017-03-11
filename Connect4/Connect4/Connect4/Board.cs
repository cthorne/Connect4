using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Board
    {
        // Board rows / cols
        public int numberRows { get; set; }
        public int numberCols { get; set; }

        public string[,] gameBoard { get; set; }

        public Dictionary<int, int> columnCounters { get; set; }

        public int boardMovesMade { get; set; }

        public int maxBoardMoves {
            get {
                return numberRows * numberCols;
            }
        }


        public bool gameWon { get; set; }

        public Board(int rows, int cols)
        {
            numberRows = rows;
            numberCols = cols;
            gameBoard = new string[rows, cols];
            gameWon = false;
            columnCounters = new Dictionary<int, int>();
            for (int i = 0; i < numberCols; i++)
            {
                columnCounters[i] = 0;
            }
        }
        

        // Action input

        // Check win cons after move

        // Hold current board state, build from players?
    }
}
