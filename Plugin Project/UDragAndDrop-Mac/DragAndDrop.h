#ifndef DragAndDrop_h
#define DragAndDrop_h

#import <Foundation/Foundation.h>
#import <AppKit/AppKit.h>
#import <Cocoa/Cocoa.h>

typedef void (*cs_callback)(const char*);

void Initialize(cs_callback callback);

@interface DragAndDropView : NSImageView

@property (nonatomic) cs_callback callback;

@end


#endif /* DragAndDrop_h */
