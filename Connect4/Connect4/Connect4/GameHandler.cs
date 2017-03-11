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
        // Play game
        public static void playGame(Board board, List<Player> players)
        {
            // Play game until won or out of moves
            while (board.boardMovesMade < board.maxBoardMoves && !board.gameWon)
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
            if (!board.gameWon)
            {
                printDrawMessage(board);
            }
            Console.WriteLine("Press any key to exit the game.");
            Console.ReadKey();
        }

        private static void printDrawMessage(Board board)
        {
            Console.WriteLine(GAME_DRAWN);            
        }
    }
}
