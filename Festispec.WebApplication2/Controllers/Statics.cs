using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Festispec.WebApplication2.Controllers
{
    public class Statics
    {
        public static string SHA256(string text)
        {
            using (var crypt = new System.Security.Cryptography.SHA256Managed())
            {
                var hash = new System.Text.StringBuilder();
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }
                return hash.ToString();
            }
        }
    }
}