using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;
#if UNITY_STANDALONE_OSX
using AOT;
using Newtonsoft.Json;
#endif

namespace NKStudio
{
    public static class UDragAndDrop
    {
        /// <summary>
        /// Callback to return the local path of a drag-and-drop file
        /// </summary>
        public static Action<string[]> OnDragAndDropFilesPath;

        /// <summary>
        /// Initialization functions that must be called
        /// </summary>
        public static void Initialize()
        {
#if UNITY_STANDALONE_OSX
            Initialize(cs_callback);
#endif
        }

        delegate void callback_delegate(string val);

#if UNITY_STANDALONE_OSX
        [DllImport("UniDragAndDrop")]
        private static extern void Initialize(callback_delegate callback);

        // call from Objective-C
        [MonoPInvokeCallback(typeof(callback_delegate))]
        private static void cs_callback(string val)
        {
            var links = JsonConvert.DeserializeObject<string[]>(val);
            OnDragAndDropFilesPath?.Invoke(links);
        }
#endif
        public static async Task<bool> ApplyFastStart(string file)
        {
            var appPath = Application.dataPath;
            var frameworksPath = System.IO.Path.Combine(appPath, "Frameworks");
            var ffmpegPath = System.IO.Path.Combine(frameworksPath, "ffmpeg");

            // file에 확장자 제거
            var outputFile = file.Replace(".mp4", "-faststart.mp4");

            await Task.Run(() =>
            {
                using (Process process = new Process())
                {
                    var command = $"{ffmpegPath} -i \"{file}\" -c copy -movflags +faststart \"{outputFile}\"";
                    process.StartInfo.FileName = "/bin/zsh";
                    process.StartInfo.Arguments = $"-c \"{command}\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = false;

                    process.Start();
                    process.WaitForExit();

                    return true;
                }
            });
            
            return false;
        }
    }
}