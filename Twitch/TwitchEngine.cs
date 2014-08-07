using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AreGamersStreaming.Twitch
{
    using Properties;
    using System.Threading.Tasks;

    public class TwitchEngine
    {
        private string _DefaultJSONHeader = "application/json";

        public void GetTwitchStream(TwitchStream stream)
        {
            using (HttpClient client = new HttpClient())
            {
                _RetrieveJSON(stream, client);
            }
        }

        public void GetAllTwitchStream(List<TwitchStream> allStreams)
        {
            foreach (TwitchStream stream in allStreams)
            {
                using (HttpClient client = new HttpClient())
                {
                    _RetrieveJSON(stream, client);
                }
            }
        }

        private void _RetrieveJSON(TwitchStream stream, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_DefaultJSONHeader));

            HttpResponseMessage response = client.GetAsync(stream.StreamAPI).Result;
            if (response.IsSuccessStatusCode)
            {
                stream.StreamJSON = JsonConvert.DeserializeObject<TwitchStream.RootObject>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
