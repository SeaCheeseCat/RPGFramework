using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LanguageManager : Manager<LanguageManager>
{
    //Tip: ���ڵĸ��������ı�
    public List<LanguageText> languageTexts = new List<LanguageText>();
    //Tip��ȫ������������
    public List<LanguageItem> languageDatas = new List<LanguageItem>();
    /// <summary>
    /// Base:init
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        languageDatas = ArchiveManager.Instance.LoadLanguageConfigFromJson<List<LanguageItem>>();
        MsgManager.Instance.SendMessage(MSGInfo.LanguageComplete);
        return base.Init(obj);
    }

    /// <summary>
    /// Add:
    /// ���һ�������ı�
    /// </summary>
    public LanguageItem AddLanguagerText(LanguageText val)
    {
        DebugEX.Log("����ı�",val.id);
        if (!languageTexts.Contains(val))
        { 
            languageTexts.Add(val);
        }
        foreach (var item in languageDatas)
        {
            if (item.id == val.id)
                return item;
        }
        return null;
    }

    /// <summary>
    /// Update:
    /// ��������
    /// </summary>
    /// <param name="language"></param>
    public void UpdateLanguage(Language language) 
    {
        foreach (var item in languageTexts)
        {
            item.UpadteLanguage(language);
        }
    }


    /// <summary>
    /// Get:
    /// ����Config���ñ��еĶ���������
    /// {[en:NPc Not Rotate]  [jp:ssd]}  (ʾ��:����������ı�ʾ��  Ӧ��������)
    /// </summary>
    public Dictionary<Language, string> GetConfigLanguageString(string val)
    {
        var value = val;
        if (!value.Contains("{") || !value.Contains("{"))
            return null;

        // ��ȡ en �� jp ������
        Dictionary<Language, string> extractedContents = ExtractContents(value);
        return extractedContents;

    }

    /// <summary>
    /// Get:
    /// ��ȡ����Config���ñ��й��ڶ����Ե����� 
    /// {[en:a&b][jp:c&d]}
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public Dictionary<Language, string[]> GetConfigLanguageArray(string val)
    {
        var value = val;
        if (!value.Contains("{") || value.Contains("{"))
            return null;

        // ��ȡ en �� jp ������
        Dictionary<Language, string> extractedContents = ExtractContents(value);
        Dictionary<Language, string[]> results = new Dictionary<Language, string[]>();
        foreach (var item in extractedContents)
        {
            string[] arr = item.Value.Split('&');
            var res = new string[arr.Length];

            for (int k = 0; k < arr.Length; k++)
            {
                res[k] = arr[k];
            }

            results[item.Key] = res;
        }


        return results;

    }


    /// <summary>
    /// Extract:
    /// ���ַ��滻����Ҫ��
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    static Dictionary<Language, string> ExtractContents(string input)
    {
        Dictionary<Language, string> extractedContents = new Dictionary<Language, string>();

        // ƥ�� [en:...] �� [jp:...]
        Regex regex = new Regex(@"\[(en|jp|fr):(.*?)\]");

        // �������ı��в���ƥ����
        MatchCollection matches = regex.Matches(input);

        // ����ÿ��ƥ����
        foreach (Match match in matches)
        {
            // ��ȡ��ǩ������
            string tag = match.Groups[1].Value;
            string content = match.Groups[2].Value;

            // ��ӵ���ȡ�������ֵ�
            if (tag == "en")
            {
                extractedContents[Language.English] = content;
            }
            else if (tag == "jp")
            {
                extractedContents[Language.Japanese] = content;
            }
            else if (tag == "fr")
            {
                extractedContents[Language.French] = content;
            }
           
        }

        return extractedContents;
    }

}

[Serializable]
public class LanguageItem
{
    public int id;
    public string ChineseLanguage;
    [TableList]
    public List<languaItems> languages = new List<languaItems>()
    {

    };
}

[Serializable]
public class languaItems
{
    public Language language;
    public string content;
}

public class LanguageTextValue
{
    public string content;
    public Dictionary<Language, string> dic;
}

public class LanguageTextArrayValue
{
    public string[] content;
    public Dictionary<Language, string[]> dics;
}