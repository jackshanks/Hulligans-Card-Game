using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    public class Deck
    {
        public List<Card> cards { get; set; }

        public void Shuffle()
        {

        }
        public void Draw(Hand h)
        {
            Card c = cards[0];
            cards.RemoveAt(0);

            h.hand.Add(c);
            h.LastDrawnCard = c;
        }
        public void Search()
        {

        }
        public void View()
        {

        }
    }
    public class Graveyard
    {
        public List<Card> cards { get; set; }
    }
    public class Exiled
    {
        public List<Card> cards { get; set; }
    }
    public class Bench
    {
        public List<Card> benched;
    }
    public class Arena
    {
        public List<Card> combatants;
    }
    public class Field
    {
        public Card fieldEffect;
    }
    public class Hand
    {
        public List <Card> hand;

        public Card LastDrawnCard;
    }
}
