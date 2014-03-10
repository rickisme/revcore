using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using Data.Structures.Npc;
using Data.Structures.Creature;

namespace Global.Interfaces
{
    public interface IPlayerService : IComponent
    {
        void Send(ISendPacket packet);
        void InitPlayer(Player player);
        CheckNameResult CheckName(string name);
        Player CreateCharacter(IConnection connection, PlayerData playerData);
        bool IsPlayerOnline(Player player);
        void PlayerEnterWorld(Player player);
        void PlayerEndGame(Player player);
        Player GetPlayerByName(string playerName);
        void PlayerMoved(Player player, float x1, float y1, float z1, float x2, float y2, float z2, float distance, int tagert);
        void AddExp(Player player, long value, Npc npc);
        void SetExp(Player player, long value, Npc npc);
        void RessurectByPremiumItem(Player player, long value);
        void UpdateClientSetting(Player player, Settings Settings);
    }
}
