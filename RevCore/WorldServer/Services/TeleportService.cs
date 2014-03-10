using Data.Structures.Player;
using Data.Structures.World;
using Global.Interfaces;
using System;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.Services
{
    class TeleportService : ITeleportService
    {
        public void ForceTeleport(Player player, WorldPosition position)
        {
            Global.Global.MapService.PlayerLeaveWorld(player);

            player.Position.MapId = position.MapId;
            player.Position.X = position.X;
            player.Position.Y = position.Y;
            Global.Global.VisibleService.Send(player, new SpSetLocation(player));
            Global.Global.VisibleService.Send(player, new SpPlayerInfo(player));
            Global.Global.MapService.PlayerEnterWorld(player);
        }

        public WorldPosition GetBindPoint(Player player)
        {
            return null;
        }

        public void Action()
        {
            
        }
    }
}
