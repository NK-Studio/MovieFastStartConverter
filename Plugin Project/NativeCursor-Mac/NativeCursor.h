#ifndef NativeCursor_h
#define NativeCursor_h

enum MacOSCursorType {
    // 1. 매치되는 커서
    Arrow,                         // 매치: ARROW (Windows)
    IBeam,                         // 매치: IBEAM (Windows)
    Crosshair,                     // 매치: CROSS (Windows)
    PointingHand,                  // 매치: HAND (Windows)
    OperationNotAllowed,           // 매치: NO (Windows)
    ResizeUpDown,                  // 매치: SIZENS (Windows)
    ResizeLeftRight,               // 매치: SIZEWE (Windows)

    // 2. 매치되지 않는 커서
    ClosedHand,                    // 매치 불가: Windows에 직접 대응 커서 없음
    OpenHand,                      // 매치 불가: Windows에 직접 대응 커서 없음
    ResizeUp,                      // 매치 불가: UP (부분 매치)
    ResizeDown,                    // 매치 불가: SIZENESW (부분 매치)
    DisappearingItem,              // 매치 불가: Windows에 직접 대응 커서 없음
    IBeamCursorForVerticalLayout,  // 매치 불가: Windows에 직접 대응 커서 없음
    DragLink,                      // 매치 불가: Windows에 직접 대응 커서 없음
    DragCopy,                      // 매치 불가: Windows에 직접 대응 커서 없음
    ContextualMenu                 // 매치 불가: Windows에 직접 대응 커서 없음
};

#endif
