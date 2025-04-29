using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Security.Policy;

namespace AccessControl
{
    public class User
    {
        byte[] salt = new byte[16];
        public string UserName { get; set; }
        public string Password_PlainText { get; set; }
        public string Password_Hash { get; set; }
        public string Password_SaltedHash { get; set; }
        public string Password_SaltedHashSlow { get; set; }
        public string Salt { get; set; }


        public User (string _UserName, string _Password)
        {
            UserName = _UserName;
            Password_PlainText = _Password;
        }

        public User ()
        {

        }

        public string passwordToHash(string passwd) {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] passwdBytes = Encoding.UTF8.GetBytes(passwd);
                byte[] hash = mySHA256.ComputeHash(passwdBytes);
                return Convert.ToBase64String(hash);
            }
        }

        public string passwordToHashSalt(string passwd, byte[] _salt)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] passwdBytes = Encoding.UTF8.GetBytes(passwd);
                byte[] passwd_Salt = new byte[passwdBytes.Length + _salt.Length];
                Buffer.BlockCopy(passwdBytes, 0, passwd_Salt, 0, passwdBytes.Length);
                Buffer.BlockCopy(_salt, 0, passwd_Salt, passwdBytes.Length, _salt.Length);
                byte[] hash_Salt = mySHA256.ComputeHash(passwd_Salt);
                return Convert.ToBase64String(hash_Salt);
            }
        }

        public string passwordToHashSalSlow(string passwd, byte[] _salt)
        {

            int iterations = 10000;
            using (var pbkdf2 = new Rfc2898DeriveBytes(passwd, _salt, iterations, HashAlgorithmName.SHA256))
            {
                byte[] passwod_Byte_SlowHash_Salt = pbkdf2.GetBytes(32);

                byte[] passwod_Byte_hash_total = new byte[passwod_Byte_SlowHash_Salt.Length + _salt.Length];
                Array.Copy(_salt, 0, passwod_Byte_hash_total, 0, _salt.Length);
                Array.Copy(passwod_Byte_SlowHash_Salt, 0, passwod_Byte_hash_total, _salt.Length, passwod_Byte_SlowHash_Salt.Length);

                return Convert.ToBase64String(passwod_Byte_hash_total);
            }
        }


        public void AddUser()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                this.Salt = Convert.ToBase64String(salt);
            }
            this.Password_Hash = passwordToHash(this.Password_PlainText);
            this.Password_SaltedHash = passwordToHashSalt(this.Password_PlainText, salt);
            this.Password_SaltedHashSlow = passwordToHashSalSlow(this.Password_PlainText, salt);


            ((App)Application.Current).Database.Add(this);            
        }

        public bool Validate (string _UserName, string _Password)
        {
            User MyUser = ((App)Application.Current).Database.Find(User => User.UserName == _UserName);

            //Validate amb Text pla
            //if (!ReferenceEquals(MyUser, null))
            //{
            //    if (MyUser.Password_PlainText.Equals(_Password))
            //        return true;
            //    else
            //        return false;
            //}
            //else
            //{
            //    return false;
            //}

            //Validate amb Hash (comenta l'anterior validació)
            //if (!ReferenceEquals(MyUser, null))
            //{
            //    if (MyUser.Password_Hash.Equals(passwordToHash(_Password)))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}

            //Validate amb Hash i salt (comenta l'anterior validació)
            //if (!ReferenceEquals(MyUser, null))
            //{
            //    byte[] storedSalt = Convert.FromBase64String(MyUser.Salt);
            //    if (MyUser.Password_SaltedHash.Equals(passwordToHashSalt(_Password, storedSalt)))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}

            //Validate amb Hash slow i salt. Pots utilitzar la classe Rfc2898DeriveBytes
            if (!ReferenceEquals(MyUser, null))
            {
                byte[] storedSalt = Convert.FromBase64String(MyUser.Salt);
                if (MyUser.Password_SaltedHashSlow.Equals(passwordToHashSalSlow(_Password, storedSalt)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        string BytesToStringHex (byte[] result)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in result)
                stringBuilder.AppendFormat("{0:x2}", b);

            return stringBuilder.ToString();
        }
    }

}
