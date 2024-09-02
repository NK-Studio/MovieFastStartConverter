namespace NKStudio
{
    public enum MacCursorType
    {
        Arrow = 0,
        IBeam = 1,
        Crosshair = 2,
        OpenHand = 3,
        PointingHand = 4,
        ResizeUp = 5,
        ResizeDown = 6,
        ResizeUpDown = 7,
        DisappearingItem = 7,
        IBeamCursorForVerticalLayout = 9,
        OperationNotAllowed = 10,
        DragLink = 11,
        DragCopy = 12,
        ContextualMenu = 13,
    };
    
    public enum CursorType
    {
#if UNITY_STANDALONE_OSX
        Hand = 4,
#elif UNITY_STANDALONE_WIN
        Hand = 32649,
#endif

#if UNITY_STANDALONE_OSX
        Standard = 0,
#elif UNITY_STANDALONE_WIN
        Standard = 32512,
#endif
    }
    
    public enum WindowsCursorType
    {
        StandardArrow = 32512,
        Hand = 32649,
        // 추가 커서 타입...
    }
}