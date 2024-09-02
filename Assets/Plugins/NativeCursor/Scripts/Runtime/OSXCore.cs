#if UNITY_STANDALONE_OSX
using System.Runtime.InteropServices;
#endif

namespace NKStudio
{
    public static class OSXCore
    {
#if UNITY_STANDALONE_OSX
        [DllImport("NativeCursor")]
        private static extern void _SetCursor(MacOSCursorType cursorType);
#endif

        /// <summary>
        /// Sets the cursor.
        /// </summary>
        /// <param name="cursorType">The type of cursor to set.</param>
        internal static void SetCursor(MacOSCursorType cursorType)
        {
#if UNITY_STANDALONE_OSX
            _SetCursor(cursorType);
#endif
        }
    }
}