using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem
{
    public interface IPlayerController
    {
        public Task AwaitConfirmation();
        public Task<bool> AwaitItemActivation(out int itemToActivate);

        public Task<bool> AwaitCardAttack(out int cardToActivate,out int cardToTarget);
        public Task Notify();
        public Task<Card> GetCardToPlace();
        public Task<int> GetAreaToPlace();
    }
}
