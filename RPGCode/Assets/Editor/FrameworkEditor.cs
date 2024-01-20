using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public class FrameworkEditor : OdinMenuEditorWindow
{
    //Tip: 项目文档
    public static DocumentEditor documentEditor;
    //Tip: 项目设置
    public static GameSetEditor gameEditor;
    //Tip: Language
    public static LanguageEditor languageEditor;
    [MenuItem("阴暗地爬行.jpg/Open FrameWork")]
    private static void OpenWindow()
    {
        GetWindow<FrameworkEditor>().Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        MapConfig.Instance.InitData();
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;
        documentEditor = new DocumentEditor();
        gameEditor = new GameSetEditor();
        languageEditor = new LanguageEditor();
        tree.Add("项目设置", gameEditor);
        tree.Add("多语言", languageEditor);
        tree.Add("Document", documentEditor);
        languageEditor.LoadConfig();
        return tree;
    }
}




public class DocumentEditor
{
    [BoxGroup("Framework Document")]
    [DisplayAsString(false), HideLabel]
    public string Doc = "  阴暗地爬行.jpg\n  关于框架的更多信息可以访问 CheeseFramework @Github  \n  (那里有我用md写的具体文档)";
    [BoxGroup("Document"),Button, LabelText("打开GitHub")]
    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/3382634691/CheeseFramework");

    }

    [HideLabel]
    [DisplayAsString(false)]
    [FoldoutGroup("如何使用关卡编辑器")]
    public string SomeText = 
        " -打开Unity关卡编辑器场景Edit\n" +
        "\n -找到场景下的MapConfig\n" +
        "\n -将你的预制体放到该物体下\n" +
        "\n -打开关卡编辑器 基础配置 列，配置好相应的章节与关卡  点击生成\n" +
        "\n -关卡就这么简单的配置好了！\n"+
            "\n -美美收官！去喝杯热茶！摸一把妮娜";

    [FoldoutGroup("如何使用多语言编辑器"), DisplayAsString(false), HideLabel]
    public string SomeText3 =
      " -打开语言编辑器，然后点击新增列表添加新的语言选项后\n" +
      "\n -点击生成配置\n" +
      "\n -生成配置后，要在需要配置多语言的地方（组件）上新增一个LanguageText组件，并且按照相应的ID对应即可";


    [FoldoutGroup("需要注意什么"), DisplayAsString(false), HideLabel]
    public string SomeText2 =
        " -在编辑场景之前，先将MapConfig下的物体清理干净\n"+
        "\n -相同关卡配置一遍后，再次配置将覆盖原本的配置\n"+
        "\n -不要让妮娜跑到键盘上来！";


}



#region Hide Serialized
public class YourAttributeProcessor<T> : OdinAttributeProcessor<T>
{
    public override void ProcessChildMemberAttributes(InspectorProperty _, MemberInfo member, List<Attribute> attributes)
    {
        if (member.Name == "m_SerializedDataModeController")
        {
            attributes.Add(new HideInInspector());
        }
    }
}
#endregion*/


// 自定义Drawer，用于在Odin Inspector中显示Emoji
public class EmojiDrawer : OdinValueDrawer<Texture2D>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        Rect rect = EditorGUILayout.GetControlRect();

        if (ValueEntry.SmartValue != null)
        {
            GUI.DrawTexture(rect, ValueEntry.SmartValue);
        }

        CallNextDrawer(label);
    }
}



