#include <windows.h>

enum class UnityMode
{
    Editor,
    Runtime
};

// 유니티의 메인 윈도우 핸들을 얻기 위한 함수
HWND GetUnityWindowHandle()
{
    // Unity의 메인 윈도우 클래스 이름
    const LPCWSTR unityWndClass = L"UnityWndClass";
    
    // Unity 메인 윈도우 핸들을 얻기 위한 FindWindow 호출
    HWND hwnd = FindWindow(unityWndClass, nullptr);
    
    // Unity 메인 윈도우 핸들이 존재하는지 확인
    if (hwnd == nullptr)
        return nullptr;

    return hwnd;
}

// 유니티의 컨테이너 창 핸들을 얻기 위한 함수
HWND GetUnityContainerWindowHandle()
{
    // Unity의 컨테이너 윈도우 클래스 이름
    const LPCWSTR unityContainerWndClass = L"UnityContainerWndClass";

    // Unity 컨테이너 윈도우 핸들을 얻기 위한 FindWindowEx 호출
    HWND hwndContainer = FindWindowEx(nullptr, nullptr, unityContainerWndClass, nullptr);

    // Unity 컨테이너 윈도우 핸들이 존재하는지 확인
    if (hwndContainer == nullptr)
        return nullptr;

    return hwndContainer;
}


// 메시지 박스를 호출하는 함수 정의
extern "C" __declspec(dllexport) void show_message_box(UnityMode mode, LPCWSTR title, LPCWSTR message)
{
    HWND hwnd = mode == UnityMode::Runtime ? GetUnityWindowHandle() : GetUnityContainerWindowHandle();
    MessageBox(hwnd, message, title, MB_OK);
}
