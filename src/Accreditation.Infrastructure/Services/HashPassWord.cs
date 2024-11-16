using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Accreditation.Infrastructure.Services
{
    public interface IHashPassWord
    {
        public string Encryption(string Password);
    }

    public sealed class HashPassWord: IHashPassWord
    {
       


        //}


        public string Encryption(string Password)
        {
  
        }

    }
}
