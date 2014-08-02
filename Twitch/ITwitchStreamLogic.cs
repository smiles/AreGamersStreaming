using System;
namespace AreGamersStreaming.Twitch
{
    interface ITwitchStreamLogic
    {
        event EventHandler<TwitchStreamInfo> SomeoneIsStreamingEvent;
        void StartCheckingForStreams();
        void StopCheckingForStreams();
        void UpdateHowOftenToCheck();
        void UpdateListFromDB();
    }
}
