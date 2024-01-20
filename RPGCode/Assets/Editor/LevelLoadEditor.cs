using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using static LevelEditor;

public class LevelLoadEditor
{
    private BaseSetEditor baseSetEditor;
    public void Init(BaseSetEditor baseSetEditor)
    {
        this.baseSetEditor = baseSetEditor;
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
    [FoldoutGroup("����"), LabelText("���عؿ�")]
    private void LoadMap()
    {
        MapConfig.Instance.InitData();
        var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter, Level);
        if (data == null)
        {
            DebugEX.LogError("��ȡ�ؿ���������ʧ��");
            return;
        }
        MapConfig.Instance.LoadMapConfig(data, true);
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
            var name = ParseAndPrintLevelInfo(Path.GetFileName(item)) + " [" + Path.GetFileName(item) + "]";
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
