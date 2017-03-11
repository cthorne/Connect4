using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Player
    {
        public Player(string name)
        {
            playerName = name;
            playerTurn = 1;
            playerMovesMade = 0;
        }
        // Player name
        public string playerName { get; set; }
        public string playerIcon { get; set; }
        // Current turn
        public int playerTurn { get; set; }
        // Number of moves made
        public int playerMovesMade { get; set; }

        public void printPlayerTurn()
        {
            Console.WriteLine("{0} turn:\n {1}", playerName, playerTurn);
        }
    }
}
