using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AreGamersStreaming.Model
{
    using AreGamersStreaming.Twitch;
    using Properties;

    public class StreamList
    {

        public StreamList()
        {

        }

        public List<TwitchStream> AllStreamList
        {
            get;
            private set;
        }

        public void AddStream(TwitchStream stream)
        {
            this.AllStreamList.Add(stream);
        }

        public void DeleteStream(TwitchStream stream)
        {
            this.AllStreamList.Remove(stream);
        }
    }
}
