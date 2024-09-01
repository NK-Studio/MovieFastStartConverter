#include "Plugin.pch"


const char* DialogOpenFilePanel(const char* title, const char* directory, const char* filters, bool multiselect) {
    StandaloneFileBrowser* dialog = [[StandaloneFileBrowser alloc] init];
    NSString* path = [dialog dialogOpenFilePanel:[NSString stringWithUTF8String:title]
                                       directory:[NSString stringWithUTF8String:directory]
                                         filters:[NSString stringWithUTF8String:filters]
                                     multiselect:multiselect
                                  canChooseFiles:YES
                                canChooseFolders:NO];
    return [path UTF8String];
}

const char* DialogOpenFolderPanel(const char* title, const char* directory, bool multiselect) {
    StandaloneFileBrowser* dialog = [[StandaloneFileBrowser alloc] init];
    NSString* path = [dialog dialogOpenFilePanel:[NSString stringWithUTF8String:title]
                                       directory:[NSString stringWithUTF8String:directory]
                                         filters:[NSString stringWithUTF8String:""]
                                     multiselect:multiselect
                                  canChooseFiles:NO
                                canChooseFolders:YES];
    return [path UTF8String];
}

const char* DialogSaveFilePanel(const char* title, const char* directory, const char* defaultName, const char* filters) {
    StandaloneFileBrowser* dialog = [[StandaloneFileBrowser alloc] init];
    NSString* path = [dialog dialogSaveFilePanel:[NSString stringWithUTF8String:title]
                                       directory:[NSString stringWithUTF8String:directory]
                                     defaultName:[NSString stringWithUTF8String:defaultName]
                                         filters:[NSString stringWithUTF8String:filters]];
    return [path UTF8String];
}

@implementation StandaloneFileBrowser

- (id)init {
    if (self = [super init]) {
        NSLog(@"init");
    }
    return self;
}

- (NSString *)dialogOpenFilePanel:(NSString *)title directory:(NSString *)directory filters:(NSString *)filters multiselect:(BOOL)multiselect canChooseFiles:(BOOL)canChooseFiles canChooseFolders:(BOOL)canChooseFolders {
    
    @try {
        NSMutableArray<NSString *> *filterItems = [[NSMutableArray alloc] init];
        NSMutableArray<NSArray<NSString *> *> *extensions = [[NSMutableArray alloc] init];
        [self parseFilter:filters filters:filterItems extensions:extensions];
        
        NSOpenPanel *panel = [NSOpenPanel openPanel];
        
        if (filterItems.count > 0) {
            PopUpButtonHandler *popUpHandler = [[PopUpButtonHandler alloc] initWithPanel:panel];
            [popUpHandler setExtensions:extensions];
            
            NSView *accessoryView = [[NSView alloc] initWithFrame:NSMakeRect(0.0, 0.0, 200, 24.0)];
            NSTextField *label = [[NSTextField alloc] initWithFrame:NSMakeRect(0, 0, 60, 22)];
            [label setEditable:NO];
            [label setStringValue:@"File type:"];
            [label setBordered:NO];
            [label setBezeled:NO];
            [label setDrawsBackground:NO];
            
            NSPopUpButton *popupButton = [[NSPopUpButton alloc] initWithFrame:NSMakeRect(61.0, 2, 140, 22.0) pullsDown:NO];
            [popupButton addItemsWithTitles:filterItems];
            [popupButton setTarget:popUpHandler];
            [popupButton setAction:@selector(selectFormatOpen:)];
            
            [accessoryView addSubview:label];
            [accessoryView addSubview:popupButton];
            
            [panel setAccessoryView:accessoryView];
            if ([panel respondsToSelector:@selector(setAccessoryViewDisclosed:)]) {
                [panel setAccessoryViewDisclosed:YES];
            }
            
            NSMutableArray<UTType *> *contentTypes = [NSMutableArray array];
            
            for (NSArray<NSString *> *fileExtensions in extensions) {
                for (NSString *ext in fileExtensions) {
                    UTType *type = [UTType typeWithFilenameExtension:ext];
                    if (type) {
                        [contentTypes addObject:type];
                    }
                }
            }
            
            panel.allowedContentTypes = contentTypes;
        }
        
        if (title.length != 0) {
            [panel setMessage:title];
        }
        [panel setCanChooseFiles:canChooseFiles];
        [panel setCanChooseDirectories:canChooseFolders];
        [panel setAllowsMultipleSelection:multiselect];
        [panel setDirectoryURL:[NSURL fileURLWithPath:directory]];
        
        if ([panel runModal] == NSModalResponseOK) {
            NSArray<NSURL *> *urls = [panel URLs];
            if (urls.count > 0) {
                NSString *separator = [NSString stringWithFormat:@"%c", 28];
                NSMutableArray<NSString *> *paths = [NSMutableArray arrayWithCapacity:urls.count];
                
                for (NSURL *url in urls) {
                    [paths addObject:url.path];
                }
                
                return [paths componentsJoinedByString:separator];
            }
        }
    }
    @catch (NSException *exception) {
        NSLog(@"SFB::dialogOpenFilePanel Exception: %@", exception.reason);
    }
    
    return @"";
}

