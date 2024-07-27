using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class ImageReconstructor : EditorWindow
{
    private void DrawDividerWindow()
    {
        // 创建一个水平布局组，使标签和字段水平排列。
        GUILayout.BeginHorizontal(GUILayout.Height(100));

        // 创建一个垂直布局组，以便于在垂直方向上添加一些空间。
        GUILayout.BeginVertical();

        // 垂直居中
        GUILayout.FlexibleSpace();
        
        // 使用GUILayout.Label创建一个标签，并通过EditorStyles来设置样式。
        // GUILayout.ExpandWidth(true)使标签宽度自动扩展以填充可用空间。
        EditorGUILayout.LabelField("Texture", EditorStyles.boldLabel, GUILayout.Width(70));

        // 垂直居中
        GUILayout.FlexibleSpace();
        
        GUILayout.EndVertical(); //结束垂直布局组
        
        // 使用GUILayout.ObjectField来创建一个对象字段，允许用户选择Texture2D类型的资源。
        // 这里不需要指定Rect，因为GUILayout会自动处理布局。
        texture = (Texture2D)EditorGUILayout.ObjectField(
            texture,    // 当前选中的对象。
            typeof(Texture2D), // 允许选择的对象类型。
            GUILayout.Width(100), // 设置控件的宽度，或者使用GUILayout.ExpandWidth(true)来自动扩展宽度。
            GUILayout.Height(100)  // 设置控件的高度，或者使用GUILayout.ExpandHeight(true)来自动扩展高度。
        );
        GUILayout.EndHorizontal();
        
        // 创建一个水平布局组，使按钮水平排列。
        GUILayout.BeginHorizontal();

        // 添加弹性空间，将按钮推向中间。
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("点击拆分",GUILayout.Width(120),GUILayout.Height(40)))
        {
            if (texture == null)
            {
                message = "贴图未放入";
                messageType = MessageType.Error;
                GUIUtility.ExitGUI();//提前结束绘制，不加这个报错不匹配
                return;
            }
            // 弹出文件选择窗口  
            currentPath = EditorUtility.OpenFolderPanel("选择路径","","");
            SplitToAlpha8();
            message = "通道分离成功!";
            messageType = MessageType.Info;
        }

        // 再次添加弹性空间，使按钮水平居中。
        GUILayout.FlexibleSpace();

        // 结束水平布局组。
        GUILayout.EndHorizontal();
        
        //消息盒子
        if (!string.IsNullOrEmpty(message))
        {
            EditorGUILayout.HelpBox(message, messageType);
        }
        
    }
    
    private void SplitToAlpha8()
    {
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
        
        //分离R通道
        SplitR(width, height,ref sourceImporter);
        //分离G通道
        SplitG(width, height,ref sourceImporter);
        //分离B通道
        SplitB(width, height,ref sourceImporter);
        //分离A通道
        SplitA(width, height,ref sourceImporter);
    }
    
    

    void SplitR(int width,int height,ref TextureImporter sourceImporter)
    {
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
        SaveTexture(alphaTexture, currentPath +"/RTexture.png");
        ResetAlphaImporter(currentPath +"/RTexture.png", ref sourceImporter);

    }
    
    void SplitG(int width,int height,ref TextureImporter sourceImporter)
    {
        // 创建一个新的Texture2D用于存储Alpha通道
        Texture2D alphaTexture = new Texture2D(width, height);

        // 从原始纹理中获取像素数据
        Color32[] pixels = texture.GetPixels32();
        
        // 将像素数据转换为Alpha通道数据
        Color32[] alphaPixels = new Color32[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            alphaPixels[i] = new Color32(pixels[i].g, pixels[i].g, pixels[i].g, pixels[i].g); // 只保留Alpha值
        }
        
        // 应用像素数据到新的纹理
        alphaTexture.SetPixels32(alphaPixels);
        alphaTexture.Apply();

        // 保存新的Alpha纹理
        SaveTexture(alphaTexture, currentPath +"/GTexture.png");
        ResetAlphaImporter(currentPath +"/GTexture.png", ref sourceImporter);
    }
    void SplitB(int width,int height,ref TextureImporter sourceImporter)
    {
        // 创建一个新的Texture2D用于存储Alpha通道
        Texture2D alphaTexture = new Texture2D(width, height);

        // 从原始纹理中获取像素数据
        Color32[] pixels = texture.GetPixels32();
        
        // 将像素数据转换为Alpha通道数据
        Color32[] alphaPixels = new Color32[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            alphaPixels[i] = new Color32(pixels[i].b, pixels[i].b, pixels[i].b, pixels[i].b); // 只保留Alpha值
        }
        
        // 应用像素数据到新的纹理
        alphaTexture.SetPixels32(alphaPixels);
        alphaTexture.Apply();

        // 保存新的Alpha纹理
        SaveTexture(alphaTexture, currentPath +"/BTexture.png");
        ResetAlphaImporter(currentPath +"/BTexture.png", ref sourceImporter);
    }
    void SplitA(int width,int height,ref TextureImporter sourceImporter)
    {
        // 创建一个新的Texture2D用于存储Alpha通道
        Texture2D alphaTexture = new Texture2D(width, height);

        // 从原始纹理中获取像素数据
        Color32[] pixels = texture.GetPixels32();
        
        // 将像素数据转换为Alpha通道数据
        Color32[] alphaPixels = new Color32[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            alphaPixels[i] = new Color32(pixels[i].a, pixels[i].a, pixels[i].a, pixels[i].a); // 只保留Alpha值
        }
        
        // 应用像素数据到新的纹理
        alphaTexture.SetPixels32(alphaPixels);
        alphaTexture.Apply();

        // 保存新的Alpha纹理
        SaveTexture(alphaTexture, currentPath +"/ATexture.png");
        ResetAlphaImporter(currentPath +"/ATexture.png", ref sourceImporter);
    }

    private void ResetAlphaImporter(string filePath,ref TextureImporter sourceImporter)
    {
        //必须是相对路径才能拿到importer
        filePath = filePath.Substring(filePath.IndexOf("Assets"));
        //修改新纹理的设置.
        TextureImporter importer = AssetImporter.GetAtPath(filePath) as TextureImporter;
        // 确保纹理导入器不是null
        if (importer)
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
