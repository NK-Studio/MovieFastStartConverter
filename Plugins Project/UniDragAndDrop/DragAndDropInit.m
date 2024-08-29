#import "DragAndDrop.h"

void Initialize(cs_callback callback)
{
    NSArray *windows = [NSApp orderedWindows];
    if ([windows count] == 0) {
        return;
    }
    
    NSWindow *window = [windows firstObject];
    NSView *view = [window contentView];
    
    DragAndDropView *dragView = [[DragAndDropView alloc] initWithFrame:view.frame];
    [view addSubview:dragView];
    
    dragView.callback = callback;
}

