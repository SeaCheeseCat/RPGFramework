using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public BaseData baseData;
    public List<LevelData> levelData;
}

public class BaseData
{
    //Tip: 当前进行到的最大章节,用于载入主界面的胶卷
    public int chapterMax;
    //Tip: 当前进行的章节
    public int currentChapter;
    //Tip: 当前继续的关卡
    public int currentLevel;
    //Tip: 是否是第一次进入
    public bool firstEntry;
}

public class LevelData 
{
    //Tip: 当前章节
    public int chapter;
    //Tip: 当前关卡
    public int level;
    //Tip: 已经完成的任务Id
    public List<int> completedTask;
    //Tip: 关卡是否已经完成
    public bool completed;
}

