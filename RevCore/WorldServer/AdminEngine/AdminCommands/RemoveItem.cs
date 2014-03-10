using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.AdminEngine.AdminCommands
{
    public class RemoveItem : ACommand
    {
        public override void Process(IConnection connection, string[] msg)
        {
            try
            {
                Player player = connection.Player;
                switch (msg[0])
                {
                    case "id":
                        {
                            int itemid = int.Parse(msg[1]);
                            int count = (msg.Length > 2) ? int.Parse(msg[2]) : 1;
                            Global.Global.StorageService.RemoveItemById(player, player.Inventory, itemid, count);
                            break;
                        }
                    case "slot":
                        {
                            int slot = int.Parse(msg[1]);
                            int count = (msg.Length > 2) ? int.Parse(msg[2]) : 1;
                            Global.Global.StorageService.RemoveItem(player, player.Inventory, slot, count);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Print(connection, "Wrong syntax!");
                Print(connection, "Syntax: @removeitem {id|slot} {item_id|item_slot} {counter}");
                Log.Warn(e.ToString());
            }
        }
    }
}
