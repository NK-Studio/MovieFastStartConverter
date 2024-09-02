namespace NKStudio
{
    public enum MacOSCursorType {
        // 1. 매치되는 커서
        Arrow,                           // 매치: NORMAL (Windows)
        IBeam,                           // 매치: IBEAM (Windows)
        Crosshair,                       // 매치: CROSS (Windows)
        PointingHand,                    // 매치: HAND (Windows)
        OperationNotAllowed,             // 매치: NO (Windows)
        ResizeLeftRight,                 // 매치: SIZEWE (Windows)

        // 2. 부분 매치되는 커서
        ResizeUp,                        // 매치: UP (Windows, 부분 매치)
        ResizeDown,                      // 매치: SIZENESW (Windows, 부분 매치)
        ResizeUpDown,                    // 매치: SIZENS

        // 3. 매치되지 않는 커서
        OpenHand,                        // 매치 불가: Windows에 해당 커서 없음
        ClosedHand,                      // 매치 불가: Windows에 해당 커서 없음
        DisappearingItem,                // 매치 불가: Windows에 해당 커서 없음
        IBeamCursorForVerticalLayout,    // 매치 불가: Windows에 해당 커서 없음
        DragLink,                        // 매치 불가: Windows에 해당 커서 없음
        DragCopy,                        // 매치 불가: Windows에 해당 커서 없음
        ContextualMenu,                  // 매치 불가: Windows에 해당 커서 없음
    };
    
    public enum WindowsCursorType
    {
        // 1. 매치되는 커서
        NORMAL,       // 매치: Arrow
        IBEAM,        // 매치: IBeam
        CROSS,        // 매치: Crosshair
        HAND,         // 매치: PointingHand
        NO,           // 매치: OperationNotAllowed
        SIZEWE,       // 매치: ResizeLeftRight 
        
        // 2. 부분 매치되는 커서
        UP,           // 매치: ResizeUp (부분 매치)
        
        SIZENS,       // 매치: 

        // 3. 매치되지 않는 커서
        SIZENWSE,     // 매치 불가: macOS에 해당 커서 없음
        SIZENESW,     // 매치 불가: macOS에 해당 커서 없음
        WAIT,         // 매치 불가: macOS에 해당 커서 없음
        SIZEALL,      // 매치 불가: macOS에 해당 커서 없음
        APPSTARTING   // 매치 불가: macOS에 해당 커서 없음
    }
    
    public enum CursorType
    {
        NORMAL,       // 매치: Arrow
        IBEAM,        // 매치: IBeam
        CROSS,        // 매치: Crosshair
        HAND,         // 매치: PointingHand
        NO,           // 매치: OperationNotAllowed

        // 2. 부분 매치되는 커서
        UP,           // 매치: ResizeUp (부분 매치)
        SIZENWSE,     // 매치: ResizeUpDown (부분 매치)
        SIZENESW,     // 매치: ResizeDown (부분 매치)
        SIZEWE,       // 매치: ResizeUpDown (부분 매치)
        SIZENS,       // 매치: ResizeUpDown
    }
}