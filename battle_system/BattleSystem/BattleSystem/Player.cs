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
        public async Task InvokeEffect(Card c, Effect e)
        {
        }

        public async Task CheckInvoke(Condition condition)
        {
            foreach (Card c in CardsActiveEffects)
            {
                foreach ((Condition, Effect) con in c.conditions)
                {
                    if (con.Item1 == condition)
                    {
                        await InvokeEffect(c, con.Item2);
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
            int itemToActivate;
            while (await PlayerController.AwaitItemActivation(out itemToActivate))
            {
                // activate item corrosponding to the int provided - does not limit activations in one turn
            }
            await AttackPhase();
        }
        public async Task AttackPhase()
        {
            int cardToAttackWith;
            int cardToAttack;
            while(await PlayerController.AwaitCardAttack(out cardToAttackWith,out cardToAttack))
            {
                //do attack
            }
            await EndTurn();
        }

        public async Task EndTurn() 
        {
            //cleanup turn
        }

        public async void EnemyTurnStart(object sender, EventArgs? e)
        {
            await CheckInvoke(Condition.onEnemyTurnStart);
        }
        public async void EnemyTurnEnd(object sender, EventArgs? e)
        {
            await CheckInvoke(Condition.onEnemyTurnEnd);
        }

        public async void EnemyDraw(object sender, EventArgs? e)
        {
            
        }
        public async void EnemyPlace(object sender, EventArgs? e)
        {

        }
        public async void EnemyItemActivated(object sender, EventArgs? e)
        {

        }
        public async void EnemyAttackMade(object sender, EventArgs? e)
        {

        }
    }
}
