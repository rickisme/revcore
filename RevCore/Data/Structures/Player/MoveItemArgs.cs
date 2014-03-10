using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Player
{
    public class MoveItemArgs
    {
        public StorageItem ItemToMove;

        public StorageItem ItemToReplace;

        public byte IsFromInventory;

        public byte IsToInventory;

        public byte FromSlot;

        public byte ToSlot;
    }
}
