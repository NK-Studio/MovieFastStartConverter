using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static class Imports
        {
            [DllImport("FileDragBridge.dll")]
            public static extern void AddHook(DragEndCallback callback);

            [DllImport("FileDragBridge.dll")]
            public static extern void RemoveHook();
        }

        private delegate void DragEndCallback(int length, IntPtr arrayPointer);

        [AOT.MonoPInvokeCallback(typeof(DragEndCallback))]
        private static void onBegin(int length, IntPtr arrayPointer)
        {
            var paths = new List<string>(length);

            var arrayResult = new IntPtr[length];
            Marshal.Copy(arrayPointer, arrayResult, 0, length);

            for (int i = 0; i < length; i++)
            {
                string res = Marshal.PtrToStringUni(arrayResult[i]);
                paths.Add(res);
            }

            OnDragAndDropFilesPath?.Invoke(paths);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void OnDomainReload()
        {
            OnDragAndDropFilesPath = null;
        }
        
        /// <summary>
        /// Initialization functions that must be called
        /// </summary>
        public static void Initialize()
        {
#if UNITY_STANDALONE_WIN
#if !UNITY_EDITOR
            Imports.AddHook(onBegin);
#else
            Debug.LogWarning("Editor에서는 동작하지 않습니다.");            
#endif
#elif UNITY_STANDALONE_OSX
            Initialize(cs_callback);
#endif
        }
        
        /// <summary>
        /// Unsubscribe to disable drag feature.
        /// </summary>
        public static void Release()
        {
#if !UNITY_EDITOR_WIN && UNITY_STANDALONE_WIN
			Imports.RemoveHook();
#elif UNITY_EDITOR_WIN
            Debug.Log("FileBridge doesn't work on Editor Platform");
#endif
        }
        
        /// <summary>
        /// Callback to return the local path of a drag-and-drop file
        /// </summary>
        public static Action<List<string>> OnDragAndDropFilesPath;
        
       private delegate void callback_delegate(string val);

#if UNITY_STANDALONE_OSX
        [DllImport("UniDragAndDrop")]
        private static extern void Initialize(callback_delegate callback);

        // call from Objective-C
        [MonoPInvokeCallback(typeof(callback_delegate))]
        private static void cs_callback(string val)
        {
            var links = JsonConvert.DeserializeObject<List<string>>(val);
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
    }
}