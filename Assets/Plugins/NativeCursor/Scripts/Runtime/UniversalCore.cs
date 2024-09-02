namespace NKStudio
{
    public static class UniversalCore
    {
        /// <summary>
        /// Sets the cursor.
        /// </summary>
        /// <param name="cursorType">The type of cursor to set.</param>
        internal static void SetCursor(CursorType cursorType)
        {
#if UNITY_STANDALONE_WIN
            var universalCursorType = (WindowsCursorType)cursorType;
            WindowsCore.SetCursor(universalCursorType);
#elif UNITY_STANDALONE_OSX
            var universalCursorType = (MacOSCursorType)cursorType;
            OSXCore.SetCursor(universalCursorType);
#endif
        }
    }
}