using System.IO;
using System.Threading.Tasks;

namespace MixTelematics.Helpers
{
    public class StringManipulation
    {
        public static async Task<string> ReadNullTerminatedStringAsync(BinaryReader reader, int maxLength)
        {
            await using MemoryStream memoryStream = new MemoryStream();
            byte currentByte;
            while ((currentByte = reader.ReadByte()) != 0 && memoryStream.Length < maxLength)
            {
                memoryStream.WriteByte(currentByte);
            }

            return await Task.FromResult(System.Text.Encoding.UTF8.GetString(memoryStream.ToArray()));
        }
    }
}
