using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using Data.Structures.Template.Item;
using System;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.AdminEngine.AdminCommands
{
    public class AddItem : ACommand
    {
        public override void Process(IConnection connection, string[] msg)
        {
            try
            {
                Player player = connection.Player;
                Player target = player.Target as Player;
                if (target == null)
                {
                    target = player;
                }

                long itemid = 0;
                int count = 1;

                switch (msg.Length)
                {
                    case 2:
                        {
                            if (msg[0].Equals("money") || int.Parse(msg[0]) == 2000000000)
                            {
                                long money = long.Parse(msg[1]);
                                Global.Global.StorageService.AddMoneys(target, target.Inventory, money);
                                return;
                            }

                            itemid = long.Parse(msg[0]);
                            count = (msg.Length > 1) ? int.Parse(msg[1]) : 1;
                            break;
                        }
                    case 3:
                        {
                            target = Global.Global.VisibleService.FindTarget(player, msg[0]);

                            if (msg[1].Equals("money") || int.Parse(msg[1]) == 2000000000)
                            {
                                long money = long.Parse(msg[2]);
                                Global.Global.StorageService.AddMoneys(target, target.Inventory, money);
                                return;
                            }

                            itemid = long.Parse(msg[1]);
                            count = (msg.Length > 1) ? int.Parse(msg[2]) : 1;
                            break;
                        }
                    default:
                        Print(connection, "Wrong syntax!");
                        Print(connection, "Syntax: @additem <{item_id} {counter}> | <{target name} {item_id} {counter}>");
                        break;
                }

                ItemTemplate itemTemplate = Data.Data.ItemTemplates[itemid];
                if (itemTemplate == null)
                {
                    return;
                }

                StorageItem item = new StorageItem() { ItemId = itemid, Amount = count, State = ItemState.NEW };
                Global.Global.StorageService.AddItem(target, target.Inventory, item);
            }
            catch (Exception e)
            {
                Print(connection, "Wrong syntax!");
                Print(connection, "Syntax: @additem <{item_id} {counter}> | <{target name} {item_id} {counter}>");
                Log.Warn(e.ToString());
            }
        }
    }
}
