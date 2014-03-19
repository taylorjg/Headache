using System;
using System.IO;

namespace Headache
{
    internal static class Thunks
    {
        public static Action<Action<Exception, Byte[]>> ReadFile(string fileName)
        {
            return callback => ReadFile(fileName, callback);
        }

        private static async void ReadFile(string fileName, Action<Exception, Byte[]> callback)
        {
            try
            {
                using (var fileStream = File.Open(fileName, FileMode.Open))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileStream.CopyToAsync(memoryStream);
                        var data = memoryStream.ToArray();
                        callback(null, data);
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
