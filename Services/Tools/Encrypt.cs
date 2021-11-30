using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tools.Encrypt
{
    /// <summary>
    /// Clase para encriptar datos
    /// </summary>
    public static class Encrypt
    {
        /// <summary>
        /// Calcula el Hash de un string usando SHA256
        /// </summary>
        /// <param name="messageString"></param>
        /// <returns></returns>
        public static int CalcularHash(string messageString)
        {
            byte[] hashValue;

            //Convierto desde codificación UTF8
            UTF8Encoding ue = new UTF8Encoding();

            //Convierto el string a un flujo de bytes
            byte[] messageBytes = ue.GetBytes(messageString);

            //Utilizo SHA256 para calcular un hash
            SHA256 shHash = SHA256.Create();

            //Calculo el hash del mensaje en bytes...
            hashValue = shHash.ComputeHash(messageBytes); // 32 bytes encriptados...

            int valor = hashValue.Sum(o => o);

            return valor;
        }

        /// <summary>
        /// Encriptar password usando MD5
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string EncodePasswordMd5(string pass) //Encrypt using MD5
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            
            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        /// <summary>
        /// Codifica un string usando Base 64 que luego se puede decodificar
        /// </summary>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static string base64Encode(string sData) // Encode
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        /// <summary>
        /// Decodifica un string previamente codificado con Base 64
        /// </summary>
        /// <param name="sData"></param>
        /// <returns></returns>
        public static string base64Decode(string sData) //Decode
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
    }
}
