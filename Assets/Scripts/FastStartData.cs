using Unity.Properties;
using UnityEngine.UIElements;

public class FastStartData
{
    public string URL;
    public bool IsFinishConvert;
    
    [CreateProperty]
    public Visibility Visibility => IsFinishConvert ? Visibility.Visible : Visibility.Hidden;

    public FastStartData()
    {
    }

    public FastStartData(string url, bool isFinishConvert)
    {
        URL = url;
        IsFinishConvert = isFinishConvert;
    }
}