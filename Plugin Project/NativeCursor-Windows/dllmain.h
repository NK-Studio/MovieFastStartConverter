// Windows CursorType
enum WindowsCursorType {
    // 1. 매치되는 커서
    ARROW,           // 매치: Arrow
    IBEAM,           // 매치: IBeam
    CROSS,           // 매치: Crosshair
    HAND,            // 매치: PointingHand
    NO,              // 매치: OperationNotAllowed
    SIZENS,          // 매치: ResizeUpDown
    SIZEWE,          // 매치: ResizeLeftRight

    // 2. 매치되지 않는 커서
    UPARROW,         // 매치 불가: macOS에서 직접 대응 커서 없음
    UP,              // 매치 불가: ResizeUp (부분 매치)
    SIZENWSE,        // 매치 불가: macOS에서 직접 대응 커서 없음
    SIZENESW,        // 매치 불가: ResizeDown (부분 매치)
    PERSON,          // 매치 불가: macOS에서 직접 대응 커서 없음
    PIN,             // 매치 불가: macOS에서 직접 대응 커서 없음
    HELP,            // 매치 불가: macOS에서 직접 대응 커서 없음
    WAIT,            // 매치 불가: macOS에서 직접 대응 커서 없음
    SIZEALL,         // 매치 불가: macOS에서 직접 대응 커서 없음
    APPSTARTING      // 매치 불가: macOS에서 직접 대응 커서 없음
};

#ifndef OCR_NORMAL
#define OCR_NORMAL 32512  // Normal arrow
#endif


  

  
     
   
  
           
         
  
         
          
      