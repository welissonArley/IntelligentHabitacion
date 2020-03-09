using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IntelligentHabitacion.Api.Repository.Cryptography
{
#pragma warning disable CA2202
    public class Cryptography
    {
        public string Encrypt(string data, string salt)
        {
            if (string.IsNullOrWhiteSpace(data))
                return "";

            using (var rijndaeController = RijndaelController(salt))
            {
                using (var encripter = rijndaeController.CreateEncryptor(rijndaeController.Key, rijndaeController.IV))
                {
                    using (var streamResult = new MemoryStream())
                    {
                        var csStream = new CryptoStream(streamResult, encripter, CryptoStreamMode.Write);
                        try
                        {
                            using (var writer = new StreamWriter(csStream))
                            {
                                csStream = null;
                                writer.Write(data);
                            }

                            return ArrayBytesToHexString(streamResult.ToArray());
                        }
                        finally
                        {
                            if (csStream != null)
                                csStream.Dispose();
                        }
                    }
                }
            }
        }
        public string Dencrypt(string data, string salt)
        {
            if (string.IsNullOrWhiteSpace(data))
                return data;

            using (var rijndaeController = RijndaelController(salt))
            {
                var decriptador = rijndaeController.CreateDecryptor(rijndaeController.Key, rijndaeController.IV);
                string result;
                using (var streamTextoEncriptado = new MemoryStream(HexStringToArrayByte(data)))
                {
                    var csStream = new CryptoStream(streamTextoEncriptado, decriptador, CryptoStreamMode.Read);
                    try
                    {
                        using (var reader = new StreamReader(csStream))
                        {
                            csStream = null;
                            result = reader.ReadToEnd();
                        }
                    }
                    finally
                    {
                        if (csStream != null)
                            csStream.Dispose();
                    }
                }

                return result;
            }
        }

        private Rijndael RijndaelController(string salt)
        {
            return RijndaelManaged("L1BL6D-YQM5-UP2K-LYSG-JLEYQ2CKZM", salt);
        }
        private Rijndael RijndaelManaged(string key, string iv)
        {
            var algorithm = Rijndael.Create();
            algorithm.Key = Encoding.ASCII.GetBytes(key);
            algorithm.IV = Encoding.ASCII.GetBytes(iv);

            return algorithm;
        }
        private string ArrayBytesToHexString(byte[] content)
        {
            string[] arrayHex = Array.ConvertAll(content, b => b.ToString("X2"));

            return string.Concat(arrayHex);
        }
        private byte[] HexStringToArrayByte(string content)
        {
            int qtdBytesEncriptados = content.Length / 2;
            byte[] arrayContent = new byte[qtdBytesEncriptados];
            for (var i = 0; i < qtdBytesEncriptados; i++)
            {
                arrayContent[i] = Convert.ToByte(content.Substring(i * 2, 2), 16);
            }

            return arrayContent;
        }
    }
}
