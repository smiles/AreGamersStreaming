using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 

namespace AreGamersStreaming.AGS_Core
{
    using Twitch;

    public class AGS_Logic
    {
        private ITwitchStreamLogic _StreamLogic = new TwitchStreamLogic();
        private AGS_TaskBar _TaskBar = new AGS_TaskBar();

        public AGS_Logic()
        { }

        private async void TestNetwork(object sender)
        {

        }
    }
}
