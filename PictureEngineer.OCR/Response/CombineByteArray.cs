using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Ocr.Response
{
    public class CombineByteArray
    {
        public byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
    }
}
