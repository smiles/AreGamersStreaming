using System;
namespace AreGamersStreaming.Twitch
{
    interface ITwitchStreamLogic
    {
        bool IsChecking { get; set; }
        void RestartCheck();
        event EventHandler<TwitchStreamInfo> SomeoneIsStreamingEvent;
        public event EventHandler<TwitchStreamInfo> SomeoneHasStopStreamingEvent;
        void StartCheckingForStreams();
        void StopCheckingForStreams();
        void UpdateHowOftenToCheck();
        void UpdateListFromDB();
    }
}
