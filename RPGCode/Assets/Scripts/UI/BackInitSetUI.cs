using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackInitSetUI : SetItemUI
{
    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.CloseAllUI();
        GameManager.Instance.Recycle();
        ArchiveManager.Instance.SaveDataToJsonFile();
        LoadManager.Instance.LoadScene("Init");
        GameBase.Instance.gameScene = GameScene.Init;
    }
}

