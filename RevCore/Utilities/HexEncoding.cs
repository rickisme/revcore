using System;
using System.Globalization;

namespace Utilities
{
    class HexEncoding
    {
        public static int GetByteCount(string hexString)
        {
            int num = 0;
            for (int i = 0; i < hexString.Length; i++)
            {
                char ch = hexString[i];
                if (IsHexDigit(ch))
                {
                    num++;
                }
            }
            if ((num % 2) != 0)
            {
                num--;
            }
            return (num / 2);
        }

        public static byte[] GetBytes(string hexString, out int discarded)
        {
            int num;
            discarded = 0;
            string str = "";
            for (num = 0; num < hexString.Length; num++)
            {
                char ch = hexString[num];
                if (IsHexDigit(ch))
                {
                    str = str + ch;
                }
                else
                {
                    discarded++;
                }
            }
            if ((str.Length % 2) != 0)
            {
                discarded++;
                str = str.Substring(0, str.Length - 1);
            }
            int num2 = str.Length / 2;
            byte[] buffer = new byte[num2];
            int num3 = 0;
            for (num = 0; num < buffer.Length; num++)
            {
                string str2 = new string(new char[] { str[num3], str[num3 + 1] });
                buffer[num] = HexToByte(str2);
                num3 += 2;
            }
            return buffer;
        }

        private static byte HexToByte(string string_0)
        {
            if ((string_0.Length > 2) || (string_0.Length <= 0))
            {
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            }
            return byte.Parse(string_0, NumberStyles.HexNumber);
        }

        public static bool InHexFormat(string hexString)
        {
            foreach (char ch in hexString)
            {
                if (!IsHexDigit(ch))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsHexDigit(char char_0)
        {
            int num = Convert.ToInt32('A');
            int num2 = Convert.ToInt32('0');
            char_0 = char.ToUpper(char_0);
            int num3 = Convert.ToInt32(char_0);
            return (((num3 >= num) && (num3 < (num + 6))) || ((num3 >= num2) && (num3 < (num2 + 10))));
        }

        public static string ToString(byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str = str + bytes[i].ToString("X2");
            }
            return str;
        }

    }
}
