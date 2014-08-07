using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreGamersStreaming.Twitch
{
    public static class TwitchValidation
    {
        private const string TwitchTV = "www.twitch.tv/";
        private const string HTTPSTwitchTV = "http://www.twitch.tv/";

        public static bool IsValidStream(string URL)
        {
            if(URL.StartsWith(TwitchTV) || URL.StartsWith(HTTPSTwitchTV))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}
