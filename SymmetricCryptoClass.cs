using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CloudBasedEncryptedCrime
{
    public class SymmetricCryptoClass
    {
        /** SYMMETRIC CRYPTO EXPLANATION
    * For a given secret key k, a simple block cipher that does not use an initialization vector
    * will encrypt the same input block of plain text into the same output block of cipher text.
    * If you have duplicate blocks within your plain text stream, you will have duplicate blocks
    * within your cipher text stream. If unauthorized users know anything about the structure of
    * a block of your plain text, they can use that information to decipher the known cipher text
    * block and possibly recover your key. To combat this problem, information from the previous 
    * block is mixed into the process of encrypting the next block. Thus, the output of two 
    * identical plain text blocks is different. Because this technique uses the previous block 
    * to encrypt the next block, an initialization vector is needed to encrypt the first block 
    * of data.
    * */
        public static Tuple<byte[], byte[]> GenerateSymmetricKeys(string _fileKey)
        {
            //Salt Data helps in creating key which is harder to guess
            string _saltText = "MYSAX901AEQWZ";

            // Generate the key from the shared secret and the salt
            Rfc2898DeriveBytes _key = new Rfc2898DeriveBytes(_fileKey, Encoding.ASCII.GetBytes(_saltText));

            // Create a RijndaelManaged object with the specified key and IV
            RijndaelManaged _aesAlg = new RijndaelManaged();

            //Generate Secret Key of 256 bits(or 32 Bytes) for Rijndael Symmetric Algorithm
            byte[] _mainKeyInByte = _key.GetBytes(_aesAlg.KeySize / 8);//256 / 8 = 32 Bytes

            //The Encryption technique uses the previous block to encrypt the next block, 
            //so an Initialization Vector(IV) is needed to encrypt the first block of data.
            //Generate IV of 128 bits(or 16 Bytes)
            byte[] _IVInByte = _key.GetBytes(_aesAlg.BlockSize / 8);//128 / 8 = 16 Bytes

            // Create a Key Tuple Object with the specified key and IV
            return new Tuple<byte[], byte[]>(_mainKeyInByte, _IVInByte);
        }

        public static bool Encryption(string _filePath, string _fileKey)
        {
            byte[] _plainByte = File.ReadAllBytes(_filePath);

            // Encrypted string to return
            byte[] _cipherByte = null;

            // Create a RijndaelManaged object with the specified key and IV
            RijndaelManaged _aesAlg = new RijndaelManaged();

            try
            {
                //Get RijndaelManaged Keys (MainKey & IV)
                Tuple<byte[], byte[]> _symmetricKeys = GenerateSymmetricKeys(_fileKey);

                //Creates Symmetric Encryptor Object with Secret Key and Initialization Vector
                ICryptoTransform _encryptor = _aesAlg.CreateEncryptor(_symmetricKeys.Item1, _symmetricKeys.Item2);

                // Create the streams used for encryption
                using (MemoryStream _msEncrypt = new MemoryStream())
                {
                    using (CryptoStream _csEncrypt = new CryptoStream(_msEncrypt, _encryptor, CryptoStreamMode.Write))
                    {
                        using (BinaryWriter _bwEncrypt = new BinaryWriter(_csEncrypt))
                        {
                            //Write all data to the stream
                            _bwEncrypt.Write(_plainByte);
                        }
                    }
                    _cipherByte = _msEncrypt.ToArray();
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (_aesAlg != null)
                    _aesAlg.Clear();
            }

            File.WriteAllBytes(_filePath, _cipherByte);
            return true;
        }

        public static bool Decryption(string _filePath, string _fileKey)
        {
            byte[] _cipherByte = File.ReadAllBytes(_filePath);
            // Decrypted string to return
            byte[] _plainByte = null;

            // Create a RijndaelManaged object with the specified key and IV
            RijndaelManaged _aesAlg = new RijndaelManaged();

            try
            {
                //Get RijndaelManaged Keys (MainKey & IV)
                Tuple<byte[], byte[]> _symmetricKeys = GenerateSymmetricKeys(_fileKey);

                // Create a transformer object to perform the stream transform
                ICryptoTransform _decryptor = _aesAlg.CreateDecryptor(_symmetricKeys.Item1, _symmetricKeys.Item2);

                // Create the streams used for decryption               
                using (MemoryStream _msDecrypt = new MemoryStream(_cipherByte))
                {
                    using (CryptoStream _csDecrypt = new CryptoStream(_msDecrypt, _decryptor, CryptoStreamMode.Read))
                    {
                        using (BinaryReader _brDecrypt = new BinaryReader(_csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream and place them in a string
                            _plainByte = _brDecrypt.ReadBytes(_cipherByte.Length);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (_aesAlg != null)
                    _aesAlg.Clear();
            }

            File.WriteAllBytes(_filePath, _plainByte);
            return true;
        }
    }
}