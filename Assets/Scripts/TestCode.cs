
    using System;
    using NKStudio;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class TestCode : MonoBehaviour
    {
        private void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                NativeCursor.SetWindowsCursor(WindowsCursorType.NORMAL);
            }
        }
    }
