#include <windows.h>
#include "dllmain.h"

extern "C" __declspec(dllexport) void _SetCursor(WindowsCursorType cursorType)
{
    LPCTSTR cursorName = IDC_ARROW; // Default cursor

    switch (cursorType)
    {
    case UPARROW:
        cursorName = IDC_UPARROW;
        break;
    case ARROW:
        cursorName = IDC_ARROW;
        break;
    case IBEAM:
        cursorName = IDC_IBEAM;
        break;
    case CROSS:
        cursorName = IDC_CROSS;
        break;
    case HAND:
        cursorName = IDC_HAND;
        break;
    case NO:
        cursorName = IDC_NO;
        break;
    case SIZENWSE:
        cursorName = IDC_SIZENWSE;
        break;
    case SIZENESW:
        cursorName = IDC_SIZENESW;
        break;
    case SIZEWE:
        cursorName = IDC_SIZEWE;
        break;
    case SIZENS:
        cursorName = IDC_SIZENS;
        break;
    case SIZEALL:
        cursorName = IDC_SIZEALL;
        break;
    case APPSTARTING:
        cursorName = IDC_APPSTARTING;
        break;
    case HELP:
        cursorName = IDC_HELP;
        break;
    default:
        cursorName = IDC_ARROW;
        break;
    }

    // hWnd가 NULL이면, 현재 포그라운드 윈도우를 가져옴
    SetSystemCursor(LoadCursor(nullptr, cursorName), OCR_NORMAL);
}