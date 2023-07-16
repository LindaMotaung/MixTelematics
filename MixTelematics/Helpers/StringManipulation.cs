using System.IO;

namespace MixTelematics.Helpers
{
    public class StringManipulation
    {
        public static string ReadNullTerminatedString(BinaryReader reader, int maxLength)
        {
            var chars = new char[maxLength];

            for (var i = 0; i < maxLength; i++)
            {
                chars[i] = reader.ReadChar();
                if (chars[i] == '\0')
                    break;
            }

            return new string(chars).TrimEnd('\0');
        }
    }
}
