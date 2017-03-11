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
            if (movesPossible(board))
            {
                int currentPlayersMove = InputHandler.getPlayerMove(player, board);

                if (!validatePlayerMove(board, currentPlayersMove))
                {
                    handlePlayerMove(player, board);
                    return;
                }
                enactMove(board, player, currentPlayersMove);
            }            
        }

        private static bool movesPossible(Board board)
        {
            return board.boardMovesMade < board.maxBoardMoves;
        }

        // Verify valid move
        private static bool validatePlayerMove(Board board, int playerMove)
        {
            int countersInColumn;
            board.columnCounters.TryGetValue(playerMove, out countersInColumn);            
            if (countersInColumn == board.numberRows)
            {
                Console.WriteLine("Column {0} is full, please select another.", playerMove + 1);
                return false; 
            }
            if (!string.IsNullOrEmpty(board.gameBoard[countersInColumn, playerMove]))
                { return false; }
            return true;
        }

        private static void enactMove(Board board, Player player, int playerMove)
        {
            int countersInColumn;

            board.columnCounters.TryGetValue(playerMove, out countersInColumn);
            board.gameBoard[countersInColumn, playerMove] = player.playerName;
            board.columnCounters[playerMove]++;
            board.boardMovesMade++;

            player.playerMovesMade++;
            player.playerTurn++;
        }
        // Determine win status
    }
}
