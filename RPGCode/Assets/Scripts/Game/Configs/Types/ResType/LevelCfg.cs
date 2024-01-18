using System;
using System.Collections.Generic;
/// <summary>
/// 关卡配置表
/// </summary>
public class LevelCfg : BaseResData
{
    public int Chapter;
    public int Level;
    public int[] Trigger;
    public bool Storymode;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Chapter);
        ResourceInitUtil.InitField(array[2],out this.Level);
        ResourceInitUtil.InitShortArray(array[3],out this.Trigger);
        ResourceInitUtil.InitField(array[4],out this.Storymode);

    }

public new static bool HasKey {get{return true;}}
}
