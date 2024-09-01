#if UNITY_STANDALONE_OSX
using System.Runtime.InteropServices;
#endif

public class NativeDialog
{
#if UNITY_STANDALONE_OSX
   [DllImport("DialogBox")]
   private static extern void _ShowDialogBox(string title, string message, string yes);
#endif
   
   public static void ShowDialogBox(string title, string message, string yes)
   {
#if UNITY_STANDALONE_OSX
      _ShowDialogBox(title, message, yes);
#endif
   }
}
