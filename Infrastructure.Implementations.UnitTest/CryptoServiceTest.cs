using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Infrastructure.Implementations.UnitTest
{
    [TestClass]
    public class CryptoServiceTest
    {
        [TestMethod]
        public void ProvidedInputStringToEncryptAfter_Decrypt_TheyShouldmatch()
        {
            CryptoService service = new CryptoService();
            var encryptedString = service.Encrypt("Ashwin");
            var decryptedString = service.Decrypt(encryptedString);
            Assert.AreEqual("Ashwin", decryptedString);

        }

        [TestMethod]
        public void ProvidedInputStringToEncryptAfter_Encrypt_ShouldReturnEncryptedString()
        {
            CryptoService service = new CryptoService();
            var encryptedString = service.Encrypt("Ashwin");
            Assert.IsNotNull(encryptedString);

        }


        [TestMethod]
        public void ProvidedInputByteArrayToEncryptAfter_Encrypt_ShouldReturnEncryptedByteArray()
        {
            CryptoService service = new CryptoService();
            var encryptedByte = service.Encrypt(new byte[2] { 10, 11 });
            Assert.IsNotNull(encryptedByte);

        }

        [TestMethod]
        public void ProvidedInputBytesToEncryptAfter_Decrypt_TheyShouldmatch()
        {
            CryptoService service = new CryptoService();
            var encryptedByte = service.Encrypt(new byte[2] { 10, 11 });
            var decryptedByte = service.Decrypt(encryptedByte);
            Assert.AreEqual(10, decryptedByte[0]);
            Assert.AreEqual(11, decryptedByte[1]);

        }
    }
}
