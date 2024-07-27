using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ImageReconstructor : EditorWindow
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
    
    [MenuItem("Tools/ImageReconstructor(图片重构工具)")]
    public static void ShowWindow()
    {
        GetWindow<ImageReconstructor>("ImageReconstructor");
    }
    
    enum TextureFormat
    {
        Alpha8, 
        RGB24, 
        RGBA32, 
        // 其他贴图格式...
    }
    // 定义一个变量来存储当前选中的贴图格式
    TextureFormat currentFormat = TextureFormat.Alpha8; // 假设textureFormat是你要展示的成员变量

    private Texture2D[] _textureOutput;//

    private ComputeShader computeShader;

    private string currentPath;//图片保存路径

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
        computeShader = AssetDatabase.LoadAssetAtPath<ComputeShader>("Packages/com.ahd2.image-reconstructor/Shaders/ImageReconstructor.compute");
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
                AssemblerWindow();
                break;
        }
    }

    private void AssemblerWindow()
    {
        throw new System.NotImplementedException();
    }

    private void DrawDividerWindow()
    {
        // 创建一个水平布局组，使标签和字段水平排列。
        GUILayout.BeginHorizontal(GUILayout.Height(70));

        // 创建一个垂直布局组，以便于在垂直方向上添加一些空间。
        GUILayout.BeginVertical();

        // 添加一些垂直空间，以确保标签和字段之间有足够的间隔。
        GUILayout.Space(20);
        
        // 使用GUILayout.Label创建一个标签，并通过EditorStyles来设置样式。
        // GUILayout.ExpandWidth(true)使标签宽度自动扩展以填充可用空间。
        EditorGUILayout.LabelField("Texture", EditorStyles.boldLabel, GUILayout.Width(70));

        // 再次添加一些垂直空间。
        GUILayout.Space(25);
        
        GUILayout.EndVertical(); //结束垂直布局组
        
        // 使用GUILayout.ObjectField来创建一个对象字段，允许用户选择Texture2D类型的资源。
        // 这里不需要指定Rect，因为GUILayout会自动处理布局。
        texture = (Texture2D)EditorGUILayout.ObjectField(
            texture,    // 当前选中的对象。
            typeof(Texture2D), // 允许选择的对象类型。
            GUILayout.Width(60), // 设置控件的宽度，或者使用GUILayout.ExpandWidth(true)来自动扩展宽度。
            GUILayout.Height(60)  // 设置控件的高度，或者使用GUILayout.ExpandHeight(true)来自动扩展高度。
        );
        
        //下拉框
        GUILayout.BeginVertical();
        //填充上部,使下拉框居中
        // 添加弹性空间，居中
        GUILayout.FlexibleSpace();
        currentFormat = (TextureFormat)EditorGUILayout.Popup("拆分后的贴图格式", (int)currentFormat, Enum.GetNames(typeof(TextureFormat)));
        // 添加弹性空间，居中
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical(); //结束垂直布局组
        GUILayout.EndHorizontal();
        
        // 创建一个水平布局组，使按钮水平排列。
        GUILayout.BeginHorizontal();

        // 添加弹性空间，将按钮推向中间。
        GUILayout.FlexibleSpace();

        // 创建一个按钮，文本为"Click Me"。
        if (GUILayout.Button("点击拆分",GUILayout.Width(120),GUILayout.Height(40)))
        {
            // 弹出文件选择窗口  
            currentPath = EditorUtility.OpenFolderPanel("选择路径","","");
            Debug.Log(currentPath);
            // 根据选择的纹理格式来决定处理方式
            switch (currentFormat)
            {
                case TextureFormat.Alpha8:
                    SplitToAlpha8();
                    break;
                case TextureFormat.RGB24:
                    Debug.Log("3");
                    break;
                case TextureFormat.RGBA32:
                    Debug.Log("4");
                    break;
            }
        }

        // 再次添加弹性空间，使按钮水平居中。
        GUILayout.FlexibleSpace();

        // 结束水平布局组。
        GUILayout.EndHorizontal();
        
    }

    private void SplitToAlpha8()
    {
        if (texture == null)
        {
            Debug.LogError("No texture selected.");
            return;
        }

        // 原始纹理的尺寸
        int width = texture.width;
        int height = texture.height;
        
        //原始纹理的importor
        string sourcePath = AssetDatabase.GetAssetPath(texture); 
        TextureImporter sourceImporter = AssetImporter.GetAtPath(sourcePath) as TextureImporter;
        
        //设置原始纹理为可读(注意,后面没关闭可读.看以后会不会有bug吧.)
        sourceImporter.isReadable = true;
        // 应用更改
        AssetDatabase.ImportAsset(sourcePath);

        // 创建一个新的Texture2D用于存储Alpha通道
        Texture2D alphaTexture = new Texture2D(width, height);

        // 从原始纹理中获取像素数据
        Color32[] pixels = texture.GetPixels32();
        
        // 将像素数据转换为Alpha通道数据
        Color32[] alphaPixels = new Color32[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            alphaPixels[i] = new Color32(pixels[i].r, pixels[i].r, pixels[i].r, pixels[i].r); // 只保留Alpha值
        }
        
        // 应用像素数据到新的纹理
        alphaTexture.SetPixels32(alphaPixels);
        alphaTexture.Apply();

        // 保存新的Alpha纹理
        SaveTexture(alphaTexture, currentPath +"/AlphaTexture.png",ref sourceImporter);
    }
    
    private void SaveTexture(Texture2D texture, string filePath,ref TextureImporter sourceImporter)
    {
        byte[] pngData = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, pngData);
        AssetDatabase.Refresh();
        
        //修改新纹理的设置.
        TextureImporter importer = AssetImporter.GetAtPath(filePath) as TextureImporter;
        // 确保纹理导入器不是null
        if (importer != null)
        {
            //复制TextureImporterSettings
            TextureImporterSettings sourceImporterSettings = new TextureImporterSettings();
            sourceImporter.ReadTextureSettings(sourceImporterSettings);
            importer.SetTextureSettings(sourceImporterSettings);
            // 设置纹理格式为Alpha8
            importer.textureType = TextureImporterType.SingleChannel;
            importer.textureFormat = TextureImporterFormat.Alpha8;
            // 应用更改
            AssetDatabase.ImportAsset(filePath);
        }
    }
}
