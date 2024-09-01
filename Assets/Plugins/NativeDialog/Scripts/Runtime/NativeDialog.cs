using System.Runtime.InteropServices;


public static class NativeDialog
{
#if UNITY_STANDALONE_WIN
    [DllImport("DialogBox.dll", CharSet = CharSet.Unicode)]
    private static extern void ShowMessageBox(string title, string message);
#elif UNITY_STANDALONE_OSX
    [DllImport("DialogBox")]
    private static extern void _ShowDialogBox(string title, string message, string yes);
#endif

    public static void ShowDialogBox(string title, string message, string yes)
    {
#if UNITY_STANDALONE_WIN
        ShowMessageBox(title, message);
#elif UNITY_STANDALONE_OSX
        _ShowDialogBox(title, message, yes);
#endif
    }
}