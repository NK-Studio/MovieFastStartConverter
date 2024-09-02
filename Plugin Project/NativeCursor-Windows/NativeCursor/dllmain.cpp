#include <windows.h>

enum class WindowsCursorType {
    ARROW,
    IBEAM,
    CROSS,
    HAND,
    NO,
    SIZENS,
    SIZEWE,
    SIZENWSE,
    SIZENESW,
    SIZEALL,
    APPSTARTING,
    HELP,
    UPARROW,        // Unused in Windows API
    UP,             // Unused in Windows API
    PERSON,         // Unused in Windows API
    PIN,            // Unused in Windows API
    WAIT            // Unused in Windows API
};

// Define OCR_NORMAL if it is not already defined
#ifndef OCR_NORMAL
#define OCR_NORMAL 32512  // Normal arrow
#endif

// Function to set the system cursor
extern "C"{
     __declspec(dllexport) void set_cursor(WindowsCursorType cursorType)
     {
         LPCTSTR cursor_type = IDC_ARROW; // Default cursor

         switch (cursorType)
         {
         case WindowsCursorType::ARROW:
             cursor_type = IDC_ARROW;
             break;
         case WindowsCursorType::IBEAM:
             cursor_type = IDC_IBEAM;
             break;
         case WindowsCursorType::CROSS:
             cursor_type = IDC_CROSS;
             break;
         case WindowsCursorType::HAND:
             cursor_type = IDC_HAND;
             break;
         case WindowsCursorType::NO:
             cursor_type = IDC_NO;
             break;
         case WindowsCursorType::SIZENS:
             cursor_type = IDC_SIZENS;
             break;
         case WindowsCursorType::SIZEWE:
             cursor_type = IDC_SIZEWE;
             break;
         case WindowsCursorType::SIZENWSE:
             cursor_type = IDC_SIZENWSE;
             break;
         case WindowsCursorType::SIZENESW:
             cursor_type = IDC_SIZENESW;
             break;
         case WindowsCursorType::SIZEALL:
             cursor_type = IDC_SIZEALL;
             break;
         case WindowsCursorType::APPSTARTING:
             cursor_type = IDC_APPSTARTING;
             break;
         case WindowsCursorType::HELP:
             cursor_type = IDC_HELP;
             break;
         default:
             cursor_type = IDC_ARROW;
             break;
         }

         // Load the cursor and set it as the system cursor
         HCURSOR hCursor = LoadCursor(nullptr, cursor_type);
         if (hCursor)
         {
             SetSystemCursor(hCursor, OCR_NORMAL);
         }
     }
}