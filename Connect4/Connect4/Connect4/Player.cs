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

        /// <summary>
        /// Icon used to reprsent player on board.
        /// </summary>
        public string playerIcon { get; set; }

        /// <summary>
        /// Current turn for this player.
        /// </summary>
        public int playerTurn { get; set; }

        /// <summary>
        /// Number of moves made by player.
        /// </summary>
        public int playerMovesMade { get; set; }

        /// <summary>
        /// Print current players turn.
        /// </summary>
        public void printPlayerTurn()
        {
            Console.WriteLine("{0} turn:\n {1}", playerName, playerTurn);
        }
    }
}
