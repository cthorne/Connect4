using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class MoveHandler
    {
        // Handle move
        public static void handlePlayerMove(Player player, Board board)
        {
            int currentPlayersMove = InputHandler.getPlayerMove(player, board);
            if (!validatePlayerMove(board, currentPlayersMove))
            {
                Console.WriteLine("Please enter a valid move.");
                handlePlayerMove(player, board);
                return;
            }
        }

        // Verify valid move
        private static bool validatePlayerMove(Board board, int playerMove)
        {
            int countersInColumn;
            board.columnCounters.TryGetValue(playerMove, out countersInColumn);
            if (countersInColumn == null)
                { return false; }
            if (countersInColumn == board.numberRows)
                { return false; }
            if (!string.IsNullOrEmpty(board.gameBoard[playerMove, countersInColumn]))
                { return false; }
            return true;
        }

        // Determine win status
    }
}
