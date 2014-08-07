using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
namespace Coneixement.Infrastructure.Helpers
{
   public class FileEncryption
    {
        ///<summary>
        /// Vivek Sriavstava
        ///
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                string cryptFile = outputFile;
                using (FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create))
                {
                    RijndaelManaged RMCrypto = new RijndaelManaged()
                    {
                        Mode = CipherMode.CBC,
                        Padding = PaddingMode.PKCS7,
                        KeySize = 128,
                        BlockSize = 128
                    };
                    CryptoStream cs = new CryptoStream(fsCrypt,
                        RMCrypto.CreateEncryptor(key, key),
                        CryptoStreamMode.Write);
                    FileStream fsIn = new FileStream(inputFile, FileMode.Open);
                    int data;
                    while ((data = fsIn.ReadByte()) != -1)
                        cs.WriteByte((byte)data);
                    fsIn.Close();
                    cs.Close();
                    fsCrypt.Close();
                }
            }
            catch
            {
                //MessageBox.Show("Encryption failed!", "Error");
            }
        }
        ///<summary>
        /// Vivek Srivastava
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static void DecryptFile(string inputFile, string outputFile)
        {
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                try
                {
                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        RijndaelManaged RMCrypto = new RijndaelManaged()
                        {
                            Mode = CipherMode.CBC,
                            Padding = PaddingMode.PKCS7,
                            KeySize = 128,
                            BlockSize = 128
                        };
                        CryptoStream cs = new CryptoStream(fsCrypt,
                         RMCrypto.CreateDecryptor(key, key),
                         CryptoStreamMode.Read);
                        FileStream fsOut = new FileStream(outputFile, FileMode.Create);
                        int data;
                        while ((data = cs.ReadByte()) != -1)
                            fsOut.WriteByte((byte)data);
                        fsOut.Close();
                        fsOut.Dispose();
                        cs.Close();
                        fsCrypt.Close();
                        fsOut.Dispose();
                    }
                }
                catch
                {
                }
            }
        }
    }
}
