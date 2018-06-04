using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Password
{
    internal static class SecuredStringUtility
    {

        /// <summary>
        /// Converts a string to a SecureString
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SecureString CreateFromString(string source)
        {
            if (source != null && source.Trim().Length > 0)
            {
                SecureString secureString= new SecureString();

                foreach (char c in source) secureString.AppendChar(c);

                secureString.MakeReadOnly();
                return secureString;
            }
            else throw new ArgumentNullException("source");
        }

        /// <summary>
        /// Converts the SecureString to a string and Disposes it.
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
        public static string GetStringFinalize(this SecureString secureString)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                secureString.Dispose();
                secureString = null;
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

    }
}
