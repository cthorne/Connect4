using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public static class GameHandler
    {
        // Play game
        public static void playGame(Board board, List<Player> players)
        {
            while (board.boardMovesMade < board.maxBoardMoves && !board.gameWon)
            {
                foreach (Player player in players)
                {
                    player.printPlayerTurn();
                    MoveHandler.handlePlayerMove(player, board);
                }
            }
            printCompletionMessage(board);
        }

        private static void printCompletionMessage(Board board)
        {

        }
    }
}
