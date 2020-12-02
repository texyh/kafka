using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Kafka.Framework
{
    public static class Encode
    {
        public static byte[] FromString(string s) =>
            Encoding.UTF8.GetBytes(s);
    }

    public static class Decode
    {
        public static string IntoString(byte[] data) => Encoding.UTF8.GetString(data);
    }
}