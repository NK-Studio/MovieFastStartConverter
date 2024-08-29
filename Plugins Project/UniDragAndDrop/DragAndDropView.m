#import "DragAndDrop.h"

@implementation DragAndDropView

- (instancetype)initWithCoder:(NSCoder *)coder
{
    self = [super initWithCoder:coder];
    
    if (self) {
        // 드래그 앤 드롭을 지원하는 타입으로 NSPasteboardTypeFileURL을 등록합니다.
        [self registerForDraggedTypes:@[NSPasteboardTypeFileURL]];
    }
    return self;
}

- (NSDragOperation)draggingEntered:(id<NSDraggingInfo>)sender
{
    return NSDragOperationEvery;
}

- (BOOL)prepareForDragOperation:(id<NSDraggingInfo>)sender
{
    return YES;
}

- (BOOL)performDragOperation:(id<NSDraggingInfo>)sender
{
    NSPasteboard *pboard = [sender draggingPasteboard];
    // NSPasteboardTypeFileURL 타입의 데이터를 읽습니다.
    NSArray<NSURL *> *fileURLs = [pboard readObjectsForClasses:@[[NSURL class]] options:nil];
    
    NSString *test = [NSString stringWithFormat:@"Number of files: %lu", (unsigned long)fileURLs.count];
    NSLog(@"%@", test);

    
    
    NSURL *fileURL = [NSURL URLFromPasteboard:pboard];
    NSString *url = fileURL ? [fileURL path] : @"";
    const char *path = [test UTF8String];
    
    if (self.callback) {
        self.callback(path);
    }
    
    return NO;
}

@end
