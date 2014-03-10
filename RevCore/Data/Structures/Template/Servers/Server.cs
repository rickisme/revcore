using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Structures.Template.Servers
{
    public class Server
    {
        public virtual int id { get; set; }

        public virtual string name { get; set; }

        public virtual string ip { get; set; }

        private List<Channel> _channels = new List<Channel>();

        public virtual List<Channel> channels { set { _channels = (value); } get { return _channels; } }
    }
}
