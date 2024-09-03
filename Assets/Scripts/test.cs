using NKStudio;
using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            var extension = new[]
            {
                new ExtensionFilter("MP4 Files", "mp4"),
                new ExtensionFilter("All Files", "*"),
            };
         
            var result = NativeFileBrowser.OpenFolderPanel("Open File", "", extension, true);

            if (result == null)
            {
                return;
            }
            foreach (var d in result)
            {
                Debug.Log(d);
            }
        }
    }
}