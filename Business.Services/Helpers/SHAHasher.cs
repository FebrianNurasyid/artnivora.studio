using System;
using System.Security.Cryptography;
using System.Text;

namespace Artnivora.Studio.Portal.Business.Services.Helpers
{
	public class ShaHasher
	{
		public static string Hash(string value)
		{
            using (SHA256 mySHA256 = SHA256.Create())
            {
                StringBuilder Sb = new StringBuilder();

                Byte[] result = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(value));

                foreach (Byte b in result)
                {
                    Sb.Append(b.ToString("x2"));
                }

                return Sb.ToString();
            }
        }
	}
}
