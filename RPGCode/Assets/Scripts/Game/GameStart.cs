using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡的启动器
/// 从这里启动
/// </summary>
public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.Init();
    }
    private void Start()
    {
        if (GameBaseData.levelType == LevelType.COMMON)
        {
            LevelSystem.Instance.Load(GameBaseData.Chapter, GameBaseData.Level);
            GameManager.Instance.LoadLevelData();
        }
        else if (GameBaseData.levelType == LevelType.HAVESUBTITLE)
        {
            EventManager.Instance.TriggerEvent(GameBaseData.EventName);
            LevelSystem.Instance.Load(GameBaseData.Chapter, GameBaseData.Level);
            GameManager.Instance.LoadLevelData();
        }
    }


    public void DelayTime() 
    {
      
    }

}
