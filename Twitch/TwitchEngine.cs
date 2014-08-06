using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AreGamersStreaming.Twitch
{
    using Smiles.Common.Http;
    using Properties;
    using System.Threading.Tasks;

    public class TwitchEngine
    {
        private JSONtoString _GetTwitchJSON = new JSONtoString();

        public void GetTwitchStream(TwitchStream stream)
        {
            stream.StreamJSON = JsonConvert.DeserializeObject<TwitchStream.RootObject>(_GetTwitchJSON.GetJSON(stream.StreamAPI));
        }

        public void GetAllTwitchStream(List<TwitchStream> allStreams)
        {
            foreach (TwitchStream stream in allStreams)
            {
                 

                    stream.StreamJSON = JsonConvert.DeserializeObject<TwitchStream.RootObject>(_GetTwitchJSON.GetJSON(stream.StreamURL));
                }
        }
    }
}
