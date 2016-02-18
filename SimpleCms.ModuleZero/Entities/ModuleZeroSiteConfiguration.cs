using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleZero.Entities
{
    public class ModuleZeroSiteConfiguration : FullAuditedEntity, IMustHaveTenant
    {
        public int IdConfiguration { get; set; }
        public int TenantId { get; set; }
        public string HashPayPalClientId { get; set; }
        public string HashPayPalClientSecret { get; set; }
        public string HashGoogleAnalyticsClientId { get; set; }
        public string GlobalKey { get; set; }
        public virtual ICollection<CustomSiteConfigProperties> CustomProps { get; set; }
        [NotMapped]
        public static string UniqueHashPassword { get; protected set; }
        [NotMapped]
        public static string UniqueSaltKey { get; protected set; }
        [NotMapped]
        public static string V1Key { get; protected set; }
        public virtual string HashValue(string value,string uniqueHashPassword,string uniqueSaltKey,string v1Key)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            var keyBytes = new Rfc2898DeriveBytes(uniqueHashPassword,Encoding.ASCII.GetBytes(uniqueSaltKey)).GetBytes(256/8);
            var symmetricKey = new RijndaelManaged() {Mode=CipherMode.CBC,Padding = PaddingMode.Zeros};
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(v1Key));
            byte[] cypherBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream,encryptor,CryptoStreamMode.Read))
                {
                    cryptoStream.Write(plainTextBytes,0,plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cypherBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
            }
            return Convert.ToBase64String(cypherBytes);
        }
        public virtual string DeHashValue(string value, string uniqueHashPassword, string uniqueSaltKey, string v1Key)
        {
            var cipherTextBytes = Convert.FromBase64String(value);
            var keyBytes = new Rfc2898DeriveBytes(uniqueHashPassword, Encoding.ASCII.GetBytes(uniqueSaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(v1Key));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];

            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}
