using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class BuildProcess_MacOS 
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.StandaloneOSX)
        {
            // 빌드된 .app 번들의 경로
            string appPath = Path.Combine(pathToBuiltProject, "Contents");
            string frameworksPath = Path.Combine(appPath, "Frameworks");

            // Frameworks 폴더 생성
            if (!Directory.Exists(frameworksPath))
            {
                Directory.CreateDirectory(frameworksPath);
            }

            var ffmpegPath = AssetDatabase.GUIDToAssetPath("3784cd0d07ec34f65876587a43e8ee4b");
            
            // frameworksPath 경로로 복사
            string sourceFilePath = ffmpegPath;
            string destinationDirectory = frameworksPath;

            FileOperations.CopyFileToDirectory(sourceFilePath, destinationDirectory);
        }
    }
}
