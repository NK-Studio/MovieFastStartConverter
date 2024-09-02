import Foundation

@_cdecl("_ShowDialogBox")
func ShowDialogBox(title : UnsafePointer<CChar>, message: UnsafePointer<CChar>, yes:UnsafePointer<CChar>){
    let dialog = UDialog();
    dialog.ShowDialogBox(title: String(cString: title), message: String(cString: message), yes: String(cString: yes))
}
