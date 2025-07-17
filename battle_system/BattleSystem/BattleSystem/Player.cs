using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    internal class Player
    {
        public event EventHandler TurnStart;
        public event EventHandler TurnEnd;

        public Deck Deck;
        public Graveyard Graveyard;
        public Exiled Exiled;

        public Bench Bench;
        public Arena Arena;

        public List<Card> CardsActiveEffects;

        public void StartTurn()
        {
            TurnStart.Invoke(this,null);
        }

        public void EndTurn() 
        {
            TurnEnd.Invoke(this, null);
        }

        public void EnemyTurnStart(object sender, EventArgs? e)
        {

        }
        public void EnemyTurnEnd(object sender, EventArgs? e)
        {

        }

    }
}
