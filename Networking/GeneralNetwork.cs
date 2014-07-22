using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Smiles.Common.Lib
{
    public static class GeneralNetwork
    {
        public static bool IsNetworkConnected()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
