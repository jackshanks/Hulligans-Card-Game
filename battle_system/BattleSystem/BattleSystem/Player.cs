using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    internal class Player
    {
        public int MaxPlayerHp = 5;
        public int PlayerHp = 5;


        public event EventHandler TurnStart;
        public event EventHandler TurnEnd;
        public event EventHandler Drawn;
        public event EventHandler Placed;
        public event EventHandler ItemActivated;
        public event EventHandler AttackMade;

        public Deck Deck;
        public Graveyard Graveyard;
        public Exiled Exiled;
        public Hand Hand;

        public Bench Bench;
        public Arena Arena;

        public List<Card> CardsActiveEffects;

        public IPlayerController PlayerController;

        public Player(IPlayerController controller)
        {
            PlayerController = controller;
        }

        public void SetEnemy(Player p)
        {
            p.TurnEnd += EnemyTurnEnd;
            p.TurnStart += EnemyTurnStart;
            p.AttackMade += EnemyAttackMade;
            p.ItemActivated += EnemyItemActivated;
            p.Drawn += EnemyDraw;
            p.Placed += EnemyPlace;
        }
        public void InvokeEffect(Card c, Effect e)
        {
        }

        public async void CheckInvoke(Condition condition)
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

        public async Task StartTurn()
        {
            TurnStart.Invoke(this,null);
            await DrawPhase();
        }

        public async Task DrawPhase()
        {
            Deck.Draw(Hand);
            Drawn.Invoke(this,null);
            await PlacePhase();
        }
        public async Task PlacePhase()
        {
            Card toplace = await PlayerController.GetCardToPlace();
            int AreaToPlace = await PlayerController.GetAreaToPlace();
            Placed.Invoke(this,null);
            await ItemActivationPhase();
        }
        public async Task ItemActivationPhase()
        {
            await AttackPhase();
        }
        public async Task AttackPhase()
        {
            await EndTurn();
        }

        public async Task EndTurn() 
        {
        }

        public void EnemyTurnStart(object sender, EventArgs? e)
        {
            CheckInvoke(Condition.onEnemyTurnStart);
        }
        public void EnemyTurnEnd(object sender, EventArgs? e)
        {
            CheckInvoke(Condition.onEnemyTurnEnd);
        }

        public void EnemyDraw(object sender, EventArgs? e)
        {

        }
        public void EnemyPlace(object sender, EventArgs? e)
        {

        }
        public void EnemyItemActivated(object sender, EventArgs? e)
        {

        }
        public void EnemyAttackMade(object sender, EventArgs? e)
        {

        }
    }
}
