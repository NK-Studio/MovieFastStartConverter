// C++ 20을 요구합니다.
#include <Windows.h> // 윈도우 API를 사용하기 위한 라이브러리
#include <commdlg.h> // 파일 열기 대화상자를 위한 라이브러리
#include <format>    // 문자열 포맷팅을 위한 라이브러리
#include <tchar.h>   // 유니코드와 ASCII 문자 처리를 위한 라이브러리
#include <string>    // 문자열을 다루기 위한 라이브러리

using namespace std;

// 최대 경로 길이를 정의하는 상수
constexpr size_t MAX_PATH_SIZE = 256;

// 윈도우 핸들을 찾기 위한 콜백 함수
BOOL CALLBACK EnumThreadWndProc(HWND hwnd, LPARAM lParam)
{
    TCHAR className[MAX_PATH_SIZE]; // 윈도우 클래스 이름을 저장할 배열
    GetClassName(hwnd, className, MAX_PATH_SIZE); // 윈도우의 클래스 이름을 가져온다

    // 윈도우 클래스 이름이 UnityWndClass 또는 UnityContainerWndClass인지 확인
    if (_tcscmp(className, _T("UnityWndClass")) == 0 ||
        _tcscmp(className, _T("UnityContainerWndClass")) == 0)
    {
        HWND* pHwnd = reinterpret_cast<HWND*>(lParam); // LPARAM을 HWND 포인터로 변환
        *pHwnd = hwnd; // 찾은 윈도우 핸들을 저장
        return FALSE; // 탐색을 중지한다
    }
    return TRUE; // 계속 탐색한다
}

// 현재 스레드에 대해 윈도우 핸들을 찾는 함수
HWND GetWindowHandle()
{
    HWND hwnd = nullptr; // 윈도우 핸들을 저장할 변수 (초기값은 nullptr)
    DWORD threadId = GetCurrentThreadId(); // 현재 스레드의 ID를 가져온다
    EnumThreadWindows(threadId, EnumThreadWndProc, reinterpret_cast<LPARAM>(&hwnd)); // 현재 스레드의 모든 윈도우를 열거한다
    return hwnd; // 찾은 윈도우 핸들을 반환
}

// 파일 선택 결과를 처리하는 함수
wstring ProcessFileSelection(TCHAR* fileBuffer)
{
    wstring result; // 결과를 저장할 문자열
    wchar_t* p = fileBuffer; // 파일 버퍼의 시작 주소를 가리키는 포인터
    wstring directory = p; // 첫 번째 문자열을 디렉토리로 가정
    p += directory.length() + 1; // 다음 파일 이름으로 포인터를 이동

    if (*p) // 여러 파일이 선택된 경우
    {
        while (*p) // 파일 이름이 끝나지 않은 경우
        {
            // 디렉토리와 파일 이름을 결합하고 결과 문자열에 추가
            result += std::format(L"{}\\{}|", directory, p);
            p += wcslen(p) + 1; // 다음 파일 이름으로 포인터를 이동
        }
        if (!result.empty())
            result.pop_back(); // 마지막 '|' 문자 제거
    }
    else // 단일 파일이 선택된 경우
    {
        result = directory; // 결과 문자열을 디렉토리로 설정
    }

    return result; // 최종 결과 문자열 반환
}

// 파일 열기 대화상자를 표시하는 함수
void ShowFileDialog(HWND hwnd, const wchar_t* title, const wchar_t* directory,
                    const wchar_t* filter, bool multiSelect, wchar_t** resultPath)
{
    wchar_t originalDirectory[MAX_PATH_SIZE]; // 원래 디렉토리를 저장할 배열
    GetCurrentDirectory(MAX_PATH_SIZE, originalDirectory); // 현재 작업 디렉토리를 가져온다

    TCHAR file[1024 * 10] = L""; // 파일 경로를 저장할 큰 버퍼
    OPENFILENAME ofn; // 파일 열기 대화상자에 대한 구조체
    ZeroMemory(&ofn, sizeof(ofn)); // 구조체를 0으로 초기화
    ofn.hwndOwner = hwnd; // 대화상자의 소유자 윈도우 핸들
    ofn.lStructSize = sizeof(ofn); // 구조체의 크기
    ofn.lpstrFilter = filter; // 파일 필터
    ofn.lpstrFile = file; // 파일 경로를 저장할 버퍼
    ofn.lpstrInitialDir = directory; // 초기 디렉토리
    ofn.nMaxFile = _countof(file); // 파일 경로 버퍼의 크기
    ofn.lpstrTitle = title; // 대화상자 제목
    ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST | OFN_EXPLORER; // 대화상자의 플래그 설정

    if (multiSelect)
        ofn.Flags |= OFN_ALLOWMULTISELECT; // 여러 파일 선택 허용

    if (GetOpenFileName(&ofn)) // 파일 열기 대화상자를 표시하고 파일 선택을 처리
    {
        const wstring result = ProcessFileSelection(file); // 파일 선택 결과를 처리

        SetCurrentDirectory(originalDirectory); // 원래 디렉토리로 복원

        // 결과 문자열의 메모리를 할당
        const size_t length = result.length() + 1; // 결과 문자열의 길이
        *resultPath = static_cast<wchar_t*>(CoTaskMemAlloc(length * sizeof(wchar_t))); // COM 메모리 할당

        if (*resultPath) // 메모리 할당이 성공했는지 확인
        {
            wcsncpy_s(*resultPath, length, result.c_str(), length - 1); // 문자열 복사
            (*resultPath)[length - 1] = L'\0'; // null-terminate the string
        }
    }

    SetCurrentDirectory(originalDirectory); // 원래 디렉토리로 복원
}


extern "C" {
__declspec(dllexport) wchar_t* _ShowFileDialog(const wchar_t* title, const wchar_t* directory,
                                               const wchar_t* filter, bool multiSelect)
{
    HWND hwnd = GetWindowHandle();

    wchar_t* result = nullptr;
    ShowFileDialog(hwnd, title, directory, filter, multiSelect, &result);

    return result;
}
}
