using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public static class GameHandler
    {
        const string GAME_WON = "Player {0} has won the game.";
        const string GAME_DRAWN = "Game has ended in a draw.";
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
            printDrawMessage(board);
            Console.ReadKey();
        }

        private static void printDrawMessage(Board board)
        {
            Console.WriteLine(GAME_DRAWN);            
        }
    }
}
