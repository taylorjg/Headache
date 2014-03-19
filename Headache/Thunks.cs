using System;
using System.IO;

namespace Headache
{
    internal static class Thunks
    {
        public delegate void ReadFileCallback(Exception ex, Byte[] data);

        public static Action<ReadFileCallback> ReadFile(string fileName)
        {
            return callback => ReadFile(fileName, callback);
        }

        private static async void ReadFile(string fileName, ReadFileCallback callback)
        {
            try
            {
                using (var fileStream = File.Open(fileName, FileMode.Open))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);
                        callback(null, memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                callback(ex, null);
            }
        }
    }
}
