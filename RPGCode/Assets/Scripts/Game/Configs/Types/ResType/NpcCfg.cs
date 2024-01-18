using System;
using System.Collections.Generic;
/// <summary>
/// None
/// </summary>
public class NpcCfg : BaseResData
{
    public string Dialog;
    public Dictionary<Language, string> DialogLanguage;
    public string Path;
    public Dictionary<Language, string> PathLanguage;
    public bool Islens;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Dialog,out this.DialogLanguage);
        ResourceInitUtil.InitField(array[2],out this.Path,out this.PathLanguage);
        ResourceInitUtil.InitField(array[3],out this.Islens);

    }

public new static bool HasKey {get{return true;}}
}
