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
                // After validation, conduct move
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
            if (board.gameBoard[countersInColumn, playerMove] != Board.positionPlaceholder)
                { return false; }
            return true;
        }

        private static void enactMove(Board board, Player player, int playerMove)
        {
            int countersInColumn = board.columnCounters[playerMove];
            board.gameBoard[countersInColumn, playerMove] = player.playerIcon;
            board.columnCounters[playerMove]++;
            board.boardMovesMade++;

            player.playerMovesMade++;
            if (player.playerMovesMade >= 4)
            {
                if (checkHorizontalWinStatus(playerMove, countersInColumn, player, board)
                    || checkVerticalWinStatus(playerMove, player, board) || checkDiagonalWinStatus(player, board))
                {
                    board.gameWon = true;
                    Console.WriteLine(GameHandler.GAME_WON, player.playerName);
                }
            }
            player.playerTurn++;
            board.printBoard();
        }
        // Determine win status
        private static bool checkHorizontalWinStatus(int playerMove, int countersInColumn, 
            Player player, Board board)
        {
            int countersInRow = 0;
            for (int i = 0; i < board.numberCols; i++)
            {
                if (board.gameBoard[countersInColumn, i] == player.playerIcon && countersInRow < 4)
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
                 
            return countersInRow >= 4;
        }

        private static bool checkVerticalWinStatus(int playerMove,
            Player player, Board board)
        {
            int countersInCol = 0;
            for (int i = 0; i < board.numberRows; i++)
            {
                if (board.gameBoard[i, playerMove] == player.playerIcon && countersInCol < 4)
                {
                    countersInCol++;
                }
                else if (countersInCol >= 4)
                {
                    break;
                }
                else
                {
                    countersInCol = 0;
                }
            }
            
            return countersInCol >= 4;
        }

        private static bool checkDiagonalWinStatus(Player player, Board board)
        {
            int countersInDiagonal = 0;
            for (int i = 0; i < board.numberRows && i < board.numberCols; i++)
            {
                if (board.gameBoard[i, i] == player.playerIcon && countersInDiagonal < 4)
                {
                    countersInDiagonal++;
                }
                else if (countersInDiagonal >= 4)
                {
                    break;
                }
                else
                {
                    countersInDiagonal = 0;
                }
            }

            return countersInDiagonal >= 4;
        }
    }
}
