using System.Collections.Generic;

namespace AreGamersStreaming.Model
{
    interface IUserPref
    {
        bool IsStartAtBoot
        {
            get;
            set;
        }

        bool IsMinamizeAtStart
        {
            get;
            set;
        }

        int HowOftenToCheck
        {
            get;
            set;
        }

        List<string> AllStreamList
        {
            get;
            set;
        }
    }
}
