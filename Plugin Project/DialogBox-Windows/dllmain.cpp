#include <windows.h>

// 메시지 박스를 호출하는 함수 정의
extern "C" __declspec(dllexport) void ShowMessageBox(LPCWSTR title, LPCWSTR message)
{
    // hWnd가 NULL이면, 현재 포그라운드 윈도우를 가져옴
    HWND hWnd = GetForegroundWindow();
    MessageBox(hWnd, message, title, MB_OK);
}
