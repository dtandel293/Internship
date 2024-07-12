using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Helper
{
    public class EncDecPassword
    {
        public static string secretKey = "thereisalwaysahope";

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            else
            {
                password = password + secretKey;
                var passwordInBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordInBytes);
            }
        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            else
            {
                var encodedBytes = Convert.FromBase64String(password);
                var actualPassword = Encoding.UTF8.GetString(encodedBytes);
                actualPassword = actualPassword.Substring(0, actualPassword.Length - secretKey.Length);
                return actualPassword;

            }
        }
    }
}
