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
    [Title("关卡列表")]
    [ReadOnly]
    public List<string> LevelList;


    [LabelText("章节"), FoldoutGroup("加载"), InfoBox("请确保该关卡已经配置完成（可以看看上面的关卡列表中有没有此关） (阴暗地飞行)")]
    public int Chapter;
    [LabelText("关卡"), FoldoutGroup("加载")]
    public int Level;

    [GUIColor(0, 1, 0)]
    [Button(ButtonSizes.Large)]
    [FoldoutGroup("加载"), LabelText("加载关卡")]
    private void LoadMap()
    {
        MapConfig.Instance.InitData();
        var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter, Level);
        if (data == null)
        {
            DebugEX.LogError("读取关卡配置数据失败");
            return;
        }
        MapConfig.Instance.LoadMapConfig(data, true);
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
            var name = ParseAndPrintLevelInfo(Path.GetFileName(item)) + " [" + Path.GetFileName(item) + "]";
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
