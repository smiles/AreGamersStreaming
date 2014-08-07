using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace AreGamersStreaming.AGS_Core
{
    using Twitch;
    using ViewModel;

    public class AGS_Logic
    {
        private ITwitchStreamLogic _StreamLogic = new TwitchStreamLogic();
        private AGS_TaskBar _TaskBar = new AGS_TaskBar();
        private List<TwitchStreamInfo> _ListOfStreamers = new List<TwitchStreamInfo>();
        private int _BalloonTimeVisiable = 7000;

        public AGS_Logic()
        {
            _TaskBar.NoOneIsstreamingICO();
            NetworkWatch();
            _StreamLogic.UpdateListFromDB();
            _StreamLogic.StartCheckingForStreams();
            _StreamLogic.SomeoneIsStreamingEvent += SomeoneStreaming;
            _StreamLogic.SomeoneHasStopStreamingEvent += SomeoneStopStreaming;
            AGS_UserControl.ListHasBeenUpdatedEvent += ListOfStreamersChanged;

        }

        #region Private Method

        private void SomeoneStreaming(object sender, TwitchStreamInfo e)
        {
            _ListOfStreamers.Add(e);
            _TaskBar.SomeoneIsStreamingAlertICO(_BalloonTimeVisiable, e.BaseStreamName, e.URL);
        }

        private void SomeoneStopStreaming(object sender, TwitchStreamInfo e)
        {
            _ListOfStreamers.RemoveAll(x => x.URL == e.URL);
        }

        private void ListOfStreamersChanged(object sender, EventArgs e)
        {
            _StreamLogic.UpdateListFromDB();
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
