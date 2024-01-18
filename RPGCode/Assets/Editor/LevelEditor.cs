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
    [MenuItem("阴暗地爬行.jpg/关卡编辑器")]
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
        tree.Add("基础配置 □", baseseteditor);
        tree.Add("关卡列表 □", loadeditor);
        tree.Add("生成配置 □", new BuildEditor());
        tree.Add("关卡数据 ▲", dataeditor);
        tree.Add("Document", new DocumentEditor());
        loadeditor.Init();
        dataeditor.RefreshData();
        return tree;
    }

    public class BaseSetEditor
    {
        
        [EnumToggleButtons, LabelText("关卡类型")]
        public LevelType leveltype;

        [LabelText("章节"), FoldoutGroup("关卡配置"), InfoBox("请确保关卡编辑器运行之前\n所有关卡物体均在MapConfig物体下 (阴暗地飞行)")]
        public int Chapter;
        [LabelText("关卡"), FoldoutGroup("关卡配置")]
        public int Level;

        [GUIColor(1, 1, 1)]
        [Button(ButtonSizes.Large)]
        [ButtonGroup("功能区"), LabelText("预加载")]
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
        [ButtonGroup("功能区"), LabelText("生成关卡配置")]
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
            DebugEX.Log("生成地图配置成功");
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
            DebugEX.Log("加载音乐路径", musicpath);
            Musicclip = Resources.Load<AudioClip>("Audio/Music/"+musicpath);
            //UnityEditor.EditorUtility.SetDirty(Musicclip);
        }


        /// <summary>
        /// 刷新数据区
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
        [ButtonGroup("功能区"), LabelText("重置场景")]
        private void Delete()
        {
            MapConfig.Instance.InitData();
            DeleteChild(MapConfig.Instance.npcsTrans);
            DeleteChild(MapConfig.Instance.modelsTrans);
            DeleteChild(MapConfig.Instance.particleTrans);
        }

        /// <summary>
        /// 在Eidt模式删除一个物体下面所有的子物体
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

        [FoldoutGroup("详细配置")]
        [EnumToggleButtons, LabelText("文件储存类型")]
        public SaveDataType savedataEnum;

        [FoldoutGroup("详细配置"), LabelText("背景音乐")]
        public AudioClip Musicclip;


        [FoldoutGroup("详细配置"), LabelText("是否包含过场")]
        public bool isCg;

        [FoldoutGroup("关卡任务"), LabelText("任务id")]
        public List<int> Tasks;


        [FoldoutGroup("数据"), InfoBox("关卡数据点击预加载后自动生成，不需要手动配置，检查数据使用x"), LabelText("Npc")]
        public List<int> Npcs;
        [FoldoutGroup("数据"), LabelText("模型")]
        public List<int> Models;
        [FoldoutGroup("数据"), LabelText("粒子")]
        public List<int> Particles;
        [FoldoutGroup("数据"), LabelText("地面数据")]
        public Vector3 Lands;

    }


    public class BuildEditor
    {
        [FoldoutGroup("详细配置")]
        [EnumToggleButtons, LabelText("文件储存类型")]
        public SaveDataType SomeEnumField;
    }


    public class DataEditor
    {
        [LabelText("当前章节"), ReadOnly]
        public int chapter;
        [LabelText("当前关卡"), ReadOnly]
        public int level;

        [FoldoutGroup("场景数据")]
        [LabelText("Npc数据"),ReadOnly]
        public List<int> Npcs;

        [FoldoutGroup("场景数据")]
        [LabelText("模型数据"), ReadOnly]
        public List<int> Modes;

        [FoldoutGroup("场景数据")]
        [LabelText("粒子数据"), ReadOnly]
        public List<int> Particles;

        [FoldoutGroup("文件数据")]
        [LabelText("文件列表"), ReadOnly]
        public List<string> Files;


        [Button(ButtonSizes.Large),LabelText("更新数据")]
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

        [BoxGroup("关卡编辑器 Document")]
        [DisplayAsString(false), HideLabel]
        public string Doc = "  阴暗地爬行.jpg\n  关于框架的更多信息可以访问 CheeseFramework @Github  \n  (那里有我用md写的具体文档)";
        [BoxGroup("关卡编辑器 Document"),Button, LabelText("打开GitHub")]
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


        [FoldoutGroup("需要注意什么"), DisplayAsString(false), HideLabel]
        public string SomeText2 =
           " -在编辑场景之前，先将MapConfig下的物体清理干净\n"+
           "\n -相同关卡配置一遍后，再次配置将覆盖原本的配置\n"+
           "\n -不要让妮娜跑到键盘上来！";
    }


    public class LoadEditor
    {
        public void Init() 
        {
            LoadEditData();
        }
        [Title("关卡列表")]
        [ReadOnly]
        public List<string> LevelList;


        [LabelText("章节"), FoldoutGroup("加载"), InfoBox("请确保该关卡已经配置完成（可以看看上面的关卡列表中有没有此关） (阴暗地飞行)")]
        public int Chapter;
        [LabelText("关卡"), FoldoutGroup("加载")]
        public int Level;

        [GUIColor(0, 1, 0)]
        [Button(ButtonSizes.Large)]
        [ FoldoutGroup("加载"),LabelText("加载关卡")]
        private void LoadMap()
        {
            MapConfig.Instance.InitData();
            var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter,Level);
            if (data == null)
            {
                DebugEX.LogError("读取关卡配置数据失败");
                return;
            }
            MapConfig.Instance.LoadMapConfig(data,true);
            baseseteditor.SetData(data.Charpter, data.Level, data.aduioPath);
        }
        
        [GUIColor(1, 0f, 0)]
        [Button(ButtonSizes.Medium)]
        [FoldoutGroup("加载"), LabelText("删除关卡")]
        private void DeleteLevel()
        {
            ArchiveManager.Instance.DeleteMapConfig(Chapter, Level);
            LoadEditData();
        }


        [GUIColor(1, 1, 1)]
        [Button(ButtonSizes.Large)]
        [ButtonGroup("功能区"), LabelText("刷新关卡列表")]
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
            // 使用正则表达式解析文件名
            Match match = Regex.Match(fileName, @"mapconfig_(\d+)_(\d+).json");

            if (match.Success)
            {
                int chapter = int.Parse(match.Groups[1].Value);
                int level = int.Parse(match.Groups[2].Value);
                return "第 " + chapter + " 章 " + " 第" + level + "关";
            }

            return "关卡解析失败";
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





}


public class SuccesTip : OdinEditorWindow
{
    
    [Title("关卡配置生成成功！")]
    public string path = "";

    [LabelText("章节"), FoldoutGroup("数据"), InfoBox("生成成功后数据保存在路径下面，此处仅预览"),ReadOnly]
    public int Chapter;
    [LabelText("关卡"), FoldoutGroup("数据"), ReadOnly]
    public int Level;

    [FoldoutGroup("数据"), LabelText("Npc")]
    public List<int> Npcs;
    [FoldoutGroup("数据"), LabelText("模型")]
    public List<int> Models;
    [FoldoutGroup("数据"), LabelText("地面数据")]
    public Vector3 Land;

    [Button(ButtonSizes.Large)]
    [ButtonGroup("功能区"), LabelText("打开路径")]
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
    [ButtonGroup("功能区"), LabelText("关闭")]
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
