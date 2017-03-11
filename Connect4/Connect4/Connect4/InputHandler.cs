using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public static class InputHandler
    {
        const int NUMBER_OF_INPUTS = 2;
        // Get initial input from user
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
            bool rowsValid = Int32.TryParse(splitInputs[0], out rows);
            bool colsValid = Int32.TryParse(splitInputs[1], out cols);
            if (!rowsValid || !colsValid)
            {
                Console.WriteLine("Please enter only whole numbers for the rows and columns.");
                getBoardDimensions(out rows, out cols);
                return;
            }            
        }
        // Verify initial input for board initialisation
        private static bool verifyBoardDimensionsInput(string[] inputs)
        {
            return inputs.Count() == NUMBER_OF_INPUTS;
        }

        // Get column to insert players move into
        public static int getPlayerMove(Player player, Board board)
        {
            int columnToInsertInto;
            Console.WriteLine("Please enter a column number between 0 and "
                + board.numberCols + " to insert into.");

            if (!Int32.TryParse(Console.ReadLine(), out columnToInsertInto) 
                || (columnToInsertInto < 0 || columnToInsertInto > board.numberCols))
            {
                Console.WriteLine("Please only enter whole numbers between 0 and " + board.numberCols);
                return getPlayerMove(player, board);
            }
            return columnToInsertInto - 1;
        }


    }
}
