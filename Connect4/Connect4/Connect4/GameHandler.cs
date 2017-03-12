using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public static class GameHandler
    {
        public const string GAME_WON = "{0} has won the game.";
        const string GAME_DRAWN = "Game has ended in a draw.";
        /// <summary>
        /// Command to play the game until out of moves or game won.
        /// </summary>
        /// <param name="board">Object of play board.</param>
        /// <param name="players">List of players in the game.</param>
        public static void playGame(Board board, List<Player> players)
        {
            // Play game until won or out of moves
            while (board.boardMovesMade < board.maxBoardMoves && !board.gameWon)
            {
                actionPlayerTurn(board, players);
            }
            if (!board.gameWon)
            {
                printDrawMessage(board);
            }
            Console.WriteLine("Press any key to exit the game.");
            Console.ReadKey();
        }

        /// <summary>
        /// Complete a turn for each player, or until game won.
        /// </summary>
        /// <param name="board">Object of play board.</param>
        /// <param name="players">List of players in the game.</param>
        private static void actionPlayerTurn(Board board, List<Player> players)
        {
            foreach (Player player in players)
            {
                if (!board.gameWon)
                {
                    player.printPlayerTurn();
                    MoveHandler.handlePlayerMove(player, board);
                }
            }
        }

        /// <summary>
        /// Prints the draw message.
        /// </summary>
        /// <param name="board">Object of play board.</param>
        private static void printDrawMessage(Board board)
        {
            Console.WriteLine(GAME_DRAWN);            
        }
    }
}
