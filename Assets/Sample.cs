using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Shibuya24.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField] private Text _text;

    void Start()
    {
        UniDragAndDrop.OnDragAndDropFilesPath += OnDragAndDropFilePath;
        UniDragAndDrop.Initialize();
    }

    private void OnDragAndDropFilePath(string[] obj)
    {
        StringBuilder sb = new StringBuilder();
        
        foreach (var s in obj)
        {
            sb.AppendLine(s);
        }
        _text.text = sb.ToString();
    }
}