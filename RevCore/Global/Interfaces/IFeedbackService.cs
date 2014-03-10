using Data.Enums;
using Data.Interfaces;
using Data.Structures.Creature;
using Data.Structures.Player;

namespace Global.Interfaces
{
    public interface IFeedbackService : IComponent
    {
        void OnAuthorized(IConnection connection);
        void SendPlayerLists(IConnection connection);
        void SendCheckNameResult(IConnection connection, string name, CheckNameResult result);
        void SendCreateCharacterResult(IConnection connection, bool result);
        void SendCreatureInfo(IConnection connection, Creature creature);
        void SendRemoveCreature(IConnection connection, Creature creature);
        void PlayerMoved(Player player, float x1, float y1, float z1, float x2, float y2, float z2, float distance, int tagert);
        void HpMpSpChanged(Player player);
        void AttackStageEnd(Creature creature);
        void AttackFinished(Creature creature);
        void PlayerDied(Player player);
        void StatsUpdated(Player player);
        void Exit(IConnection connection);
        void Logout(IConnection connection);
        void PlayerAction(Player player, int ActionId);
        void PlayerLevelUp(Player player);
        void PlayerLearnSkill(Player player);
    }
}
