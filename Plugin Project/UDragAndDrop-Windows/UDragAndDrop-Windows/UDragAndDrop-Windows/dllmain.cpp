#include <windows.h>
#include <iostream>
#include <shellapi.h>

HWND unityHandle = nullptr;
HHOOK hook;
WCHAR** result;

using Callback = void(__stdcall*)(int length, WCHAR** pathURLs);
Callback UnityCallback;

#define UNITY_WINDOW_CLASS_NAME L"UnityContainerWndClass"
#define MAX_CLASS_NAME_LENGTH 256
#define MAX_PATH_LENGTH 1024

BOOL CALLBACK FindUnityHWND(const HWND hwnd, LPARAM lParam)
{
    // skip invisible window
    if (!IsWindowVisible(hwnd)) 
        return TRUE;

    // skip when we already have main window handle
    if (unityHandle == nullptr)
        unityHandle = hwnd;
    else
    {
        // FindWindow searches all unity window classess of all instanced of unity.
        // So, use sub window enumeration in current thread instead FindWindow
        WCHAR lp_class_name[MAX_CLASS_NAME_LENGTH];
        GetClassName(hwnd, lp_class_name, MAX_CLASS_NAME_LENGTH);
        if (wcscmp(lp_class_name, UNITY_WINDOW_CLASS_NAME) == 0)
            unityHandle = hwnd;
    }

    return TRUE;
}


LRESULT HookCallback(int code, WPARAM wordParam, LPARAM longParam)
{
    if (code == WH_JOURNALRECORD)
    {
        // longParam을 MSG로 캐스팅하여 메시지 정보를 가져온다.
        const auto pmsg = reinterpret_cast<PMSG>(longParam);

        // 드롭다운 메시지가 발생했을 때
        if (pmsg->message == WM_DROPFILES)
        {
            POINT pos;

            // word parameter로 전달된 핸들을 HDROP(Handle to a Drop)로 캐스팅하여 드롭다운된 파일의 정보를 가져온다.
            const auto dropDownInfo = reinterpret_cast<HDROP>(pmsg->wParam);

            // DragQueryPoint에 handleDrop을 전달하고 pos에 드롭다운된 파일의 위치/좌표(x, y)를 반환 받는다.
            DragQueryPoint(dropDownInfo, &pos);

            // 드래그 앤 드롭된 파일의 총 수를 가져옵니다.
            // DragQueryFile 함수는 파일 수를 가져오는 데 사용됩니다. 0xFFFFFFFF를 전달하면 파일 수를 반환합니다.
            const auto fileCount = static_cast<int>(DragQueryFile(dropDownInfo, 0xFFFFFFFF, nullptr, 0));

            // 파일 경로를 저장할 배열을 동적으로 할당합니다.
            result = new WCHAR*[static_cast<int>(fileCount)];

            // 각 파일의 경로를 가져와서 result 배열에 저장합니다. 각 파일 경로를 저장하기 위해 메모리를 동적으로 할당합니다.
            for (int i = 0; i < fileCount; i++)
            {
                const auto fileName = new WCHAR[MAX_PATH_LENGTH];
                // // 파일 경로의 길이를 가져옵니다.
                // int fileLength = DragQueryFile(dropDownInfo, i, nullptr, 0) + 1;
                //
                // // fileLength 크기의 TCHAR 배열을 동적으로 할당합니다.
                // // 메모리 할당 후 0으로 초기화합니다.
                // TCHAR* fileName = new TCHAR[fileLength];
                // memset(fileName, 0, sizeof(TCHAR) * fileLength);

                // hDrop : 드롭다운 핸들
                // iFile : 0xFFFFFFFF를 전달하면 드롭된 파일의 총 개수를 반환
                // lpszFile : 파일 경로를 저장할 버퍼
                // cch : 버퍼의 길이
                //DragQueryFile(dropDownInfo, i, fileName, fileLength);
                DragQueryFile(dropDownInfo, i, fileName, MAX_PATH_LENGTH);

                result[i] = fileName;

                delete[] fileName;
            }

            // 핸들을 해제하여 시스템 자원을 반환
            DragFinish(dropDownInfo);

            // Unity로 파일 경로를 전달합니다.
            UnityCallback(static_cast<int>(fileCount), result);

            // 파일 경로를 저장한 동적 배열을 해제합니다.
            delete[] result;
        }
    }

    // 후크 체인에서 다음 후크를 호출하여 후크 체인이 계속 작동하도록 합니다.
    // hhk : 후크 프로시저의 핸들
    // nCode : 후크 프로시저에 전달된 후크 코드
    // wParam : 메시지에 대한 추가 정보
    // lParam : 메시지에 대한 추가 정보
    return CallNextHookEx(hook, code, wordParam, longParam);
}

extern "C" __declspec(dllexport) void AddHook(Callback callback)
{
    UnityCallback = callback;

    // 현재 모듈의 핸들을 가져옵니다.
    HMODULE h_module = GetModuleHandle(nullptr);

    // 현재 스레드의 ID를 가져옵니다.
    DWORD tid = GetCurrentThreadId();
    
    if (tid > 0)
        EnumThreadWindows(tid, FindUnityHWND, 0);
    
    hook = SetWindowsHookEx(WH_GETMESSAGE, HookCallback, h_module, tid);
    DragAcceptFiles(unityHandle, true);
}

extern "C" __declspec(dllexport) void RemoveHook()
{
    UnhookWindowsHookEx(hook);
    DragAcceptFiles(unityHandle, false);

    hook = nullptr;
    UnityCallback = nullptr;
}
