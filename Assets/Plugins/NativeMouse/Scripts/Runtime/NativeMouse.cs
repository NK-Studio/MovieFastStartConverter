using System;
#if UNITY_STANDALONE_WIN
using System.Drawing;
#endif
using System.Runtime.InteropServices;
using UnityEngine;

public static class NativeMouse
{
#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")]
    private static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern bool DeleteObject(IntPtr hObject);
    
    [StructLayout(LayoutKind.Sequential)]
    private struct ICONINFO
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
#endif
    
    public enum CursorType
    {
        StandardArrow = 32512,
        Hand = 32649,
        // 추가 커서 타입...
    }

    private static Texture2D _standardAndArrowTexture;
    private static Texture2D _handTexture;

#if UNITY_STANDALONE_WIN
    private static Texture2D LoadSystemCursor(CursorType cursorType)
    {
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
        return null;
    }

    private static Texture2D TextureFromBitmaps(Bitmap colorBmp, Bitmap maskBmp)
    {
        int width = colorBmp.Width;
        int height = colorBmp.Height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var colorPixel = colorBmp.GetPixel(x, y);
                var maskPixel = maskBmp.GetPixel(x, y);

                byte alpha = (byte)(maskPixel.R == 0 ? 255 : 0);
                Color32 targetColor = new Color32(colorPixel.R, colorPixel.G, colorPixel.B, alpha);

                tex.SetPixel(x, height - y - 1, targetColor);
            }
        }

        tex.Apply();
        return tex;
    }
#endif

    public static void ApplyCursor(CursorType cursorType)
    {
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

#if UNITY_STANDALONE_WIN
        if (cursorTexture == null) 
            cursorTexture = LoadSystemCursor(cursorType);
#endif
        
        if (cursorTexture != null) 
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}