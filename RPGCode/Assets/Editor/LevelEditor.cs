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
    public static LoadEditor loadeditor;
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
        if (scene.name != "Demo")
            return tree;
        MapConfig.Instance.InitData();
        tree.Selection.SupportsMultiSelect = false;
        loadeditor = new LoadEditor();
        baseseteditor = new BaseSetEditor();
        var dataeditor = new DataEditor();
        tree.Add("�������� ��", baseseteditor);
        tree.Add("�ؿ��б� ��", loadeditor);
        tree.Add("�������� ��", new BuildEditor());
        tree.Add("�ؿ����� ��", dataeditor);
        tree.Add("Document", new DocumentEditor());
        loadeditor.Init();
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
            var window = GetWindow<SuccesTip>();
            window.InitData(data);
            window.Show();
            loadeditor.Init();
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


    public class BuildEditor
    {
        [FoldoutGroup("��ϸ����")]
        [EnumToggleButtons, LabelText("�ļ���������")]
        public SaveDataType SomeEnumField;
    }


    public class DataEditor
    {
        [LabelText("��ǰ�½�"), ReadOnly]
        public int chapter;
        [LabelText("��ǰ�ؿ�"), ReadOnly]
        public int level;

        [FoldoutGroup("��������")]
        [LabelText("Npc����"),ReadOnly]
        public List<int> Npcs;

        [FoldoutGroup("��������")]
        [LabelText("ģ������"), ReadOnly]
        public List<int> Modes;

        [FoldoutGroup("��������")]
        [LabelText("��������"), ReadOnly]
        public List<int> Particles;

        [FoldoutGroup("�ļ�����")]
        [LabelText("�ļ��б�"), ReadOnly]
        public List<string> Files;


        [Button(ButtonSizes.Large),LabelText("��������")]
        public void RefreshData() 
        {
            MapConfig.Instance.InitData();
            Npcs = new List<int>();
            Modes = new List<int>();
            Particles = new List<int>();
            chapter = GameBaseData.Chapter;
            level = GameBaseData.Level;

            var npcdatas = MapConfig.Instance.Getmapnpcdatas();
            foreach (var item in npcdatas)
            {
                Npcs.Add(item.ID);
            }

            var modeldatas = MapConfig.Instance.Getmapmodelsdatas();
            foreach (var item in modeldatas)
            {
                Modes.Add(item.ID);
            }

            var particledatas = MapConfig.Instance.Getmapparticledatas();
            foreach (var item in particledatas)
            {
                Particles.Add(item.ID);
            }


        }
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


    public class LoadEditor
    {
        public void Init() 
        {
            LoadEditData();
        }
        [Title("�ؿ��б�")]
        [ReadOnly]
        public List<string> LevelList;


        [LabelText("�½�"), FoldoutGroup("����"), InfoBox("��ȷ���ùؿ��Ѿ�������ɣ����Կ�������Ĺؿ��б�����û�д˹أ� (�����ط���)")]
        public int Chapter;
        [LabelText("�ؿ�"), FoldoutGroup("����")]
        public int Level;

        [GUIColor(0, 1, 0)]
        [Button(ButtonSizes.Large)]
        [ FoldoutGroup("����"),LabelText("���عؿ�")]
        private void LoadMap()
        {
            MapConfig.Instance.InitData();
            var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter,Level);
            if (data == null)
            {
                DebugEX.LogError("��ȡ�ؿ���������ʧ��");
                return;
            }
            MapConfig.Instance.LoadMapConfig(data,true);
            baseseteditor.SetData(data.Charpter, data.Level, data.aduioPath);
        }
        
        [GUIColor(1, 0f, 0)]
        [Button(ButtonSizes.Medium)]
        [FoldoutGroup("����"), LabelText("ɾ���ؿ�")]
        private void DeleteLevel()
        {
            ArchiveManager.Instance.DeleteMapConfig(Chapter, Level);
            LoadEditData();
        }


        [GUIColor(1, 1, 1)]
        [Button(ButtonSizes.Large)]
        [ButtonGroup("������"), LabelText("ˢ�¹ؿ��б�")]
        private void LoadEditData()
        {
            LevelList = new List<string>();
            var list = ArchiveManager.Instance.LaodMapConfigDic();
            foreach (var item in list)
            {
                var name = ParseAndPrintLevelInfo(Path.GetFileName(item))+ " ["+ Path.GetFileName(item)+"]";
                LevelList.Add(name);
            }

        }


        string ParseAndPrintLevelInfo(string fileName)
        {
            // ʹ��������ʽ�����ļ���
            Match match = Regex.Match(fileName, @"mapconfig_(\d+)_(\d+).json");

            if (match.Success)
            {
                int chapter = int.Parse(match.Groups[1].Value);
                int level = int.Parse(match.Groups[2].Value);
                return "�� " + chapter + " �� " + " ��" + level + "��";
            }

            return "�ؿ�����ʧ��";
        }


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


public class SuccesTip : OdinEditorWindow
{
    
    [Title("�ؿ��������ɳɹ���")]
    public string path = "";

    [LabelText("�½�"), FoldoutGroup("����"), InfoBox("���ɳɹ������ݱ�����·�����棬�˴���Ԥ��"),ReadOnly]
    public int Chapter;
    [LabelText("�ؿ�"), FoldoutGroup("����"), ReadOnly]
    public int Level;

    [FoldoutGroup("����"), LabelText("Npc")]
    public List<int> Npcs;
    [FoldoutGroup("����"), LabelText("ģ��")]
    public List<int> Models;
    [FoldoutGroup("����"), LabelText("��������")]
    public Vector3 Land;

    [Button(ButtonSizes.Large)]
    [ButtonGroup("������"), LabelText("��·��")]
    private void OpenPath()
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        System.Diagnostics.Process.Start(direction.FullName);
    }

    public void InitData(MapConfigData data) 
    {
        Npcs = new List<int>();
        Chapter = data.Charpter;
        Level = data.Level;

        foreach (var item in data.mapnpcdata)
        {
            Npcs.Add(item.ID);
        }

        Land = new Vector3((float)data.maplanddata.x, (float)data.maplanddata.y, (float)data.maplanddata.z);
        path = Application.dataPath+ "/Resources/Config/Map";
    }
    [Button(ButtonSizes.Large)]
    [ButtonGroup("������"), LabelText("�ر�")]
    private void CloseWinds()
    {
        Close();
    }

    public static string PersistentDataPath
    {
        get
        {
            string path =
#if UNITY_ANDROID
         Application.persistentDataPath;
#elif UNITY_IPHONE && !UNITY_EDITOR
         Application.persistentDataPath;
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
         Application.persistentDataPath;
#else
        string.Empty;
#endif
            return path;
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
