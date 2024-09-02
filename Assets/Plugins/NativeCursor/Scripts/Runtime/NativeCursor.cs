namespace NKStudio
{
    public static class NativeCursor
    {
        /// <summary>
        /// Sets the cursor for MacOS.
        /// </summary>
        /// <param name="cursorType">The type of cursor to change to.</param>
        public static void SetMacCursor(MacOSCursorType cursorType)
        {
            OSXCore.SetCursor(cursorType);
        }

        /// <summary>
        /// Sets the cursor for Windows.
        /// </summary>
        /// <param name="cursorType">The type of cursor to change to.</param>
        public static void SetWindowsCursor(WindowsCursorType cursorType)
        {
            WindowsCore.SetCursor(cursorType);
        }

        /// <summary>
        /// Sets the cursor for both Windows and Mac.
        /// </summary>
        /// <param name="cursorType">The type of cursor to change to.</param>
        public static void SetCursor(CursorType cursorType)
        {
            UniversalCore.SetCursor(cursorType);
        }

        /// <summary>
        /// Releases the cursor textures.
        /// For Windows, turn off the cursor texture.
        /// </summary>
        public static void Release()
        {
#if UNITY_STANDALONE_WIN
            WindowsCore.Release();
#elif UNITY_STANDALONE_OSX
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
#endif
        }
    }
}