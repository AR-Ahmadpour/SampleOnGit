using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Accreditation.Infrastructure.Services
{
    /// <summary>
    /// برای استفاده رمز نگاری بین صفحات
    /// </summary>
    public static class Encryptions
    {
   
        public static string Encrypt(string text)
        {
            string keyString = "Negso1403ToKEYS";
            var key = Encoding.UTF8.GetBytes(keyString);

      
        }

        public static string Decrypt(string cipherText)
        {
            string keyString = "Negso1403ToKEYS";
            var fullCipher = Convert.FromBase64String(cipherText);
            var iv = new byte[16];
            var cipher = new byte[16];

            
        }

    }
}
