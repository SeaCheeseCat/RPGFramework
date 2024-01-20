using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataEditor
{
    [LabelText("��ǰ�½�"), ReadOnly]
    public int chapter;
    [LabelText("��ǰ�ؿ�"), ReadOnly]
    public int level;

    [FoldoutGroup("��������")]
    [LabelText("Npc����"), ReadOnly]
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


    [Button(ButtonSizes.Large), LabelText("��������")]
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
