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

        public static Card? LookupCard(int cardId)
        {
            Card? card = null;
            if (LibraryAccess.WaitOne())
            {
                card = Library[cardId];
                LibraryAccess.ReleaseMutex();
            }
            return card;
        }

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
        public readonly int id;

        public readonly List<(Condition,Effect)> conditions;
    }

    public enum Condition {
        onSelfTurnStart,
        onSelfTurnEnd,
        onEnemyTurnStart,
        onEnemyTurnEnd,
    }
    public enum Effect { }

}
