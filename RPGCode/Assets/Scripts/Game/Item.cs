using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //Tip: ����id
    public int ID;

    //Tip: �����ֵ�
    public Dictionary<Language, string> languageDic = new Dictionary<Language, string>();
    //Tip: ���������ֵ�
    public Dictionary<Language, string[]> languagearrayDic = new Dictionary<Language, string[]>();

    /// <summary>
    /// Init:
    /// ��ʼ��������
    /// </summary>
    public void InitLanguage(Dictionary<Language, string> languageDic) 
    {
        DebugEX.Log("��ʼ������");
        this.languageDic = languageDic;

        foreach (var item in languagearrayDic)
        {
            DebugEX.Log("��ʼ�������ı���", item.Key ,item.Value);
        }
    }

    /// <summary>
    /// Init:
    /// ��ʼ��������
    /// </summary>
    public void InitLanguage(Dictionary<Language, string[]> languagearrayDic)
    {
        this.languagearrayDic = languagearrayDic;
    }
    
    /// <summary>
    /// Base:start
    /// </summary>
    public virtual void Start()
    {
        MsgManager.Instance.AddObserver(MSGInfo.SWITCHLANGUAGE, OnSwitchLanguageEvent);
    }

    /// <summary>
    /// Callback:
    /// �������л�ʱִ��
    /// </summary>
    /// <param name="msgData"></param>
    public virtual void OnSwitchLanguageEvent(MsgData msgData) 
    { 
       
    }

    /// <summary>
    /// Get:
    /// ��ȡ��ͬ���Ե��ı�ֵ
    /// </summary>
    /// <param name="language"></param>
    public string GetLanguageValue(Language language)
    {
        if (languageDic.ContainsKey(language))
            return languageDic[language];
        return "";
    }

    /// <summary>
    /// Get:
    /// ��ȡ��ͬ���Ե��ı�ֵ
    /// </summary>
    /// <param name="language"></param>
    public string[] GetLanguageArrayValue(Language language)
    {
        if (languagearrayDic.ContainsKey(language))
            return languagearrayDic[language];
        return new string[0];
    }

}
