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
    //Tip: ��Ŀ�ĵ�
    public static DocumentEditor documentEditor;
    //Tip: ��Ŀ����
    public static GameSetEditor gameEditor;
    //Tip: Language
    public static LanguageEditor languageEditor;
    [MenuItem("����������.jpg/Open FrameWork")]
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
        tree.Add("��Ŀ����", gameEditor);
        tree.Add("������", languageEditor);
        tree.Add("Document", documentEditor);
        languageEditor.LoadConfig();
        return tree;
    }
}




public class DocumentEditor
{
    [BoxGroup("Framework Document")]
    [DisplayAsString(false), HideLabel]
    public string Doc = "  ����������.jpg\n  ���ڿ�ܵĸ�����Ϣ���Է��� CheeseFramework @Github  \n  (����������mdд�ľ����ĵ�)";
    [BoxGroup("Document"),Button, LabelText("��GitHub")]
    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/3382634691/CheeseFramework");

    }

    [HideLabel]
    [DisplayAsString(false)]
    [FoldoutGroup("���ʹ�ùؿ��༭��")]
    public string SomeText = 
        " -��Unity�ؿ��༭������Edit\n" +
        "\n -�ҵ������µ�MapConfig\n" +
        "\n -�����Ԥ����ŵ���������\n" +
        "\n -�򿪹ؿ��༭�� �������� �У����ú���Ӧ���½���ؿ�  �������\n" +
        "\n -�ؿ�����ô�򵥵����ú��ˣ�\n"+
            "\n -�����չ٣�ȥ�ȱ��Ȳ裡��һ������";

    [FoldoutGroup("���ʹ�ö����Ա༭��"), DisplayAsString(false), HideLabel]
    public string SomeText3 =
      " -�����Ա༭����Ȼ���������б�����µ�����ѡ���\n" +
      "\n -�����������\n" +
      "\n -�������ú�Ҫ����Ҫ���ö����Եĵط��������������һ��LanguageText��������Ұ�����Ӧ��ID��Ӧ����";


    [FoldoutGroup("��Ҫע��ʲô"), DisplayAsString(false), HideLabel]
    public string SomeText2 =
        " -�ڱ༭����֮ǰ���Ƚ�MapConfig�µ���������ɾ�\n"+
        "\n -��ͬ�ؿ�����һ����ٴ����ý�����ԭ��������\n"+
        "\n -��Ҫ�������ܵ�����������";


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


// �Զ���Drawer��������Odin Inspector����ʾEmoji
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



