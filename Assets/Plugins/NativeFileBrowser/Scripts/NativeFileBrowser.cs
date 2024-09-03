using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NKStudio
{
    public class NativeFileBrowser
    {
        /// <summary>
        /// Imports the native method to show a file dialog.
        /// </summary>
        /// <returns>A string containing the selected file paths separated by '|'.</returns>
        [DllImport("NativeOpenPanel.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr _ShowFileDialog(string title, string directory, string filter, bool multiSelect);

        /// <summary>
        /// Shows a file dialog and returns the selected file paths as a list of strings.
        /// </summary>
        /// <returns>A list of selected file paths, or null if no files were selected.</returns>
        public static List<string> OpenFolderPanel(string title, string directory, ExtensionFilter filter,
            bool multiSelect = false)
        {
            IntPtr ptr = _ShowFileDialog(title, directory, filter.ToString(), multiSelect);

            if (ptr != IntPtr.Zero)
            {
                try
                {
                    string result = Marshal.PtrToStringUni(ptr);
                    if (result != null)
                    {
                        List<string> files = new List<string>(result.Split('|'));
                        return files;
                    }
                }
                finally
                {
                    Marshal.FreeCoTaskMem(ptr);
                }
            }

            return null;
        }

        /// <summary>
        /// Shows a file dialog and returns the selected file paths as a list of strings.
        /// </summary>
        /// <returns>A list of selected file paths, or null if no files were selected.</returns>
        public static List<string> OpenFolderPanel(string title, string directory, ExtensionFilter[] filter,
            bool multiSelect = false)
        {
            string extensions = ExtensionFilter.ToString(filter);

            IntPtr ptr = _ShowFileDialog(title, directory, extensions, multiSelect);

            if (ptr != IntPtr.Zero)
            {
                try
                {
                    string result = Marshal.PtrToStringUni(ptr);
                    if (result != null)
                    {
                        List<string> files = new List<string>(result.Split('|'));
                        return files;
                    }
                }
                finally
                {
                    Marshal.FreeCoTaskMem(ptr);
                }
            }

            return null;
        }
    }
}