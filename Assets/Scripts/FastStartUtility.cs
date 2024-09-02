using System;
using System.IO;

namespace DefaultNamespace
{
    public class FastStartUtility
    {
        /// <summary>
        /// Checks if the video file at the specified path has Fast Start enabled.
        /// </summary>
        /// <param name="fullFilePath">The full path to the video file.</param>
        /// <returns>True if Fast Start is enabled, otherwise false.</returns>
        public static bool IsVideoFastStartEnabled(string fullFilePath)
        {
            try
            {
                /////////////////////////////////////////////////////////////////////////////////
                // Fast start is enabled if "moov" is before "mdat" in first 4096 bytes fo file
                // https://trac.ffmpeg.org/wiki/HowToCheckIfFaststartIsEnabledForPlayback
                /////////////////////////////////////////////////////////////////////////////////

                byte[] buffer = new byte[4096];
                using (FileStream fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                }

                var byteSpan = buffer.AsSpan();

                var moovPattern = new byte[] { 0x6d, 0x6f, 0x6f, 0x76, 0x00 }.AsSpan();
                var moovIndex = byteSpan.IndexOf(moovPattern);

                var mdatPattern = new byte[] { 0x6d, 0x64, 0x61, 0x74, 0x00 }.AsSpan();
                var mdatIndex = byteSpan.IndexOf(mdatPattern);

                if (moovIndex > 0 && (mdatIndex == -1 || moovIndex < mdatIndex))
                {
                    // Fast start is enabled
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}