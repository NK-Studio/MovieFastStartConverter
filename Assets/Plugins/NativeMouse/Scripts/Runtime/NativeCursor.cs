namespace NKStudio
{
    public static class NativeCursor
    {
        /// <summary>
        /// 윈도우와 맥에서 동작하는 커서를 설정합니다.
        /// </summary>
        /// <param name="cursorType">변경할 커서 타입</param>
        public static void SetCursor(CursorType cursorType)
        {
            UniversalCore.SetCursor(cursorType);
        }

        /// <summary>
        /// MacOS에서 동작하는 커서를 설정합니다.
        /// </summary>
        /// <param name="cursorType">변경할 커서 타입</param>
        public static void SetMacCursor(MacCursorType cursorType)
        {
            OSXCore.SetCursor(cursorType);
        }

        /// <summary>
        /// Windows에서 동작하는 커서를 설정합니다.
        /// </summary>
        /// <param name="cursorType">변경할 커서 타입</param>
        public static void SetWindowsCursor(WindowsCursorType cursorType)
        {
            WindowsCore.SetCursor(cursorType);
        }
    }
}