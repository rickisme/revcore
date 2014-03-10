using Data.Structures.Player;
using Data.Structures.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.Services
{
    public class DialogService
    {
        public static void OpenDialog(Player player, int dialogId, int type, int npcId)
        {
            /*
             * AA55
             * 2700
             * 012C01
             * 9100
             * 1800
             * 0180 dialogId
             * 0000
             * 01000000
             * 01000000 
             * 00000000
             * 0000000000000000
             * 0000000000000000
             * 55AA
             */

            //byte[] array = Converter.hexStringToByte(string_);
            //byte[] array2 = new byte[2];
            //byte[] array3 = new byte[2];
            //Buffer.BlockCopy(byte_data, 19, array2, 0, 2);
            //Buffer.BlockCopy(byte_data, 11, array3, 0, 2);
            //int count = (int)BitConverter.ToInt16(array2, 0);
            //int dialogId = (int)BitConverter.ToInt16(array3, 0);

            switch (dialogId)
            {
                case 1:
                    //this.打开仓库中 = true;
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //this.合成物品表.Clear();
                    break;
                case 2:
                    //this.打开仓库中 = false;
                    //this.合成系统解锁();
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    break;
                case 3:
                    new SpOpenShop(dialogId, type, npcId).Send(player);
                    return;
                case 4:
                case 6:
                case 7:
                case 8:
                    break;
                case 5:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //this.打开仓库中 = true;
                    //this.打开个人仓库();
                    //this.打开综合仓库();
                    break;
                case 9:
                    if (type <= 28)
                    {
                        switch (type)
                        {
                            case 15:
                                //Buffer.BlockCopy(array2, 0, array, 19, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                                //this.Teleport(0f, -600f, 15f, 401);
                                break;
                            case 16:
                                //Buffer.BlockCopy(array2, 0, array, 19, 1);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                                //this.Teleport(0f, -600f, 15f, 501);
                                break;
                            case 27:
                                //Buffer.BlockCopy(array2, 0, array, 19, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                                //this.Teleport(750f, -880f, 15f, 601);
                                break;
                            case 28:
                                //Buffer.BlockCopy(array2, 0, array, 19, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                                //this.Teleport(-850f, -800f, 15f, 701);
                                break;
                            case 33:
                                //Buffer.BlockCopy(array2, 0, array, 19, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                                //this.Teleport(1630f, -1578f, 15f, 1701);
                                break;
                            case 34:
                                //Buffer.BlockCopy(array2, 0, array, 19, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                                //this.Teleport(-1624f, 1561f, 15f, 1401);
                                break;
                            case 74:
                                //Buffer.BlockCopy(array2, 0, array, 19, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                                //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                                //if (base.Player_Zx == 1)
                                //{
                                //    this.Teleport(1125f, -1555f, 15f, 2001);
                                //    break;
                                //}
                                //this.Teleport(-2231f, 1495f, 15f, 2001);
                                break;
                        }
                    }
                    break;
                case 12:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(12), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(12), 0, array, 15, 2);
                    //base.发送势力战消息1();
                    break;
                case 15:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(10f, 10f, 15f, 1201);
                    break;
                case 17:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(422f, 2194f, 15f, 101);
                    break;
                case 22:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(-302f, 288f, 15f, 2301);
                    break;
                case 23:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(-302f, 288f, 15f, 2341);
                    break;
                case 38:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(1900f, -820f, 15f, 5001);
                    break;
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 46:
                case 47:
                case 48:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 64:
                    break;
                case 45:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(-239f, -226f, 15f, 20001);
                    break;
                case 49:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(345f, 300f, 15f, 21001);
                    break;
                case 62:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(-1f, -221f, 15f, 23001);
                    break;
                case 63:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(422f, 2194f, 15f, 101);
                    break;
                case 65:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23100);
                    break;
                case 66:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23001);
                    break;
                case 67:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23200);
                    break;
                case 68:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23100);
                    break;
                case 69:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23300);
                    break;
                case 70:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23200);
                    break;
                case 71:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23400);
                    break;
                case 72:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23300);
                    break;
                case 73:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23500);
                    break;
                case 74:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23400);
                    break;
                case 75:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23600);
                    break;
                case 76:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23500);
                    break;
                case 77:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23700);
                    break;
                case 78:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23600);
                    break;
                case 79:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23800);
                    break;
                case 80:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23700);
                    break;
                case 81:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23900);
                    break;
                case 82:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23800);
                    break;
                case 92:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(0f, -10f, 15f, 23050);
                    break;
                case 94:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(-900f, 145f, 15f, 6001);
                    break;
                case 101:
                    //this.打开仓库中 = true;
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //this.合成物品表.Clear();
                    break;
                case 102:
                    //this.打开仓库中 = true;
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //this.合成物品表.Clear();
                    break;
                case 103:
                    //this.打开仓库中 = true;
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //this.合成物品表.Clear();
                    break;
                case 110:
                    //this.打开仓库中 = true;
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //SendPacket 发包类2 = new SendPacket();
                    //发包类2.Write2(base.PlayerObjectID);
                    //if (base.Client != null)
                    //{
                    //    base.Client.SendPak(发包类2, 16663, base.PlayerObjectID);
                    //    break;
                    //}
                    break;
                case 111:
                    //this.打开仓库中 = false;
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    break;
                case 117:
                    //this.LookIngotCount();
                    //if (base.FLD_RXPIONT >= 50 && base.CharacterMoney >= 50000000L)
                    //{
                    //    Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //    Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //    Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //    break;
                    //}
                    //base.SystemMessage("你的钱数或是元宝不够不能申请帮派战，申请帮战最少50元宝和5千万游戏币", 9, "系統提示");
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    break;
                case 122:
                    //this.打开仓库中 = true;
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //this.合成物品表.Clear();
                    break;
                case 146:
                    //this.打开仓库中 = true;
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(array3, 0, array, 11, 2);
                    //Buffer.BlockCopy(array3, 0, array, 15, 2);
                    //this.合成物品表.Clear();
                    break;
                case 147:
                case 148:
                case 149:
                case 151:
                case 152:
                case 155:
                    break;
                case 150:
                    //if (base.Player_Job_leve >= 6)
                    //{
                    //    Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //    Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //    Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //    this.Teleport(1884f, -1635f, 15f, 25100);
                    //}
                    //base.SystemMessage("只有升天以后才可以进入", 9, "系統提示");
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    break;
                case 153:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(-360f, -363f, 15f, 25201);
                    break;
                case 154:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(117f, -267f, 15f, 25202);
                    break;
                case 156:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(117f, -267f, 15f, 25301);
                    break;
                case 157:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    Random random = new Random();
                    int num4 = random.Next(2, 10);
                    switch (num4)
                    {
                        case 2:
                            //this.Teleport(117f, -267f, 15f, 25200 + num4);
                            break;
                        case 3:
                            //this.Teleport(125f, -329f, 15f, 25200 + num4);
                            break;
                        case 4:
                            //this.Teleport(124f, -244f, 15f, 25200 + num4);
                            break;
                        case 5:
                            //this.Teleport(138f, -271f, 15f, 25200 + num4);
                            break;
                        case 6:
                            //this.Teleport(158f, 256f, 15f, 25200 + num4);
                            break;
                        case 7:
                            //this.Teleport(300f, 92f, 15f, 25200 + num4);
                            break;
                        case 8:
                            //this.Teleport(158f, -302f, 15f, 25200 + num4);
                            break;
                        case 9:
                            //this.Teleport(32f, 92f, 15f, 25200 + num4);
                            break;
                        case 10:
                            //this.Teleport(154f, 30f, 15f, 25200 + num4);
                            break;
                        default:
                            break;
                    }
                    break;
                case 158:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);
                    //this.Teleport(-360f, -363f, 15f, 25201);
                    break;
                case 173:
                    //Buffer.BlockCopy(array2, 0, array, 19, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 11, 2);
                    //Buffer.BlockCopy(BitConverter.GetBytes(2), 0, array, 15, 2);

                    WorldPosition position = new WorldPosition();
                    position.X = 1989;
                    position.Y = -2027;
                    position.Z = 15;
                    position.MapId = 26000;
                    Global.Global.TeleportService.ForceTeleport(player, position);
                    break;
            }
        }
    }
}
