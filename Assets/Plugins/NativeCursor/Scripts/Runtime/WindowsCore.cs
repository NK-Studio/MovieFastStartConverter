#if UNITY_STANDALONE_WIN
using System.Runtime.InteropServices;
#endif

namespace NKStudio
{
    public static class WindowsCore
    {
#if UNITY_STANDALONE_WIN
        [DllImport("NativeCursor.dll")]
        private static extern void _SetCursor(WindowsCursorType cursorType);
#endif
        
        /// <summary>
        /// Sets the cursor.
        /// </summary>
        internal static void SetCursor(WindowsCursorType cursorType)
        {
#if UNITY_STANDALONE_WIN
            _SetCursor(cursorType);
#endif
        }
    }
}