using Data.Enums;
using Data.Interfaces;
using Data.Structures.Player;
using System;
using System.IO;
using System.Text;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.OuterNetwork
{
    public abstract class OuterNetworkSendPacket : ISendPacket
    {
        protected byte[] Datas;
        protected object WriteLock = new object();
        public OuterNetworkConnection State;

        public void Send(Player player)
        {
            Send(player.Connection);
        }

        public void Send(params Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
                Send(players[i].Connection);
        }

        public void Send(params IConnection[] states)
        {
            for (int i = 0; i < states.Length; i++)
                Send(states[i]);
        }

        public void Send(IConnection state)
        {
            if (state == null || !state.IsValid)
                return;

            State = (OuterNetworkConnection)state;

            if (GetType() != typeof(SpTest) && !OuterNetworkOpcode.Send.ContainsKey(GetType()))
            {
                Log.Warn("UNKNOWN packet opcode: {0}", GetType().Name);
                return;
            }

            lock (WriteLock)
            {
                if (Datas == null)
                {
                    try
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            int lengthoffset = 6;

                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                WriteH(writer, 0); //Reserved for packet length

                                switch (WorldServer.CountryCode)
                                {
                                    case CountryCode.CN:
                                        {
                                            WriteC(writer, 0);
                                            lengthoffset = 7;
                                        }
                                        break;
                                    case CountryCode.EN:
                                        {
                                            lengthoffset = 6;
                                        }
                                        break;
                                    case CountryCode.TH:
                                        {
                                            lengthoffset = 6;
                                        }
                                        break;
                                    case CountryCode.TW:
                                        {
                                            WriteC(writer, 0);
                                            lengthoffset = 7;
                                        }
                                        break;
                                }

                                WriteH(writer, State.UID);
                                WriteH(writer, (GetType() != typeof(SpTest)) ? OuterNetworkOpcode.Send[GetType()] : 0);
                                WriteH(writer, 0); //Reserved for data length
                                Write(writer);
                            }
                            Datas = stream.ToArray();
                            BitConverter.GetBytes((short)(Datas.Length - 2)).CopyTo(Datas, 0);
                            BitConverter.GetBytes((short)(Datas.Length - 8)).CopyTo(Datas, lengthoffset);

                            //Log.Debug("Send Data {0}: [{1}] {2}", GetType().Name, Datas.Length, Datas.FormatHex());

                            WriteScope(ref Datas);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warn("Can't write packet: {0}", GetType().Name);
                        Log.WarnException("ASendPacket", ex);
                        return;
                    }
                }
            }

            state.PushPacket(Datas);
        }

        public void WriteScope(ref byte[] Data)
        {
            byte[] start_scope = new byte[2] { 0xAA, 0x55 };
            byte[] end_scope = new byte[2] { 0x55, 0xAA };
            byte[] buffer = new byte[Data.Length + 4];
            Buffer.BlockCopy(start_scope, 0, buffer, 0, 2);
            Buffer.BlockCopy(Data, 0, buffer, 2, Data.Length);
            Buffer.BlockCopy(end_scope, 0, buffer, Data.Length + 2, 2);
            Data = buffer;
        }

        public abstract void Write(BinaryWriter writer);

        protected void WriteD(BinaryWriter writer, int val)
        {
            writer.Write(val);
        }

        protected void WriteD(BinaryWriter writer, long val)
        {
            writer.Write((int)val);
        }

        protected void WriteH(BinaryWriter writer, short val)
        {
            writer.Write(val);
        }

        protected void WriteH(BinaryWriter writer, int val)
        {
            writer.Write((short)val);
        }

        protected void WriteC(BinaryWriter writer, byte val)
        {
            writer.Write(val);
        }

        protected void WriteC(BinaryWriter writer, int val)
        {
            writer.Write((byte)val);
        }

        protected void WriteDf(BinaryWriter writer, double val)
        {
            writer.Write(val);
        }

        protected void WriteF(BinaryWriter writer, float val)
        {
            writer.Write(val);
        }

        protected void WriteQ(BinaryWriter writer, long val)
        {
            writer.Write(val);
        }

        protected void WriteS(BinaryWriter writer, String text)
        {
            if (text.Length > 0)
            {
                writer.Write((byte)text.Length);
                writer.Write(Encoding.Default.GetBytes(text));
            }
        }

        protected void WriteSH(BinaryWriter writer, String text)
        {
            if (text.Length > 0)
            {
                writer.Write((short)text.Length);
                writer.Write(Encoding.Default.GetBytes(text));
            }
        }

        protected void WriteSN(BinaryWriter writer, String text)
        {
            byte[] names = Encoding.Default.GetBytes(text);
            byte[] val = new byte[15];
            Buffer.BlockCopy(names, 0, val, 0, names.Length);
            writer.Write(val);
        }

        protected void WriteB(BinaryWriter writer, string hex)
        {
            writer.Write(hex.ToBytes());
        }

        protected void WriteB(BinaryWriter writer, byte[] data)
        {
            writer.Write(data);
        }

        protected void WriteItemInfo(BinaryWriter writer, StorageItem item)
        {
            if (item != null)
            {
                WriteQ(writer, item.UID);
                WriteQ(writer, item.ItemId);
                WriteD(writer, item.Amount); // Amount
                WriteD(writer, item.Magic0); // FLD_MAGIC0
                WriteD(writer, item.Magic1); // FLD_MAGIC1
                WriteD(writer, item.Magic2); // FLD_MAGIC2
                WriteD(writer, item.Magic3); // FLD_MAGIC3
                WriteD(writer, item.Magic4); // FLD_MAGIC4
                WriteH(writer, 0);
                WriteH(writer, 0); // (IsBlue == true ? 1 : 0)
                WriteH(writer, item.BonusMagic1); // BonusMagic1
                WriteH(writer, item.BonusMagic2); // BonusMagic2
                WriteH(writer, item.BonusMagic3); // BonusMagic3
                WriteH(writer, item.BonusMagic4); // BonusMagic4
                WriteH(writer, item.BonusMagic5); // BonusMagic5
                WriteD(writer, 0);
                WriteD(writer, (item.LimitTime > 0 ? 1 : 0));
                WriteD(writer, item.LimitTime);
                WriteH(writer, item.Upgrade);
                WriteH(writer, item.ItemTemplate.Category); // ItemType
                WriteH(writer, 0); // 0
                WriteH(writer, 0);
                WriteQ(writer, 0);
                WriteB(writer, new byte[6]);
            }
            else
                WriteB(writer, new byte[88]);
        }
    }
}
