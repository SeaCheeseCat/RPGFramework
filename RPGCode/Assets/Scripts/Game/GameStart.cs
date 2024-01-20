using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ؿ���������
/// ����������
/// </summary>
public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.Init();
    }
    private void Start()
    {
        InitGame();
       
        
    }


    public void InitGame()
    {
        MapConfig.Instance.InitData();
        if (GameBaseData.levelType == LevelType.COMMON)
        {
            //LevelSystem.Instance.Load(GameBaseData.Chapter, GameBaseData.Level);
            GameManager.Instance.LoadLevelData();
        }
        GameManager.Instance.player = UnitManager.Instance.CreateNpc<Player>(1);
    }


    public void DelayTime() 
    {
      
    }

}
