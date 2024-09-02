using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NKStudio
{
    public static class WindowsCore
    {
#if UNITY_STANDALONE_WIN
        [StructLayout(LayoutKind.Sequential)]
        private struct ICONINFO
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("NativeCursor.dll")]
        private static extern int convert_cursor_id(WindowsCursorType cursorType);
#endif

        private static readonly Dictionary<WindowsCursorType, Texture2D> CursorCache = new();

        /// <summary>
        /// Converts bitmaps to a Texture2D object.
        /// </summary>
        private static Texture2D TextureFromBitmaps(Bitmap colorBmp, Bitmap maskBmp)
        {
            int width = colorBmp.Width;
            int height = colorBmp.Height;
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var colorPixel = colorBmp.GetPixel(x, y);
                    var maskPixel = maskBmp.GetPixel(x, y);

                    byte alpha = (byte)(maskPixel.R == 0 ? 255 : 0);
                    Color32 targetColor = new Color32(colorPixel.R, colorPixel.G, colorPixel.B, alpha);

                    texture.SetPixel(x, height - y - 1, targetColor);
                }
            }

            texture.Apply();
            return texture;
        }

        /// <summary>
        /// Loads a system cursor and caches it.
        /// </summary>
        private static Texture2D LoadSystemCursor(WindowsCursorType cursorType)
        {
#if UNITY_STANDALONE_WIN
            if (CursorCache.TryGetValue(cursorType, out var cachedTexture))
                return cachedTexture;

            IntPtr hCursor = LoadCursor(IntPtr.Zero, convert_cursor_id(cursorType));

            if (hCursor == IntPtr.Zero)
                return null;

            if (GetIconInfo(hCursor, out ICONINFO iconInfo))
            {
                Bitmap colorBmp = Image.FromHbitmap(iconInfo.hbmColor);
                Bitmap maskBmp = Image.FromHbitmap(iconInfo.hbmMask);

                var resultTexture = TextureFromBitmaps(colorBmp, maskBmp);

                DeleteObject(iconInfo.hbmColor);
                DeleteObject(iconInfo.hbmMask);

                CursorCache[cursorType] = resultTexture;
                return resultTexture;
            }
#endif
            return null;
        }

        /// <summary>
        /// Sets the cursor to the specified type.
        /// </summary>
        internal static void SetCursor(WindowsCursorType cursorType)
        {
#if UNITY_STANDALONE_WIN
            Texture2D cursorTexture = LoadSystemCursor(cursorType);

            if (cursorTexture)
                Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
#endif
        }

        /// <summary>
        /// Releases all cached cursor textures.
        /// </summary>
        internal static void Release()
        {
            foreach (var texture in CursorCache.Values)
            {
                if (texture != null)
                {
                    UnityEngine.Object.Destroy(texture);
                }
            }

            CursorCache.Clear();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}