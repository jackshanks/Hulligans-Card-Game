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

        public void shuffle()
        {

        }
        public Card draw()
        {
            return cards[0];
        }
        public void search()
        {

        }
        public void view()
        {

        }
    }
    internal class Graveyard
    {
        public List<Card> cards { get; set; }
    }
    internal class Exiled
    {
        public List<Card> cards { get; set; }
    }
    internal class Bench
    {
        public List<Card> benched;
    }
    internal class Arena
    {
        public List<Card> combatants;
    }
    internal class Field
    {
        public Card fieldEffect;
    }
}
