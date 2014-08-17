using System;
using System.Collections.Generic;
namespace AreGamersStreaming.Twitch
{
    interface ITwitchStreamLogic
    {
        bool IsChecking { get; set; }
        void RestartCheck();
        event EventHandler<TwitchStreamInfo> SomeoneIsStreamingEvent;
        event EventHandler<TwitchStreamInfo> SomeoneHasStopStreamingEvent;
        void StartCheckingForStreams(int minutes);
        void StopCheckingForStreams();
        void NewStreamList(List<string> newStreamList);
    }
}
