namespace NKStudio
{
    public static class UniversalCore
    {
        /// <summary>
        /// 커서를 적용합니다.
        /// </summary>
        /// <param name="cursorType">적용할 커서의 타입입니다.</param>
        internal static void SetCursor(CursorType cursorType)
        {
#if UNITY_STANDALONE_WIN
        var universalCursorType = (WindowsCore.WindowsCursorType)cursorType;
        WindowsCore.ApplyCursor(universalCursorType);
#elif UNITY_STANDALONE_OSX
            var universalCursorType = (MacCursorType)cursorType;
            OSXCore.SetCursor(universalCursorType);
#endif
        }
    }
}