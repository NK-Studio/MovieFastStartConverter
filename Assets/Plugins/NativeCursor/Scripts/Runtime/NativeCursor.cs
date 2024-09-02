namespace NKStudio
{
    public static class NativeCursor
    {
        /// <summary>
        /// Sets the cursor for both Windows and Mac.
        /// </summary>
        /// <param name="cursorType">The type of cursor to change to.</param>
        public static void SetCursor(CursorType cursorType)
        {
            UniversalCore.SetCursor(cursorType);
        }

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
    }
}