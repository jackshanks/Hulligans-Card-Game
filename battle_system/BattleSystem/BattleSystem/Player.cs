using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    internal class Player
    {
        public event EventHandler SystemTurnTerminate;

        public event EventHandler TurnStart;
        public event EventHandler TurnEnd;

        public Deck Deck;
        public Graveyard Graveyard;
        public Exiled Exiled;

        public Bench Bench;
        public Arena Arena;

        public List<Card> CardsActiveEffects;

        public void InvokeEffect(Card c, Effect e)
        {

        }

        public void CheckInvoke(Condition condition)
        {
            foreach (Card c in CardsActiveEffects)
            {
                foreach ((Condition, Effect) con in c.conditions)
                {
                    if (con.Item1 == condition)
                    {
                        InvokeEffect(c, con.Item2);
                    }
                }
            }
        }

        public void StartTurn()
        {
            TurnStart.Invoke(this,null);
        }

        public void EndTurn() 
        {
            TurnEnd.Invoke(this, null);
            SystemTurnTerminate.Invoke(this, null);
        }

        public void EnemyTurnStart(object sender, EventArgs? e)
        {
            CheckInvoke(Condition.onEnemyTurnStart);
        }
        public void EnemyTurnEnd(object sender, EventArgs? e)
        {
            CheckInvoke(Condition.onEnemyTurnEnd);
        }

    }
}
