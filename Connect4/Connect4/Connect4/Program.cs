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
        static void Main(string[] args)
        {
            int rows, cols;
            InputHandler.getBoardDimensions(out rows, out cols);
            Board gameBoard = new Board(rows, cols);
            List<Player> players = new List<Player>();
            for (int i = 0; i < numberPlayers; i++)
            {
                Player player = new Player("Player " + i);
                players.Add(player);
            }
            GameHandler.playGame(gameBoard, players);

        }
    }
}
