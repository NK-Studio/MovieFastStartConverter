using System;
using System.IO;

public class FileOperations
{
    public static void CopyFileToDirectory(string sourceFilePath, string destinationDirectory)
    {
        if (!File.Exists(sourceFilePath))
        {
            throw new FileNotFoundException("Source file not found.", sourceFilePath);
        }

        if (!Directory.Exists(destinationDirectory))
        {
            Directory.CreateDirectory(destinationDirectory);
        }

        string fileName = Path.GetFileName(sourceFilePath);
        string destinationFilePath = Path.Combine(destinationDirectory, fileName);

        File.Copy(sourceFilePath, destinationFilePath, true);
    }
}