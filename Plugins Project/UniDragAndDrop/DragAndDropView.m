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
    
    NSString *resultJson;
    
    // 파일 URL 배열이 비어있으면 빈 JSON 배열을 반환합니다.
    if (fileURLs == nil || [fileURLs count] == 0) {
        resultJson = @"[]";
    }else{
        // 파일 URL을 파일 경로 문자열로 변환합니다.
        NSMutableArray *filePaths = [NSMutableArray arrayWithCapacity:[fileURLs count]];
        for (NSURL *url in fileURLs) {
            [filePaths addObject:[url path]]; // URL에서 파일 경로를 추출하여 배열에 추가합니다.
        }
        
        // NSArray를 JSON 데이터로 변환합니다.
        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:filePaths options:NSJSONWritingPrettyPrinted error:&error];
        
        if (!jsonData) {
            resultJson = @"[]";
        }
        
        // JSON 데이터를 NSString으로 변환합니다.
        resultJson = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    }
    
    if (self.callback) {
        self.callback([resultJson UTF8String]);
    }
    
    return NO;
}

@end
