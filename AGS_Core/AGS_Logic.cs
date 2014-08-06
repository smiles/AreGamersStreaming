using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace AreGamersStreaming.AGS_Core
{
    using Twitch;

    public class AGS_Logic
    {
        private ITwitchStreamLogic _StreamLogic = new TwitchStreamLogic();
        private AGS_TaskBar _TaskBar = new AGS_TaskBar();
        private List<TwitchStreamInfo> _ListOfStreamers = new List<TwitchStreamInfo>();

        public AGS_Logic()
        {
            _TaskBar.NoOneIsstreamingICO();
            NetworkWatch();
            _StreamLogic.UpdateListFromDB();
            _StreamLogic.StartCheckingForStreams();
            _StreamLogic.SomeoneIsStreamingEvent += SomeoneStreaming;
            _StreamLogic.SomeoneHasStopStreamingEvent += SomeoneStopStreaming;

        }

        #region Private Method

        private void SomeoneStreaming(object sender, TwitchStreamInfo e)
        {
            _ListOfStreamers.Add(e);
            _TaskBar.SomeoneIsStreamingAlertICO(7000, e.BaseStreamName, e.URL);
        }

        private void SomeoneStopStreaming(object sender, TwitchStreamInfo e)
        {
            _ListOfStreamers.RemoveAll(x => x.URL == e.URL);
        }

        private void NetworkWatch()
        {
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkHasChanged);
        }

        private void NetworkHasChanged(object sender, EventArgs e)
        {
            if(NetworkInterface.GetIsNetworkAvailable())
            {
                if(!_StreamLogic.IsChecking)
                {
                    _StreamLogic.RestartCheck();
                }

                _TaskBar.NoOneIsstreamingICO();
            }
            else
            {
                _StreamLogic.StopCheckingForStreams();
                _TaskBar.DisconnectedNetworkAlertICO();
            }

        }

        #endregion
    }
}
