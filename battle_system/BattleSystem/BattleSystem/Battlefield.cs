using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    internal class Battlefield
    {
        bool BattleComplete = false;
        public Battlefield()
        {
            player1 = new Player(null);
            player2 = new Player(null);
            
            player1.SetEnemy(player2);
            player2.SetEnemy(player1);
        }

        int turnCount = 0;

        public Player player1;
        public Player player2;

        public async Task StartBattle()
        {
            while (!BattleComplete)
            {
                await runNextTurn();
            }
        }

        public async Task runNextTurn()
        {
            Player playing = getCurrentActivePlayer();
            turnCount++;
            await playing.StartTurn();
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
