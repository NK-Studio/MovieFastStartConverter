#include <Cocoa/Cocoa.h>
#include "NativeCursor.h"

#ifdef __cplusplus
extern "C" {
#endif

    void _SetCursor(MacOSCursorType cursorType) {
        NSCursor* cursor = nil;
        
        switch (cursorType) {
            case Arrow:
                cursor = [NSCursor arrowCursor];
                break;
            case IBeam:
                cursor = [NSCursor IBeamCursor];
                break;
            case Crosshair:
                cursor = [NSCursor crosshairCursor];
                break;
            case PointingHand:
                cursor = [NSCursor pointingHandCursor];
                break;
            case OperationNotAllowed:
                cursor = [NSCursor operationNotAllowedCursor];
                break;
            case ResizeUpDown:
                cursor = [NSCursor resizeUpDownCursor];
                break;
            case ResizeLeftRight:
                cursor = [NSCursor resizeLeftRightCursor];
                break;
            case ClosedHand:
                cursor = [NSCursor closedHandCursor];
                break;
            case OpenHand:
                cursor = [NSCursor openHandCursor];
                break;
            case ResizeUp:
                cursor = [NSCursor resizeUpCursor];
                break;
            case ResizeDown:
                cursor = [NSCursor resizeDownCursor];
                break;
            case DisappearingItem:
                cursor = [NSCursor disappearingItemCursor];
                break;
            case IBeamCursorForVerticalLayout:
                cursor = [NSCursor IBeamCursorForVerticalLayout];
                break;
            case DragLink:
                cursor = [NSCursor dragLinkCursor];
                break;
            case DragCopy:
                cursor = [NSCursor dragCopyCursor];
                break;
            case ContextualMenu:
                cursor = [NSCursor contextualMenuCursor];
                break;
        }

        [cursor set];
    }
    
#ifdef __cplusplus
} // extern "C"
#endif
