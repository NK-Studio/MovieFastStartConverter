using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SFB;
using NKStudio;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class UIController : MonoBehaviour
{
    private readonly List<FastStartData> _files = new();

    private Button _convertButton;
    private Button _clearButton;
    private ListView _listView;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = FindAnyObjectByType<AudioSource>();
        
        UDragAndDrop.Initialize();
        UDragAndDrop.OnDragAndDropFilesPath += OnDragAndDropFilePath;

        InitUI();
    }
    
    private void OnDestroy()
    {
        UDragAndDrop.OnDragAndDropFilesPath -= OnDragAndDropFilePath;
        UDragAndDrop.Release();
    }

    private void InitUI()
    {
        if (TryGetComponent(out UIDocument uiDocument))
        {
            _convertButton = uiDocument.rootVisualElement.Q<Button>("ConvertButton");
            _convertButton.SetEnabled(false);
            
            _listView = uiDocument.rootVisualElement.Q<ListView>("FileListView");
            _clearButton = uiDocument.rootVisualElement.Q<Button>("ClearButton");
            _listView.makeNoneElement = () =>
            {
                var emptyGroup = new VisualElement();
                emptyGroup.AddToClassList("emptyGroup");

                var emptyLabel = new Label("");
                emptyLabel.text = "비어있음";
                emptyLabel.AddToClassList("unity-list-view__empty-label");
                emptyGroup.Add(emptyLabel);

                var selectButton = new Button();
                selectButton.name = "SelectFileButton";
                selectButton.text = "mp4 파일 선택";
                selectButton.AddToClassList("selectFileLabel");
                emptyGroup.Add(selectButton);

                selectButton.clicked += () =>
                {
                    var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "mp4", true);
                    OnDragAndDropFilePath(paths.ToList());
                };
                
                selectButton.RegisterCallback<MouseEnterEvent>(_ => NativeCursor.SetCursor(CursorType.HAND));
                selectButton.RegisterCallback<MouseLeaveEvent>(_ => NativeCursor.SetCursor(CursorType.ARROW));
                

                return emptyGroup;
            };
            _listView.itemsSource = _files;

            _convertButton.clicked += Convert;
            _convertButton.RegisterCallback<MouseEnterEvent>(_ => NativeCursor.SetCursor(CursorType.HAND));
            _convertButton.RegisterCallback<MouseLeaveEvent>(_ =>NativeCursor.SetCursor(CursorType.ARROW));
            
            _clearButton.clicked += () => _files.Clear();
            _clearButton.RegisterCallback<MouseEnterEvent>(_ => NativeCursor.SetCursor(CursorType.HAND));
            _clearButton.RegisterCallback<MouseLeaveEvent>(_ => NativeCursor.SetCursor(CursorType.ARROW));
        }
    }

    private void OnDragAndDropFilePath(List<string> obj)
    {
        _files.Clear();

        foreach (var link in obj)
        {
            // mp4가 아니면 제외
            if (!link.EndsWith(".mp4"))
                continue;

            FastStartData data = new FastStartData();
            data.URL = link;
            data.IsFinishConvert = IsVideoFastStartEnabled(link);
            _files.Add(data);
        }

        if (_files.Count > 0)
        {
            _convertButton.SetEnabled(true);
        }
    }

    private void Convert()
    {
        _listView.SetEnabled(false);
        _convertButton.SetEnabled(false);
        ConvertAsync();
    }

    private async void ConvertAsync()
    {
        for (int i = 0; i < _files.Count; i++)
        {
            if (_files[i].IsFinishConvert)
                continue;

            bool result = await UDragAndDrop.ApplyFastStart(_files[i].URL);
            _files[i].IsFinishConvert = result;

            if (i + 1 == _files.Count)
                break;

            await Awaitable.WaitForSecondsAsync(0.1f);
        }

        _listView.SetEnabled(true);
        _convertButton.SetEnabled(false);
        _files.Clear();
        NativeDialog.ShowDialogBox("Fast Start Converter", "적용 완료", "확인");
        _audioSource.Play();
    }

    private static bool IsVideoFastStartEnabled(string fullFilePath)
    {
        try
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Fast start is enabled if "moov" is before "mdat" in first 4096 bytes fo file
            // https://trac.ffmpeg.org/wiki/HowToCheckIfFaststartIsEnabledForPlayback
            /////////////////////////////////////////////////////////////////////////////////

            byte[] buffer = new byte[4096];
            using (FileStream fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
            }

            var byteSpan = buffer.AsSpan();

            var moovPattern = new byte[] { 0x6d, 0x6f, 0x6f, 0x76, 0x00 }.AsSpan();
            var moovIndex = byteSpan.IndexOf(moovPattern);

            var mdatPattern = new byte[] { 0x6d, 0x64, 0x61, 0x74, 0x00 }.AsSpan();
            var mdatIndex = byteSpan.IndexOf(mdatPattern);

            if (moovIndex > 0 && (mdatIndex == -1 || moovIndex < mdatIndex))
            {
                // Fast start is enabled
                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}