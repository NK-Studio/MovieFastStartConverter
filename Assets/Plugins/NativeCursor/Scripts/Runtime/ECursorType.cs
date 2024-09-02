namespace NKStudio
{
    public enum MacOSCursorType {
        // 1. Matching cursors
        Arrow,                         // Match: ARROW (Windows)
        IBeam,                         // Match: IBEAM (Windows)
        Crosshair,                     // Match: CROSS (Windows)
        PointingHand,                  // Match: HAND (Windows)
        OperationNotAllowed,           // Match: NO (Windows)
        ResizeUpDown,                  // Match: SIZENS (Windows)
        ResizeLeftRight,               // Match: SIZEWE (Windows)

        // 2. Non-matching cursors
        ClosedHand,                    // No match: No direct corresponding cursor in Windows
        OpenHand,                      // No match: No direct corresponding cursor in Windows
        ResizeUp,                      // No match: UP (partial match)
        ResizeDown,                    // No match: SIZENESW (partial match)
        DisappearingItem,              // No match: No direct corresponding cursor in Windows
        IBeamCursorForVerticalLayout,  // No match: No direct corresponding cursor in Windows
        DragLink,                      // No match: No direct corresponding cursor in Windows
        DragCopy,                      // No match: No direct corresponding cursor in Windows
        ContextualMenu                 // No match: No direct corresponding cursor in Windows
    };
    
    public enum WindowsCursorType
    {
        // 1. Matching cursors
        ARROW,           // Match: Arrow
        IBEAM,           // Match: IBeam
        CROSS,           // Match: Crosshair
        HAND,            // Match: PointingHand
        NO,              // Match: OperationNotAllowed
        SIZENS,          // Match: ResizeUpDown
        SIZEWE,          // Match: ResizeLeftRight

        // 2. Non-matching cursors
        UPARROW,         // No match: No direct corresponding cursor in macOS
        UP,              // No match: ResizeUp (partial match)
        SIZENWSE,        // No match: No direct corresponding cursor in macOS
        SIZENESW,        // No match: ResizeDown (partial match)
        PERSON,          // No match: No direct corresponding cursor in macOS
        PIN,             // No match: No direct corresponding cursor in macOS
        HELP,            // No match: No direct corresponding cursor in macOS
        WAIT,            // No match: No direct corresponding cursor in macOS
        SIZEALL,         // No match: No direct corresponding cursor in macOS
        APPSTARTING      // No match: No direct corresponding cursor in macOS
    }
    
    public enum CursorType
    {
        // 1. Matching cursors
        ARROW,           // Match: Arrow
        IBEAM,           // Match: IBeam
        CROSS,           // Match: Crosshair
        HAND,            // Match: PointingHand
        NO,              // Match: OperationNotAllowed
        UpDown,          // Match: ResizeUpDown
        LeftRight,       // Match: ResizeLeftRight
    }
}