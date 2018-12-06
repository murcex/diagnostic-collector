namespace Implements
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Encryption : IDisposable
    {
        /// <summary>
        /// Encrytion Constructor - Key and IV required.
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_iv"></param>
        public Encryption(string _key, string _iv)
        {
            if (_iv.Length == 16)
            {
                Set(_key, _iv);
            }
            else
            {
                throw new Exception($"IV must be 16 characters! Current: {_iv.Length}");
            }
        }

        /// <summary>
        /// ASE Password.
        /// </summary>
        private string ASEPassword { get; set; }

        /// <summary>
        /// ASE IV.
        /// </summary>
        private string ASEIV { get; set; }

        /// <summary>
        /// Disposable flag.
        /// </summary>
        bool dispose = false;

        /// <summary>
        /// Set the Key and IV used by the Encryption instance.
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_iv"></param>
        public void Set(string _key, string _iv)
        {
            ASEPassword = _key;
            ASEIV = _iv;
        }

        /// <summary>
        /// Encrypting process; encrypt a List of strings.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>List of strings in Base64</returns>
        public List<string> Encrypt(List<string> lines)
        {
            List<string> encryptedLines = new List<string>();

            byte[] passwordBytes = Encoding.UTF8.GetBytes(ASEPassword);
            byte[] vectorBytes = Encoding.ASCII.GetBytes(ASEIV);

            try
            {
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                foreach (string line in lines)
                {
                    byte[] encryptedBytes = null;
                    byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(line);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Aes myAes = Aes.Create())
                        {
                            myAes.KeySize = 256;
                            myAes.BlockSize = 128;

                            myAes.Key = passwordBytes;
                            myAes.IV = vectorBytes;

                            myAes.Mode = CipherMode.CBC;

                            using (var cs = new CryptoStream(ms, myAes.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                                cs.Close();
                            }

                            encryptedBytes = ms.ToArray();

                            string encryptedLine = Convert.ToBase64String(encryptedBytes);

                            encryptedLines.Add(encryptedLine);
                        }
                    }
                }

                return encryptedLines;
            }
            catch (Exception e)
            {
                throw new Exception($"Internal Exception [Encrypt]: {e.ToString()}");
            }
        }

        /// <summary>
        /// Decrypting process; decrypt a List of (Base64) strings.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>List of strings in UTF8</returns>
        public List<string> Decrypt(List<string> lines)
        {
            List<string> decryptedLines = new List<string>();

            byte[] passwordBytes = Encoding.UTF8.GetBytes(ASEPassword);
            byte[] vectorBytes = Encoding.ASCII.GetBytes(ASEIV);

            try
            {
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                foreach (string line in lines)
                {
                    byte[] decryptedBytes = null;
                    byte[] bytesToBeDecrypted = Convert.FromBase64String(line);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Aes myAes = Aes.Create())
                        {
                            myAes.KeySize = 256;
                            myAes.BlockSize = 128;

                            myAes.Key = passwordBytes;
                            myAes.IV = vectorBytes;

                            using (var cs = new CryptoStream(ms, myAes.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                                cs.Close();
                            }

                            decryptedBytes = ms.ToArray();

                            string decryptedLine = Encoding.UTF8.GetString(decryptedBytes);

                            decryptedLines.Add(decryptedLine);
                        }
                    }
                }

                return decryptedLines;
            }
            catch (Exception e)
            {
                throw new Exception($"Internal Exception [Decrypt]: {e.ToString()}");
            }
        }

        /// <summary>
        /// Disposable Logic.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }
            
            //dispose unmanaged resources
            dispose = true;
        }

        /// <summary>
        /// Disposable process.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
