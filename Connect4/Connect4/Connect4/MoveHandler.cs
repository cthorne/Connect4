using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class MoveHandler
    {
        const int NUMBER_CONNECTS_TO_WIN = 4; // Constant to determine number in row to win.
        /// <summary>
        /// Determines if number of max moves have been made.
        /// </summary>
        /// <param name="board">Object of play board.</param>
        /// <returns>Boolean showing if more moves are possible.</returns>
        private static bool movesPossible(Board board)
        {
            return board.boardMovesMade < board.maxBoardMoves;
        }

        /// <summary>
        /// Handle the players movement request.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
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


        /// <summary>
        /// Verify the requested player move is valid.
        /// </summary>
        /// <param name="board">Object of play board.</param>
        /// <param name="playerMove">Column to insert into.</param>
        /// <returns>Boolean showing if move is valid.</returns>
        private static bool validatePlayerMove(Board board, int playerMove)
        {
            int countersInColumn;
            board.columnCounters.TryGetValue(playerMove, out countersInColumn);
            if (countersInColumn == board.numberRows) // Column full
            {
                Console.WriteLine("Column {0} is full, please select another.", playerMove + 1);
                return false;
            }
            if (board.gameBoard[countersInColumn, playerMove] != Board.POSITION_PLACEHOLDER) // Check if already has counter
            { 
                return false; 
            }
            return true;
        }
        /// <summary>
        /// Actions move after validating it is acceptable.
        /// </summary>
        /// <param name="board">Object of play board.</param>
        /// <param name="player">Current player.</param>
        /// <param name="playerMove">Column to insert into.</param>
        private static void enactMove(Board board, Player player, int playerMove)
        {
            int countersInColumn = board.columnCounters[playerMove];
            board.gameBoard[countersInColumn, playerMove] = player.playerIcon;
            board.columnCounters[playerMove]++;
            board.boardMovesMade++;

            player.playerMovesMade++;
            if (player.playerMovesMade >= NUMBER_CONNECTS_TO_WIN) // Minimum number of moves to win
            {
                if (checkHorizontalWinStatus(playerMove, countersInColumn, player, board)
                    || checkVerticalWinStatus(playerMove, countersInColumn, player, board) || checkDiagonalWinStatus(player, board, countersInColumn, playerMove))
                {
                    board.gameWon = true;
                    Console.WriteLine(GameHandler.GAME_WON, player.playerName);
                }
            }
            player.playerTurn++;
            board.printBoard();
        }
        #region Horizontal checks
        /// <summary>
        /// Check for number of counters to left of insertion.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
        /// <param name="countersInColumn">Current number of counters in column.</param>
        /// <param name="playerMove">Column inserted into.</param>
        /// <returns>Number of counters to left of insertion.</returns>
        private static int checkCountersLeft(Player player, Board board, int countersInColumn, int playerMove)
        {
            int count = 0;
            for (int i = 1; i < NUMBER_CONNECTS_TO_WIN; i++)
            {
                if (playerMove - i >= 0
                    && board.gameBoard[countersInColumn, playerMove - i] == player.playerIcon)
                {
                    count++;
                }
                else
                {
                    return count; // Exit early if row of players counters interrupted
                }
            }
            return count;
        }
        /// <summary>
        /// Check for number of counters to right of insertion.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
        /// <param name="countersInColumn">Current number of counters in column.</param>
        /// <param name="playerMove">Column inserted into.</param>
        /// <returns>Number of counters to right of insertion.</returns>
        private static int checkCountersRight(Player player, Board board, int countersInColumn, int playerMove)
        {
            int count = 0;
            for (int i = 1; i < NUMBER_CONNECTS_TO_WIN; i++)
            {
                if (playerMove + i < board.numberCols
                    && board.gameBoard[countersInColumn, playerMove + i] == player.playerIcon)
                {
                    count++;
                }
                else
                {
                    return count; // Exit early if row of players counters interrupted
                }
            }
            return count;
        }

        /// <summary>
        /// Determine if player has won horizontally.
        /// </summary>
        /// <param name="playerMove">Column inserted into.</param>
        /// <param name="countersInColumn">Number of counters in column.</param>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
        /// <returns>Boolean representing if player has won horizontally.</returns>
        private static bool checkHorizontalWinStatus(int playerMove, int countersInColumn,
            Player player, Board board)
        {
            int countersLeft = checkCountersLeft(player, board, countersInColumn, playerMove);
            int countersRight = checkCountersRight(player, board, countersInColumn, playerMove);
            return countersLeft + countersRight >= 3;
        }
        #endregion
        #region Vertical checks
        /// <summary>
        /// Determine if player has won vertically.
        /// </summary>
        /// <param name="playerMove">Column inserted into.</param>
        /// <param name="countersInColumn">Number of counters in column.</param>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
        /// <returns>Boolean representing if player has won vertically.</returns>
        private static bool checkVerticalWinStatus(int playerMove, int countersInColumn,
            Player player, Board board)
        {
            int countersBelow = 0;

            for (int i = 1; i < NUMBER_CONNECTS_TO_WIN; i++)
            {
                // Only check below as cannot be any counters above
                if (countersInColumn - i >= 0 
                    && board.gameBoard[countersInColumn - i, playerMove] == player.playerIcon)
                {
                    countersBelow++;
                }
                else
                {
                    return countersBelow >= 3; // Exit early if row of players counters interrupted
                }
            } 
            return countersBelow >= 3;
        }
        #endregion
        #region Diagonal checks
        /// <summary>
        /// Check left-upper diagonal for counters.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
        /// <param name="countersInColumn">Counters in column inserted into.</param>
        /// <param name="playerMove">Column inserted into.</param>
        /// <returns>Number of counters in left-upper diagonal direction.</returns>
        private static int checkLeftUpperDiagonal(Player player, Board board, int countersInColumn, int playerMove)
        {
            int count = 0;

            for (int i = 1; i < NUMBER_CONNECTS_TO_WIN; i++)
            {
                if (countersInColumn + i < board.numberRows && playerMove - i >= 0
                    && board.gameBoard[countersInColumn + i, playerMove - i] == player.playerIcon)
                {

                    count++;
                }
                else
                {
                    return count;
                }
            }

            return count;
        }

        /// <summary>
        /// Check left-lower diagonal for counters.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
        /// <param name="countersInColumn">Counters in column inserted into.</param>
        /// <param name="playerMove">Column inserted into.</param>
        /// <returns>Number of counters in left-lower diagonal direction.</returns>
        private static int checkLeftLowerDiagonal(Player player, Board board, int countersInColumn, int playerMove)
        {
            int count = 0;

            for (int i = 1; i < NUMBER_CONNECTS_TO_WIN; i++)
            {
                if (countersInColumn - i >= 0 && playerMove - i >= 0
                    && board.gameBoard[countersInColumn - i, playerMove - i] == player.playerIcon)
                {

                    count++;
                }
                else
                {
                    return count;
                }
            }

            return count;
        }

        /// <summary>
        /// Check right-lower diagonal for counters.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
        /// <param name="countersInColumn">Counters in column inserted into.</param>
        /// <param name="playerMove">Column inserted into.</param>
        /// <returns>Number of counters in right-lower diagonal direction.</returns>
        private static int checkRightLowerDiagonal(Player player, Board board, int countersInColumn, int playerMove)
        {
            int count = 0;

            for (int i = 1; i < NUMBER_CONNECTS_TO_WIN; i++)
            {
                if (countersInColumn - i >= 0 && playerMove + i < board.numberCols
                    && board.gameBoard[countersInColumn - i, playerMove + i] == player.playerIcon)
                {

                    count++;
                }
                else
                {
                    return count;
                }
            }

            return count;
        }

        /// <summary>
        /// Check right-upper diagonal for counters.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of play board.</param>
        /// <param name="countersInColumn">Counters in column inserted into.</param>
        /// <param name="playerMove">Column inserted into.</param>
        /// <returns>Number of counters in right-upper diagonal direction.</returns>
        private static int checkRightUpperDiagonal(Player player, Board board, int countersInColumn, int playerMove)
        {
            int count = 0;

            for (int i = 1; i < NUMBER_CONNECTS_TO_WIN; i++)
            {
                if (countersInColumn + i < board.numberRows && playerMove + i < board.numberCols
                    && board.gameBoard[countersInColumn + i, playerMove + i] == player.playerIcon)
                {

                    count++;
                }
                else
                {
                    return count;
                }
            }

            return count;
        }
        /// <summary>
        /// Determine if player has won via diagonal directions.
        /// </summary>
        /// <param name="player">Current player.</param>
        /// <param name="board">Object of player board.</param>
        /// <param name="countersInColumn">Counters in column inserted into.</param>
        /// <param name="playerMove">Column inserted into.</param>
        /// <returns>Boolean representing if player has won diagonally.</returns>
        private static bool checkDiagonalWinStatus(Player player, Board board, int countersInColumn, int playerMove)
        {
            int leftUpperDiagonal = checkLeftUpperDiagonal(player, board, countersInColumn, playerMove); 
            int leftLowerDiagonal = checkLeftLowerDiagonal(player, board, countersInColumn, playerMove);

            int rightUpperDiagonal = checkRightUpperDiagonal(player, board, countersInColumn, playerMove);
            int rightLowerDiagonal = checkRightLowerDiagonal(player, board, countersInColumn, playerMove);

            // Checks two diagonal directions to see if combination of NUMBER_CONNECTS_TO_WIN
            return (leftUpperDiagonal + rightLowerDiagonal >= 3) || (rightUpperDiagonal + leftLowerDiagonal >= 3);
        }
        #endregion
    }
}
