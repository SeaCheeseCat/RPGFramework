using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetEditor : OdinMenuEditorWindow
{
    [Title("CheeseFramework","��ӭʹ�ÿ�ܣ��ȸ��Լ��������һ�����������,����������.jpg")]
    [LabelText("��Ϸ����"), BoxGroup("Tool")]
    public string gameName;
    [LabelText("Ĭ������"), BoxGroup("Tool")]
    public Language defalutLanguage = Language.Chinese;

    [Button("��������",ButtonSizes.Large),GUIColor(35f/225f, 219f/225f, 66f/225f)]
    public void BuildConfig() 
    {
        SaveConfig();
        var window = GetWindow<ConfigSuccesTip>();
        var path = "/Resources/Config/GameConfig";
        window.InitData(path);
        window.Show();
    }

    /// <summary>
    /// Save:
    /// ��������
    /// </summary>
    public void SaveConfig()
    {
        GameConfigData data = new GameConfigData();
        data.gamename = gameName;
        data.defalutlanguage = defalutLanguage;
        ArchiveManager.Instance.SaveGameConfigToJsonFile<GameConfigData>(data);
    }

    public void OnCreate()
    {
        Load();
    }

    public void Load()
    {
        var data = ArchiveManager.Instance.LoadGameConfigFromJson<GameConfigData>();
        if (data == null)
            return;
        gameName = data.gamename;
        defalutLanguage = data.defalutlanguage;
    }

    protected override OdinMenuTree BuildMenuTree()
    {
       
        throw new System.NotImplementedException();
    }
}
