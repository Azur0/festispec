using System;
using System.Text;
using Windows.UI.Popups;

namespace Festispec.DesktopApplication.DesktopControllers
{
    public static class Crypt
    {
        public static string SHA256(string text)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            crypt.Dispose();
            return hash.ToString();
        }
    }
}