- (NSString*)dialogSaveFilePanel:(NSString*)title
                       directory:(NSString*)directory
                     defaultName:(NSString*)defaultName
                         filters:(NSString*)filters {
    @try {
        NSMutableArray<NSString *> *filterItems = [[NSMutableArray alloc] init];
        NSMutableArray<NSArray<NSString *> *> *extensions = [[NSMutableArray alloc] init];
        [self parseFilter:filters filters:filterItems extensions:extensions];
        
        NSSavePanel *panel = [NSSavePanel savePanel];
        
        if (filterItems.count > 0) {
            PopUpButtonHandler *popupHandler = [[PopUpButtonHandler alloc] initWithPanel:panel];
            [popupHandler setExtensions:extensions];
            
            NSView *accessoryView = [[NSView alloc] initWithFrame:NSMakeRect(0.0, 0.0, 220, 24.0)];
            NSTextField *label = [[NSTextField alloc] initWithFrame:NSMakeRect(0, 0, 80, 22)];
            [label setEditable:NO];
            [label setStringValue:@"Save as type:"];
            [label setBordered:NO];
            [label setBezeled:NO];
            [label setDrawsBackground:NO];
            
            NSPopUpButton *popupButton = [[NSPopUpButton alloc] initWithFrame:NSMakeRect(81.0, 2, 140, 22.0) pullsDown:NO];
            [popupButton addItemsWithTitles:filterItems];
            [popupButton setTarget:popupHandler];
            [popupButton setAction:@selector(selectFormatSave:)];
            
            [accessoryView addSubview:label];
            [accessoryView addSubview:popupButton];
            
            [panel setAccessoryView:accessoryView];
            
            // 최신 macOS에서는 UTI 대신 UTType을 사용합니다.
            NSMutableArray<UTType *> *contentTypes = [NSMutableArray array];
            
            for (NSArray<NSString *> *fileExtensions in extensions) {
                for (NSString *ext in fileExtensions) {
                    UTType *type = [UTType typeWithFilenameExtension:ext];
                    if (type) {
                        [contentTypes addObject:type];
                    }
                }
            }
            
            panel.allowedContentTypes = contentTypes;
        }
        
        if (title.length != 0) {
            [panel setMessage:title];
        }
        [panel setDirectoryURL:[NSURL fileURLWithPath:directory]];
        [panel setNameFieldStringValue:defaultName];
        
        if ([panel runModal] == NSModalResponseOK) {
            NSURL *URL = [panel URL];
            if (URL) {
                return [URL path];
            }
        }
    }
    @catch (NSException *exception) {
        NSLog(@"SFB::dialogSaveFilePanel Exception: %@", exception.reason);
    }
    
    return @"";
}

