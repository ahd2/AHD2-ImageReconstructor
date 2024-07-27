using System.IO;
using UnityEditor;
using UnityEngine;

public partial class ImageReconstructor : EditorWindow
{
    private Texture2D[] texture2Ds;
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
        //垂直布局0下水平布局0
        EditorGUILayout.LabelField("        B", EditorStyles.boldLabel, GUILayout.Width(60));
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
        //垂直布局0下水平布局0
        EditorGUILayout.LabelField("        A", EditorStyles.boldLabel, GUILayout.Width(60));
        GUILayout.EndVertical();//垂直布局3
        GUILayout.EndHorizontal();
        
        // 创建一个水平布局组，使按钮水平排列。
        GUILayout.BeginHorizontal();

        // 添加弹性空间，将按钮推向中间。
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("点击合并",GUILayout.Width(120),GUILayout.Height(40)))
        {
            // 弹出文件选择窗口  
            currentPath = EditorUtility.OpenFolderPanel("选择路径","","");
            Assemble();
        }

        // 再次添加弹性空间，使按钮水平居中。
        GUILayout.FlexibleSpace();
        //Assemble();
        // 结束水平布局组。
        GUILayout.EndHorizontal();
    }

    private void Assemble()
    {
        int width = texture2Ds[0].width;
        int height = texture2Ds[0].height;
        for (int i = 1; i < 4; i++)
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
        
        message = "合并成功!";
        messageType = MessageType.Info;
    }
}
