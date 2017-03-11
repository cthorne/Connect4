using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Connect4
{
    class Program
    {
        private const int numberPlayers = 2;
        private static readonly string[] playerIcons = {"o", "x"};
        static void Main(string[] args)
        {
            int rows, cols;
            InputHandler.getBoardDimensions(out rows, out cols);
            Board gameBoard = new Board(rows, cols);
            List<Player> players = new List<Player>();
            for (int i = 0; i < numberPlayers; i++)
            {
                Player player = new Player("Player " + (i+1));
                player.playerIcon = playerIcons[i];
                players.Add(player);
            }
            GameHandler.playGame(gameBoard, players);
        }
    }
}
