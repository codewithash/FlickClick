// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICryptoService.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// The crypto service.
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Decrypt a given byte array.
        /// </summary>
        /// <param name="cipherValue">The byte array to decrypt.</param>
        /// <returns>The decrypted object.</returns>
        byte[] Decrypt(byte[] cipherValue);

        /// <summary>
        /// Encrypt a given byte array.
        /// </summary>
        /// <param name="plainValue">The value to encrypt.</param>
        /// <returns>Encrypted string as byte array.</returns>
        byte[] Encrypt(byte[] plainValue);

        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>The decrypted text.</returns>
        string Decrypt(string cipherText);

        /// <summary>
        /// Encrypts the specified plain text.
        /// The encrypted string is encoded as Base64 so that is can be safely stored as text.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>The encrypted text, converted as Base64.</returns>
        string Encrypt(string plainText);
    }
}
