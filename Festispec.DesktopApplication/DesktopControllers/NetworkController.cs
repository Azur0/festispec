using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.DesktopApplication.DesktopControllers
{
    public static class NetworkController
    {
        private static bool? _hasInternet;

        public static bool HasInternet()
        {
            if (_hasInternet == null)
            {
                try
                {
                    var client = new WebClient();
                    _hasInternet = client.OpenRead("http://google.com/").CanRead;
                }
                catch
                {
                    _hasInternet = false;
                }
            }

            return _hasInternet.Value;
        }
    }
}
