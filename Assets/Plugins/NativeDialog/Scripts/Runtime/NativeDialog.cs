using System.Runtime.InteropServices;


public static class NativeDialog
{
    private enum UnityMode
    {
        Editor,
        Runtime
    };

#if UNITY_STANDALONE_WIN
    [DllImport("DialogBox.dll", CharSet = CharSet.Unicode)]
    private static extern void show_message_box(UnityMode mode, string title, string message);
#elif UNITY_STANDALONE_OSX
    [DllImport("DialogBox")]
    private static extern void _ShowDialogBox(string title, string message, string yes);
#endif

    public static void ShowDialogBox(string title, string message, string yes)
    {
#if UNITY_STANDALONE_WIN
#if !UNITY_EDITOR
        show_message_box(UnityMode.Runtime, title, message);
#else
        show_message_box(UnityMode.Editor, title, message);
#endif
#elif UNITY_STANDALONE_OSX
        _ShowDialogBox(title, message, yes);
#endif
    }
}