
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Common
{
    public class JMessage
    {
        public bool success { get; set; }
        public int code { get; set; }
        public object message { get; set; }
        public object data { get; set; }
        public bool errors { get; set; }
    }
}
