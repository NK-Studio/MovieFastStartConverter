import Foundation
import Cocoa

public class UDialog: NSObject {
    public func ShowDialogBox(title : String, message: String, yes:String){
        let alert = NSAlert()
        alert.messageText = title
        alert.informativeText = message
        alert.alertStyle = .warning
        alert.addButton(withTitle: yes)
        // Unity 창을 가져오기 위해 NSApplication의 mainWindow를 사용
        if let window = NSApp.mainWindow {
            alert.beginSheetModal(for: window) { (response) in
                // 다이얼로그 박스의 버튼을 클릭한 후 실행되는 코드
                // response 값에 따라 이후 동작을 정의할 수 있습니다.
            }
        } else {
            // 만약 mainWindow가 없다면 alert를 일반 모달로 표시
            alert.runModal()
        }
    }
}
