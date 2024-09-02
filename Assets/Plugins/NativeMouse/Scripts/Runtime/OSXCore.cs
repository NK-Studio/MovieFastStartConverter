using System.Runtime.InteropServices;

namespace NKStudio
{
    public static class OSXCore
    {
#if UNITY_STANDALONE_OSX
        [DllImport("NativeMouse")]
        private static extern void _SetCursor(MacCursorType cursorType);
#endif

        /// <summary>
        /// 커서를 적용합니다.
        /// </summary>
        /// <param name="cursorType">적용할 커서의 타입입니다.</param>
        internal static void SetCursor(MacCursorType cursorType)
        {
#if UNITY_STANDALONE_OSX
            _SetCursor(cursorType);
#endif
        }
    }
}