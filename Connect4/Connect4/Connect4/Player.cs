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
        }
        // Player name
        public string playerName { get; set; }
        // Player moves?

        // Turn
        public int playerTurn { get; set; }
    }
}
