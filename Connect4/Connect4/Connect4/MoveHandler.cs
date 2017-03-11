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
            int countersInColumn = board.columnCounters[playerMove];
            board.gameBoard[countersInColumn, playerMove] = player.playerName;
            board.columnCounters[playerMove]++;
            board.boardMovesMade++;

            player.playerMovesMade++;
            if (player.playerMovesMade >= 4)
            {
                if (checkHorizontalWinStatus(playerMove, countersInColumn, player, board))
                {
                    board.gameWon = true;
                    Console.WriteLine(GameHandler.GAME_WON, player.playerName);
                }
            }
            player.playerTurn++;
        }
        // Determine win status
        private static bool checkHorizontalWinStatus(int playerMove, int countersInColumn, 
            Player player, Board board)
        {
            int countersInRow = 0;
            if (playerMove > 0)
            {
                for (int i = 0; i < board.numberCols; i++)
                {
                    if (board.gameBoard[countersInColumn, i] == player.playerName && countersInRow < 4)
                    {
                        countersInRow++;
                    }
                    else if (countersInRow >= 4)
                    {
                        break;
                    }
                    else
                    {
                        countersInRow = 0;
                    }
                }                    
            }           
            return countersInRow >= 4;
        }
    }
}
