using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class ImageReconstructor : EditorWindow
{
    private Texture2D texture;
    // 子界面的枚举类型，用于切换
    private enum SubWindowType
    {
        DividerWindow,//分离窗口
        AssemblerWindow//组合窗口
    }

    // 当前选中的子界面类型
    private SubWindowType currentSubWindow = SubWindowType.DividerWindow;

    private string currentPath;//图片保存路径
    
    //消息提示框提示的消息
    string message = "";
    MessageType messageType = MessageType.None;
    
    [MenuItem("Tools/ImageReconstructor(图片重构工具)")]
    public static void ShowWindow()
    {
        GetWindow<ImageReconstructor>("ImageReconstructor");
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        texture2Ds = new Texture2D[4];
    }

    void OnGUI()
    {
        // 顶部标签页
        GUILayout.BeginHorizontal();
        currentSubWindow = (SubWindowType)GUILayout.Toolbar((int)currentSubWindow, new string[] { "图片拆分", "图片合并" });
        GUILayout.EndHorizontal();
        
        // 根据选中的标签页显示不同的子界面
        switch (currentSubWindow)
        {
            case SubWindowType.DividerWindow:
                DrawDividerWindow();
                break;
            case SubWindowType.AssemblerWindow:
                DrawAssemblerWindow();
                break;
        }
    }
    
    private void SaveTexture(Texture2D texture, string filePath)
    {
        byte[] pngData = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, pngData);
        AssetDatabase.Refresh();
    }
}
