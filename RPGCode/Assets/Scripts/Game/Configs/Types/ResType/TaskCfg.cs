using System;
using System.Collections.Generic;
/// <summary>
/// None
/// </summary>
public class TaskCfg : BaseResData
{
    public int TaskID;
    public int[] Chapter;
    public string Content;
    public Dictionary<Language, string> ContentLanguage;
    public float[] Angles;
    public string Event;
    public Dictionary<Language, string> EventLanguage;
    public string[] Args;
    public Dictionary<Language, string[]> ArgsLanguageArray;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.TaskID);
        ResourceInitUtil.InitShortArray(array[2],out this.Chapter);
        ResourceInitUtil.InitField(array[3],out this.Content,out this.ContentLanguage);
        ResourceInitUtil.InitShortArray(array[4],out this.Angles);
        ResourceInitUtil.InitField(array[5],out this.Event,out this.EventLanguage);
        ResourceInitUtil.InitShortArray(array[6],out this.Args,out this.ArgsLanguageArray);

    }

public new static bool HasKey {get{return true;}}
}