- (void)parseFilter:(NSString*)filter filters:(NSMutableArray*)filters extensions:(NSMutableArray*)extensions {
    if ([filter length] == 0) {
        return;
    }
    
    @try {
        NSArray* fileFilters = [filter componentsSeparatedByString:@"|"];
        for (NSString* filter in fileFilters) {
            NSArray* f = [filter componentsSeparatedByString:@";"];
            NSString* filterName = (NSString*)[f objectAtIndex:0];
            
            NSString* extNames = (NSString*)[f objectAtIndex:1];
            NSArray* exts = [extNames componentsSeparatedByString:@","];
            
            NSMutableString* filterItemName = [[NSMutableString alloc] init];
            [filterItemName appendFormat:@"%@ (", filterName];
            for (NSString* ext in exts) {
                [filterItemName appendFormat:@"*.%@,", ext];
            }
            [filterItemName deleteCharactersInRange:NSMakeRange([filterItemName length]-1, 1)];
            [filterItemName appendString:@")"];
            
            [filters addObject:filterItemName];
            [extensions addObject:exts];
        }
    } @catch (NSException *exception) {
        NSLog(@"SFB::parseFilter Exception%@", exception.reason);
    }
}

@end

@implementation PopUpButtonHandler

- (id)initWithPanel:(NSPanel*)panel {
    self = [super init];
    if (self)
        _panel = panel;
    return self;
}

- (void)setExtensions:(NSArray *)extensions {
    _extensions = extensions;
}

- (void)selectFormatOpen:(id)sender {
    NSPopUpButton* button = (NSPopUpButton *)sender;
    NSInteger selectedItemIndex = [button indexOfSelectedItem];
    
    NSString* firstExtension = (NSString*)[[_extensions objectAtIndex:selectedItemIndex] objectAtIndex:0];
    
    if ([firstExtension isEqualToString:@""] || [firstExtension isEqualToString:@"*"]) {
        // 모든 파일 형식을 허용하려면 빈 배열을 전달
        [((NSOpenPanel*)_panel) setAllowedContentTypes:@[]];
    } else {
        // 선택된 확장자를 기반으로 UTType 생성
        NSArray<NSString *> *selectedExtensions = [_extensions objectAtIndex:selectedItemIndex];
        NSMutableArray<UTType *> *contentTypes = [NSMutableArray array];
        
        for (NSString *ext in selectedExtensions) {
            UTType *type = [UTType typeWithFilenameExtension:ext];
            if (type) {
                [contentTypes addObject:type];
            }
        }
        
        // `allowedContentTypes`에 UTType 배열 설정
        [((NSOpenPanel*)_panel) setAllowedContentTypes:contentTypes];
    }
}


- (void)selectFormatSave:(id)sender {
    NSPopUpButton* button = (NSPopUpButton *)sender;
    NSInteger selectedItemIndex = [button indexOfSelectedItem];
    NSString* nameFieldString = [((NSSavePanel*)_panel) nameFieldStringValue];
    NSString* trimmedNameFieldString = [nameFieldString stringByDeletingPathExtension];
    
    NSString* ext = [[_extensions objectAtIndex:selectedItemIndex] objectAtIndex:0];
    NSString* nameFieldStringWithExt = nil;
    
    if ([ext isEqualToString:@""] || [ext isEqualToString:@"*"]) {
        nameFieldStringWithExt = trimmedNameFieldString;
        // 모든 파일 형식을 허용하려면 빈 배열을 사용합니다.
        [((NSSavePanel*)_panel) setAllowedContentTypes:@[]];
    } else {
        nameFieldStringWithExt = [NSString stringWithFormat:@"%@.%@", trimmedNameFieldString, ext];
        
        // 선택된 확장자에 맞는 UTType 생성
        UTType *fileType = [UTType typeWithFilenameExtension:ext];
        if (fileType) {
            [((NSSavePanel*)_panel) setAllowedContentTypes:@[fileType]];
        } else {
            [((NSSavePanel*)_panel) setAllowedContentTypes:@[]];
        }
    }
    
    [((NSSavePanel*)_panel) setNameFieldStringValue:nameFieldStringWithExt];
}


@end
