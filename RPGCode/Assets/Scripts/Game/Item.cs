using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        this.languageDic = languageDic;
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
    /// Set:
    /// ���ö�����
    /// </summary>
    /// <param name="contentText"></param>
    /// <param name="val"></param>
    public void SetTextLanguage(Text contentText, string val) 
    {
        if (GameBaseData.language == Language.Chinese)
        {
            contentText.text = val;
            return;
        }
        contentText.text = GetLanguageValue(GameBaseData.language);
    }

    /// <summary>
    /// Get:
    /// ��ȡ��ͬ���Ե��ı�ֵ
    /// </summary>
    /// <param name="language"></param>
    public  string GetLanguageValue(Language language)
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
