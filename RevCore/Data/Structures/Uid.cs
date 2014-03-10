using Data.Enums;
using Utilities;

namespace Data.Structures
{
    public abstract class Uid : Statistical
    {
        private int _uid;

        private UidFactory _uidFactory;

        public int UID
        {
            get
            {
                if (_uid == 0)
                {
                    RegisterUid(_uidFactory);
                }
                return _uid;
            }
        }

        public void RegisterUid(UidFactory uidFactory = null)
        {
            _uidFactory = uidFactory ?? UidFactory.Factory(this);
            _uid = _uidFactory.RegisterObject(this);
        }

        public virtual void Release()
        {
            if (_uidFactory == null)
                return;

            ReleaseUniqueId();
            _uidFactory = null;
        }

        public void ReleaseUniqueId()
        {
            _uidFactory.Release(this, _uid);
        }

        //

        public static Uid GetObject(long longUID)
        {
            ObjectFamily family = ObjectFamily.System;

            if (longUID >= 10000 && longUID < 15000)
                family = ObjectFamily.Npc;

            if (longUID >= 15000 && longUID < 20000)
                family = ObjectFamily.Item;

            return UidFactory.Factory(family).FindObject((int)longUID);
        }
    }
}
