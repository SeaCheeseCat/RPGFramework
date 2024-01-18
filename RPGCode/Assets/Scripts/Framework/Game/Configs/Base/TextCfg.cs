using System;
using System.Diagnostics;

/// <summary>
/// 文本配置
/// </summary>
public class TextCfg : BaseResData
{
    public string cn;
    public string en;
    public string gcn;
    public string tcn;
    public string jp;

    public override void FillData(string data)
    {
        var array = data.Split('|');

        try
        {
            ResourceInitUtil.InitField(array[0], out this.ID);
            
        }
        catch (Exception)
        {
            UnityEngine.Debug.Log("错误文本"+data);
        }
    }

    public new static bool HasKey { get { return true; } }
}
