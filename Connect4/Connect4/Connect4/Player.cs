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
            movesMade = new List<Tuple<int,int>>();
            playerMovesMade = 0;
        }
        // Player name
        public string playerName { get; set; }
        // Player moves?
        public List<Tuple<int, int>> movesMade { get; set; }
        // Turn
        public int playerTurn { get; set; }

        public int playerMovesMade { get; set; }

        public void printPlayerTurn()
        {
            Console.WriteLine("{0} turn:\n {1}", playerName, playerTurn);
        }
    }
}
