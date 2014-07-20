using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        List<string> UserStreamList
        {
            get;
            set;
        }
    }
}
