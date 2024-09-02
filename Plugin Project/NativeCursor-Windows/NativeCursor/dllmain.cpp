#include <windows.h>

enum class WindowsCursorType
{
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
    UPARROW, // Unused in Windows API
    UP, // Unused in Windows API
    PERSON, // Unused in Windows API
    PIN, // Unused in Windows API
    WAIT // Unused in Windows API
};

// Function to set the system cursor
extern "C" {
__declspec(dllexport) LPWSTR convert_cursor_id(const WindowsCursorType cursor_type)
{
    switch (cursor_type)
    {
    case WindowsCursorType::ARROW:
        return IDC_ARROW;
    case WindowsCursorType::IBEAM:
        return IDC_IBEAM;
    case WindowsCursorType::CROSS:
        return IDC_CROSS;
    case WindowsCursorType::HAND:
        return IDC_HAND;
    case WindowsCursorType::NO:
        return IDC_NO;
    case WindowsCursorType::SIZENS:
        return IDC_SIZENS;
    case WindowsCursorType::SIZEWE:
        return IDC_SIZEWE;
    case WindowsCursorType::SIZENWSE:
        return IDC_SIZENWSE;
    case WindowsCursorType::SIZENESW:
        return IDC_SIZENESW;
    case WindowsCursorType::SIZEALL:
        return IDC_SIZEALL;
    case WindowsCursorType::APPSTARTING:
        return IDC_APPSTARTING;
    case WindowsCursorType::HELP:
        return IDC_HELP;
    default:
        return IDC_ARROW;
    }
}
}
