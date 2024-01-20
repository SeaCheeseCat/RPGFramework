using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitUI : MonoBehaviour
{
    public Button CloseBtn;
    public Button TestBtn;
    private void Awake()
    {
        CloseBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<SetUI>();
           /* GameManager.Instance.Recycle();
            ArchiveManager.Instance.SaveDataToJsonFile();
            LoadManager.Instance.LoadScene("Init");
            GameBase.Instance.gameScene = GameScene.Init;*/

        });

        TestBtn.onClick.AddListener(() =>
        {
            LoadManager.Instance.LoadScene("House");
        });
    }

}
