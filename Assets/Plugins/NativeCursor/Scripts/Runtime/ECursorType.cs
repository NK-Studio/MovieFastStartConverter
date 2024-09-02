namespace NKStudio
{
    public enum MacOSCursorType
    {
        // 1. Matching cursors
        Arrow, // Match: ARROW (Windows)
        IBeam, // Match: IBEAM (Windows)
        Crosshair, // Match: CROSS (Windows)
        PointingHand, // Match: HAND (Windows)
        OperationNotAllowed, // Match: NO (Windows)
        ResizeUpDown, // Match: SIZENS (Windows)
        ResizeLeftRight, // Match: SIZEWE (Windows)

        // 2. Non-matching cursors
        ClosedHand, // No match: No direct corresponding cursor in Windows
        OpenHand, // No match: No direct corresponding cursor in Windows
        ResizeUp, // No match: UP (partial match)
        ResizeDown, // No match: SIZENESW (partial match)
        DisappearingItem, // No match: No direct corresponding cursor in Windows
        IBeamCursorForVerticalLayout, // No match: No direct corresponding cursor in Windows
        DragLink, // No match: No direct corresponding cursor in Windows
        DragCopy, // No match: No direct corresponding cursor in Windows
        ContextualMenu // No match: No direct corresponding cursor in Windows
    };

    public enum WindowsCursorType
    {
        // 1. 매치되는 커서
        ARROW, // 매치: Arrow
        IBEAM, // 매치: IBeam
        CROSS, // 매치: Crosshair
        HAND, // 매치: PointingHand
        NO, // 매치: OperationNotAllowed
        SIZENS, // 매치: ResizeUpDown
        SIZEWE, // 매치: ResizeLeftRight

        // 2. 매치되지 않는 커서
        UPARROW, // 매치 불가: macOS에서 직접 대응 커서 없음
        UP, // 매치 불가: ResizeUp (부분 매치)
        SIZENWSE, // 매치 불가: macOS에서 직접 대응 커서 없음
        SIZENESW, // 매치 불가: ResizeDown (부분 매치)
        PERSON, // 매치 불가: macOS에서 직접 대응 커서 없음
        PIN, // 매치 불가: macOS에서 직접 대응 커서 없음
        HELP, // 매치 불가: macOS에서 직접 대응 커서 없음
        WAIT, // 매치 불가: macOS에서 직접 대응 커서 없음
        SIZEALL, // 매치 불가: macOS에서 직접 대응 커서 없음
        APPSTARTING // 매치 불가: macOS에서 직접 대응 커서 없음
    };

    public enum CursorType
    {
        // 1. Matching cursors
        ARROW, // Match: Arrow
        IBEAM, // Match: IBeam
        CROSS, // Match: Crosshair
        HAND, // Match: PointingHand
        NO, // Match: OperationNotAllowed
        UpDown, // Match: ResizeUpDown
        LeftRight, // Match: ResizeLeftRight
    }
}