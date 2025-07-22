using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    internal static class CardLibrary
    {
        private static Mutex LibraryAccess = new();
        private static Dictionary<int, Card> Library = new();

        /// <summary>
        /// Gets a copy of the card from the library
        /// Thread Safe
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static Card? LookupCard(int cardId)
        {
            Card card = null;
            if (LibraryAccess.WaitOne())
            {
                card = Library[cardId].Copy();
                LibraryAccess.ReleaseMutex();
            }
            return card;
        }
        /// <summary>
        /// Adds a card into the library, should not be called by battle code
        /// </summary>
        /// <param name="card"></param>
        public static void LoadCard(Card card)
        {
            if (LibraryAccess.WaitOne())
            {
                Library.Add(card.id, card);
                LibraryAccess.ReleaseMutex();
            }
        }
    }
    public class Card
    {
        public int id;

        public List<(Condition,Effect)> conditions;

        public Card Copy()
        {
            return new Card(this);
        }

        public Card(Card card)
        {
            this.id = card.id;
            this.conditions = card.conditions;
        }
    }

    public enum Condition {
        onSelfTurnStart,
        onSelfTurnEnd,
        onEnemyTurnStart,
        onEnemyTurnEnd,
    }
    public enum Effect { }

}
