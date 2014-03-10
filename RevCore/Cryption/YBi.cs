using System;
using System.Collections;
using System.IO;

namespace Cryption
{
    public class YBi
    {
        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] numArray = new byte[bits.Length / 8];
            bits.CopyTo(numArray, 0);
            return numArray;
        }

        public static BitArray ByteArraytoBitArray(byte[] bytes)
        {
            BitArray bitArrays = new BitArray(bytes);
            return bitArrays;
        }

        public static byte[] Encrypt(string DecYbiFile)
        {
            byte[] numArray;
            Stream stream = File.OpenRead(DecYbiFile);
            using (stream)
            {
                numArray = new byte[stream.Length];
                stream.Read(numArray, 0, (int)numArray.Length);
            }
            for (int i = 0; i < (int)numArray.Length; i = i + 4)
            {
                int num = BitConverter.ToInt32(numArray, i);
                int num1 = 0;
                num1 = num1 | YBi.MoveBit(num, 0, 26);
                num1 = num1 | YBi.MoveBit(num, 1, 31);
                num1 = num1 | YBi.MoveBit(num, 2, 17);
                num1 = num1 | YBi.MoveBit(num, 3, 10);
                num1 = num1 | YBi.MoveBit(num, 4, 30);
                num1 = num1 | YBi.MoveBit(num, 5, 16);
                num1 = num1 | YBi.MoveBit(num, 6, 24);
                num1 = num1 | YBi.MoveBit(num, 7, 2);
                num1 = num1 | YBi.MoveBit(num, 8, 29);
                num1 = num1 | YBi.MoveBit(num, 9, 8);
                num1 = num1 | YBi.MoveBit(num, 10, 20);
                num1 = num1 | YBi.MoveBit(num, 11, 15);
                num1 = num1 | YBi.MoveBit(num, 12, 28);
                num1 = num1 | YBi.MoveBit(num, 13, 11);
                num1 = num1 | YBi.MoveBit(num, 14, 13);
                num1 = num1 | YBi.MoveBit(num, 15, 4);
                num1 = num1 | YBi.MoveBit(num, 16, 19);
                num1 = num1 | YBi.MoveBit(num, 17, 23);
                num1 = num1 | YBi.MoveBit(num, 18, 0);
                num1 = num1 | YBi.MoveBit(num, 19, 12);
                num1 = num1 | YBi.MoveBit(num, 20, 14);
                num1 = num1 | YBi.MoveBit(num, 21, 27);
                num1 = num1 | YBi.MoveBit(num, 22, 6);
                num1 = num1 | YBi.MoveBit(num, 23, 18);
                num1 = num1 | YBi.MoveBit(num, 24, 21);
                num1 = num1 | YBi.MoveBit(num, 25, 3);
                num1 = num1 | YBi.MoveBit(num, 26, 9);
                num1 = num1 | YBi.MoveBit(num, 27, 7);
                num1 = num1 | YBi.MoveBit(num, 28, 22);
                num1 = num1 | YBi.MoveBit(num, 29, 1);
                num1 = num1 | YBi.MoveBit(num, 30, 25);
                num1 = num1 | YBi.MoveBit(num, 31, 5);
                numArray[i] = (byte)(num1 & 255);
                numArray[i + 1] = (byte)(num1 >> 8 & 255);
                numArray[i + 2] = (byte)(num1 >> 16 & 255);
                numArray[i + 3] = (byte)(num1 >> 24 & 255);
            }
            return numArray;
        }

        public static byte[] Decrypt(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i = i + 4)
            {
                int num = BitConverter.ToInt32(buffer, i);
                int num1 = 0;
                num1 = num1 | YBi.MoveBit(num, 26, 0);
                num1 = num1 | YBi.MoveBit(num, 31, 1);
                num1 = num1 | YBi.MoveBit(num, 17, 2);
                num1 = num1 | YBi.MoveBit(num, 10, 3);
                num1 = num1 | YBi.MoveBit(num, 30, 4);
                num1 = num1 | YBi.MoveBit(num, 16, 5);
                num1 = num1 | YBi.MoveBit(num, 24, 6);
                num1 = num1 | YBi.MoveBit(num, 2, 7);
                num1 = num1 | YBi.MoveBit(num, 29, 8);
                num1 = num1 | YBi.MoveBit(num, 8, 9);
                num1 = num1 | YBi.MoveBit(num, 20, 10);
                num1 = num1 | YBi.MoveBit(num, 15, 11);
                num1 = num1 | YBi.MoveBit(num, 28, 12);
                num1 = num1 | YBi.MoveBit(num, 11, 13);
                num1 = num1 | YBi.MoveBit(num, 13, 14);
                num1 = num1 | YBi.MoveBit(num, 4, 15);
                num1 = num1 | YBi.MoveBit(num, 19, 16);
                num1 = num1 | YBi.MoveBit(num, 23, 17);
                num1 = num1 | YBi.MoveBit(num, 0, 18);
                num1 = num1 | YBi.MoveBit(num, 12, 19);
                num1 = num1 | YBi.MoveBit(num, 14, 20);
                num1 = num1 | YBi.MoveBit(num, 27, 21);
                num1 = num1 | YBi.MoveBit(num, 6, 22);
                num1 = num1 | YBi.MoveBit(num, 18, 23);
                num1 = num1 | YBi.MoveBit(num, 21, 24);
                num1 = num1 | YBi.MoveBit(num, 3, 25);
                num1 = num1 | YBi.MoveBit(num, 9, 26);
                num1 = num1 | YBi.MoveBit(num, 7, 27);
                num1 = num1 | YBi.MoveBit(num, 22, 28);
                num1 = num1 | YBi.MoveBit(num, 1, 29);
                num1 = num1 | YBi.MoveBit(num, 25, 30);
                num1 = num1 | YBi.MoveBit(num, 5, 31);
                buffer[i] = (byte)(num1 & 255);
                buffer[i + 1] = (byte)(num1 >> 8 & 255);
                buffer[i + 2] = (byte)(num1 >> 16 & 255);
                buffer[i + 3] = (byte)(num1 >> 24 & 255);
            }
            return buffer;
        }

        private static int MoveBit(int src, int oldLoc, int newLoc)
        {
            return (src >> (oldLoc & 31) & 1) << (newLoc & 31);
        }
    }
}
