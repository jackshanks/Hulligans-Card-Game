using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    internal class Battlefield
    {
        public Battlefield()
        {
            player1 = new Player();
            player2 = new Player();
            
            player1.TurnStart += player2.EnemyTurnStart;
            player2.TurnStart += player1.EnemyTurnStart;
            
            player1.TurnEnd += player2.EnemyTurnEnd;
            player2.TurnEnd += player1.EnemyTurnEnd;
        }

        int turnCount = 0;

        public Player player1;
        public Player player2;

        public void runNextTurn()
        {
            Player playing = getCurrentActivePlayer();
            turnCount++;
            playing.StartTurn();
        }

        public Player getCurrentActivePlayer()
        {
            if (turnCount%2 == 0)
            {
                return player1;
            }
            return player2;
        }
    }

}
