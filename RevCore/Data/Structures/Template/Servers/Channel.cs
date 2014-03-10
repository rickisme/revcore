using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Template.Servers
{
    public class Channel
    {
        public virtual int wid { get; set; }

        public virtual int id { get; set; }

        public virtual string name { get; set; }

        public virtual int port { get; set; }

        public virtual int max_user { get; set; }

        public virtual int type { get; set; }
    }
}
