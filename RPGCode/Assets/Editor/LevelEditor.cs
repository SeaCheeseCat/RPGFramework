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
using UnityEngine.SceneManagement;

public class LevelEditor : OdinMenuEditorWindow
{
    public static LevelLoadEditor loadeditor;
    public static BaseSetEditor baseseteditor;
    [MenuItem("����������.jpg/�ؿ��༭��")]
    private static void OpenWindow()
    {
        GetWindow<LevelEditor>().Show();
    }


    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Game")
            return tree;
        MapConfig.Instance.InitData();
        tree.Selection.SupportsMultiSelect = false;
        loadeditor = new LevelLoadEditor();
        baseseteditor = new BaseSetEditor();
        var dataeditor = new LevelDataEditor();
        tree.Add("�������� ��", baseseteditor);
        tree.Add("�ؿ��б� ��", loadeditor);
        tree.Add("�������� ��", new LevelBuildEditor());
        tree.Add("�ؿ����� ��", dataeditor);
        tree.Add("Document", new DocumentEditor());
        loadeditor.Init(baseseteditor);
        dataeditor.RefreshData();
        return tree;
    }

    public class BaseSetEditor
    {
        
        [EnumToggleButtons, LabelText("�ؿ�����")]
        public LevelType leveltype;

        [LabelText("�½�"), FoldoutGroup("�ؿ�����"), InfoBox("��ȷ���ؿ��༭������֮ǰ\n���йؿ��������MapConfig������ (�����ط���)")]
        public int Chapter;
        [LabelText("�ؿ�"), FoldoutGroup("�ؿ�����")]
        public int Level;

        [GUIColor(1, 1, 1)]
        [Button(ButtonSizes.Large)]
        [ButtonGroup("������"), LabelText("Ԥ����")]
        private void LoadEditData()
        {
            MapConfig.Instance.InitData();
            var data = MapConfig.Instance.SaveMapConfig();
            data.Charpter = Chapter;
            data.Level = Level;
            RefreshDatas(data);
        }

        [GUIColor(0, 0.8f, 0)]
        [Button(ButtonSizes.Large)]
        [ButtonGroup("������"), LabelText("���ɹؿ�����")]
        private void SaveEditData()
        {
            MapConfig.Instance.InitData();
            var data = MapConfig.Instance.SaveMapConfig();
            data.Charpter = Chapter;
            data.Level = Level;
            RefreshDatas(data);
            if(Musicclip != null)
                data.aduioPath = Musicclip.name;
            ArchiveManager.Instance.SaveMapConfigToJsonFile<MapConfigData>(data, Chapter, Level);
            DebugEX.Log("���ɵ�ͼ���óɹ�");
            var window = GetWindow<LevelSuccesTip>();
            window.InitData(data);
            window.Show();
            loadeditor.Init(baseseteditor);
            AssetDatabase.Refresh();
        }

        public void SetData(int chapter,int level,string musicpath) 
        {
            Chapter = chapter;
            Level = level;
            DebugEX.Log("��������·��", musicpath);
            Musicclip = Resources.Load<AudioClip>("Audio/Music/"+musicpath);
            //UnityEditor.EditorUtility.SetDirty(Musicclip);
        }


        /// <summary>
        /// ˢ��������
        /// </summary>
        public void RefreshDatas(MapConfigData data) 
        {
            Npcs = new List<int>();
            foreach (var item in data.mapnpcdata)
            {
                Npcs.Add(item.ID);
            }

            Models = new List<int>();
            foreach (var item in data.mapmodeldatas)
            {
                Models.Add(item.ID);
            }

            Particles = new List<int>();
            foreach (var item in data.mapparticledatas)
            {
                Particles.Add(item.ID);
            }

            Lands = new Vector3((float)data.maplanddata.x, (float)data.maplanddata.y, (float)data.maplanddata.z);

        }

        [GUIColor(0.7f, 0, 0)]
        [ButtonGroup("������"), LabelText("���ó���")]
        private void Delete()
        {
            MapConfig.Instance.InitData();
            DeleteChild(MapConfig.Instance.npcsTrans);
            DeleteChild(MapConfig.Instance.modelsTrans);
            DeleteChild(MapConfig.Instance.particleTrans);
        }

        /// <summary>
        /// ��Eidtģʽɾ��һ�������������е�������
        /// </summary>
        /// <param name="item"></param>
        private void DeleteChild(Transform item)
        {
            GameObject[] items = new GameObject[item.childCount];
            for (int i = 0; i < item.childCount; i++)
            {
                items[i] = item.GetChild(i).gameObject;
            }

            foreach (var obj in items)
            {
                DestroyImmediate(obj);
            }
        }

        [FoldoutGroup("��ϸ����")]
        [EnumToggleButtons, LabelText("�ļ���������")]
        public SaveDataType savedataEnum;

        [FoldoutGroup("��ϸ����"), LabelText("��������")]
        public AudioClip Musicclip;


        [FoldoutGroup("��ϸ����"), LabelText("�Ƿ��������")]
        public bool isCg;

        [FoldoutGroup("�ؿ�����"), LabelText("����id")]
        public List<int> Tasks;


        [FoldoutGroup("����"), InfoBox("�ؿ����ݵ��Ԥ���غ��Զ����ɣ�����Ҫ�ֶ����ã��������ʹ��x"), LabelText("Npc")]
        public List<int> Npcs;
        [FoldoutGroup("����"), LabelText("ģ��")]
        public List<int> Models;
        [FoldoutGroup("����"), LabelText("����")]
        public List<int> Particles;
        [FoldoutGroup("����"), LabelText("��������")]
        public Vector3 Lands;

    }
    
    public class DocumentEditor
    {

        [BoxGroup("�ؿ��༭�� Document")]
        [DisplayAsString(false), HideLabel]
        public string Doc = "  ����������.jpg\n  ���ڿ�ܵĸ�����Ϣ���Է��� CheeseFramework @Github  \n  (����������mdд�ľ����ĵ�)";
        [BoxGroup("�ؿ��༭�� Document"),Button, LabelText("��GitHub")]
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

}


public enum SaveDataType
{ 
    Json,
    Xml
}

public enum LevelType
{
     Normal,
     Dream,
     Cg
}
