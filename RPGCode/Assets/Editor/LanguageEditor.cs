using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LanguageEditor
{
    [LabelText("����ѡ��"),BoxGroup("Preview")]
    public Language language = Language.Chinese;
    [TableList, LabelText("�����Ա༭��:Ԥ���ʿ�"),BoxGroup("Preview")]
    public List<LanguageItem> languageList = new List<LanguageItem>()
    {

    };

    [Button("���¼���",30), BoxGroup("Preview")]
    public void PreviewOnclick()
    {
        LoadConfig();
        UpdateLanguageTexts(language);
    }

    [Button("��������", 30), BoxGroup("Preview")]
    public void BuildOnclick()
    {
        ArchiveManager.Instance.SaveLanguageConfigToJsonFile<List<LanguageItem>>(languageList);
        AssetDatabase.Refresh();
    }

    private void UpdateLanguageTexts(Language language)
    {
        LanguageText[] languageTexts = GameObject.FindObjectsOfType<LanguageText>();

        foreach (LanguageText languageText in languageTexts)
        {
            // ��ȡText���
            Text textComponent = languageText.GetComponent<Text>();

            if (textComponent != null)
            {
                // �����ı����ݣ������������һ����ȡ�ı����ݵķ���������Ҫ����ʵ������޸�
                string newText = GetTextFromYourSource(languageText.id, language);

                textComponent.text = newText;

                // ��ӡ��־�Ա��ڿ���̨�в鿴
                Debug.Log("Updated LanguageText: " + languageText.gameObject.name);
            }
        }
    }

    private string GetTextFromYourSource(int id, Language language)
    {
        foreach (var item in languageList)
        {
            if (item.id == id)
            {
                if (language == Language.Chinese)
                {
                    return item.ChineseLanguage;
                }
                foreach (var val in item.languages)
                {
                    if (val.language == language)
                        return val.content;
                }
            }
        }
        return "";
    }

    public void LoadConfig() 
    {
        var data = ArchiveManager.Instance.LoadLanguageConfigFromJson<List<LanguageItem>>();
        languageList = data;
    }
}

