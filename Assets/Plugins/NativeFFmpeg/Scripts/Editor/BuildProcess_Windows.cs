using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class BuildProcess_Windows
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.StandaloneWindows64)
        {
            string executablePath = Path.GetDirectoryName(pathToBuiltProject);
            string dataDirectory = Path.Combine(executablePath, $"{Application.productName}_Data");
            string pluginsDirectory = Path.Combine(dataDirectory, "Plugins");
            string x86Directory = Path.Combine(pluginsDirectory, "x86_64");
            
            var ffmpegPath = AssetDatabase.GUIDToAssetPath("64186bb4ca0b0054ea1de9d41f9b12a4");
            
            // frameworksPath 경로로 복사
            string sourceFilePath = ffmpegPath;
            string destinationDirectory = x86Directory;
            
            FileOperations.CopyFileToDirectory(sourceFilePath, destinationDirectory);
        }
    }
}
