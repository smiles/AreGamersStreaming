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
        private JSONtoString _GetTwitchJSON = new JSONtoString(Settings.Default.TwitchJSONHeaderv3);

        public void GetTwitchStream(TwitchStream stream)
        {
            Task<string> holder = _GetTwitchJSON.GetJSON(stream.StreamAPI);
            stream.StreamJSON = JsonConvert.DeserializeObject<TwitchStream.RootObject>(holder.ToString());
        }

        public void GetAllTwitchStream(List<TwitchStream> allStreams)
        {
            foreach (TwitchStream stream in allStreams)
            {
                Task<string> holder = _GetTwitchJSON.GetJSON(stream.StreamAPI);

                    stream.StreamJSON = JsonConvert.DeserializeObject<TwitchStream.RootObject>(holder.ToString());
                }
        }
    }
}
