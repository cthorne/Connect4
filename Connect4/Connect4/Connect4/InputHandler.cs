using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public static class InputHandler
    {
        const int NUMBER_OF_INPUTS_FOR_BOARD = 2;
        /// <summary>
        /// Get initial input from user for board dimensions, used to setup game board.
        /// </summary>
        /// <param name="rows">Output number of rows entered.</param>
        /// <param name="cols">Output number of columns entered.</param>
        public static void getBoardDimensions(out int rows, out int cols)
        {
            Console.WriteLine("Please enter the board dimensions (number of rows, number of columns)");
            string input = Console.ReadLine();
            string[] splitInputs = input.Split(null);

            if (!verifyBoardDimensionsInput(splitInputs))
            {
                Console.WriteLine("Please enter values in the format 'rows cols', with a space between.");
                getBoardDimensions(out rows, out cols);
                return;
            }
            if (!verifyBoardInputNumbers(splitInputs, out rows, out cols))
            {
                getBoardDimensions(out rows, out cols);
            }
            
        }

        /// <summary>
        /// Get the column to insert into for player.
        /// </summary>
        /// <param name="player">Current player to get move for.</param>
        /// <param name="board">Current play board.</param>
        /// <returns>Int number representing column to insert into.</returns>
        public static int getPlayerMove(Player player, Board board)
        {
            int columnToInsertInto;
            Console.WriteLine("Please enter a column number between 1 and "
                + board.numberCols + " to insert into.");

            if (!Int32.TryParse(Console.ReadLine(), out columnToInsertInto)
                || (columnToInsertInto <= 0 
                || columnToInsertInto > board.numberCols))
            {
                Console.WriteLine("Please only enter whole numbers between 1 and " + board.numberCols);
                return getPlayerMove(player, board);
            }
            return columnToInsertInto - 1;
        }

        /// <summary>
        /// Verify number of inputs from user is correct.
        /// </summary>
        /// <param name="inputs">Array of strings to check</param>
        /// <returns>Boolean showing if number of inputs correct.</returns>
        private static bool verifyBoardDimensionsInput(string[] inputs)
        {
            return inputs.Count() == NUMBER_OF_INPUTS_FOR_BOARD;
        }

        /// <summary>
        /// Verify number inputs are in correct format
        /// </summary>
        /// <param name="inputs">Array string of inputs.</param>
        /// <param name="rows">Output number of rows.</param>
        /// <param name="cols">Output number of columns.</param>
        /// <returns>Boolean determining if input was acceptable.</returns>
        private static bool verifyBoardInputNumbers(string[] inputs, out int rows, out int cols)
        {
            bool rowsValid = Int32.TryParse(inputs[0], out rows);
            bool colsValid = Int32.TryParse(inputs[1], out cols);
            // Ensure integers were entered
            if (!rowsValid || !colsValid)
            {
                Console.WriteLine("Please enter only whole numbers for the rows and columns.");
                return false;
            }
            // Ensure board space is winnable
            if (rows < 4 || cols < 4)
            {
                Console.WriteLine("Please enter at least a 4 by 4 grid - lesser grids are unwinnable.");
                return false;
            }
            return true;
        }
    }
}
