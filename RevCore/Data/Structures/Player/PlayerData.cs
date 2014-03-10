using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Enums;
using Utilities;

namespace Data.Structures.Player
{
    public class PlayerData
    {
        public Gender Gender;

        public PlayerClass Class;

        public string Name = "Error!";

        public int Title = 0;

        public int Forces = 0;

        public int HairStyle;

        public int HairColor;

        public int Face;

        public int Voice;

        public byte[] NameStyle = "0000000000000000FFFFFFFFFFFFFFFF0100000000000000FFFFFFFFFFFFFFFF0200000000000000FFFFFFFFFFFFFFFF".ToBytes();
    }
}
