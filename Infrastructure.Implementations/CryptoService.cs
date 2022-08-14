// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CryptoService.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Implementations
{
    using Infrastructure.Interfaces;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="CryptoService" />.
    /// </summary>
    public class CryptoService : ICryptoService
    {
        /// <summary>
        /// Defines the aesIv.
        /// </summary>
        private static readonly byte[] aesIv = Encoding.UTF8.GetBytes("zBgqesP1n1QMp8.i");

        /// <summary>
        /// Defines the aesKey.
        /// </summary>
        private static readonly byte[] aesKey = Encoding.UTF8.GetBytes("L9IqPujmK0[IL!O0QTJOlaBc:l6p*50H");

        /// <summary>
        /// The Decrypt.
        /// </summary>
        /// <param name="cipherValue">The cipherValue<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] Decrypt(byte[] cipherValue)
        {
            var plainBytes = DecryptAes(cipherValue, aesKey, aesIv);
            return plainBytes;
        }

        /// <summary>
        /// The Encrypt.
        /// </summary>
        /// <param name="plainValue">The plainValue<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] Encrypt(byte[] plainValue)
        {
            var cipherBytes = EncryptAes(plainValue, aesKey, aesIv);
            return cipherBytes;
        }

        /// <summary>
        /// The Decrypt.
        /// </summary>
        /// <param name="cipherText">The cipherText<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            var plainBytes = DecryptAes(cipherBytes, aesKey, aesIv);
            var plainText = Encoding.Unicode.GetString(plainBytes);
            return plainText;
        }

        /// <summary>
        /// The Encrypt.
        /// </summary>
        /// <param name="plainText">The plainText<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string Encrypt(string plainText)
        {
            var plainBytes = Encoding.Unicode.GetBytes(plainText);
            var cipherBytes = EncryptAes(plainBytes, aesKey, aesIv);
            var cipherText = Convert.ToBase64String(cipherBytes);
            return cipherText;
        }

        /// <summary>
        /// The EncryptSaltedHash.
        /// </summary>
        /// <param name="plainText">The plainText<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string EncryptSaltedHash(string plainText)
        {
            return GenerateSaltedHash(plainText);
        }

        /// <summary>
        /// The DecryptAes.
        /// </summary>
        /// <param name="cipherBytes">The cipherBytes<see cref="byte[]"/>.</param>
        /// <param name="key">The key<see cref="byte[]"/>.</param>
        /// <param name="iv">The iv<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        private static byte[] DecryptAes(byte[] cipherBytes, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherBytes == null)
            {
                throw new ArgumentNullException("cipherBytes");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("iv");
            }

            // Create an AesCryptoServiceProvider object with the specified key and IV.
            using (var provider = new AesCryptoServiceProvider { Key = key, IV = iv })
            {
                var decryptor = provider.CreateDecryptor(provider.Key, provider.IV);

                using (var memoryStream = new MemoryStream(cipherBytes))
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    // the "plain bytes" may be less, but will never be more that the "cipher bytes"
                    byte[] plainBytes = new byte[cipherBytes.Length];
                    int readCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                    // if the "plain bytes" are less that originally assumed, we have to return a new byte[] with the proper length
                    if (readCount != plainBytes.Length)
                    {
                        byte[] tempBytes = new byte[readCount];
                        Array.Copy(plainBytes, tempBytes, tempBytes.Length);
                        return tempBytes;
                    }

                    return plainBytes;
                }
            }
        }

        /// <summary>
        /// The EncryptAes.
        /// </summary>
        /// <param name="plainBytes">The plainBytes<see cref="byte[]"/>.</param>
        /// <param name="key">The key<see cref="byte[]"/>.</param>
        /// <param name="iv">The iv<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        private static byte[] EncryptAes(byte[] plainBytes, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainBytes == null)
            {
                throw new ArgumentNullException("plainBytes");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Create an AesCryptoServiceProvider object with the specified key and IV.
            using (var provider = new AesCryptoServiceProvider { Key = key, IV = iv })
            {
                var encryptor = provider.CreateEncryptor(provider.Key, provider.IV);

                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] cipherBytes = memoryStream.ToArray();
                    return cipherBytes;
                }
            }
        }

        /// <summary>
        /// The GenerateSaltedHash.
        /// </summary>
        /// <param name="plainText">The plainText<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        private static byte[] GenerateSaltedHash(byte[] plainText)
        {


            using (var algorithm = new SHA1Managed())
            {
                var enc = Encoding.ASCII;
                var salt = enc.GetBytes("74qW401");

                var plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

                Array.Copy(plainText, plainTextWithSaltBytes, plainText.Length);
                Array.Copy(salt, 0, plainTextWithSaltBytes, plainText.Length, salt.Length);

                var hash = algorithm.ComputeHash(plainTextWithSaltBytes);
                return hash;
            }
        }

        /// <summary>
        /// The GenerateSaltedHash.
        /// </summary>
        /// <param name="plainText">The plainText<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GenerateSaltedHash(string plainText)
        {
            var plainBytes = Encoding.Unicode.GetBytes(plainText);
            var saltedHashBytes = GenerateSaltedHash(plainBytes);
            var saltedHashText = Convert.ToBase64String(saltedHashBytes);
            return saltedHashText;
        }
    }
}
