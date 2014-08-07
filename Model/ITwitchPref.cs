
namespace AreGamersStreaming.Model
{
    interface ITwitchPref
    {
        string TwitchAPI
        {
            get;
            set;
        }

        string TwitchAPIStream
        {
            get;
            set;
        }

        string TwitchHeaderv2
        {
            get;
            set;
        }

        string TwitchHeaderv3
        {
            get;
            set;
        }
    }
}
