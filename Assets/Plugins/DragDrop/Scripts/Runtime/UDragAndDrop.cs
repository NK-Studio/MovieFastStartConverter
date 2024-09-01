using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
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

#if UNITY_STANDALONE_WIN
#if UNITY_EDITOR
            appPath = appPath.Replace("/Assets", "");
            var ffmpegPath = appPath + "/Assets/Plugins/DragDrop/Plugins/Windows/ffmpeg.exe";
#else
            var ffmpegPath = appPath + "/Plugins/x86_64/ffmpeg.exe";
#endif
#elif UNITY_STANDALONE_OSX
            var frameworksPath = System.IO.Path.Combine(appPath, "Frameworks");
            var ffmpegPath = System.IO.Path.Combine(frameworksPath, "ffmpeg");
#endif

            // file에 확장자 제거
            var outputFile = file.Replace(".mp4", "-faststart.mp4");
            
            // fast start 적용 명령
            string command = $"-i \"{file}\" -c copy -movflags +faststart \"{outputFile}\"";

            await Task.Run(() =>
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = ffmpegPath;
                    process.StartInfo.Arguments = $"{command}";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();
                    
                    return true;
                }
            });
            
            return false;
        }
        
        private static string RunCmd(string args)

        {
            Process pro = new Process();

 

            pro.StartInfo.FileName = "ffmpeg.exe"; // 환경 변수 사용시 ffmpeg.exe로 호출 가능

 

            pro.StartInfo.CreateNoWindow = true;  // cmd창을 띄우지 안도록 하기

            pro.StartInfo.UseShellExecute = false;

            //process.RedirectStandardOutput = true;  // cmd창에서 데이터를 가져오기

            pro.StartInfo.RedirectStandardInput = true;  // cmd창으로 데이터 보내기

            pro.StartInfo.RedirectStandardError = true;  // cmd창에서 오류 내용 가져오기

            pro.EnableRaisingEvents = true;

 

            pro.StartInfo.Arguments = args;

            pro.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            
            pro.Start();
            
            pro.StandardInput.Close();

 

            string result = pro.StandardError.ReadToEnd().ToLower();

 

            pro.WaitForExit();

            pro.StandardError.Close();

 

            return result;

        }
    }
}