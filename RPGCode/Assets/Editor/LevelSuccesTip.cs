using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSuccesTip : OdinEditorWindow
{

    [Title("�ؿ��������ɳɹ���")]
    public string path = "";

    [LabelText("�½�"), FoldoutGroup("����"), InfoBox("���ɳɹ������ݱ�����·�����棬�˴���Ԥ��"), ReadOnly]
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
        path = Application.dataPath + "/Resources/Config/Map";
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
