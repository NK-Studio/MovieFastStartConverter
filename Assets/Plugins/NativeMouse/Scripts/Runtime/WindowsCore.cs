using System;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NKStudio
{
    public static class WindowsCore
    {
#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")]
    private static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern bool DeleteObject(IntPtr hObject);
#endif
        [StructLayout(LayoutKind.Sequential)]
        private struct ICONINFO
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        private static Texture2D _standardAndArrowTexture;
        private static Texture2D _handTexture;

        /// <summary>
        /// 시스템 커서를 로드합니다.
        /// </summary>
        /// <param name="cursorType">로드할 커서의 타입입니다.</param>
        /// <returns>Texture2D 객체로 변환된 커서입니다. 실패 시 null을 반환합니다.</returns>
        private static Texture2D LoadSystemCursor(WindowsCursorType cursorType)
        {
#if UNITY_STANDALONE_WIN
        IntPtr hCursor = LoadCursor(IntPtr.Zero, (int)cursorType);
        if (hCursor == IntPtr.Zero)
        {
            Debug.LogError("Failed to load system cursor.");
            return null;
        }

        if (GetIconInfo(hCursor, out ICONINFO iconInfo))
        {
            Bitmap colorBmp = Image.FromHbitmap(iconInfo.hbmColor);
            Bitmap maskBmp = Image.FromHbitmap(iconInfo.hbmMask);

            Texture2D tex = TextureFromBitmaps(colorBmp, maskBmp);

            DeleteObject(iconInfo.hbmColor);
            DeleteObject(iconInfo.hbmMask);

            return tex;
        }

        Debug.LogError("Failed to get icon info.");
#endif
            return null;
        }

        /// <summary>
        /// 두 개의 비트맵(컬러와 마스크)을 Texture2D 객체로 변환합니다.
        /// </summary>
        /// <param name="colorBitmap">커서의 컬러 비트맵입니다.</param>
        /// <param name="maskBmp">커서의 마스크 비트맵입니다.</param>
        /// <returns>커서를 나타내는 Texture2D 객체입니다.</returns>
        private static Texture2D TextureFromBitmaps(Bitmap colorBitmap, Bitmap maskBmp)
        {
#if UNITY_STANDALONE_WIN
        int width = colorBitmap.Width;
        int height = colorBitmap.Height;
        Texture2D cursorTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
        {
            var colorPixel = colorBitmap.GetPixel(x, y);
            var maskPixel = maskBmp.GetPixel(x, y);

            byte alpha = (byte)(maskPixel.R == 0 ? 255 : 0);
            Color32 targetColor = new Color32(colorPixel.R, colorPixel.G, colorPixel.B, alpha);

            cursorTexture.SetPixel(x, height - y - 1, targetColor);
        }

        cursorTexture.Apply();
        return cursorTexture;
#endif
            return null;
        }

        /// <summary>
        /// 커서를 적용합니다.
        /// </summary>
        /// <param name="cursorType">적용할 커서의 타입입니다.</param>
        internal static void SetCursor(WindowsCursorType cursorType)
        {
#if UNITY_STANDALONE_WIN
        Texture2D cursorTexture = null;
        switch (cursorType)
        {
            case CursorType.StandardArrow:
                if (_standardAndArrowTexture != null)
                    cursorTexture = _standardAndArrowTexture;
                break;
            case CursorType.Hand:
                if (_handTexture != null)
                    cursorTexture = _handTexture;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cursorType), cursorType, null);
        }

        if (cursorTexture == null)
            cursorTexture = LoadSystemCursor(cursorType);

        if (cursorTexture != null)
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
#endif
        }
    }
}