    using System.IO;
using UnityEditor;
using UnityEngine;

public partial class ImageReconstructor : EditorWindow
{
    private Texture2D[] texture2Ds;

    private int width , height;
    private bool rExist, gExist, bExist, aExist;
    Color32 rColor,gColor,bColor,aColor;
    
    private void DrawAssemblerWindow()
    {
        // 创建一个水平布局组，使标签和字段水平排列。
        GUILayout.BeginHorizontal(GUILayout.Height(100));
        //垂直布局0
        GUILayout.BeginVertical(GUILayout.Width(80));
        // 使用GUILayout.ObjectField来创建一个对象字段，允许用户选择Texture2D类型的资源。
        // 这里不需要指定Rect，因为GUILayout会自动处理布局。
        texture2Ds[0] = (Texture2D)EditorGUILayout.ObjectField(
            texture2Ds[0],    // 当前选中的对象。
            typeof(Texture2D), // 允许选择的对象类型。
            GUILayout.Width(60), // 设置控件的宽度，或者使用GUILayout.ExpandWidth(true)来自动扩展宽度。
            GUILayout.Height(60)  // 设置控件的高度，或者使用GUILayout.ExpandHeight(true)来自动扩展高度。
        );
        //垂直布局0下水平布局0
        EditorGUILayout.LabelField("        R", EditorStyles.boldLabel, GUILayout.Width(60));
        
        GUILayout.BeginHorizontal();
        rExist = EditorGUILayout.Toggle(rExist);

        rColor = EditorGUILayout.ColorField(rColor);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();//垂直布局0
        
        GUILayout.FlexibleSpace();
        
        //垂直布局1
        GUILayout.BeginVertical(GUILayout.Width(80));
        // 使用GUILayout.ObjectField来创建一个对象字段，允许用户选择Texture2D类型的资源。
        // 这里不需要指定Rect，因为GUILayout会自动处理布局。
        texture2Ds[1] = (Texture2D)EditorGUILayout.ObjectField(
            texture2Ds[1],    // 当前选中的对象。
            typeof(Texture2D), // 允许选择的对象类型。
            GUILayout.Width(60), // 设置控件的宽度，或者使用GUILayout.ExpandWidth(true)来自动扩展宽度。
            GUILayout.Height(60)  // 设置控件的高度，或者使用GUILayout.ExpandHeight(true)来自动扩展高度。
        );
        //垂直布局0下水平布局0
        EditorGUILayout.LabelField("        G", EditorStyles.boldLabel, GUILayout.Width(60));
        
        GUILayout.BeginHorizontal();
        gExist = EditorGUILayout.Toggle(gExist);

        gColor = EditorGUILayout.ColorField(gColor);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();//垂直布局1
        
        GUILayout.FlexibleSpace();
        
        //垂直布局2
        GUILayout.BeginVertical(GUILayout.Width(80));
        // 使用GUILayout.ObjectField来创建一个对象字段，允许用户选择Texture2D类型的资源。
        // 这里不需要指定Rect，因为GUILayout会自动处理布局。
        texture2Ds[2] = (Texture2D)EditorGUILayout.ObjectField(
            texture2Ds[2],    // 当前选中的对象。
            typeof(Texture2D), // 允许选择的对象类型。
            GUILayout.Width(60), // 设置控件的宽度，或者使用GUILayout.ExpandWidth(true)来自动扩展宽度。
            GUILayout.Height(60)  // 设置控件的高度，或者使用GUILayout.ExpandHeight(true)来自动扩展高度。
        );
        EditorGUILayout.LabelField("        B", EditorStyles.boldLabel, GUILayout.Width(60));
        
        GUILayout.BeginHorizontal();
        bExist = EditorGUILayout.Toggle(bExist);

        bColor = EditorGUILayout.ColorField(bColor);
        GUILayout.EndHorizontal();
        
        GUILayout.EndVertical();//垂直布局2
        
        GUILayout.FlexibleSpace();
        
        //垂直布局3
        GUILayout.BeginVertical(GUILayout.Width(80));
        // 使用GUILayout.ObjectField来创建一个对象字段，允许用户选择Texture2D类型的资源。
        // 这里不需要指定Rect，因为GUILayout会自动处理布局。
        texture2Ds[3] = (Texture2D)EditorGUILayout.ObjectField(
            texture2Ds[3],    // 当前选中的对象。
            typeof(Texture2D), // 允许选择的对象类型。
            GUILayout.Width(60), // 设置控件的宽度，或者使用GUILayout.ExpandWidth(true)来自动扩展宽度。
            GUILayout.Height(60)  // 设置控件的高度，或者使用GUILayout.ExpandHeight(true)来自动扩展高度。
        );
        EditorGUILayout.LabelField("        A", EditorStyles.boldLabel, GUILayout.Width(60));
        
        GUILayout.BeginHorizontal();
        aExist = EditorGUILayout.Toggle(aExist);

        aColor = EditorGUILayout.ColorField(aColor);
        GUILayout.EndHorizontal();
        
        GUILayout.EndVertical();//垂直布局3
        GUILayout.EndHorizontal();
        
        // 大小输入
        GUILayout.BeginVertical();
        width = EditorGUILayout.IntField("图片宽（width >0）", width);
        height = EditorGUILayout.IntField("图片高（height >0）", height);
        GUILayout.EndVertical();
        
        
        // 创建一个水平布局组，使按钮水平排列。
        GUILayout.BeginHorizontal();

        // 添加弹性空间，将按钮推向中间。
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("点击合并",GUILayout.Width(120),GUILayout.Height(40)))
        {
            if (width <= 0 || height <= 0)
            {
                message = "尺寸值错误";
                messageType = MessageType.Error;
                GUIUtility.ExitGUI();//提前结束绘制，不加这个报错不匹配
                return;
            }
            //排除没有勾选，且图像是空白的情况
            // 检查所有通道是否存在贴图或已被勾选
            for (int i = 0; i < 4; i++)
            {
                if (!GetBoolByIndex(i) && texture2Ds[i] == null)
                {
                    message = $"通道 {(i == 0 ? "R" : i == 1 ? "G" : i == 2 ? "B" : "A")} 未选择贴图且未勾选使用纯色。";
                    messageType = MessageType.Error;
                    GUIUtility.ExitGUI();//提前结束绘制，不加这个报错不匹配
                    return;
                }
            }
            
            // 弹出文件选择窗口  
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(assetPath)) 
            {
                assetPath = "Assets";
            }
            currentPath = EditorUtility.OpenFolderPanel("选择路径", assetPath, "");
            Assemble();
        }

        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("获取大小 \n （需拖入贴图）",GUILayout.Width(120),GUILayout.Height(40)))
        {
            for (int i = 0; i < 4; i++)
            {
                if (texture2Ds[i] != null)
                {
                    width = texture2Ds[i].width;
                    height = texture2Ds[i].height;
                    
                    message = $"已取读通道 {(i == 0 ? "R" : i == 1 ? "G" : i == 2 ? "B" : "A")} 的贴图大小。";
                    messageType = MessageType.Info;
                    GUIUtility.ExitGUI();//提前结束绘制，不加这个报错不匹配
                    return;
                }
            }
            message = "未拖入贴图，无法取读。";
            messageType = MessageType.Error;
            GUIUtility.ExitGUI();//提前结束绘制，不加这个报错不匹配
        }
        GUILayout.FlexibleSpace();
        // 结束水平布局组。
        GUILayout.EndHorizontal();
        
        //消息盒子
        if (!string.IsNullOrEmpty(message))
        {
            EditorGUILayout.HelpBox(message, messageType);
        }
    }

    private void Assemble()
    {
        for (int i = 0; i < 4; i++)
        {
            // 如果当前通道勾选纯色，创建一个新的纯色Texture2D
            if (GetBoolByIndex(i))
            {
                // 创建一个与指定宽度和高度相同的新Texture2D
                texture2Ds[i] = new Texture2D(width, height);
                // 为Texture2D设置像素颜色
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // 根据索引设置颜色，这里我们使用Color32方便设置RGBA值
                        Color32 color = GetColorByIndex(i);
                        texture2Ds[i].SetPixel(x, y, color);
                    }
                }
                // 应用设置的像素颜色
                texture2Ds[i].Apply();
            }
        }

        for (int i = 0; i < 4; i++)
        {
            //如果贴图大小不一致，则返回报错
            if (texture2Ds[i].width != width || texture2Ds[i].height != height)
            {
                message = "贴图大小不一致";
                messageType = MessageType.Error;
                GUIUtility.ExitGUI();//提前结束绘制，不加这个报错不匹配
                return;
            }
        }
        
        // 创建一个新的Texture2D用于存储合并后的结果
        Texture2D combinedTexture = new Texture2D(width, height);
        
        Color32[] pixelsR = texture2Ds[0].GetPixels32();
        Color32[] pixelsG = texture2Ds[1].GetPixels32();
        Color32[] pixelsB = texture2Ds[2].GetPixels32();
        Color32[] pixelsA = texture2Ds[3].GetPixels32();

        // 从原始纹理中获取像素数据
        Color32[] combinedPixels = new Color32[pixelsR.Length];

        for (int i = 0; i < combinedPixels.Length; i++)
        {
            combinedPixels[i] = new Color32(pixelsR[i].a, pixelsG[i].a, pixelsB[i].a, pixelsA[i].a); // 只保留Alpha值
        }

        // 应用像素数据到新的纹理
        combinedTexture.SetPixels32(combinedPixels);
        combinedTexture.Apply();
        
        // 保存新的Alpha纹理
        SaveTexture(combinedTexture, currentPath +"/combinedTexture.png");
        //清空纹理数组（纯色的清空，纹理的不清才对。）
        for (int i = 0; i < 4; i++)
        {
            if (GetBoolByIndex(i))
            {
                texture2Ds[i] = null;
            }
        }
        
        message = "合并成功!";
        messageType = MessageType.Info;
    }
    
    // 辅助方法，根据索引返回对应的颜色
    private Color32 GetColorByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return rColor;
            case 1:
                return gColor;
            case 2:
                return bColor;
            case 3:
                return aColor;
            default:
                return new Color32(255, 255, 255, 255); // 默认白色
        }
    }
    
    // 辅助方法，根据索引返回对应的布尔(布尔值表示该通道是否勾选使用纯色)
    private bool GetBoolByIndex(int index)
    {
        switch (index)
        {
            case 0:
                return rExist;
            case 1:
                return gExist;
            case 2:
                return bExist;
            case 3:
                return aExist;
            default:
                return true;
        }
    }
}
