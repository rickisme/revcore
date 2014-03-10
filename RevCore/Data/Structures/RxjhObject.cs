namespace Data.Structures
{
    public abstract class RxjhObject : Statistical
    {
        private long _uid;

        public virtual long UID
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public RxjhObject Parent;

        public virtual void Release()
        {
            Parent = null;
        }
    }
}
