using System;
using System.Collections.Generic;
/// <summary>
/// None
/// </summary>
public class NpcCfg : BaseResData
{
    public string Path;
    public Dictionary<Language, string> PathLanguage;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        ResourceInitUtil.InitField(array[0],out this.ID);
        ResourceInitUtil.InitField(array[1],out this.Path,out this.PathLanguage);

    }

public new static bool HasKey {get{return true;}}
}
