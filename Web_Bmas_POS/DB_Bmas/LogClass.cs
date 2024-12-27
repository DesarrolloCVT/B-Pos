using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DB_Bmas
{
    public class LogClass
    {
        public LogClass() { }

        public string Encrypt(string toEncrypt, bool useHashing)//true
        {
            byte[] buffer;
            byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
            AppSettingsReader reader = new AppSettingsReader();
            string s = (string)reader.GetValue("SecurityKey", typeof(string));
            if (useHashing)
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(s));
                provider.Clear();
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(s);
            }
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] inArray = provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            provider2.Clear();
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public string Decrypt(string cipherString, bool useHashing)//true
        {
            byte[] buffer;
            byte[] inputBuffer = Convert.FromBase64String(cipherString);
            AppSettingsReader reader = new AppSettingsReader();
            string s = (string)reader.GetValue("SecurityKey", typeof(string));
            if (useHashing)
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(s));
                provider.Clear();
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(s);
            }
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] bytes = provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            provider2.Clear();
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
